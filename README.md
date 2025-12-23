# SchoolWebInternalAPI

**Internal CMS & Admin API for a School Website**

A production-ready **ASP.NET Core (.NET 8)** Internal API built with **Clean Architecture**, **JWT authentication with refresh tokens**, and **role-based authorization**.  
Designed for long-term maintainability, security, and extensibility.

---

## 📌 Overview

This API powers the **administrative content management system (CMS)** of a school website.

**Core characteristics:**
- Website pages are predefined (fixed structure)
- All content (text, images, SEO metadata) is editable by admins
- Public users only consume read-only (GET) endpoints
- Admin users manage content via secured endpoints

---

## 🎯 Goals

- Secure internal CMS
- Strict separation of concerns
- Fully testable business logic
- Future-proof for MVC or SPA frontend
- CI/CD ready for production deployment

---

## 🏗 Architecture Overview

The project follows **Clean Architecture** principles strictly.

```
┌─────────────────────────────┐
│ API                         │
│ Controllers, Middleware     │
│ Auth, Swagger, RateLimit    │
└────────────▲────────────────┘
             │
┌────────────┴────────────────┐
│ Application                 │
│ DTOs, Services, Validators  │
│ Interfaces, Mapping         │
└────────────▲────────────────┘
             │
┌────────────┴────────────────┐
│ Domain                      │
│ Entities, Value Objects     │
│ Business Rules              │
└─────────────────────────────┘
             ▲
             │
┌────────────┴────────────────┐
│ Infrastructure              │
│ EF Core, Identity, Auth     │
│ Repositories, Migrations    │
└─────────────────────────────┘
```

### Dependency Rules

- Domain → depends on nothing
- Application → depends only on Domain
- Infrastructure → implements Application interfaces
- API → composes and wires everything together

---

## 🧱 Design Principles

### SOLID Principles

- **Single Responsibility**  
  One service per feature (Page, Auth, Teacher)

- **Open / Closed**  
  New pages can be added without modifying existing logic

- **Liskov Substitution**  
  DTOs safely replace entities via mapping

- **Interface Segregation**  
  Small, focused interfaces

- **Dependency Inversion**  
  Controllers depend only on abstractions

### Additional Principles

- Separation of concerns
- Explicit architectural boundaries
- Fail-fast configuration
- Security-first defaults

---

## 📄 CMS Page Model

Each CMS page follows a strict 1-to-1 structure:

- One database table
- One domain entity
- One repository
- One application service
- One controller

### Implemented Pages

- Home  
- Contact  
- History  
- Mission  
- Organization  
- Links  
- Footer  
- SiteSettings  

### CMS Rules

- `GET` → Public (anonymous access)
- `PUT` → Admin only (`[Authorize(Roles = "Admin")]`)
- Page structure is fixed, content is editable

---

## 🔐 Authentication & Authorization

### Identity

- ASP.NET Identity with custom `ApplicationUser`
- Role support (`Admin`)
- Stored in the same SQLite database

### JWT Authentication

- Short-lived Access Tokens
- Long-lived Refresh Tokens
- Role claims embedded in JWT
- Role enforcement via attributes

### Refresh Token System

- Token rotation
- Token family invalidation
- Logout-from-all-devices support
- Background cleanup service

---

## 🔁 API Response Wrapper

All endpoints return a consistent response structure:

```json
{
  "success": true,
  "data": {},
  "message": "",
  "errors": []
}
```

**Benefits:**
- Predictable API behavior
- Cleaner controllers
- Centralized error handling

---

## ✅ Validation

- FluentValidation for all DTOs
- Executed before service logic
- Clear, structured error messages
- Separate validators per feature

---

## 🧪 Testing Strategy

### Covered by Tests

- Application services
- Authentication logic (login, refresh, revoke)
- Core business rules

### Intentionally Not Tested

- Controllers (thin orchestration only)
- EF Core internals
- Framework behavior

**Test suite characteristics:**
- Fast
- Deterministic
- Maintainable

---

## ⚙️ Infrastructure Features

- Global exception handling middleware
- JWT authentication & authorization
- Role-based access control
- Rate limiting (global + auth endpoints)
- Background hosted services
- SQLite with EF Core migrations

---

## 🚀 CI/CD

GitHub Actions pipeline:

1. Restore
2. Build
3. Test
4. Publish (main branch)
5. Upload build artifacts

---

## 🔮 Future Work

- MVC frontend
- Admin UI
- Content versioning
- Audit logging

---

## 👤 Author

**Daniel Nicuta**  
.NET Backend Engineer  

Clean Architecture • Security • Maintainability
