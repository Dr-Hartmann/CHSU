import 'dart:convert';
import 'dart:io';
import 'dart:math' as math;

import 'package:drift/native.dart';
import 'package:drift/drift.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:path/path.dart' as p;

import 'package:fit_tracker/core/storage/database.dart';

void main() {
  group('Full workout load test', () {
    test(
      'insert full workouts (with groups, logs and sets)',
      timeout: const Timeout(Duration(minutes: 90)),
      () async {
        final totalWorkouts =
            int.tryParse(Platform.environment['LOAD_TEST_TOTAL'] ?? '') ??
            10000;
        final chunkSize =
            int.tryParse(Platform.environment['LOAD_TEST_CHUNK'] ?? '') ??
            10000;
        final setsPerWorkout =
            int.tryParse(Platform.environment['SETS_PER_WORKOUT'] ?? '') ?? 25;
        final exercisesPerWorkout =
            int.tryParse(Platform.environment['EXERCISES_PER_WORKOUT'] ?? '') ??
            6;

        final tempDir = await Directory.systemTemp.createTemp(
          'fit_tracker_full_load_test',
        );
        final dbFile = File(p.join(tempDir.path, 'load_test_full.db'));
        if (dbFile.existsSync()) await dbFile.delete();

        final database = AppDatabase.test(
          NativeDatabase.createInBackground(dbFile),
        );

        try {
          // create a user to attach workouts to
          final nowMs = DateTime.now().millisecondsSinceEpoch;
          final userId = await database
              .into(database.users)
              .insert(
                UsersCompanion.insert(name: 'LoadTestUser', updatedAt: nowMs),
              );

          final insertTimer = Stopwatch()..start();

          var inserted = 0;
          final baseDate = DateTime(2020, 1, 1);

          while (inserted < totalWorkouts) {
            final batchSize = math.min(chunkSize, totalWorkouts - inserted);

            final workoutRows = <WorkoutsCompanion>[];
            final groupRows = <ExerciseGroupsCompanion>[];
            final exerciseLogRows = <ExerciseLogsCompanion>[];
            final setLogRows = <SetLogsCompanion>[];

            for (var i = 0; i < batchSize; i++) {
              final index = inserted + i;
              final wid = 'w_$index';
              final date = baseDate.add(Duration(minutes: index));
              workoutRows.add(
                WorkoutsCompanion.insert(
                  id: wid,
                  userId: userId,
                  date: date,
                  updatedAt: nowMs + index,
                ),
              );

              // distribute sets across exercises
              final baseSets = setsPerWorkout ~/ exercisesPerWorkout;
              var remainder = setsPerWorkout % exercisesPerWorkout;

              for (var eg = 0; eg < exercisesPerWorkout; eg++) {
                final gid = 'g_${index}_$eg';
                groupRows.add(
                  ExerciseGroupsCompanion.insert(
                    id: gid,
                    workoutId: wid,
                    orderIndex: eg,
                    updatedAt: nowMs + index,
                  ),
                );

                final elId = 'el_${index}_$eg';
                exerciseLogRows.add(
                  ExerciseLogsCompanion.insert(
                    id: elId,
                    exerciseGroupId: gid,
                    exerciseId: 'bench_press',
                    orderInGroup: eg,
                    updatedAt: nowMs + index,
                  ),
                );

                final setsForThis = baseSets + (remainder > 0 ? 1 : 0);
                if (remainder > 0) remainder--;

                for (var s = 0; s < setsForThis; s++) {
                  final sid = 's_${index}_${eg}_$s';
                  final metrics = jsonEncode({
                    'reps': 8 + (s % 5),
                    'weight': 60 + (s % 20),
                  });
                  setLogRows.add(
                    SetLogsCompanion.insert(
                      id: sid,
                      exerciseLogId: elId,
                      metrics: metrics,
                      isWarmup: const Value(false),
                      parentSetId: const Value(null),
                      updatedAt: nowMs + index,
                    ),
                  );
                }
              }
            }

            // perform batch inserts
            await database.batch((b) {
              if (workoutRows.isNotEmpty) {
                b.insertAll(database.workouts, workoutRows);
              }
              if (groupRows.isNotEmpty) {
                b.insertAll(database.exerciseGroups, groupRows);
              }
              if (exerciseLogRows.isNotEmpty) {
                b.insertAll(database.exerciseLogs, exerciseLogRows);
              }
              if (setLogRows.isNotEmpty) {
                b.insertAll(database.setLogs, setLogRows);
              }
            });

            inserted += batchSize;
            if (inserted % math.max(10000, chunkSize) == 0 ||
                inserted == totalWorkouts) {
              // ignore: avoid_print
              print('Inserted $inserted / $totalWorkouts full workouts...');
            }
          }

          insertTimer.stop();
          // ignore: avoid_print
          print('Insertion took ${insertTimer.elapsed}');

          // pick a target to lookup
          final targetId = 'w_${(totalWorkouts * 0.4).floor()}';
          final searchTimer = Stopwatch()..start();
          final workout = database.select(database.workouts)
            ..where((t) => t.id.equals(targetId));
          final got = await workout.getSingleOrNull();
          searchTimer.stop();
          // ignore: avoid_print
          print(
            'Lookup for $targetId took ${searchTimer.elapsedMilliseconds} ms, found=${got != null}',
          );

          final statsTimer = Stopwatch()..start();
          // reuse a simple count of workouts as stats example
          final totalCount = await database
              .customSelect('SELECT COUNT(id) as c FROM workouts')
              .getSingle();
          statsTimer.stop();
          // ignore: avoid_print
          print(
            'Statistics computation took ${statsTimer.elapsed} (count=${totalCount.read<int>('c')})',
          );

          await database.customStatement('PRAGMA wal_checkpoint(FULL);');
          await database.close();

          final filesToMeasure = [
            dbFile,
            File('${dbFile.path}-wal'),
            File('${dbFile.path}-shm'),
          ];
          var totalBytes = 0;
          for (final f in filesToMeasure) {
            if (f.existsSync()) {
              final size = await f.length();
              totalBytes += size;
              // ignore: avoid_print
              print(
                'File ${f.path} size: ${(size / (1024 * 1024)).toStringAsFixed(2)} MB',
              );
            }
          }

          final totalMb = totalBytes / (1024 * 1024);
          // ignore: avoid_print
          print(
            'Total database footprint: ${totalMb.toStringAsFixed(2)} MB for $totalWorkouts full workouts',
          );
        } finally {
          try {
            await database.close();
          } catch (_) {}
          if (tempDir.existsSync()) await tempDir.delete(recursive: true);
        }
      },
    );
  });
}
