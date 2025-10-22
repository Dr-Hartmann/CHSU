import 'package:flutter_test/flutter_test.dart';
import 'package:fit_tracker/core/storage/database.dart';
import 'package:fit_tracker/core/repositories/workout_repository.dart';
import 'package:fit_tracker/core/repositories/exercise_repository.dart';
import 'package:fit_tracker/features/workouts/presentation/blocs/workout_session_bloc.dart';
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
  group('WorkoutSessionBloc full flow', () {
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
      'BLoC flow: adding sets attaches them to correct exercise logs',
      () async {
        // Insert two exercises required by the BLoC flow
        await database
            .into(database.exercises)
            .insertOnConflictUpdate(
              ExercisesCompanion.insert(
                id: 'bench_press',
                nameKey: 'exercise.bench_press',
                logType: 'weight',
              ),
            );

        await database
            .into(database.exercises)
            .insertOnConflictUpdate(
              ExercisesCompanion.insert(
                id: 'pull_ups',
                nameKey: 'exercise.pull_ups',
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
        final startState = await bloc.stream.firstWhere(
          (s) => s is WorkoutSessionInProgress,
        );
        expect((startState as WorkoutSessionInProgress).workout, isNotNull);

        // Add first exercise to session (creates a new group)
        bloc.add(const AddExerciseToSession(exerciseId: 'bench_press'));
        final afterFirstState = await bloc.stream.firstWhere((s) {
          if (s is! WorkoutSessionInProgress) return false;
          return s.exerciseGroups.length == 1;
        });
        final afterFirst = afterFirstState as WorkoutSessionInProgress;

        final groupId = afterFirst.exerciseGroups[0].exerciseGroup.id;

        // Add second exercise into the same group (turn into a superset)
        bloc.add(
          AddExerciseToGroup(exerciseGroupId: groupId, exerciseId: 'pull_ups'),
        );
        await bloc.stream.firstWhere((s) {
          if (s is! WorkoutSessionInProgress) return false;
          final g = s.exerciseGroups.firstWhere(
            (g) => g.exerciseGroup.id == groupId,
          );
          return g.exercises.length == 2;
        });

        // Add a set for bench_press via BLoC
        bloc.add(AddSet(exerciseGroupId: groupId, exerciseId: 'bench_press'));
        final afterSet1 =
            await bloc.stream.firstWhere((s) {
                  if (s is! WorkoutSessionInProgress) return false;
                  final g = s.exerciseGroups.firstWhere(
                    (g) => g.exerciseGroup.id == groupId,
                  );
                  final ex = g.exercises.firstWhere(
                    (e) => e.exercise.id == 'bench_press',
                  );
                  return ex.setLogs.length == 1;
                })
                as WorkoutSessionInProgress;

        final set1 = afterSet1.exerciseGroups
            .firstWhere((g) => g.exerciseGroup.id == groupId)
            .exercises
            .firstWhere((e) => e.exercise.id == 'bench_press')
            .setLogs
            .first;

        // Add a set for pull_ups via BLoC
        bloc.add(AddSet(exerciseGroupId: groupId, exerciseId: 'pull_ups'));
        final afterSet2 =
            await bloc.stream.firstWhere((s) {
                  if (s is! WorkoutSessionInProgress) return false;
                  final g = s.exerciseGroups.firstWhere(
                    (g) => g.exerciseGroup.id == groupId,
                  );
                  final ex = g.exercises.firstWhere(
                    (e) => e.exercise.id == 'pull_ups',
                  );
                  return ex.setLogs.length == 1;
                })
                as WorkoutSessionInProgress;

        final set2 = afterSet2.exerciseGroups
            .firstWhere((g) => g.exerciseGroup.id == groupId)
            .exercises
            .firstWhere((e) => e.exercise.id == 'pull_ups')
            .setLogs
            .first;

        // Finish workout
        bloc.add(
          const FinishWorkoutWithDuration(duration: Duration(seconds: 1)),
        );
        await bloc.stream.firstWhere((s) => s is WorkoutSessionCompleted);

        // Retrieve exercise logs from repository and verify mapping
        final logs = await workoutRepository.getExerciseLogsForGroup(groupId);
        final benchLog = logs.firstWhere((l) => l.exerciseId == 'bench_press');
        final pullLog = logs.firstWhere((l) => l.exerciseId == 'pull_ups');

        final dbSet1 = await workoutRepository.getSetLogById(set1.id);
        final dbSet2 = await workoutRepository.getSetLogById(set2.id);

        expect(dbSet1, isNotNull);
        expect(dbSet2, isNotNull);

        expect(dbSet1!.exerciseLogId, equals(benchLog.id));
        expect(dbSet2!.exerciseLogId, equals(pullLog.id));

        await bloc.close();
      },
    );
  });
}
