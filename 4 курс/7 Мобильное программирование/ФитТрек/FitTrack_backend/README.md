# Краткая сводка

## Команды перед пушем

### 1. Проверка форматирования кода
Проверьте, что код соответствует правилам форматирования .NET (без применения изменений):
```bash
dotnet format ./FitTrack.sln --verify-no-changes
```
Если команда завершается с ошибкой (exit code 1), примените форматирование:
```bash
dotnet format ./FitTrack.sln
```

### 2. Запуск тестов
Убедитесь, что все тесты проходят:
```bash
dotnet test ./FitTrack.sln
```

### 3. Строгий билд проекта
Выполните билд в Release конфигурации с анализаторами для выявления предупреждений:
```bash
dotnet build ./FitTrack.sln --configuration Release --no-restore /p:RunAnalyzers=true /p:AnalysisLevel=latest
```

## Документация API
После запуска приложения документация API доступна по адресу:

- Swagger UI: http://localhost:[port]/swagger
- OpenAPI спецификация: http://localhost:[port]/swagger/v1/swagger.json

## Типы исключений в проекте

- ArgumentException
- ArgumentNullException
- Exception
- UnauthorizedAccessException
...

## Паттерны

1. Data Transfer Objects (DTO)
2. Repository + Unit of Work (сам DbContext)
3. Domain Model / Rich Entities (сущность содержит инварианты и бизнес-логику)
4. Factory Method / Static Factory (сущность сама контролирует своё валидное состояние)
...
