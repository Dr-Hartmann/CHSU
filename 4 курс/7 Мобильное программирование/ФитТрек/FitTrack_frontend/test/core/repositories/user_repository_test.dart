import 'package:flutter_test/flutter_test.dart';
import 'package:fit_tracker/core/repositories/user_repository.dart';
import 'package:fit_tracker/core/storage/database.dart';
import 'package:fit_tracker/core/models/models.dart';
import '../../test_helpers.dart';

void main() {
  group('UserRepository', () {
    late AppDatabase database;
    late UserRepository userRepository;

    setUp(() async {
      database = createInMemoryDb();
      userRepository = UserRepository(database);
    });

    tearDown(() async {
      await database.close();
    });

    group('User Creation and Retrieval', () {
      test('should create default user when none exists', () async {
        // Act
        final user = await userRepository.getOrCreateDefaultUser();

        // Assert
        expect(user.id, greaterThan(0));
        expect(user.name, equals('User'));
        expect(user.isDeleted, isFalse);
        expect(user.updatedAt, greaterThan(0));
      });

      test('should return existing user when one exists', () async {
        // Arrange - Create initial user
        final firstUser = await userRepository.getOrCreateDefaultUser();

        // Act - Get user again
        final secondUser = await userRepository.getOrCreateDefaultUser();

        // Assert
        expect(firstUser.id, equals(secondUser.id));
        expect(firstUser.name, equals(secondUser.name));
      });

      test('should create default settings when creating new user', () async {
        // Arrange & Act
        final user = await userRepository.getOrCreateDefaultUser();
        final settings = await userRepository.getUserSettings(user.id);

        // Assert
        expect(settings, isNotNull);
        expect(settings!.userId, equals(user.id));
        expect(settings.language, equals('en'));
        expect(settings.theme, equals('light'));
        expect(settings.restTimerDuration, equals(60));
        expect(settings.isDeleted, isFalse);

        // Check default weekly limits
        final weeklyLimits = WeeklyLimits.fromJsonString(
          settings.weeklyLimits!,
        );
        final defaultLimits = WeeklyLimits.getDefault();
        expect(
          weeklyLimits.toJsonString(),
          equals(defaultLimits.toJsonString()),
        );
      });

      test('should get user by id', () async {
        // Arrange
        final createdUser = await userRepository.getOrCreateDefaultUser();

        // Act
        final retrievedUser = await userRepository.getUserById(createdUser.id);
        final nonExistentUser = await userRepository.getUserById(999);

        // Assert
        expect(retrievedUser, isNotNull);
        expect(retrievedUser!.id, equals(createdUser.id));
        expect(retrievedUser.name, equals(createdUser.name));

        expect(nonExistentUser, isNull);
      });
    });

    group('Settings Management', () {
      late int userId;

      setUp(() async {
        final user = await userRepository.getOrCreateDefaultUser();
        userId = user.id;
      });

      test('should get user settings', () async {
        // Act
        final settings = await userRepository.getUserSettings(userId);

        // Assert
        expect(settings, isNotNull);
        expect(settings!.userId, equals(userId));
        expect(settings.language, equals('en'));
        expect(settings.theme, equals('light'));
        expect(settings.restTimerDuration, equals(60));
      });

      test('should return null for non-existent user settings', () async {
        // Act
        final settings = await userRepository.getUserSettings(999);

        // Assert
        expect(settings, isNull);
      });

      test('should update user language setting', () async {
        // Act
        await userRepository.updateUserSettings(userId, language: 'ru');
        final updatedSettings = await userRepository.getUserSettings(userId);

        // Assert
        expect(updatedSettings, isNotNull);
        expect(updatedSettings!.language, equals('ru'));
        // Other settings should remain unchanged
        expect(updatedSettings.theme, equals('light'));
        expect(updatedSettings.restTimerDuration, equals(60));
      });

      test('should update user theme setting', () async {
        // Act
        await userRepository.updateUserSettings(userId, theme: 'dark');
        final updatedSettings = await userRepository.getUserSettings(userId);

        // Assert
        expect(updatedSettings, isNotNull);
        expect(updatedSettings!.theme, equals('dark'));
        // Other settings should remain unchanged
        expect(updatedSettings.language, equals('en'));
        expect(updatedSettings.restTimerDuration, equals(60));
      });

      test('should update rest timer duration', () async {
        // Act
        await userRepository.updateUserSettings(userId, restTimerDuration: 90);
        final updatedSettings = await userRepository.getUserSettings(userId);

        // Assert
        expect(updatedSettings, isNotNull);
        expect(updatedSettings!.restTimerDuration, equals(90));
        // Other settings should remain unchanged
        expect(updatedSettings.language, equals('en'));
        expect(updatedSettings.theme, equals('light'));
      });

      test('should update weekly limits', () async {
        // Arrange
        final newLimits = const WeeklyLimits(
          limits: {'Chest': 20, 'Back': 25, 'Shoulders': 18},
        );

        // Act
        await userRepository.updateUserSettings(
          userId,
          weeklyLimits: newLimits,
        );
        final updatedSettings = await userRepository.getUserSettings(userId);

        // Assert
        expect(updatedSettings, isNotNull);
        final updatedLimits = WeeklyLimits.fromJsonString(
          updatedSettings!.weeklyLimits!,
        );
        expect(updatedLimits.limits['Chest'], equals(20));
        expect(updatedLimits.limits['Back'], equals(25));
        expect(updatedLimits.limits['Shoulders'], equals(18));
      });

      test('should update multiple settings at once', () async {
        // Arrange
        final newLimits = const WeeklyLimits(
          limits: {'Chest': 18, 'Back': 22, 'Shoulders': 16},
        );

        // Act
        await userRepository.updateUserSettings(
          userId,
          language: 'es',
          theme: 'dark',
          restTimerDuration: 45,
          weeklyLimits: newLimits,
        );
        final updatedSettings = await userRepository.getUserSettings(userId);

        // Assert
        expect(updatedSettings, isNotNull);
        expect(updatedSettings!.language, equals('es'));
        expect(updatedSettings.theme, equals('dark'));
        expect(updatedSettings.restTimerDuration, equals(45));

        final updatedLimits = WeeklyLimits.fromJsonString(
          updatedSettings.weeklyLimits!,
        );
        expect(updatedLimits.limits['Chest'], equals(18));
        expect(updatedLimits.limits['Back'], equals(22));
        expect(updatedLimits.limits['Shoulders'], equals(16));
      });

      test('should update timestamp when updating settings', () async {
        // Arrange
        final initialSettings = await userRepository.getUserSettings(userId);
        final initialTimestamp = initialSettings!.updatedAt;

        // Wait a moment to ensure timestamp difference
        await Future.delayed(const Duration(milliseconds: 10));

        // Act
        await userRepository.updateUserSettings(userId, language: 'fr');
        final updatedSettings = await userRepository.getUserSettings(userId);

        // Assert
        expect(updatedSettings, isNotNull);
        expect(updatedSettings!.updatedAt, greaterThan(initialTimestamp));
      });
    });

    group('Data Integrity', () {
      test('should handle sequential user creation', () async {
        // Act - Create users sequentially
        final firstUser = await userRepository.getOrCreateDefaultUser();
        final secondUser = await userRepository.getOrCreateDefaultUser();
        final thirdUser = await userRepository.getOrCreateDefaultUser();

        // Assert - All should return the same user
        expect(firstUser.id, equals(secondUser.id));
        expect(secondUser.id, equals(thirdUser.id));
        expect(firstUser.name, equals(secondUser.name));
      });

      test('should not create duplicate settings', () async {
        // Arrange
        final user = await userRepository.getOrCreateDefaultUser();

        // Act - Try to get/create user again
        await userRepository.getOrCreateDefaultUser();

        // Get settings count for the user
        final settingsCount = await database.select(database.settings).get();

        // Assert - Should only have one settings record
        expect(settingsCount.length, equals(1));
        expect(settingsCount.first.userId, equals(user.id));
      });

      test('should preserve user data integrity', () async {
        // Arrange
        final user = await userRepository.getOrCreateDefaultUser();

        // Act - Update settings multiple times
        await userRepository.updateUserSettings(user.id, language: 'ru');
        await userRepository.updateUserSettings(user.id, theme: 'dark');
        await userRepository.updateUserSettings(
          user.id,
          restTimerDuration: 120,
        );

        // Assert - User should still exist and be unchanged
        final retrievedUser = await userRepository.getUserById(user.id);
        expect(retrievedUser, isNotNull);
        expect(retrievedUser!.name, equals(user.name));
        expect(retrievedUser.id, equals(user.id));

        // Settings should reflect all updates
        final settings = await userRepository.getUserSettings(user.id);
        expect(settings, isNotNull);
        expect(settings!.language, equals('ru'));
        expect(settings.theme, equals('dark'));
        expect(settings.restTimerDuration, equals(120));
      });
    });
  });
}
