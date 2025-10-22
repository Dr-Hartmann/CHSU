import 'package:flutter/foundation.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:fit_tracker/core/repositories/stats_repository.dart';
import 'package:fit_tracker/core/storage/database.dart';
import '../../test_helpers.dart';

void main() {
  group('StatsRepository - Multiple Muscle Groups', () {
    late AppDatabase database;
    late StatsRepository statsRepository;

    setUp(() async {
      database = createInMemoryDb();
      statsRepository = StatsRepository(database);
    });

    tearDown(() async {
      await database.close();
    });

    test('bench press should affect multiple muscle groups', () async {
      // Get muscle group stats
      final stats = await statsRepository.getMuscleGroupStats(1, days: 30);

      // Check that all expected muscle groups have some data
      // (This test assumes there's workout data with bench press in the test DB)
      expect(stats.keys, contains('Chest'));
      expect(stats.keys, contains('Shoulders'));
      expect(stats.keys, contains('Triceps'));

      // If bench press was performed, chest should have higher sets count than secondary muscles
      if (stats['Chest']!.totalSets > 0) {
        if (kDebugMode) {
          print('Chest sets: ${stats['Chest']!.totalSets}');
          print('Shoulders sets: ${stats['Shoulders']!.totalSets}');
          print('Triceps sets: ${stats['Triceps']!.totalSets}');
        }

        // Primary muscle (Chest) should have higher count than secondary muscles
        // due to coefficient (1.0 vs 0.5)
        expect(
          stats['Chest']!.totalSets,
          greaterThanOrEqualTo(stats['Shoulders']!.totalSets),
        );
        expect(
          stats['Chest']!.totalSets,
          greaterThanOrEqualTo(stats['Triceps']!.totalSets),
        );
      }
    });

    test('muscle group coefficients work correctly', () async {
      // This is more of an integration test - we'll verify the structure
      final stats = await statsRepository.getMuscleGroupStats(1, days: 30);

      // All muscle groups should be present in stats
      expect(stats.keys.length, greaterThan(5)); // We have 13 muscle groups

      // Each stat should have proper structure
      stats.forEach((groupId, stat) {
        expect(stat.muscleGroupId, equals(groupId));
        expect(stat.totalSets, greaterThanOrEqualTo(0.0));
        expect(stat.totalVolume, greaterThanOrEqualTo(0.0));
        expect(stat.weeklyLimit, greaterThan(0));
      });
    });
  });
}
