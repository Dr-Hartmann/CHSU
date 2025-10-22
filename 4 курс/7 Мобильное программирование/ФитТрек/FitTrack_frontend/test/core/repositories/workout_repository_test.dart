import 'package:flutter_test/flutter_test.dart';
import 'package:fit_tracker/core/repositories/workout_repository.dart';
import 'package:fit_tracker/core/storage/database.dart';
import 'package:fit_tracker/core/models/models.dart';
import '../../test_helpers.dart';

void main() {
  group('WorkoutRepository', () {
    late AppDatabase database;
    late WorkoutRepository workoutRepository;

    setUp(() async {
      database = createInMemoryDb();
      workoutRepository = WorkoutRepository(database);
    });

    tearDown(() async {
      await database.close();
    });

    group('Workout Operations', () {
      test('should create workout successfully', () async {
        // Arrange
        const userId = 1;
        final date = DateTime.now();

        // Act
        final workout = await workoutRepository.createWorkout(userId, date);

        // Assert
        expect(workout.id, isNotEmpty);
        expect(workout.userId, equals(userId));
        expect(workout.date, isA<DateTime>());
        expect(workout.date.year, equals(date.year));
        expect(workout.date.month, equals(date.month));
        expect(workout.date.day, equals(date.day));
        expect(workout.date.hour, equals(date.hour));
        expect(workout.date.minute, equals(date.minute));
        expect(workout.date.second, equals(date.second));
        expect(workout.isDeleted, isFalse);
      });

      test('should get workout by id', () async {
        // Arrange
        const userId = 1;
        final date = DateTime.now();
        final createdWorkout = await workoutRepository.createWorkout(
          userId,
          date,
        );

        // Act
        final workout = await workoutRepository.getWorkoutById(
          createdWorkout.id,
        );

        // Assert
        expect(workout, isNotNull);
        expect(workout!.id, equals(createdWorkout.id));
        expect(workout.userId, equals(userId));
      });

      test('should create simple workout successfully', () async {
        // Arrange
        final date = DateTime.now();

        // Act
        final workoutId = await workoutRepository.createSimpleWorkout(
          date: date,
        );

        // Assert
        expect(workoutId, isNotEmpty);

        final workout = await workoutRepository.getWorkoutById(workoutId);
        expect(workout, isNotNull);
        expect(workout!.date, isA<DateTime>());
        expect(workout.date.year, equals(date.year));
        expect(workout.date.month, equals(date.month));
        expect(workout.date.day, equals(date.day));
        expect(workout.date.hour, equals(date.hour));
        expect(workout.date.minute, equals(date.minute));
        expect(workout.date.second, equals(date.second));
      });

      test('should get recent workouts with limit', () async {
        // Arrange
        const userId = 1;

        // Create 5 workouts with different dates
        for (int i = 1; i <= 5; i++) {
          await workoutRepository.createWorkout(
            userId,
            DateTime.now().subtract(Duration(days: i)),
          );
        }

        // Act
        final recentWorkouts = await workoutRepository.getRecentWorkouts(
          userId,
          limit: 3,
        );

        // Assert
        expect(recentWorkouts.length, equals(3));
      });

      test('should get paginated workouts', () async {
        // Arrange
        const userId = 1;

        // Create 15 workouts with different dates
        for (int i = 1; i <= 15; i++) {
          await workoutRepository.createWorkout(
            userId,
            DateTime.now().subtract(Duration(days: i)),
          );
        }

        // Act - Get first page
        final firstPage = await workoutRepository.getPaginatedWorkouts(
          userId,
          page: 0,
          limit: 10,
        );

        // Act - Get second page
        final secondPage = await workoutRepository.getPaginatedWorkouts(
          userId,
          page: 1,
          limit: 10,
        );

        // Assert
        expect(firstPage.length, equals(10));
        expect(secondPage.length, equals(5));
      });

      test('should get total workout count', () async {
        // Arrange
        const userId = 1;

        // Create 7 workouts
        for (int i = 1; i <= 7; i++) {
          await workoutRepository.createWorkout(
            userId,
            DateTime.now().subtract(Duration(days: i)),
          );
        }

        // Act
        final totalCount = await workoutRepository.getTotalWorkoutCount(userId);

        // Assert
        expect(totalCount, equals(7));
      });

      test('should complete workout successfully', () async {
        // Arrange
        const userId = 1;
        final workout = await workoutRepository.createWorkout(
          userId,
          DateTime.now(),
        );
        final duration = const Duration(minutes: 45);

        // Act
        await workoutRepository.completeWorkout(workout.id, duration);

        // Assert
        final completedWorkout = await workoutRepository.getWorkoutById(
          workout.id,
        );
        expect(completedWorkout, isNotNull);
        expect(completedWorkout!.duration, equals(45)); // Duration in minutes
      });

      test('should delete workout successfully', () async {
        // Arrange
        const userId = 1;
        final workout = await workoutRepository.createWorkout(
          userId,
          DateTime.now(),
        );

        // Act
        await workoutRepository.deleteWorkout(workout.id);

        // Assert
        final deletedWorkout = await workoutRepository.getWorkoutById(
          workout.id,
        );
        expect(deletedWorkout, isNull);
      });
    });

    group('Exercise Group Operations', () {
      test('should create exercise group successfully', () async {
        // Arrange
        const userId = 1;
        final workout = await workoutRepository.createWorkout(
          userId,
          DateTime.now(),
        );

        // Act
        final exerciseGroup = await workoutRepository.createExerciseGroup(
          workout.id,
          1,
        );

        // Assert
        expect(exerciseGroup.id, isNotEmpty);
        expect(exerciseGroup.workoutId, equals(workout.id));
        expect(exerciseGroup.orderIndex, equals(1));
      });

      test('should get exercise groups for workout', () async {
        // Arrange
        const userId = 1;
        final workout = await workoutRepository.createWorkout(
          userId,
          DateTime.now(),
        );

        await workoutRepository.createExerciseGroup(workout.id, 1);
        await workoutRepository.createExerciseGroup(workout.id, 2);

        // Act
        final exerciseGroups = await workoutRepository
            .getExerciseGroupsForWorkout(workout.id);

        // Assert
        expect(exerciseGroups.length, equals(2));
        expect(exerciseGroups[0].orderIndex, equals(1));
        expect(exerciseGroups[1].orderIndex, equals(2));
      });
    });

    group('Exercise Log Operations', () {
      test('should create exercise log successfully', () async {
        // Arrange
        const userId = 1;
        final workout = await workoutRepository.createWorkout(
          userId,
          DateTime.now(),
        );
        final exerciseGroup = await workoutRepository.createExerciseGroup(
          workout.id,
          1,
        );

        // Act
        final exerciseLog = await workoutRepository.createExerciseLog(
          exerciseGroupId: exerciseGroup.id,
          exerciseId: 'bench_press',
          orderInGroup: 1,
        );

        // Assert
        expect(exerciseLog.id, isNotEmpty);
        expect(exerciseLog.exerciseGroupId, equals(exerciseGroup.id));
        expect(exerciseLog.exerciseId, equals('bench_press'));
        expect(exerciseLog.orderInGroup, equals(1));
      });

      test('should get exercise logs for group', () async {
        // Arrange
        const userId = 1;
        final workout = await workoutRepository.createWorkout(
          userId,
          DateTime.now(),
        );
        final exerciseGroup = await workoutRepository.createExerciseGroup(
          workout.id,
          1,
        );

        await workoutRepository.createExerciseLog(
          exerciseGroupId: exerciseGroup.id,
          exerciseId: 'bench_press',
          orderInGroup: 1,
        );
        await workoutRepository.createExerciseLog(
          exerciseGroupId: exerciseGroup.id,
          exerciseId: 'incline_dumbbell_press',
          orderInGroup: 2,
        );

        // Act
        final exerciseLogs = await workoutRepository.getExerciseLogsForGroup(
          exerciseGroup.id,
        );

        // Assert
        expect(exerciseLogs.length, equals(2));
        expect(exerciseLogs[0].orderInGroup, equals(1));
        expect(exerciseLogs[1].orderInGroup, equals(2));
      });
    });

    group('Set Log Operations', () {
      test('should create set log successfully', () async {
        // Arrange
        const userId = 1;
        final workout = await workoutRepository.createWorkout(
          userId,
          DateTime.now(),
        );
        final exerciseGroup = await workoutRepository.createExerciseGroup(
          workout.id,
          1,
        );
        final exerciseLog = await workoutRepository.createExerciseLog(
          exerciseGroupId: exerciseGroup.id,
          exerciseId: 'bench_press',
          orderInGroup: 1,
        );

        final metrics = const SetMetrics(
          reps: 10,
          weight: 80.0,
          duration: 30, // Duration in seconds as int
          distance: null,
        );

        // Act
        final setLog = await workoutRepository.createSetLog(
          exerciseLogId: exerciseLog.id,
          metrics: metrics,
          isWarmup: false,
        );

        // Assert
        expect(setLog.id, isNotEmpty);
        expect(setLog.exerciseLogId, equals(exerciseLog.id));
        expect(setLog.isWarmup, isFalse);
      });

      test('should get set logs for exercise', () async {
        // Arrange
        const userId = 1;
        final workout = await workoutRepository.createWorkout(
          userId,
          DateTime.now(),
        );
        final exerciseGroup = await workoutRepository.createExerciseGroup(
          workout.id,
          1,
        );
        final exerciseLog = await workoutRepository.createExerciseLog(
          exerciseGroupId: exerciseGroup.id,
          exerciseId: 'bench_press',
          orderInGroup: 1,
        );

        final metrics1 = const SetMetrics(
          reps: 10,
          weight: 80.0,
          duration: 30,
          distance: null,
        );
        final metrics2 = const SetMetrics(
          reps: 8,
          weight: 85.0,
          duration: 25,
          distance: null,
        );

        await workoutRepository.createSetLog(
          exerciseLogId: exerciseLog.id,
          metrics: metrics1,
          isWarmup: true,
        );
        await workoutRepository.createSetLog(
          exerciseLogId: exerciseLog.id,
          metrics: metrics2,
          isWarmup: false,
        );

        // Act
        final setLogs = await workoutRepository.getSetLogsForExercise(
          exerciseLog.id,
        );

        // Assert
        expect(setLogs.length, equals(2));
        expect(setLogs.any((s) => s.isWarmup == true), isTrue);
        expect(setLogs.any((s) => s.isWarmup == false), isTrue);
      });
    });

    group('Workout Statistics', () {
      test('should get workout statistics', () async {
        // Arrange
        const userId = 1;

        final now = DateTime.now();
        // Create workouts within this week
        await workoutRepository.createWorkout(userId, now);
        await workoutRepository.createWorkout(
          userId,
          now.subtract(Duration(days: now.weekday - 1)),
        ); // Start of week
        await workoutRepository.createWorkout(
          userId,
          now.subtract(const Duration(days: 10)),
        );

        // Act
        final stats = await workoutRepository.getWorkoutStats(userId);

        // Assert
        expect(stats.totalWorkouts, equals(3));
        expect(
          stats.weekWorkouts,
          equals(2),
        ); // Should include this week's workouts
      });
    });

    group('Error Handling', () {
      test('should return null for non-existent workout', () async {
        // Act
        final workout = await workoutRepository.getWorkoutById(
          'non-existent-id',
        );

        // Assert
        expect(workout, isNull);
      });

      test('should return null for non-existent exercise group', () async {
        // Act
        final exerciseGroup = await workoutRepository.getExerciseGroupById(
          'non-existent-id',
        );

        // Assert
        expect(exerciseGroup, isNull);
      });

      test('should return null for non-existent set log', () async {
        // Act
        final setLog = await workoutRepository.getSetLogById('non-existent-id');

        // Assert
        expect(setLog, isNull);
      });
    });
  });
}
