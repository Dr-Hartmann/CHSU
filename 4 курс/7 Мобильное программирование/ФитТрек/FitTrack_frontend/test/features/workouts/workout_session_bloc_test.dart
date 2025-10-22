import 'package:flutter_test/flutter_test.dart';
import 'package:fit_tracker/core/repositories/workout_repository.dart';
import 'package:fit_tracker/core/storage/database.dart';
import '../../test_helpers.dart';

void main() {
  group('WorkoutSession BLoC integration', () {
    late AppDatabase database;
    late WorkoutRepository workoutRepository;
    setUp(() async {
      database = createInMemoryDb();
      workoutRepository = WorkoutRepository(database);
    });

    tearDown(() async {
      await database.close();
    });

    test(
      'adding sets in superset attaches sets to correct exerciseLog',
      () async {
        // Start a new workout (simulate BLoC start)
        final workoutId = await workoutRepository.createSimpleWorkout();
        final workout = await workoutRepository.getWorkoutById(workoutId);
        expect(workout, isNotNull);

        // Create a group
        final groupId = await workoutRepository.createExerciseGroup(
          workoutId,
          0,
        );

        // Create two exercise logs in the group (superset)
        final log1 = await workoutRepository.createExerciseLog(
          exerciseGroupId: groupId.id,
          exerciseId: 'bench-press',
          orderInGroup: 0,
        );

        final log2 = await workoutRepository.createExerciseLog(
          exerciseGroupId: groupId.id,
          exerciseId: 'pull-ups',
          orderInGroup: 1,
        );

        // Add sets to each exercise via repository (simulating BLoC passing exerciseId)
        final setId1 = await workoutRepository.addSetToExerciseGroup(
          exerciseGroupId: groupId.id,
          exerciseId: 'bench-press',
          setNumber: 1,
          reps: 10,
          weight: 80.0,
          restTime: 60,
          isCompleted: true,
        );

        final setId2 = await workoutRepository.addSetToExerciseGroup(
          exerciseGroupId: groupId.id,
          exerciseId: 'pull-ups',
          setNumber: 1,
          reps: 8,
          weight: 0.0,
          restTime: 90,
          isCompleted: true,
        );

        // Verify each set is attached to the correct exerciseLog
        final set1 = await workoutRepository.getSetLogById(setId1);
        final set2 = await workoutRepository.getSetLogById(setId2);

        expect(set1, isNotNull);
        expect(set2, isNotNull);

        expect(set1!.exerciseLogId, equals(log1.id));
        expect(set2!.exerciseLogId, equals(log2.id));
      },
    );
  });
}
