import 'dart:io';
import 'dart:math' as math;

import 'package:drift/native.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:path/path.dart' as p;

import 'package:fit_tracker/core/repositories/workout_repository.dart';
import 'package:fit_tracker/core/storage/database.dart';

void main() {
  group('WorkoutRepository load test', () {
    test(
      'handles 1,000,000 workouts and reports performance metrics',
      timeout: const Timeout(Duration(minutes: 45)),
      () async {
        final totalWorkouts =
            int.tryParse(Platform.environment['LOAD_TEST_TOTAL'] ?? '') ??
            100000;
        final chunkSize =
            int.tryParse(Platform.environment['LOAD_TEST_CHUNK'] ?? '') ??
            10000;

        final tempDir = await Directory.systemTemp.createTemp(
          'fit_tracker_load_test',
        );
        final dbFile = File(p.join(tempDir.path, 'load_test.db'));

        if (dbFile.existsSync()) {
          await dbFile.delete();
        }

        final database = AppDatabase.test(
          NativeDatabase.createInBackground(dbFile),
        );
        final repository = WorkoutRepository(database);

        var dbClosed = false;

        try {
          final baseUpdatedAt = DateTime.now().millisecondsSinceEpoch;
          final userId = await database
              .into(database.users)
              .insert(
                UsersCompanion.insert(
                  name: 'Load Test User',
                  updatedAt: baseUpdatedAt,
                ),
              );

          final insertTimer = Stopwatch()..start();
          final baseDate = DateTime(2020, 1, 1);
          var inserted = 0;

          while (inserted < totalWorkouts) {
            final batchSize = math.min(chunkSize, totalWorkouts - inserted);

            await database.batch((batch) {
              batch.insertAll(
                database.workouts,
                List.generate(batchSize, (i) {
                  final index = inserted + i;
                  return WorkoutsCompanion.insert(
                    id: 'workout_$index',
                    userId: userId,
                    date: baseDate.add(Duration(minutes: index)),
                    updatedAt: baseUpdatedAt + index,
                  );
                }),
              );
            });

            inserted += batchSize;
            if (inserted % math.max(100000, chunkSize) == 0 ||
                inserted == totalWorkouts) {
              // ignore: avoid_print
              print('Inserted $inserted / $totalWorkouts workouts...');
            }
          }

          insertTimer.stop();
          // ignore: avoid_print
          print('Insertion took ${insertTimer.elapsed}');

          final targetId = 'workout_${(totalWorkouts * 0.4).floor()}';
          final searchTimer = Stopwatch()..start();
          final workout = await repository.getWorkoutById(targetId);
          searchTimer.stop();

          expect(
            workout,
            isNotNull,
            reason: 'Workout lookup should succeed even under heavy load',
          );
          // ignore: avoid_print
          print(
            'Lookup for $targetId took ${searchTimer.elapsedMicroseconds / 1000} ms',
          );

          final statsTimer = Stopwatch()..start();
          final stats = await repository.getWorkoutStats(userId);
          statsTimer.stop();

          expect(stats.totalWorkouts, totalWorkouts);
          // ignore: avoid_print
          print('Statistics computation took ${statsTimer.elapsed}');

          await database.customStatement('PRAGMA wal_checkpoint(FULL);');
          await database.close();
          dbClosed = true;

          final filesToMeasure = [
            dbFile,
            File('${dbFile.path}-wal'),
            File('${dbFile.path}-shm'),
          ];

          var totalBytes = 0;
          for (final file in filesToMeasure) {
            if (file.existsSync()) {
              final size = await file.length();
              totalBytes += size;
              // ignore: avoid_print
              print(
                'File ${file.path} size: '
                '${(size / (1024 * 1024)).toStringAsFixed(2)} MB',
              );
            }
          }

          final totalMb = totalBytes / (1024 * 1024);
          // ignore: avoid_print
          print(
            'Total database footprint: '
            '${totalMb.toStringAsFixed(2)} MB for $totalWorkouts workouts',
          );
        } finally {
          if (!dbClosed) {
            await database.close();
          }

          if (tempDir.existsSync()) {
            await tempDir.delete(recursive: true);
          }
        }
      },
    );
  });
}
