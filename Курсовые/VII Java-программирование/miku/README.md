# Miku - Курсовая работа 7 семестр

## Архитектура

Проект разделён на три основных модуля:

- **common**: Содержит общие DTO, утилиты и константы, разделяемые между модулями
- **server**: Серверная часть на Spring Boot, обеспечивающая бизнес-логику и API
- **client**: Клиентская часть на Vaadin, предоставляющая пользовательский интерфейс

## Технологический стек

### Серверная часть

- Java 25
- Spring Boot 4.0+
- Spring Data JPA
- Hibernate
- PostgreSQL
- Jakarta Validation
- MapStruct
- SpringDoc OpenAPI (Swagger)
- Lombok

### Клиентская часть

- Vaadin 25
- Java 25
- Spring Boot
- Vaadin

### Инфраструктура

- Docker & Docker Compose
- Maven
- PostgreSQL 18
- pgAdmin 9.10

## Установка и запуск

### Предварительные требования

- Java 25
- Maven 3.9+
- Docker и Docker Compose

### Запуск в режиме разработки

1. Клонируйте репозиторий
2. Убедитесь, что все зависимости установлены
3. Запустите команду:

```bash
docker-compose up --build
```

Сервер будет доступен по адресу `http://localhost:8080`, клиент - по адресу `http://localhost:5050`.
