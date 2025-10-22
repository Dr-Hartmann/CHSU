import 'package:drift/native.dart';
import 'package:fit_tracker/core/storage/database.dart';

AppDatabase createInMemoryDb() {
  // Используем конструктор для тестов, который создает базу данных в памяти
  return AppDatabase.test(NativeDatabase.memory());
}
