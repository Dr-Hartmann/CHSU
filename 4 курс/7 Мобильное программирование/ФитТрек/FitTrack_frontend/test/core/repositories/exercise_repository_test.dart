import 'package:flutter_test/flutter_test.dart';
import 'package:fit_tracker/core/repositories/exercise_repository.dart';
import 'package:fit_tracker/core/storage/database.dart';
import 'package:drift/drift.dart' hide isNull, isNotNull;
import '../../test_helpers.dart';

void main() {
  group('ExerciseRepository', () {
    late AppDatabase database;
    late ExerciseRepository exerciseRepository;

    setUp(() async {
      database = createInMemoryDb();
      exerciseRepository = ExerciseRepository(database);

      // Insert some test data
      await _insertTestData(database);
    });

    tearDown(() async {
      await database.close();
    });

    group('Exercise Operations', () {
      test('should get all exercises', () async {
        // Act
        final exercises = await exerciseRepository.getAllExercises();

        // Assert
        expect(exercises, isNotEmpty);
        expect(exercises.length, greaterThanOrEqualTo(3));

        // Check that we have the test exercises
        final exerciseIds = exercises.map((e) => e.id).toSet();
        expect(exerciseIds, contains('bench_press'));
        expect(exerciseIds, contains('squats'));
        expect(exerciseIds, contains('pull_ups'));
      });

      test('should get exercises by muscle group', () async {
        // Act
        final chestExercises = await exerciseRepository
            .getExercisesByMuscleGroup('Chest');
        final legsExercises = await exerciseRepository
            .getExercisesByMuscleGroup('Quads');

        // Assert
        expect(chestExercises, isNotEmpty);

        expect(legsExercises, isNotEmpty);

        // Check specific exercises
        expect(chestExercises.any((e) => e.id == 'bench_press'), isTrue);
        expect(
          chestExercises.any((e) => e.id == 'incline_bench_press'),
          isTrue,
        );
        expect(legsExercises.any((e) => e.id == 'squats'), isTrue);
      });

      test('should get exercise by id', () async {
        // Act
        final exercise = await exerciseRepository.getExerciseById(
          'bench_press',
        );
        final nonExistentExercise = await exerciseRepository.getExerciseById(
          'non_existent',
        );

        // Assert
        expect(exercise, isNotNull);
        expect(exercise!.id, equals('bench_press'));
        expect(exercise.nameKey, equals('exercise.bench_press'));

        expect(nonExistentExercise, isNull);
      });

      test('should search exercises by name', () async {
        // Act
        final benchResults = await exerciseRepository.searchExercises('bench');
        final squatResults = await exerciseRepository.searchExercises('squat');
        final noResults = await exerciseRepository.searchExercises('xyz123');

        // Assert
        expect(benchResults, isNotEmpty);
        expect(benchResults.any((e) => e.id == 'bench_press'), isTrue);

        expect(squatResults, isNotEmpty);
        expect(squatResults.any((e) => e.id == 'squats'), isTrue);

        expect(noResults, isEmpty);
      });

      test('should get exercises with muscle groups', () async {
        // Act
        final exercisesWithGroups = await exerciseRepository
            .getExercisesWithMuscleGroups();

        // Assert
        expect(exercisesWithGroups, isNotEmpty);

        // Check that each result has the expected structure
        for (final result in exercisesWithGroups) {
          expect(result['exercise'], isNotNull);
          expect(result['muscleGroups'], isNotNull);
          expect(result['primaryMuscleGroup'], isA<MuscleGroup?>());

          final exercise = result['exercise'] as Exercise;
          final muscleGroups =
              result['muscleGroups'] as List<Map<String, dynamic>>;

          expect(exercise.id, isNotEmpty);
          expect(muscleGroups, isNotEmpty);

          // Check that at least one muscle group is marked as primary
          final hasPrimary = muscleGroups.any((mg) => mg['isPrimary'] == true);
          expect(hasPrimary, isTrue);
        }
      });
    });

    group('Muscle Group Operations', () {
      test('should get all muscle groups', () async {
        // Act
        final muscleGroups = await exerciseRepository.getAllMuscleGroups();

        // Assert
        expect(muscleGroups, isNotEmpty);
        expect(muscleGroups.length, greaterThanOrEqualTo(3));

        // Check that we have the test muscle groups
        final groupIds = muscleGroups.map((g) => g.id).toSet();
        expect(groupIds, contains('Chest'));
        expect(groupIds, contains('Back'));
        expect(groupIds, contains('Quads'));
      });

      test('should get muscle group by id', () async {
        // Act
        final chestGroup = await exerciseRepository.getMuscleGroupById('Chest');
        final nonExistentGroup = await exerciseRepository.getMuscleGroupById(
          'NonExistent',
        );

        // Assert
        expect(chestGroup, isNotNull);
        expect(chestGroup!.id, equals('Chest'));
        expect(chestGroup.nameKey, equals('muscle_group.chest'));

        expect(nonExistentGroup, isNull);
      });
    });

    group('Model Conversion', () {
      test('should convert exercise to ExerciseInfo model', () async {
        // Arrange
        final exercise = await exerciseRepository.getExerciseById(
          'bench_press',
        );
        final primaryMuscleGroup = await exerciseRepository
            .getPrimaryMuscleGroupForExercise('bench_press');

        // Act
        final exerciseInfo = exerciseRepository.exerciseToInfo(
          exercise!,
          primaryMuscleGroupId: primaryMuscleGroup?.id,
        );

        // Assert
        expect(exerciseInfo.id, equals('bench_press'));
        expect(exerciseInfo.nameKey, equals('exercise.bench_press'));
        expect(exerciseInfo.muscleGroup.value, equals('Chest'));
        expect(exerciseInfo.logType.value, equals('weight'));
      });
    });

    group('Many-to-Many Relationships', () {
      test('should get muscle groups for exercise', () async {
        // Act
        final benchPressMuscleGroups = await exerciseRepository
            .getMuscleGroupsForExercise('bench_press');

        // Assert
        expect(benchPressMuscleGroups, isNotEmpty);
        expect(
          benchPressMuscleGroups.length,
          greaterThanOrEqualTo(2),
        ); // At least Chest and one secondary

        // Check primary muscle group
        final primaryGroup = benchPressMuscleGroups.firstWhere(
          (mg) => mg['isPrimary'] == true,
        );
        final primaryMuscleGroup = primaryGroup['muscleGroup'] as MuscleGroup;
        expect(primaryMuscleGroup.id, equals('Chest'));

        // Check that we have secondary muscle groups
        final secondaryGroups = benchPressMuscleGroups.where(
          (mg) => mg['isPrimary'] == false,
        );
        expect(secondaryGroups, isNotEmpty);
      });

      test('should get primary muscle group for exercise', () async {
        // Act
        final primaryMuscleGroup = await exerciseRepository
            .getPrimaryMuscleGroupForExercise('bench_press');
        final nonExistentPrimary = await exerciseRepository
            .getPrimaryMuscleGroupForExercise('non_existent');

        // Assert
        expect(primaryMuscleGroup, isNotNull);
        expect(primaryMuscleGroup!.id, equals('Chest'));

        expect(nonExistentPrimary, isNull);
      });

      test(
        'should get exercises by muscle group with primary filter',
        () async {
          // Act
          final allChestExercises = await exerciseRepository
              .getExercisesByMuscleGroup2('Chest', primaryOnly: false);
          final primaryChestExercises = await exerciseRepository
              .getExercisesByMuscleGroup2('Chest', primaryOnly: true);

          // Assert
          expect(allChestExercises, isNotEmpty);
          expect(primaryChestExercises, isNotEmpty);
          expect(
            primaryChestExercises.length,
            lessThanOrEqualTo(allChestExercises.length),
          );

          // Check that bench_press is in primary chest exercises
          expect(
            primaryChestExercises.any((e) => e.id == 'bench_press'),
            isTrue,
          );
        },
      );

      test('should handle exercises with multiple muscle groups', () async {
        // Act
        final benchPressGroups = await exerciseRepository
            .getMuscleGroupsForExercise('bench_press');
        final pullUpsGroups = await exerciseRepository
            .getMuscleGroupsForExercise('pull_ups');

        // Assert
        // Bench press should target multiple groups
        expect(benchPressGroups.length, greaterThan(1));

        // Pull ups should target multiple groups
        expect(pullUpsGroups.length, greaterThan(1));

        // Each should have exactly one primary muscle group
        final benchPressPrimary = benchPressGroups.where(
          (mg) => mg['isPrimary'] == true,
        );
        final pullUpsPrimary = pullUpsGroups.where(
          (mg) => mg['isPrimary'] == true,
        );

        expect(benchPressPrimary.length, equals(1));
        expect(pullUpsPrimary.length, equals(1));
      });
    });

    group('Data Validation', () {
      test('should handle empty search queries', () async {
        // Act
        final results = await exerciseRepository.searchExercises('');

        // Assert
        expect(results, isNotEmpty); // Empty query should return all exercises
      });

      test('should handle case-insensitive searches', () async {
        // Act
        final lowerCase = await exerciseRepository.searchExercises('bench');
        final upperCase = await exerciseRepository.searchExercises('BENCH');
        final mixedCase = await exerciseRepository.searchExercises('BeNcH');

        // Assert
        expect(lowerCase.length, equals(upperCase.length));
        expect(lowerCase.length, equals(mixedCase.length));
        expect(lowerCase.any((e) => e.id == 'bench_press'), isTrue);
      });

      test('should return empty list for non-existent muscle group', () async {
        // Act
        final exercises = await exerciseRepository.getExercisesByMuscleGroup(
          'NonExistent',
        );

        // Assert
        expect(exercises, isEmpty);
      });
    });
  });
}

// Helper function to insert test data
Future<void> _insertTestData(AppDatabase database) async {
  // Insert test muscle groups
  final muscleGroupsData = [
    {'id': 'Chest', 'name_key': 'muscle_group.chest'},
    {'id': 'Back', 'name_key': 'muscle_group.back'},
    {'id': 'Quads', 'name_key': 'muscle_group.quads'},
    {'id': 'Shoulders', 'name_key': 'muscle_group.shoulders'},
    {'id': 'Triceps', 'name_key': 'muscle_group.triceps'},
    {'id': 'Biceps', 'name_key': 'muscle_group.biceps'},
  ];

  for (final group in muscleGroupsData) {
    await database
        .into(database.muscleGroups)
        .insertOnConflictUpdate(
          MuscleGroupsCompanion.insert(
            id: group['id']!,
            nameKey: group['name_key']!,
          ),
        );
  }

  // Insert test exercises (without muscleGroupId)
  final exercisesData = [
    {
      'id': 'bench_press',
      'name_key': 'exercise.bench_press',
      'log_type': 'weight',
    },
    {
      'id': 'incline_bench_press',
      'name_key': 'exercise.incline_bench_press',
      'log_type': 'weight',
    },
    {'id': 'pull_ups', 'name_key': 'exercise.pull_ups', 'log_type': 'weight'},
    {'id': 'squats', 'name_key': 'exercise.squats', 'log_type': 'weight'},
    {
      'id': 'shoulder_press',
      'name_key': 'exercise.shoulder_press',
      'log_type': 'weight',
    },
  ];

  for (final exercise in exercisesData) {
    await database
        .into(database.exercises)
        .insertOnConflictUpdate(
          ExercisesCompanion.insert(
            id: exercise['id']!,
            nameKey: exercise['name_key']!,
            logType: exercise['log_type']!,
          ),
        );
  }

  // Insert exercise-muscle group mappings
  final mappingsData = [
    {
      'exercise_id': 'bench_press',
      'muscle_group_id': 'Chest',
      'is_primary': true,
    },
    {
      'exercise_id': 'bench_press',
      'muscle_group_id': 'Shoulders',
      'is_primary': false,
    },
    {
      'exercise_id': 'bench_press',
      'muscle_group_id': 'Triceps',
      'is_primary': false,
    },

    {
      'exercise_id': 'incline_bench_press',
      'muscle_group_id': 'Chest',
      'is_primary': true,
    },
    {
      'exercise_id': 'incline_bench_press',
      'muscle_group_id': 'Shoulders',
      'is_primary': false,
    },

    {'exercise_id': 'pull_ups', 'muscle_group_id': 'Back', 'is_primary': true},
    {
      'exercise_id': 'pull_ups',
      'muscle_group_id': 'Biceps',
      'is_primary': false,
    },

    {'exercise_id': 'squats', 'muscle_group_id': 'Quads', 'is_primary': true},

    {
      'exercise_id': 'shoulder_press',
      'muscle_group_id': 'Shoulders',
      'is_primary': true,
    },
    {
      'exercise_id': 'shoulder_press',
      'muscle_group_id': 'Triceps',
      'is_primary': false,
    },
  ];

  for (final mapping in mappingsData) {
    await database
        .into(database.exerciseMuscleGroups)
        .insertOnConflictUpdate(
          ExerciseMuscleGroupsCompanion.insert(
            exerciseId: mapping['exercise_id'] as String,
            muscleGroupId: mapping['muscle_group_id'] as String,
            isPrimary: Value(mapping['is_primary'] as bool),
          ),
        );
  }
}
