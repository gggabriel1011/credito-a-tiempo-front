# Shared Component Library

Generic, domain-agnostic components living in `WebApp/Components/`. Available in **any page or module** with no extra `@using` directives.

---

## Why they exist

Before this library, each module (Dashboard, CRM, CreditFactory) duplicated the same patterns: KPI cards, page headers, chart wrappers, status badges and tables. Any design change had to be applied in 3+ places.

Now there is **one component per visual concept**. Changing `MetricCard` automatically updates every module that uses it.

---

## How they work without `@using`

`_Imports.razor` (at the WebApp root) contains:

```razor
@using CreditInTimeFront.WebApp.Components
```

This applies globally to every page and component in the project. All shared components declare `@namespace CreditInTimeFront.WebApp.Components` at the top of their `.razor` file, so Blazor resolves them without individual imports.

---

## Folder structure

```
Components/
  Cards/
    MetricCard/
      MetricCard.razor        ← markup + parameters
      MetricCard.razor.css    ← scoped styles
  Charts/
    ChartCard/
      ChartCard.razor
      ChartCard.razor.css
  Feedback/
    AlertMessage/
      AlertMessage.razor
      AlertMessage.razor.css
    StatusBadge/
      StatusBadge.razor
      StatusBadge.razor.css
  Layout/
    PageHeader/
      PageHeader.razor
      PageHeader.razor.css
    FormSection/
      FormSection.razor
      FormSection.razor.css
  Modals/
    ConfirmDialog/
      ConfirmDialog.razor
      ConfirmDialog.razor.css
  Tables/
    DataTable/
      DataTable.razor         ← generic filterable + paginated grid
      DataTable.razor.cs      ← code-behind (parameters, filter logic)
      DataTable.razor.css     ← scoped toolbar + grid overrides
      FilterDef.cs            ← filter definition model
```

> **Rule:** each new component lives in its own subfolder `Components/<Category>/<Name>/` with its `.razor` and `.razor.css` files. Add `.razor.cs` when C# logic is non-trivial.

---

## Available components

---

### `<PageHeader>`

Standard page header. Green left border, title, green accent text and subtitle.

**File:** `Components/Layout/PageHeader/PageHeader.razor`

#### Parameters

| Parameter     | Type              | Required | Description                                           |
|---------------|-------------------|----------|-------------------------------------------------------|
| `Title`       | `string`          | Yes      | Main title text                                       |
| `TitleAccent` | `string?`         | No       | Green-colored text after the title (e.g. `"- CRM"`)  |
| `Subtitle`    | `string?`         | No       | Small subtitle below the title                        |
| `Actions`     | `RenderFragment?` | No       | Right slot for buttons or other controls              |

#### Basic usage

```razor
<PageHeader Title="Collections"
            TitleAccent="- Payment Management"
            Subtitle="Track overdue portfolios and payment agreements" />
```

#### With action button

```razor
<PageHeader Title="Reports" TitleAccent="- Panel">
    <Actions>
        <MudButton Variant="Variant.Filled"
                   Color="Color.Primary"
                   StartIcon="@Icons.Material.Outlined.Download">
            Export
        </MudButton>
    </Actions>
</PageHeader>
```

---

### `<MetricCard>`

KPI card with icon, label, value and optional badge.

**File:** `Components/Cards/MetricCard/MetricCard.razor`

#### Parameters

| Parameter      | Type      | Required | Description                                                  |
|----------------|-----------|----------|--------------------------------------------------------------|
| `Icon`         | `string`  | Yes      | MudBlazor icon (e.g. `@Icons.Material.Outlined.People`)      |
| `Label`        | `string`  | Yes      | Metric label (auto uppercase)                                |
| `Value`        | `string`  | Yes      | Main value displayed large                                   |
| `BadgeText`    | `string?` | No       | Badge text (not rendered when `null`)                        |
| `BadgeVariant` | `string`  | No       | Badge color. See variants below. Default: `"primary"`        |
| `ValueClass`   | `string?` | No       | Extra CSS class for the value (e.g. `"mc-value--long"`)      |

#### Badge variants (`BadgeVariant`)

| Value      | Badge color      | When to use                         |
|------------|------------------|-------------------------------------|
| `primary`  | Green            | Positive or neutral primary metric  |
| `warning`  | Orange/yellow    | Attention required                  |
| `info`     | Blue             | Informational data                  |
| `muted`    | Gray             | Secondary data                      |
| `positive` | Success green    | Positive trend                      |
| `negative` | Red              | Negative trend or alert             |

#### Basic usage

```razor
<MetricCard Icon="@Icons.Material.Outlined.People"
            Label="Active Clients"
            Value="1,284"
            BadgeText="+12 this month"
            BadgeVariant="positive" />
```

#### Standard 4-column KPI grid

```razor
<MudGrid Spacing="3" Class="mb-4">
    @foreach (var card in _kpiCards)
    {
        <MudItem xs="12" sm="6" lg="3">
            <MetricCard Icon="@card.Icon"
                        Label="@card.Label"
                        Value="@card.Value"
                        BadgeText="@card.Detail"
                        BadgeVariant="@card.BadgeVariant" />
        </MudItem>
    }
</MudGrid>
```

> For long numeric values (more than 8 characters) add `ValueClass="mc-value--long"` to automatically reduce the font size.

---

### `<ChartCard>`

Chart wrapper. Provides a card with title, header actions slot and footer slot for custom legends.

**File:** `Components/Charts/ChartCard/ChartCard.razor`

#### Parameters

| Parameter       | Type              | Required | Description                                                          |
|-----------------|-------------------|----------|----------------------------------------------------------------------|
| `Title`         | `string`          | Yes      | Chart title (auto uppercase)                                         |
| `ChildContent`  | `RenderFragment`  | Yes      | The `<MudChart>` or any chart content                                |
| `HeaderActions` | `RenderFragment?` | No       | Controls on the right side of the title (e.g. year selector)        |
| `Footer`        | `RenderFragment?` | No       | Custom legend below the chart                                        |
| `Height`        | `string`          | No       | Card height. Default: `"100%"`                                       |
| `CardClass`     | `string`          | No       | Extra class on the `MudCard` for global CSS targeting (see note)     |

#### Simple usage

```razor
<ChartCard Title="Requests per Month">
    <ChildContent>
        <MudChart T="double"
                  ChartType="ChartType.Line"
                  ChartSeries="@_series"
                  ChartLabels="@_labels"
                  Width="100%"
                  Height="260px" />
    </ChildContent>
</ChartCard>
```

#### With header actions and footer legend (donut)

```razor
<ChartCard Title="Portfolio Distribution" CardClass="chart-card--pie">
    <ChildContent>
        <MudChart T="double" ChartType="ChartType.Donut" ... />
    </ChildContent>
    <Footer>
        <div class="my-legend">
            <!-- custom legend rows -->
        </div>
    </Footer>
</ChartCard>
```

#### Note on `CardClass` and global CSS

`ChartCard` is a child component — its styles are scoped. To hide MudBlazor chart internals (Y axis, legend), target them from **global** CSS (`app.css`). `CardClass` propagates the class to the inner `MudCard` so `app.css` can target it:

```css
/* app.css — hide the Y axis for the disbursements chart */
.desembolsos-card .mud-charts-yaxis { display: none !important; }
```

```razor
<ChartCard CardClass="desembolsos-card" ...>
```

---

### `<StatusBadge>`

Compact pill-shaped status badge. Replaces direct `MudChip` usage and its `::deep .mud-chip-content` boilerplate.

**File:** `Components/Feedback/StatusBadge/StatusBadge.razor`

#### Parameters

| Parameter | Type    | Required | Description                                           |
|-----------|---------|----------|-------------------------------------------------------|
| `Text`    | `string`| Yes      | Badge text                                            |
| `Color`   | `Color` | No       | MudBlazor `Color` enum. Default: `Color.Default`      |
| `Size`    | `Size`  | No       | MudBlazor `Size` enum. Default: `Size.Small`          |

#### Available colors (`Color` enum)

`Color.Success`, `Color.Warning`, `Color.Error`, `Color.Info`, `Color.Primary`, `Color.Secondary`, `Color.Default`

#### Usage

```razor
<StatusBadge Text="Active"   Color="Color.Success" />
<StatusBadge Text="Pending"  Color="Color.Warning" />
<StatusBadge Text="Rejected" Color="Color.Error"   />
```

#### Pattern with ViewModel: color helper method

```csharp
private static Color GetStatusColor(ClientStatus status) => status switch
{
    ClientStatus.Active   => Color.Success,
    ClientStatus.Inactive => Color.Default,
    ClientStatus.Blocked  => Color.Error,
    _                     => Color.Default
};
```

```razor
<StatusBadge Text="@context.Status.ToLabel()"
             Color="@GetStatusColor(context.Status)" />
```

---

### `<AlertMessage>`

Contextual alert for user feedback: success, error, warning or info. Wrapper around `MudAlert`.

**File:** `Components/Feedback/AlertMessage/AlertMessage.razor`

#### Parameters

| Parameter      | Type              | Default           | Description                                       |
|----------------|-------------------|-------------------|---------------------------------------------------|
| `Severity`     | `Severity`        | `Severity.Info`   | `Info` \| `Success` \| `Warning` \| `Error`       |
| `Variant`      | `Variant`         | `Variant.Filled`  | `Outlined` \| `Filled` \| `Text`                  |
| `Text`         | `string?`         | `null`            | Plain text message                                |
| `ChildContent` | `RenderFragment?` | `null`            | Rich content (links, bold text, etc.)             |
| `Dense`        | `bool`            | `false`           | Compact version with reduced padding              |

#### Usage

```razor
<!-- Success feedback -->
<AlertMessage Severity="Severity.Success" Text="Client saved successfully." />

<!-- API error -->
<AlertMessage Severity="Severity.Error" Text="Could not connect to the server. Please try again." />

<!-- Warning with rich content -->
<AlertMessage Severity="Severity.Warning">
    <ChildContent>
        This client has <strong>3 overdue payments</strong>. Review before approving.
    </ChildContent>
</AlertMessage>

<!-- Compact informational -->
<AlertMessage Severity="Severity.Info" Text="Changes are saved automatically." Dense="true" />
```

---

### `<FormSection>`

Groups form fields under a titled section header with icon. A bottom separator is added automatically and removed on the last section via CSS `:last-child`.

**File:** `Components/Layout/FormSection/FormSection.razor`

#### Parameters

| Parameter      | Type             | Required | Description                     |
|----------------|------------------|----------|---------------------------------|
| `Icon`         | `string`         | Yes      | MudBlazor icon for the header   |
| `Title`        | `string`         | Yes      | Section title                   |
| `ChildContent` | `RenderFragment` | Yes      | Form fields                     |

#### Usage

```razor
<MudCard Elevation="1" Class="my-card">
    <MudCardContent>

        <FormSection Icon="@Icons.Material.Outlined.Person" Title="Basic Information">
            <MudGrid Spacing="2">
                <MudItem xs="12" sm="6">
                    <MudTextField T="string" @bind-Value="_name" Label="Name"
                                  Variant="Variant.Outlined" Margin="Margin.Dense" FullWidth="true" />
                </MudItem>
            </MudGrid>
        </FormSection>

        <FormSection Icon="@Icons.Material.Outlined.Business" Title="Business Information">
            <MudGrid Spacing="2">
                <!-- more fields -->
            </MudGrid>
        </FormSection>

        <!-- Last section: bottom border removed automatically -->
        <FormSection Icon="@Icons.Material.Outlined.Notes" Title="Notes">
            <MudTextField T="string" @bind-Value="_notes" Lines="4"
                          Variant="Variant.Outlined" FullWidth="true" />
        </FormSection>

    </MudCardContent>
</MudCard>
```

---

### `<ConfirmDialog>`

Native MudBlazor confirmation modal. Opened via `IDialogService` — not placed directly in markup.

**File:** `Components/Modals/ConfirmDialog/ConfirmDialog.razor`

**Requires:** `<MudDialogProvider />` in `MainLayout.razor` (already registered).

#### Parameters

| Parameter      | Type     | Default              | Description                                                    |
|----------------|----------|----------------------|----------------------------------------------------------------|
| `Title`        | `string` | `"Confirm action"`   | Dialog title                                                   |
| `Message`      | `string` | `""`                 | Confirmation message                                           |
| `ConfirmText`  | `string` | `"Confirm"`          | Confirm button text                                            |
| `CancelText`   | `string` | `"Cancel"`           | Cancel button text                                             |
| `ConfirmColor` | `Color`  | `Color.Primary`      | Confirm button color. Use `Color.Error` for destructive actions|
| `Icon`         | `string?`| `null`               | Optional icon next to the title                                |

#### How to use — inject `IDialogService`

```csharp
// In code-behind (.razor.cs):
[Inject] private IDialogService DialogService { get; set; } = default!;

private async Task HandleDelete(string clientId)
{
    var parameters = new DialogParameters<ConfirmDialog>
    {
        { x => x.Message,       "This action will permanently delete the client." },
        { x => x.ConfirmText,   "Delete" },
        { x => x.ConfirmColor,  Color.Error },
        { x => x.Icon,          Icons.Material.Outlined.DeleteForever }
    };

    var dialog = await DialogService.ShowAsync<ConfirmDialog>("Delete client", parameters);
    var result = await dialog.Result;

    if (!result.Canceled)
    {
        await _state.DeleteClientAsync(clientId);
    }
}
```

---

### `<DataTable<T>>`

Generic filterable and paginated data grid. Wraps `MudDataGrid<T>` with a standardized toolbar, configurable filter controls and optional expandable row details.

**File:** `Components/Tables/DataTable/DataTable.razor`
**Code-behind:** `Components/Tables/DataTable/DataTable.razor.cs`
**Filter model:** `Components/Tables/DataTable/FilterDef.cs`

#### Parameters

| Parameter         | Type                          | Required | Description                                                      |
|-------------------|-------------------------------|----------|------------------------------------------------------------------|
| `T`               | type param                    | Yes      | The ViewModel type (e.g. `CreditRequestViewModel`)               |
| `Items`           | `IEnumerable<T>?`             | Yes      | Data source                                                      |
| `Title`           | `string`                      | Yes      | Toolbar title text                                               |
| `TitleIcon`       | `string`                      | No       | MudBlazor icon next to the title                                 |
| `Filters`         | `IReadOnlyList<FilterDef<T>>` | No       | Filter controls rendered in the toolbar                          |
| `FilterPredicate` | `Func<T, bool>?`              | No       | Method reference used to filter rows                             |
| `ColumnDefs`      | `RenderFragment?`             | Yes      | `<TemplateColumn>` definitions                                   |
| `RowDetails`      | `RenderFragment<T>?`          | No       | Expandable sub-row content. When `null`, no expand icon appears  |
| `PageSize`        | `int`                         | No       | Rows per page. Default: `10`                                     |

#### `FilterDef<T>` model

Defined in `FilterDef.cs`. Each instance describes one filter control in the toolbar.

| Property      | Type                      | Description                                               |
|---------------|---------------------------|-----------------------------------------------------------|
| `Type`        | `FilterType` enum         | `SearchFilter`, `SelectFilter`, `DateFilter`, `DateRangeFilter` |
| `Placeholder` | `string`                  | Input placeholder text                                    |
| `Options`     | `IEnumerable<(string Value, string Label)>?` | Select options (only for `SelectFilter`) |
| `GetValue`    | `Func<string>?`           | Returns the current filter value (for use in predicates)  |
| `SetValue`    | `Action<string>?`         | Updates the filter value on change                        |

#### Basic usage — table with search filter

```razor
@* MyTable.razor *@
<DataTable T="MyViewModel"
           Title="My Records"
           TitleIcon="@Icons.Material.Outlined.List"
           Items="@Items"
           Filters="@_filters"
           FilterPredicate="@FilterRow">

    <ColumnDefs>
        <TemplateColumn T="MyViewModel" Title="Name">
            <CellTemplate>
                <span>@context.Item.Name</span>
            </CellTemplate>
        </TemplateColumn>

        <TemplateColumn T="MyViewModel" Title="Status"
                        HeaderClass="dt-col--center"
                        CellStyle="text-align:center">
            <CellTemplate>
                <span class="my-badge my-badge--@context.Item.Status.ToString().ToLower()">
                    @context.Item.Status.ToLabel()
                </span>
            </CellTemplate>
        </TemplateColumn>
    </ColumnDefs>

</DataTable>
```

```csharp
// MyTable.razor.cs
[Parameter] public IReadOnlyList<MyViewModel> Items { get; set; } = [];

private string _search = "";

private IReadOnlyList<FilterDef<MyViewModel>> _filters => new[]
{
    new FilterDef<MyViewModel>
    {
        Type        = FilterType.SearchFilter,
        Placeholder = "Search...",
        GetValue    = () => _search,
        SetValue    = v => _search = v
    }
};

private bool FilterRow(MyViewModel row) =>
    string.IsNullOrWhiteSpace(_search) ||
    row.Name.Contains(_search, StringComparison.OrdinalIgnoreCase);
```

#### Table with expandable sub-rows

```razor
<DataTable T="ClientViewModel"
           Title="Clients"
           Items="@Clients"
           Filters="@_filters"
           FilterPredicate="@FilterClient">

    <ColumnDefs>
        <TemplateColumn T="ClientViewModel" Title="Name">
            <CellTemplate><span>@context.Item.Name</span></CellTemplate>
        </TemplateColumn>
    </ColumnDefs>

    <RowDetails Context="client">
        <div class="my-sub-table">
            <p>@client.Notes</p>
        </div>
    </RowDetails>

</DataTable>
```

> When `RowDetails` is provided, `MudDataGrid` renders an expand chevron on each row. When `null`, no empty gap is created.

#### Built-in header alignment helpers

Use `HeaderClass` on `<TemplateColumn>` to align header text:

| Class           | Effect                  |
|-----------------|-------------------------|
| `dt-col--center`| Centers the header text |
| `dt-col--right` | Right-aligns the header text |

---

## Shared table styles — `table-base.css`

Generic `MudDataGrid` styles that apply to all tables in the project. Activated by adding the class `tbl-root` to the root wrapper div of any table component.

**File:** `wwwroot/css/table-base.css`

#### What it provides automatically

- `--bg-subtle` background on toolbar and header
- Header: uppercase text, 11px, muted color, bottom border
- Body rows: standard padding, light separator border, subtle hover background
- Last row: no bottom border
- Pagination: styled with rounded bottom corners
- Horizontal scroll enabled on the table container
- Empty state classes (`tbl-empty-state`, `tbl-empty-icon`, `tbl-empty-text`)

#### How to use it in a new table component

```razor
<!-- MyTable.razor -->
<div class="my-table-root tbl-root">   <!-- tbl-root activates the base styles -->
    <MudDataGrid Items="@Items" ...>
        ...
    </MudDataGrid>
</div>
```

The component keeps its own class (`my-table-root`) for module-specific overrides in its `.razor.css`. Generic rules come from `table-base.css`.

> `DataTable<T>` already applies `tbl-root` internally — you do not need to add it manually when using the generic component.

#### Empty state

```razor
<MudDataGrid Items="@Items" ...>
    <NoRecordsContent>
        <div class="tbl-empty-state">
            <MudIcon Icon="@Icons.Material.Outlined.Inbox" Class="tbl-empty-icon" />
            <span class="tbl-empty-text">No records found</span>
        </div>
    </NoRecordsContent>
</MudDataGrid>
```

---

## Full flow: creating a new module from scratch

Example: **Collections** module.

### 1. File structure

```
Modules/
  Collections/
    Pages/
      Collections.razor
      Collections.razor.cs
    Components/
      CollectionsTable/
        CollectionsTable.razor
        CollectionsTable.razor.css
    State/
      CollectionsState.cs
    ViewModels/
      CollectionViewModel.cs
```

### 2. Main page (`Collections.razor`)

```razor
@page "/collections"
@namespace CreditInTimeFront.WebApp.Modules.Collections.Pages
@using MudBlazor
@using CreditInTimeFront.WebApp.Modules.Collections.Components
@using CreditInTimeFront.WebApp.Modules.Collections.State

<PageHeader Title="Collections"
            TitleAccent="- Payment Management"
            Subtitle="Track overdue portfolios and active payment agreements" />

<MudGrid Spacing="3" Class="mb-4">
    @foreach (var card in _kpiCards)
    {
        <MudItem xs="12" sm="6" lg="3">
            <MetricCard Icon="@card.Icon"
                        Label="@card.Label"
                        Value="@card.Value"
                        BadgeText="@card.Detail"
                        BadgeVariant="@card.BadgeVariant" />
        </MudItem>
    }
</MudGrid>

<CollectionsTable Items="@_state.Items" />
```

### 3. Code-behind (`Collections.razor.cs`)

```csharp
namespace CreditInTimeFront.WebApp.Modules.Collections.Pages;

public partial class Collections
{
    // TODO: replace with [Inject] private CollectionsState _state { get; set; } when wiring real services
    private readonly CollectionsState _state = new();

    private record KpiCard(string Icon, string Label, string Value, string Detail, string BadgeVariant);

    private readonly KpiCard[] _kpiCards =
    [
        new(Icons.Material.Outlined.RequestPage, "Total Pending",   "RD$ 4.2M", "32 accounts",       "warning"),
        new(Icons.Material.Outlined.CheckCircle, "Collected MTD",   "RD$ 1.8M", "+8% vs last month", "positive"),
        new(Icons.Material.Outlined.Warning,     "Overdue > 90d",   "18",        "Action required",   "negative"),
        new(Icons.Material.Outlined.Schedule,    "In Negotiation",  "7",         "Active agreements", "info"),
    ];
}
```

### 4. Table component (`CollectionsTable.razor`)

```razor
@namespace CreditInTimeFront.WebApp.Modules.Collections.Components
@using MudBlazor
@using CreditInTimeFront.WebApp.Modules.Collections.ViewModels

<DataTable T="CollectionViewModel"
           Title="Collection Queue"
           TitleIcon="@Icons.Material.Outlined.RequestPage"
           Items="@Items"
           Filters="@_filters"
           FilterPredicate="@FilterItem">

    <ColumnDefs>

        <TemplateColumn T="CollectionViewModel" Title="Client" HeaderStyle="min-width:200px">
            <CellTemplate>
                <span>@context.Item.ClientName</span>
            </CellTemplate>
        </TemplateColumn>

        <TemplateColumn T="CollectionViewModel" Title="Amount"
                        HeaderClass="dt-col--right"
                        CellStyle="text-align:right">
            <CellTemplate>
                <span>RD$ @context.Item.Amount.ToString("N0")</span>
            </CellTemplate>
        </TemplateColumn>

        <TemplateColumn T="CollectionViewModel" Title="Status"
                        HeaderClass="dt-col--center"
                        CellStyle="text-align:center">
            <CellTemplate>
                <StatusBadge Text="@context.Item.Status.ToLabel()"
                             Color="@GetStatusColor(context.Item.Status)" />
            </CellTemplate>
        </TemplateColumn>

    </ColumnDefs>

</DataTable>
```

### 5. Table code-behind (`CollectionsTable.razor.cs`)

```csharp
namespace CreditInTimeFront.WebApp.Modules.Collections.Components;

public partial class CollectionsTable
{
    [Parameter] public IReadOnlyList<CollectionViewModel> Items { get; set; } = [];

    private string _search = "";

    private IReadOnlyList<FilterDef<CollectionViewModel>> _filters => new[]
    {
        new FilterDef<CollectionViewModel>
        {
            Type        = FilterType.SearchFilter,
            Placeholder = "Search client...",
            GetValue    = () => _search,
            SetValue    = v => _search = v
        }
    };

    private bool FilterItem(CollectionViewModel item) =>
        string.IsNullOrWhiteSpace(_search) ||
        item.ClientName.Contains(_search, StringComparison.OrdinalIgnoreCase);

    private static Color GetStatusColor(CollectionStatus status) => status switch
    {
        CollectionStatus.Current     => Color.Success,
        CollectionStatus.Pending     => Color.Warning,
        CollectionStatus.Overdue     => Color.Error,
        _                            => Color.Default
    };
}
```

### 6. Table CSS (`CollectionsTable.razor.css`)

Only module-specific overrides. Generic rules come from `table-base.css` via `DataTable<T>`.

```css
/* No extra rules needed if the base styles are sufficient.
   Add overrides here only for module-specific adjustments. */
```

### 7. Add the link in `NavMenu.razor`

```razor
<MudNavLink Href="/collections" Icon="@Icons.Material.Outlined.RequestPage" Match="NavLinkMatch.Prefix">
    Collections
</MudNavLink>
```

---

## Rules — do not break

| Rule | Why |
|------|-----|
| Shared components have no business logic | They are pure presentation wrappers. Logic goes in `State/` |
| All data arrives via `[Parameter]` | Never call APIs from a shared component |
| No cross-module dependencies | `Modules/CRM` never imports from `Modules/CreditFactory` |
| Module CSS only has specific overrides | Generic styles go in `table-base.css` or the shared component |
| `::deep` only in `.razor.css` files | In global CSS (`app.css`, `table-base.css`) use standard selectors |
| One component per visual concept | Do not create `CollectionsBadge` if `StatusBadge` already covers the case |
| Pass method references to `FilterPredicate`, not lambdas | Avoids allocating a new `Func<T,bool>` delegate on every render |
