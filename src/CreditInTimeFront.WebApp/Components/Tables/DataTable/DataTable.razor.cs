using MudBlazor;
using Microsoft.AspNetCore.Components;

namespace CreditInTimeFront.WebApp.Components;

public partial class DataTable<T>
{
    // ── Data ─────────────────────────────────────────────────────────────────
    [Parameter] public IReadOnlyList<T> Items { get; set; } = [];

    /// <summary>
    /// Filter definitions — describes which filter controls to render in the toolbar.
    /// Order matters: filters are rendered left to right.
    /// </summary>
    [Parameter] public IReadOnlyList<FilterDef> Filters { get; set; } = [];

    /// <summary>
    /// Filtering logic provided by the parent.
    /// Receives the item and the current filter values dictionary.
    /// Use <see cref="FilterValues"/> extension methods to read typed values.
    /// Return true to show the row, false to hide it.
    /// </summary>
    [Parameter] public Func<T, IReadOnlyDictionary<string, object?>, bool>? FilterPredicate { get; set; }

    // ── Slots ─────────────────────────────────────────────────────────────────
    /// <summary>MudDataGrid column definitions (PropertyColumn, TemplateColumn, etc.).</summary>
    [Parameter] public RenderFragment? ColumnDefs { get; set; }

    /// <summary>Optional buttons rendered on the right side of the toolbar title row.</summary>
    [Parameter] public RenderFragment? HeaderActions { get; set; }

    /// <summary>
    /// Optional expandable row content (maps to MudDataGrid ChildRowContent).
    /// Requires a HierarchyColumn in ColumnDefs to show the expand icon.
    /// </summary>
    [Parameter] public RenderFragment<CellContext<T>>? RowDetails { get; set; }

    // ── Appearance ────────────────────────────────────────────────────────────
    [Parameter] public string Title     { get; set; } = "";
    [Parameter] public string TitleIcon { get; set; } = Icons.Material.Outlined.TableChart;
    [Parameter] public int    RowsPerPage { get; set; } = 8;
    [Parameter] public string EmptyText   { get; set; } = "No se encontraron registros.";
    [Parameter] public string EmptyIcon   { get; set; } = Icons.Material.Outlined.SearchOff;

    // ── Internal state ────────────────────────────────────────────────────────
    private readonly Dictionary<string, object?> _values = new();
    private readonly int[] _pageSizes = [8, 16, 24];

    // New lambda on every render → MudDataGrid always picks up the latest _values.
    private Func<T, bool> _quickFilter =>
        item => FilterPredicate is null || FilterPredicate(item, _values);

    protected override void OnParametersSet()
    {
        // Initialize default value for each filter key the first time it appears.
        foreach (var f in Filters)
        {
            if (!_values.ContainsKey(f.Key))
                _values[f.Key] = DefaultValue(f);
        }
    }

    private static object? DefaultValue(FilterDef f) => f switch
    {
        SearchFilter    => string.Empty,
        SelectFilter    => string.Empty,
        DateRangeFilter => new DateRange(null, null),
        DateFilter      => (DateTime?)null,
        _               => null
    };

    // ── Getters (used by filter control bindings in razor) ────────────────────
    private string GetString(string key)
        => _values.TryGetValue(key, out var v) ? (v as string ?? string.Empty) : string.Empty;

    private DateRange GetDateRange(string key)
        => _values.TryGetValue(key, out var v) && v is DateRange dr ? dr : new DateRange(null, null);

    private DateTime? GetDate(string key)
        => _values.TryGetValue(key, out var v) ? v as DateTime? : null;

    // ── Setters (called when user changes a filter) ───────────────────────────
    private void SetString(string key, string? value)
    {
        _values[key] = value ?? string.Empty;
        StateHasChanged();
    }

    private void SetDateRange(string key, DateRange? value)
    {
        _values[key] = value ?? new DateRange(null, null);
        StateHasChanged();
    }

    private void SetDate(string key, DateTime? value)
    {
        _values[key] = value;
        StateHasChanged();
    }
}
