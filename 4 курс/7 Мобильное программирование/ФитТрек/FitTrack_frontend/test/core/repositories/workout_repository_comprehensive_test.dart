import 'package:flutter_test/flutter_test.dart';
import 'package:fit_tracker/core/repositories/workout_repository.dart';
import 'package:fit_tracker/core/storage/database.dart';
import 'package:fit_tracker/core/models/models.dart';
import '../../test_helpers.dart';

void main() {
  group('WorkoutRepository - Extended Tests', () {
    late AppDatabase database;
    late WorkoutRepository repository;

    setUp(() async {
      database = createInMemoryDb();
      repository = WorkoutRepository(database);
    });

    tearDown(() async {
      await database.close();
    });

    group('Workout Operations', () {
      test('createWorkout creates workout with correct data', () async {
        final workout = await repository.createWorkout(
          1,
          DateTime(2024, 1, 15),
        );

        expect(workout.userId, 1);
        expect(workout.date, DateTime(2024, 1, 15));
        expect(workout.isDeleted, isFalse);
      });

      test(
        'createSimpleWorkout creates workout with current date by default',
        () async {
          final workoutId = await repository.createSimpleWorkout();

          expect(workoutId, isNotEmpty);
          final workout = await repository.getWorkoutById(workoutId);
          expect(workout, isNotNull);
          expect(workout!.date.day, DateTime.now().day);
        },
      );

      test('createSimpleWorkout creates workout with specified date', () async {
        final testDate = DateTime(2024, 2, 20);
        final workoutId = await repository.createSimpleWorkout(date: testDate);

        final workout = await repository.getWorkoutById(workoutId);
        expect(workout!.date, testDate);
      });

      test('watchWorkouts returns stream of user workouts', () async {
        // Create test workouts
        await repository.createWorkout(1, DateTime(2024, 1, 15));
        await repository.createWorkout(1, DateTime(2024, 1, 16));
        await repository.createWorkout(2, DateTime(2024, 1, 17));

        final stream = repository.watchWorkouts(1);
        final workouts = await stream.first;

        expect(workouts.length, 2);
        expect(workouts.every((w) => w.userId == 1), isTrue);
      });

      test('updateWorkout updates workout data', () async {
        final workout = await repository.createWorkout(
          1,
          DateTime(2024, 1, 15),
        );

        await repository.updateWorkout(workout);

        final fetched = await repository.getWorkoutById(workout.id);
        expect(fetched, isNotNull);
      });

      test('deleteWorkout soft deletes workout', () async {
        final workout = await repository.createWorkout(
          1,
          DateTime(2024, 1, 15),
        );

        await repository.deleteWorkout(workout.id);

        final deleted = await repository.getWorkoutById(workout.id);
        expect(deleted, isNull);
      });
    });

    group('Exercise Group Operations', () {
      test('createExerciseGroup creates group with correct data', () async {
        final workoutId = await repository.createSimpleWorkout();

        final group = await repository.createExerciseGroup(workoutId, 1);

        expect(group.workoutId, workoutId);
        expect(group.orderIndex, 1);
        expect(group.isDeleted, isFalse);
      });

      test('getExerciseGroupsForWorkout returns groups in order', () async {
        final workoutId = await repository.createSimpleWorkout();

        await repository.createExerciseGroup(workoutId, 2);
        await repository.createExerciseGroup(workoutId, 1);
        await repository.createExerciseGroup(workoutId, 3);

        final groups = await repository.getExerciseGroupsForWorkout(workoutId);

        expect(groups.length, 3);
        expect(groups[0].orderIndex, 1);
        expect(groups[1].orderIndex, 2);
        expect(groups[2].orderIndex, 3);
      });
    });

    group('Exercise Log Operations', () {
      test('createExerciseLog creates log with correct data', () async {
        final workoutId = await repository.createSimpleWorkout();
        final group = await repository.createExerciseGroup(workoutId, 1);

        final log = await repository.createExerciseLog(
          exerciseGroupId: group.id,
          exerciseId: 'bench-press',
          orderInGroup: 0,
        );

        expect(log.exerciseGroupId, group.id);
        expect(log.exerciseId, 'bench-press');
        expect(log.orderInGroup, 0);
      });

      test('getExerciseLogsForGroup returns logs in order', () async {
        final workoutId = await repository.createSimpleWorkout();
        final group = await repository.createExerciseGroup(workoutId, 1);

        await repository.createExerciseLog(
          exerciseGroupId: group.id,
          exerciseId: 'exercise-2',
          orderInGroup: 1,
        );
        await repository.createExerciseLog(
          exerciseGroupId: group.id,
          exerciseId: 'exercise-1',
          orderInGroup: 0,
        );

        final logs = await repository.getExerciseLogsForGroup(group.id);

        expect(logs.length, 2);
        expect(logs[0].orderInGroup, 0);
        expect(logs[1].orderInGroup, 1);
      });
    });

    group('Set Log Operations', () {
      test('createSetLog creates set with correct metrics', () async {
        final workoutId = await repository.createSimpleWorkout();
        final group = await repository.createExerciseGroup(workoutId, 1);
        final exerciseLog = await repository.createExerciseLog(
          exerciseGroupId: group.id,
          exerciseId: 'bench-press',
          orderInGroup: 0,
        );

        const metrics = SetMetrics(reps: 10, weight: 100.0);
        final setLog = await repository.createSetLog(
          exerciseLogId: exerciseLog.id,
          metrics: metrics,
          isWarmup: false,
        );

        expect(setLog.exerciseLogId, exerciseLog.id);
        expect(setLog.isWarmup, false);

        final parsedMetrics = SetMetrics.fromJsonString(setLog.metrics);
        expect(parsedMetrics.reps, 10);
        expect(parsedMetrics.weight, 100.0);
      });

      test('createSetLog handles warmup sets correctly', () async {
        final workoutId = await repository.createSimpleWorkout();
        final group = await repository.createExerciseGroup(workoutId, 1);
        final exerciseLog = await repository.createExerciseLog(
          exerciseGroupId: group.id,
          exerciseId: 'bench-press',
          orderInGroup: 0,
        );

        const metrics = SetMetrics(reps: 5, weight: 60.0);
        final warmupSet = await repository.createSetLog(
          exerciseLogId: exerciseLog.id,
          metrics: metrics,
          isWarmup: true,
        );

        expect(warmupSet.isWarmup, true);
      });

      test('updateSetLog updates metrics correctly', () async {
        final workoutId = await repository.createSimpleWorkout();
        final group = await repository.createExerciseGroup(workoutId, 1);
        final exerciseLog = await repository.createExerciseLog(
          exerciseGroupId: group.id,
          exerciseId: 'bench-press',
          orderInGroup: 0,
        );

        const originalMetrics = SetMetrics(reps: 8, weight: 80.0);
        final setLog = await repository.createSetLog(
          exerciseLogId: exerciseLog.id,
          metrics: originalMetrics,
        );

        const updatedMetrics = SetMetrics(reps: 10, weight: 85.0);
        await repository.updateSetLog(setLog, updatedMetrics);

        final updated = await repository.getSetLogById(setLog.id);
        final parsedMetrics = SetMetrics.fromJsonString(updated!.metrics);
        expect(parsedMetrics.reps, 10);
        expect(parsedMetrics.weight, 85.0);
      });
    });

    group('Complex Workout Operations', () {
      test(
        'addExerciseToWorkout creates complete exercise structure',
        () async {
          final workoutId = await repository.createSimpleWorkout();

          final groupId = await repository.addExerciseToWorkout(
            workoutId: workoutId,
            exerciseId: 'bench-press',
            order: 1,
          );

          final group = await repository.getExerciseGroupById(groupId);
          expect(group, isNotNull);
          expect(group!.workoutId, workoutId);
          expect(group.orderIndex, 1);

          final logs = await repository.getExerciseLogsForGroup(groupId);
          expect(logs.length, 1);
          expect(logs[0].exerciseId, 'bench-press');
        },
      );

      test('addSetToExerciseGroup creates set with all data', () async {
        final workoutId = await repository.createSimpleWorkout();
        final groupId = await repository.addExerciseToWorkout(
          workoutId: workoutId,
          exerciseId: 'bench-press',
          order: 1,
        );

        final setId = await repository.addSetToExerciseGroup(
          exerciseGroupId: groupId,
          setNumber: 1,
          reps: 10,
          weight: 100.0,
          restTime: 120,
          isCompleted: true,
        );

        expect(setId, isNotEmpty);
        final setLog = await repository.getSetLogById(setId);
        expect(setLog, isNotNull);

        final metrics = SetMetrics.fromJsonString(setLog!.metrics);
        expect(metrics.reps, 10);
        expect(metrics.weight, 100.0);
      });
    });

    group('Edge Cases', () {
      test('getWorkoutById returns null for non-existent workout', () async {
        final result = await repository.getWorkoutById('non-existent');
        expect(result, isNull);
      });

      test(
        'getExerciseGroupById returns null for non-existent group',
        () async {
          final result = await repository.getExerciseGroupById('non-existent');
          expect(result, isNull);
        },
      );

      test('getExerciseLogById returns null for non-existent log', () async {
        final result = await repository.getExerciseLogById('non-existent');
        expect(result, isNull);
      });

      test('getSetLogById returns null for non-existent set', () async {
        final result = await repository.getSetLogById('non-existent');
        expect(result, isNull);
      });

      test(
        'watchWorkouts returns empty stream for user with no workouts',
        () async {
          final stream = repository.watchWorkouts(999);
          final workouts = await stream.first;
          expect(workouts, isEmpty);
        },
      );

      test(
        'getExerciseGroupsForWorkout returns empty list for workout with no groups',
        () async {
          final workoutId = await repository.createSimpleWorkout();
          final groups = await repository.getExerciseGroupsForWorkout(
            workoutId,
          );
          expect(groups, isEmpty);
        },
      );

      test(
        'getExerciseLogsForGroup returns empty list for group with no logs',
        () async {
          final workoutId = await repository.createSimpleWorkout();
          final group = await repository.createExerciseGroup(workoutId, 1);
          final logs = await repository.getExerciseLogsForGroup(group.id);
          expect(logs, isEmpty);
        },
      );

      test(
        'getSetLogsForExercise returns empty list for exercise with no sets',
        () async {
          final workoutId = await repository.createSimpleWorkout();
          final group = await repository.createExerciseGroup(workoutId, 1);
          final exerciseLog = await repository.createExerciseLog(
            exerciseGroupId: group.id,
            exerciseId: 'bench-press',
            orderInGroup: 0,
          );

          final sets = await repository.getSetLogsForExercise(exerciseLog.id);
          expect(sets, isEmpty);
        },
      );

      test('handles invalid JSON in set metrics gracefully', () async {
        // This would test database level constraint handling
        // For now, we trust that SetMetrics.fromJsonString handles invalid JSON
        expect(
          () => SetMetrics.fromJsonString('invalid json'),
          throwsA(isA<FormatException>()),
        );
      });
    });
  });
}
