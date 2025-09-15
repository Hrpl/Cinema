# Cinema Management System
Проект системы управления кинотеатром на .NET 8 с использованием чистой архитектуры и CQRS

## 🚀 Технологический стек
### Backend
* .NET 8 - основная платформа

* PostgreSQL - реляционная база данных

* Entity Framework Core 8 - ORM

* MediatR - реализация паттерна CQRS

* FluentValidation - валидация данных

* AutoMapper - маппинг объектов

* Npgsql - провайдер PostgreSQL

* Swagger/OpenAPI - документация API

### Архитектура
* Clean Architecture - чистая архитектура

* CQRS - разделение команд и запросов

* Dependency Injection - внедрение зависимостей

## 🏗️ Установка и запуск
### Предварительные требования
* .NET 8 SDK

* PostgreSQL 12+

* Git

1. Клонирование репозитория
``` bash
git https://github.com/Hrpl/Cinema.git
```
2. Настройка подключения к БД
Отредактируйте appsettings.json:

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=CinemaDB;Username=cinema_user;Password=your_password"
  }
}
```
3. Выполнить миграции
``` bash 
Update-Database
```

## 📋 API Endpoints
### Сеансы
* GET /api/sessions - Получить список сеансов

* POST /api/sessions - Создать новый сеанс

### Фильмы
* GET /api/movies - Получить список фильмов

* PATCH /api/movies/filter - Удалить фильм из проката

* POST /api/movies - Добавить новый фильм

### Залы
* POST /api/halls - Добавить новый зал

### Скидки
* POST /api/discounts - Созданеие скидки

### Переопределение цены

* GET /api/override/prices/{id} - Получить записи по ID

* POST /api/override/prices - Создать запись

* DELETE /api/override/prices - Удалить запись