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
│   ├── Dashboard/
│   │   ├── Pages/       Dashboard.razor (.cs, .css)
│   │   ├── Components/  ChartDesembolsos, ChartDistribucion, NotificationItem
│   │   ├── State/       DashboardState.cs
│   │   └── ViewModels/  DashboardViewModel.cs
│   │
│   ├── JCE/
│   │   └── Pages/       JCE.razor (.cs, .css)
│   │
│   ├── CreditFactory/
│   │   ├── Pages/       CreditFactory.razor (.cs, .css)
│   │   ├── Components/  CreditRequestsTable.razor (.cs, .css)
│   │   ├── State/       CreditFactoryState.cs
│   │   └── ViewModels/  CreditRequestViewModel.cs
│   │
│   ├── CRM/
│   │   ├── Pages/       CRM.razor (.cs, .css), CRMAgregarCliente.razor (.css), CRMDetalleCliente.razor (.css)
│   │   ├── Components/  InteraccionesTable.razor (.cs, .css)
│   │   ├── State/       CrmState.cs
│   │   └── ViewModels/  CrmViewModel.cs
│   │
│   ├── Credits/
│   │   ├── Pages/       CreditosPage.razor
│   │   ├── Components/  CreditoForm.razor
│   │   ├── State/       CreditosState.cs
│   │   └── ViewModels/  CreditosViewModel.cs
│   │
│   ├── Customers/
│   │   ├── Pages/       ClientesPage.razor
│   │   ├── Components/  ClienteForm.razor
│   │   ├── State/       ClientesState.cs
│   │   └── ViewModels/  ClientesViewModel.cs
│   │
│   ├── CollectionManagement/
│   ├── CreditBureau/
│   ├── Profile/
│   ├── Reports/
│   └── Settings/
│
├── Components/                        # Shared component library (domain-agnostic)
│   ├── Cards/
│   │   └── MetricCard/   MetricCard.razor (.css)        # KPI card: icon, label, value, badge
│   ├── Charts/
│   │   └── ChartCard/    ChartCard.razor (.css)         # Chart wrapper with title and slots
│   ├── Feedback/
│   │   ├── AlertMessage/ AlertMessage.razor (.css)      # Contextual alert (success/error/warning/info)
│   │   └── StatusBadge/  StatusBadge.razor (.css)       # Semantic pill badge
│   ├── Layout/
│   │   ├── PageHeader/   PageHeader.razor (.css)        # Standard page header with accent + actions
│   │   └── FormSection/  FormSection.razor (.css)       # Form section grouping with icon + title
│   ├── Modals/
│   │   └── ConfirmDialog/ ConfirmDialog.razor (.css)    # Confirmation dialog via IDialogService
│   └── Tables/
│       └── DataTable/    DataTable.razor (.cs, .css)    # Generic filterable + paginated table
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

### Infrastructure
- MudBlazor 9.1.0 fully integrated (services, theme provider, CSS/JS).
- Shell layout with persistent sidebar navigation implemented.
- Light/dark mode toggle with `localStorage` persistence.
- Banco Agrícola theme (`BancoAgricolaTheme.cs`) with custom light and dark palettes.
- CSS variable system (`theme-variables.css`) fully tokenized across all modules.

### Shared Component Library (`Components/`)
- `DataTable<T>` — generic filterable, paginated table with toolbar (SearchFilter, SelectFilter, DateFilter, DateRangeFilter), expandable rows, and empty state.
- `MetricCard` — KPI card with icon, label, value and semantic badge.
- `ChartCard` — chart wrapper with title, header actions slot and footer slot.
- `PageHeader` — standard page header with title, accent text, subtitle and actions slot.
- `StatusBadge` — semantic pill badge wrapping MudChip.
- `AlertMessage` — contextual alert (success / error / warning / info).
- `FormSection` — form field grouper with icon and title.
- `ConfirmDialog` — confirmation modal via `IDialogService`.

### Modules
- **Dashboard** — KPI cards, monthly stacked bar chart, donut distribution chart, notification list.
- **JCE** — identity lookup with photo preview and data display.
- **CreditFactory** — approval queue with `DataTable<T>` (date range, product select, search filters), progress bar, status badges and print action.
- **CRM** — client interaction list with `DataTable<T>` (5 filters), expandable seguimientos sub-table, add client page and client detail page.
- **Credits, Customers** — basic structure in place, pending full UI implementation.
- **CollectionManagement, CreditBureau, Profile, Reports, Settings** — stub pages, pending implementation.

---

## 8. Ownership

Dirección de Desarrollo TIC
Banco Agrícola
Crédito a Tiempo 2.0
