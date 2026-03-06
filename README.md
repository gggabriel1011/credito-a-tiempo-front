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

UI Layer built with Blazor WebAssembly (.NET 9) with MudBlazor 9.1.0.

This project contains:

- Feature-based modules
- Shared UI components
- Shell layout with persistent sidebar
- CSS variable-based theme system with light/dark mode
- Static assets (wwwroot)


Structure:

```
CreditInTimeFront.WebApp
│
├── App.razor
├── _Imports.razor
├── Program.cs
│
├── Layout/
│   ├── MainLayout.razor (.cs, .css)   # Shell: MudLayout + MudDrawer
│   └── NavMenu.razor (.cs, .css)      # Sidebar navigation (data-driven)
│
├── Modules/
│   ├── Authentication/
│   │   ├── Pages/       LoginPage.razor
│   │   ├── Components/  LoginForm.razor
│   │   ├── State/       AuthState.cs
│   │   └── ViewModels/  AuthViewModel.cs
│   │
│   ├── Dashboard/                     # Implemented
│   │   ├── Pages/       Dashboard.razor (.cs, .css)
│   │   ├── Components/  StatCard, ChartDesembolsos, ChartDistribucion, NotificationItem
│   │   ├── State/       DashboardState.cs
│   │   └── ViewModels/  DashboardViewModel.cs
│   │
│   ├── JCE/                           # Implemented
│   │   └── Pages/       JCE.razor (.cs, .css)
│   │
│   ├── CreditFactory/                 # Partial
│   │   ├── Pages/       CreditFactory.razor
│   │   ├── Components/  FabricaWizard.razor
│   │   ├── State/       FabricaCreditoState.cs
│   │   └── ViewModels/  FabricaCreditoViewModel.cs
│   │
│   ├── CRM/                           # Partial
│   │   ├── Pages/       CRM.razor
│   │   ├── Components/  ClienteCard.razor
│   │   ├── State/       CrmState.cs
│   │   └── ViewModels/  CrmViewModel.cs
│   │
│   ├── Credits/                       # Partial
│   │   ├── Pages/       CreditosPage.razor
│   │   ├── Components/  CreditoForm.razor
│   │   ├── State/       CreditosState.cs
│   │   └── ViewModels/  CreditosViewModel.cs
│   │
│   ├── Customers/                     # Partial
│   │   ├── Pages/       ClientesPage.razor
│   │   ├── Components/  ClienteForm.razor
│   │   ├── State/       ClientesState.cs
│   │   └── ViewModels/  ClientesViewModel.cs
│   │
│   ├── CollectionManagement/          # Stub
│   ├── CreditBureau/                  # Stub
│   ├── Profile/                       # Stub
│   ├── Reports/                       # Stub
│   └── Settings/                      # Stub
│
├── Components/                        # Domain-agnostic shared components
│   ├── Feedback/    Notification.razor
│   ├── Inputs/      BaseInput.razor
│   ├── Modals/      BaseModal.razor
│   └── Tables/      BaseTable.razor
│
├── Shared/
│   ├── Constants/   UiConstants.cs
│   ├── Extensions/  NavigationExtensions.cs
│   ├── Helpers/     FormatHelper.cs
│   └── Themes/      BancoAgricolaTheme.cs   # MudTheme light + dark palettes
│
└── wwwroot/
    ├── css/
    │   ├── app.css                    # Global MudBlazor overrides
    │   └── theme-variables.css        # CSS custom properties (light/dark tokens)
    ├── images/                        # Logo and static assets
    └── index.html                     # Dark mode toggle + themeManager script
```

Feature-based organization ensures that each business domain is encapsulated and cohesive.


Each module follows the four-layer pattern:

```
ModuleName/
│
├── Pages/       # Routed pages (@page directive) — no business logic
├── Components/  # Module-scoped UI components (data via [Parameter])
├── State/       # Orchestrates service calls, holds loading/data/error state
└── ViewModels/  # Frontend-only models, never reuse backend DTOs
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

- .NET 9
- Blazor WebAssembly (WASM)
- MudBlazor 9.1.0 (UI component library)
- C#
- xUnit / bUnit (for testing)
- GitLab for version control

---

## 5. Getting Started

### Prerequisites

- .NET SDK 9.0 or later


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

### Run with hot reload
```
dotnet watch --project src/CreditInTimeFront.WebApp
```

### Run tests
```
dotnet test
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

- MudBlazor 9.1.0 fully integrated (services, theme provider, CSS/JS).
- Shell layout with persistent sidebar and light/dark mode toggle implemented.
- Banco Agrícola theme with custom light and dark palettes configured.
- CSS variable system in place (`theme-variables.css`) for consistent theming.
- **Dashboard** module implemented: KPI cards, monthly bar chart, donut chart, notifications.
- **JCE** module implemented: identity lookup with search, photo preview and data display.
- Stub pages in place for: CreditFactory, CRM, CollectionManagement, CreditBureau, Profile, Reports, Settings.
- Remaining modules ready for UI implementation.

---

## 8. Ownership

Dirección de Desarrollo TIC  
Banco Agrícola  
Crédito a Tiempo 2.0