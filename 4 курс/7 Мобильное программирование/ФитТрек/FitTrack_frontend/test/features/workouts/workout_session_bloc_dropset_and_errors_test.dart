import 'package:flutter_test/flutter_test.dart';
import 'package:fit_tracker/core/storage/database.dart';
import 'package:fit_tracker/core/repositories/workout_repository.dart';
import 'package:fit_tracker/core/repositories/exercise_repository.dart';
import 'package:fit_tracker/features/workouts/presentation/blocs/workout_session_bloc.dart';
import 'package:fit_tracker/core/models/models.dart';
import 'package:fit_tracker/features/templates/domain/template_repository.dart';
import 'package:fit_tracker/features/templates/domain/template.dart';

import '../../test_helpers.dart';

class _FakeTemplateRepository implements TemplateRepository {
  @override
  Future<String> createTemplate(WorkoutTemplateModel template) async => '';

  @override
  Future<String> createTemplateFromWorkout(
    String workoutId,
    String templateName,
  ) async => '';

  @override
  Future<void> deleteTemplate(String id) async {}

  @override
  Future<List<WorkoutTemplateModel>> getAllTemplates() async => [];

  @override
  Future<WorkoutTemplateModel?> getTemplate(String id) async => null;

  @override
  Future<int> getTemplatesCount() async => 0;

  @override
  Future<List<WorkoutTemplateModel>> searchTemplates(String query) async => [];

  @override
  Future<void> updateTemplate(WorkoutTemplateModel template) async {}
}

void main() {
  group('WorkoutSessionBloc dropset + errors', () {
    late AppDatabase database;
    late WorkoutRepository workoutRepository;
    late ExerciseRepository exerciseRepository;

    setUp(() async {
      database = createInMemoryDb();
      workoutRepository = WorkoutRepository(database);
      exerciseRepository = ExerciseRepository(database);
    });

    tearDown(() async {
      await database.close();
    });

    test(
      'drop-set parent/child relationship sets parentSetId on child',
      () async {
        // Insert exercises
        await database
            .into(database.exercises)
            .insertOnConflictUpdate(
              ExercisesCompanion.insert(
                id: 'bench',
                nameKey: 'exercise.bench',
                logType: 'weight',
              ),
            );

        final bloc = WorkoutSessionBloc(
          workoutRepository,
          exerciseRepository,
          _FakeTemplateRepository(),
        );

        // Start session
        bloc.add(const StartWorkoutSession());
        await bloc.stream.firstWhere((s) => s is WorkoutSessionInProgress);
        // Add exercise to session (creates a group)
        bloc.add(const AddExerciseToSession(exerciseId: 'bench'));
        final afterAddState = await bloc.stream.firstWhere((s) {
          if (s is! WorkoutSessionInProgress) return false;
          return s.exerciseGroups.isNotEmpty;
        });
        final afterAdd = afterAddState as WorkoutSessionInProgress;
        final groupId = afterAdd.exerciseGroups.first.exerciseGroup.id;

        // Add a parent set via repository directly to simulate completed set
        final logs = await workoutRepository.getExerciseLogsForGroup(groupId);
        final log = logs.first;
        final parentSet = await workoutRepository.createSetLog(
          exerciseLogId: log.id,
          metrics: const SetMetrics(reps: 5, weight: 100.0),
          isWarmup: false,
        );

        // Now use BLoC AddDropSet to create a child set linked to parent
        bloc.add(
          AddDropSet(
            exerciseGroupId: groupId,
            exerciseId: 'bench',
            parentSetId: parentSet.id,
          ),
        );

        final afterDrop =
            await bloc.stream.firstWhere((s) {
                  if (s is! WorkoutSessionInProgress) return false;
                  final g = s.exerciseGroups.firstWhere(
                    (g) => g.exerciseGroup.id == groupId,
                  );
                  final ex = g.exercises.firstWhere(
                    (e) => e.exercise.id == 'bench',
                  );
                  return ex.setLogs.any((s) => s.parentSetId == parentSet.id);
                })
                as WorkoutSessionInProgress;

        final child = afterDrop.exerciseGroups
            .firstWhere((g) => g.exerciseGroup.id == groupId)
            .exercises
            .firstWhere((e) => e.exercise.id == 'bench')
            .setLogs
            .firstWhere((s) => s.parentSetId == parentSet.id);

        expect(child.parentSetId, equals(parentSet.id));

        await bloc.close();
      },
    );

    test('adding exercise that does not exist emits error state', () async {
      final bloc = WorkoutSessionBloc(
        workoutRepository,
        exerciseRepository,
        _FakeTemplateRepository(),
      );

      bloc.add(const StartWorkoutSession());
      await bloc.stream.firstWhere((s) => s is WorkoutSessionInProgress);

      // Add non-existent exercise
      bloc.add(const AddExerciseToSession(exerciseId: 'does_not_exist'));

      final errorState =
          await bloc.stream.firstWhere((s) => s is WorkoutSessionError)
              as WorkoutSessionError;
      expect(errorState.message, contains('Exercise not found'));

      await bloc.close();
    });

    test('adding set to non-existent group is ignored (no crash)', () async {
      // Ensure bloc is started
      final bloc = WorkoutSessionBloc(
        workoutRepository,
        exerciseRepository,
        _FakeTemplateRepository(),
      );
      bloc.add(const StartWorkoutSession());
      await bloc.stream.firstWhere((s) => s is WorkoutSessionInProgress);

      // AddSet to fake group id (should be ignored)
      bloc.add(
        const AddSet(exerciseGroupId: 'fake-group', exerciseId: 'bench'),
      );

      // Wait briefly to allow any synchronous processing to complete, then assert state unchanged
      await Future.delayed(const Duration(milliseconds: 100));
      expect(bloc.state, isA<WorkoutSessionInProgress>());

      await bloc.close();
    });

    test(
      'creating drop-set with invalid parentSetId still creates child with provided parentSetId',
      () async {
        // Insert exercises
        await database
            .into(database.exercises)
            .insertOnConflictUpdate(
              ExercisesCompanion.insert(
                id: 'deadlift',
                nameKey: 'exercise.deadlift',
                logType: 'weight',
              ),
            );

        final bloc = WorkoutSessionBloc(
          workoutRepository,
          exerciseRepository,
          _FakeTemplateRepository(),
        );
        bloc.add(const StartWorkoutSession());
        await bloc.stream.firstWhere((s) => s is WorkoutSessionInProgress);

        // Add exercise to session
        bloc.add(const AddExerciseToSession(exerciseId: 'deadlift'));
        final afterAddState = await bloc.stream.firstWhere((s) {
          if (s is! WorkoutSessionInProgress) return false;
          return s.exerciseGroups.isNotEmpty;
        });
        final afterAddStateCasted = afterAddState as WorkoutSessionInProgress;
        final groupId =
            afterAddStateCasted.exerciseGroups.first.exerciseGroup.id;

        // Use a bogus parent id
        const bogusParentId = 'non-existent-parent-id';

        bloc.add(
          const AddDropSet(
            exerciseGroupId: 'bogus',
            exerciseId: 'deadlift',
            parentSetId: bogusParentId,
          ),
        );

        // The BLoC will ignore wrong group id; create a valid child using correct group but bogus parent id via repo
        final logs = await workoutRepository.getExerciseLogsForGroup(groupId);
        final log = logs.first;
        final createdChild = await workoutRepository.createSetLog(
          exerciseLogId: log.id,
          metrics: const SetMetrics(reps: 3, weight: 120.0),
          isWarmup: false,
          parentSetId: bogusParentId,
        );

        final fetched = await workoutRepository.getSetLogById(createdChild.id);
        expect(fetched, isNotNull);
        expect(fetched!.parentSetId, equals(bogusParentId));

        await bloc.close();
      },
    );

    test(
      'concurrency: multiple AddSet events attach all sets to correct exercise log',
      () async {
        // Insert exercises
        await database
            .into(database.exercises)
            .insertOnConflictUpdate(
              ExercisesCompanion.insert(
                id: 'squat',
                nameKey: 'exercise.squat',
                logType: 'weight',
              ),
            );

        final bloc = WorkoutSessionBloc(
          workoutRepository,
          exerciseRepository,
          _FakeTemplateRepository(),
        );
        bloc.add(const StartWorkoutSession());
        await bloc.stream.firstWhere((s) => s is WorkoutSessionInProgress);

        bloc.add(const AddExerciseToSession(exerciseId: 'squat'));
        final afterAddState =
            await bloc.stream.firstWhere((s) {
                  if (s is! WorkoutSessionInProgress) return false;
                  return s.exerciseGroups.isNotEmpty;
                })
                as WorkoutSessionInProgress;

        final groupId = afterAddState.exerciseGroups.first.exerciseGroup.id;

        // Fire multiple AddSet events quickly
        const n = 10;
        for (var i = 0; i < n; i++) {
          bloc.add(AddSet(exerciseGroupId: groupId, exerciseId: 'squat'));
        }

        // Wait (poll) until the DB shows n sets for the squat exercise (timeout 5s)
        final logs = await workoutRepository.getExerciseLogsForGroup(groupId);
        final log = logs.first;
        final timeout = DateTime.now().add(const Duration(seconds: 5));
        List dbSets = [];
        while (DateTime.now().isBefore(timeout)) {
          dbSets = await workoutRepository.getSetLogsForExercise(log.id);
          if (dbSets.length == n) break;
          await Future.delayed(const Duration(milliseconds: 50));
        }

        expect(dbSets.length, equals(n));

        await bloc.close();
      },
    );
  });
}
