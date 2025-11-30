# SchoolWebInternalAPI

This repository contains the backend REST API for the SchoolWeb project.  
It is structured using **Clean Architecture**, follows a **GitFlow branching model**, and includes CI/CD pipelines.

## 🚀 Architecture Overview

The API uses Clean Architecture layered structure:

- **Domain** – Core business entities and rules  
- **Application** – Use cases, DTOs, interfaces  
- **Infrastructure** – EF Core, database, external dependencies  
- **Api** – Controllers, routing, Swagger UI  

This separation ensures:
- Clear boundaries
- Testability
- Scalability
- Future-proof development

## 🔀 Branch Strategy (GitFlow)

- `main` – stable production code  
- `develop` – integration branch for upcoming releases  

## 🛠️ Tech Stack

- **.NET 9 Web API**
- **Entity Framework Core**
- **SQL Server**
- **xUnit** for tests

