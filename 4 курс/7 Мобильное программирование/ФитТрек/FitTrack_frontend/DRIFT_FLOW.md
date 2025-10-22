# Flow по внедрению сущности с Drift в Flutter-проект

Ниже — практический пошаговый guide (на русском) для внедрения сущности из SQLite (Drift) в Flutter-проект, организованный по feature/clean архитектуре. Файл содержит: что нужно от разработчика, пошаговый flow, чеклист файлов, примеры команд и часто встречающиеся ошибки.

## Короткий контракт
- Вход: определение таблицы (Drift table) + domain-модель + список операций (репозиторий/use-cases) + DI точка подключения + тесты.
- Выход: рабочая интеграция (table ⇄ data ⇄ domain ⇄ repository ⇄ use-cases ⇄ bloc/ui) с прогоном codegen и тестов.

Успех измеряется: `dart run build_runner build` без критических ошибок и `flutter test` — все тесты проходят.


## Структура файлов

```
lib/
├── core/
│   ├── storage/
│   │   ├── database.dart              # Основной класс базы данных
│   │   └── tables/
│   │       ├── workout_table.dart     # Определение таблицы тренировок
│   │       ├── exercise_table.dart    # Таблица упражнений
│   │       └── set_log_table.dart     # Таблица подходов
│   └── di/
│       └── injection_container.dart   # Настройка DI
├── features/
│   └── workouts/
│       ├── data/
│       │   ├── workout_model.dart           # Конвертация между Drift и Domain
│       │   └── workout_repository_impl.dart # Реализация репозитория
│       ├── domain/
│       │   ├── workout.dart                 # Domain entity
│       │   ├── workout_repository.dart      # Interface репозитория
│       │   └── workout_usecases.dart        # Use cases
│       └── presentation/
│           ├── workout_bloc.dart            # Bloc для state management
│           └── workout_list_page.dart       # UI компонент
└── main.dart                                # Точка входа
```

## Что нужно от человека (минимальный набор артефактов)
Для каждой сущности (например, `Workout`) требуется предоставить:
1. Таблица Drift: `lib/core/storage/tables/<entity>_table.dart` (описание полей, primaryKey). Обязательно: `String get tableName => 'literal';` — literal строка.
2. Data слой: `lib/features/<feature>/data/<entity>_model.dart` — мапперы `DriftData <-> Domain` и методы для создания Companion (`toCompanion()` / `toUpdateCompanion()`), использовать `Value(...)` и `Value.absentIfNull(...)`.
3. Domain слой: `lib/features/<feature>/domain/<entity>.dart` — immutable модель, `copyWith`, вычисляемые свойства.
4. Интерфейс репозитория: `lib/features/<feature>/domain/<entity>_repository.dart` (CRUD + фильтры + sync-helpers).
5. Реализация репозитория: `lib/features/<feature>/data/<entity>_repository_impl.dart` — использует `AppDatabase` и сгенерированные таблицы.
6. DI: feature-level module `lib/features/<feature>/di/<feature>_injection.dart` и вызов из `lib/core/di/injection_container.dart`.
7. Тесты: `test/<entity>_table_test.dart`, `test/<entity>_repository_test.dart` (+ дополнительные проверки).

## Пошаговый flow (реализация)
1) Создание Drift таблицы
   - Файл: `lib/core/storage/tables/<entity>_table.dart`.
   - Объявление: `@DataClassName('EntityData') class EntityTable extends Table { ... }`.
   - Важно: `@override String get tableName => 'entities';` — литерал.

2) Генерация кода
   - Запустить:
```pwsh
dart run build_runner build --delete-conflicting-outputs
# или (в режиме разработки)
dart run build_runner watch
```
   - Исправить ошибки/предупреждения: особенно те, что связаны с `tableName` и `const Constant(...)`.

3) Мапперы data ↔ domain
   - В `features/.../data/<entity>_model.dart` создать:
     - `extension EntityDataExtension on EntityData { Entity toDomain() { ... } }`
     - `extension EntityDomainExtension on Entity { EntityTableCompanion toCompanion() { ... } }`
   - Не забыть: `import 'package:drift/drift.dart';` для `Value`.

4) Реализация репозитория
   - Вставка: `_database.into(_database.entityTable).insert(companion)`.
   - Чтение: `_database.select(_database.entityTable).get()` или `getSingleOrNull()`.
   - Обновление: `.replace(companion)` или `.write(...)`.
   - Используйте `Uuid` для генерации id при необходимости.

5) DI и BLoC wiring
   - вынести registrations в feature module: `setupFeatureModule(GetIt getIt)`.
   - core DI (`setupDependencies`) регистрирует `AppDatabase` и вызывает `setupFeatureModule(getIt)`.

6) Тесты
   - Table test: вставка через `EntityTableCompanion.insert(...)` и чтение через `db.select(db.entityTable).get()`.
   - Repository tests: create/get/update/delete/getRecent/getByRange/search/dirty/markAsClean/getTotal/getAverage.
   - Запуск тестов:
```pwsh
flutter test --reporter=expanded
```

7) CI (рекомендуется)
   - GitHub Actions: установить Flutter, `flutter pub get`, `dart run build_runner build --delete-conflicting-outputs`, `flutter test`.

## Частые подводные камни и советы
- Drift codegen: `tableName` должен быть literal; defaults через `withDefault(const Constant(x))` требуют compile-time const.
- Companions: всегда используйте `Value(...)` или `Value.absent()`/`Value.absentIfNull(...)`.
- Nullability: domain-поля могут быть nullable; Drift Data-классы могут иметь non-null поля (если заданы defaults). Пропишите корректное преобразование.
- Миграции: задайте `MigrationStrategy` в `AppDatabase` (onCreate/onUpgrade).

## Короткий чеклист (copy-paste)
- [ ] `lib/core/storage/tables/<entity>_table.dart` (tableName literal)
- [ ] `lib/core/storage/database.dart` (part, @DriftDatabase, test ctor)
- [ ] `lib/features/<feature>/data/<entity>_model.dart` (toDomain, toCompanion)
- [ ] `lib/features/<feature>/domain/<entity>.dart` (domain entity)
- [ ] `lib/features/<feature>/domain/<entity>_repository.dart` (interface)
- [ ] `lib/features/<feature>/data/<entity>_repository_impl.dart` (implementation)
- [ ] `lib/features/<feature>/di/<feature>_injection.dart` (registrations)
- [ ] `lib/features/<feature>/presentation/...` (BLoC/UI)
- [ ] `test/<entity>_table_test.dart` и `test/<entity>_repository_test.dart`
- [ ] `dart run build_runner build` — без ошибок
- [ ] `flutter test` — все тесты проходят

## Команды (pwsh)
```pwsh
flutter pub get
dart run build_runner build --delete-conflicting-outputs
flutter test --reporter=expanded
```

