# FE Web Portal – Crédito a Tiempo 2.0

Frontend Web Application built with Blazor WebAssembly (WASM).

This project follows the Frontend Architecture V2 standard, focused on building modern web applications in a simple, modular, maintainable, and scalable way without introducing unnecessary complexity.

---

## 1. Project Overview

This solution represents the frontend layer of Crédito a Tiempo 2.0 and is structured to ensure:

- Clear separation of responsibilities
- Feature-based modular organization
- High testability
- Clean architecture principles
- Long-term maintainability

The architecture is designed so that business logic remains independent from the UI layer.

---

## 2. Solution Structure


The repository is organized as follows:

```
CreditoATiempo.sln
│
└── src
    ├── CreditInTimeFront.WebApp
    ├── CreditInTimeFront.Core
    └── CreditInTimeFront.Tests
```


### 2.1 CreditInTimeFront.WebApp

UI Layer built with Blazor WebAssembly.

This project contains:

- Feature-based modules
- Shared UI components
- Application entry point
- Static assets (wwwroot)


Structure:

```
CreditInTimeFront.WebApp
│
├── Modulos
│   ├── Clientes
│   ├── Creditos
│   ├── FabricaCredito
│   ├── CRM
│   └── Autenticacion
│
├── Components
│   ├── Layout
│   ├── Inputs
│   ├── Feedback
│   ├── Tables
│   └── Modals
│
├── Shared
│   ├── Constants
│   ├── Extensions
│   └── Helpers
│
└── wwwroot
```

Feature-based organization ensures that each business domain is encapsulated and cohesive.


Each module contains:

```
ModuleName
│
├── Pages
├── Components
├── State
└── ViewModels
```


This allows each feature to evolve independently without tight coupling to others.

---

### 2.2 CreditInTimeFront.Core

Application Core layer.

This project contains:

- Application services
- DTOs (Requests and Responses)
- HTTP interceptors
- Global configuration
- Constants and extensions
- Custom exceptions


Structure:

```
CreditInTimeFront.Core
│
├── Services
│
├── Dto
│   ├── Requests
│   └── Responses
│
├── Interceptors
│
└── Settings
    ├── Constants
    ├── Extensions
    ├── Global
    └── Exceptions
```


Core rules:

- Core must not depend on WebApp.
- WebApp depends on Core.
- Core contains business-oriented application logic only.

---

### 2.3 CreditInTimeFront.Tests

Testing layer.


Structure:

```
CreditInTimeFront.Tests
│
├── ComponentTests
└── IntegrationTests
```


Purpose:

- ComponentTests: UI component testing (Blazor components).
- IntegrationTests: End-to-end logic validation across services and flows.

Testing is part of the architecture and not optional.

---

## 3. Architectural Principles

This project follows:

- Clean separation of layers
- Feature-based modularization
- High cohesion within modules
- Low coupling between modules
- Explicit dependency direction
- Scalable folder organization

Dependency rule:

WebApp → Core  
Core → (no dependency to WebApp)

---

## 4. Technology Stack

- .NET 8+
- Blazor WebAssembly (WASM)
- C#
- xUnit / bUnit (for testing)
- GitLab for version control

---

## 5. Getting Started

### Prerequisites

- .NET SDK 8.0 or later


Verify installation:

```
dotnet --version
```


### Restore dependencies
```
dotnet restore
```

### Build solution
```
dotnet build
```

### Run WebApp
```
dotnet run --project src/CreditInTimeFront.WebApp
```

---

## 6. Branching Strategy


Initial branch:
```
feature/initial-architecture
```


Recommended naming convention:

- feature/<module-name>
- bugfix/<short-description>
- hotfix/<short-description>

All changes must be merged through Merge Requests.

---

## 7. Current Status

- Initial architecture structure created.
- Frontend Architecture V2 applied.
- No business logic implemented yet.
- Ready for module-based development.

---

## 8. Ownership

Dirección de Desarrollo TIC  
Banco Agrícola  
Crédito a Tiempo 2.0