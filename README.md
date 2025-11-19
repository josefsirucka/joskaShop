# 🛒 JoskaShop

> A modern, scalable ASP.NET Core API for managing articles and products with clean architecture, comprehensive logging, and database persistence.

<div align="center">

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com)
[![Platform](https://img.shields.io/badge/Platform-Windows-blue)](#)

</div>

---

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)
- [Development](#development)
- [License](#license)

---

## ✨ Features

- **RESTful API** - Clean and intuitive API design with comprehensive endpoints
- **Article Management** - Create, retrieve, and search articles with advanced filtering
- **Database Support** - Entity Framework Core with SQLite integration
- **Structured Logging** - Enterprise-grade logging with Serilog
- **API Documentation** - Interactive Swagger/OpenAPI documentation
- **Currency Support** - Multi-currency handling with ISO 4217 standards
- **Clean Architecture** - Domain-driven design with clear separation of concerns
- **Unit Testing** - Comprehensive test coverage for repositories
- **Async/Await** - Full asynchronous operation support
- **Result Pattern** - Robust error handling with structured result types

---

## 🛠️ Tech Stack

| Layer | Technology | Version |
|-------|-----------|---------|
| **Framework** | ASP.NET Core | 9.0 |
| **Database** | SQLite with EF Core | 9.0.11 |
| **Logging** | Serilog | 4.2.0 |
| **API Documentation** | Swagger/Swashbuckle | 7.0.0 |
| **Result Handling** | PerfectResult | 1.0.3 |
| **Testing** | xUnit | Latest |
| **Currency** | ISO.4217.CurrencyCodes | 1.0.10 |

---

## 📁 Project Structure

```
JoskaShop/
├── ArticleRepositories/          # Data access layer
│   ├── IArticleRepository.cs      # Repository interface
│   ├── InMemoryArticleRepository.cs
│   └── SqlArticleRepository.cs
├── Controllers/                  # API endpoints
│   └── ArticlesController.cs
├── Domains/                      # Business logic layer
│   ├── IArticleDomain.cs
│   └── ArticleDomain.cs
├── Models/                       # Data models
│   ├── Article.cs
│   └── ArticleDto.cs
├── Services/                     # Cross-cutting services
│   ├── CurrencyService.cs
│   └── DatabaseService.cs
├── BootStrappers/               # Dependency injection setup
│   ├── Logging.cs
│   └── SwaggerBootStrapper.cs
├── Extensions/                   # Helper extensions
│   ├── RegisterAllServies.cs
│   └── UseDeveloperTools.cs
├── Program.cs                    # Application entry point
└── appsettings.json             # Configuration

JoskaShop.Tests/
└── RepositoryTests.cs           # Unit tests
```

---

## 🚀 Getting Started

### Prerequisites

- **.NET 9.0 SDK** or higher
- **Visual Studio 2022**, **Visual Studio Code**, or your preferred IDE
- **PowerShell** or terminal of your choice

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/josefsirucka/joskaShop.git
   cd joskaShop
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run --project JoskaShop
   ```

   The application will start on `https://localhost:7100` (or the port defined in `launchSettings.json`)

### Database Setup

The application uses SQLite with automatic migrations. The database file (`joskashop.db`) will be created automatically on first run.

---

## 📡 API Endpoints

### Create Article
```http
POST /api/articles
Content-Type: application/json

{
  "name": "Product Name",
  "description": "Product description",
  "price": 29.99,
  "category": "Electronics",
  "currencyCode": "USD"
}
```

**Response:** `201 Created`
```json
{
  "articleId": 1,
  "name": "Product Name",
  "description": "Product description",
  "price": 29.99,
  "category": "Electronics",
  "currencyCode": "USD"
}
```

### Get Article by ID
```http
GET /api/articles/{id}
```

**Response:** `200 OK`

### Get All Articles
```http
GET /api/articles
```

**Response:** `200 OK` (Array of articles)

### Search Articles
```http
GET /api/articles?searchOrDescriptionName=laptop&searchCategory=Electronics
```

**Query Parameters:**
- `searchOrDescriptionName` (optional) - Search by product name or description
- `searchCategory` (optional) - Filter by category

**Response:** `200 OK` (Filtered array of articles)

### API Documentation

Access the interactive Swagger UI at: **`/swagger`** when running in development mode.

---

## ⚙️ Configuration

### Application Settings (`appsettings.json`)

```json
{
  "DefaultConnection": "Data Source=joskashop.db",
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "Logs/JoskaShop.log"
        }
      }
    ]
  }
}
```

### Connection Strings

- **Production Database**: Configure `DefaultConnection` in `appsettings.json`
- **Development Database**: Uses SQLite (`joskashop.db`) by default

### Logging Configuration

Logs are written to:
- **File**: `Logs/JoskaShop.log`
- **Console**: Available in development mode
- **Level**: Information and above

---

## 👨‍💻 Development

### Running Tests

```bash
dotnet test
```

### Building Documentation

The project generates XML documentation automatically. Access it in the Swagger UI or in the compiled documentation files.

### Code Style & Standards

- **Nullable Reference Types**: Enabled for null safety
- **Implicit Usings**: Enabled for cleaner code
- **XML Documentation**: Required for public members
- **Strong Naming**: Assemblies are signed

---

## 🔄 Architecture Highlights

### Layered Architecture

```
┌─────────────────────────────────────────┐
│        Controllers (API Layer)          │
├─────────────────────────────────────────┤
│        Domains (Business Logic)         │
├─────────────────────────────────────────┤
│      Repositories (Data Access)         │
├─────────────────────────────────────────┤
│        Database (SQLite + EF Core)      │
└─────────────────────────────────────────┘
```

### Design Patterns Used

- **Repository Pattern** - Abstract data access logic
- **Dependency Injection** - Loose coupling and testability
- **Result Pattern** - Structured success/failure handling
- **Domain-Driven Design** - Business logic separation
- **Async/Await** - Non-blocking operations

---

## 📦 Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.EntityFrameworkCore | 9.0.11 | ORM Framework |
| Microsoft.EntityFrameworkCore.Sqlite | 9.0.11 | Database Provider |
| Serilog | 4.2.0 | Structured Logging |
| Swashbuckle.AspNetCore | 7.0.0 | Swagger/OpenAPI |
| ISO.4217.CurrencyCodes | 1.0.10 | Currency Support |
| PerfectResult | 1.0.3 | Result Pattern |

---

## 📝 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

```
MIT License

Copyright (c) 2025 Josef Širůčka

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software")...
```

---

## 👤 Author

**Josef Širůčka** - [@josefsirucka](https://github.com/josefsirucka)

---

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

---

<div align="center">

**[⬆ back to top](#joskashop)**

Made with ❤️ by Josef Širůčka

</div>
