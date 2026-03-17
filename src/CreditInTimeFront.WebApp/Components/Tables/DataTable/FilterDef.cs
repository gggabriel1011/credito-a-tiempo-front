using MudBlazor;

namespace CreditInTimeFront.WebApp.Components;

// ── Filter definitions ────────────────────────────────────────────────────────
// Each record describes ONE filter slot in the DataTable toolbar.
// Key    → identifier used to read/write the value via FilterPredicate
// Label  → visible label shown above the control

public abstract record FilterDef(string Key, string Label);

/// <summary>Full-text search field with a magnifier adornment.</summary>
public record SearchFilter(string Key, string Label, string Placeholder = "Buscar...")
    : FilterDef(Key, Label);

/// <summary>Date range picker (Desde – Hasta).</summary>
public record DateRangeFilter(string Key, string Label)
    : FilterDef(Key, Label);

/// <summary>Single date picker.</summary>
public record DateFilter(string Key, string Label)
    : FilterDef(Key, Label);

/// <summary>Dropdown select. Options are plain string pairs (Value, Label).</summary>
public record SelectFilter(string Key, string Label, IReadOnlyList<SelectOption> Options)
    : FilterDef(Key, Label);

/// <summary>One option in a <see cref="SelectFilter"/>.</summary>
/// <param name="Value">Internal value stored in the filter dictionary.</param>
/// <param name="Label">Text shown in the dropdown.</param>
public record SelectOption(string Value, string Label);

// ── Filter value accessors ────────────────────────────────────────────────────
// Extension methods used by parent FilterPredicate implementations to read
// the current value of each filter from the dictionary DataTable manages.

public static class FilterValues
{
    public static string GetString(this IReadOnlyDictionary<string, object?> f, string key)
        => f.TryGetValue(key, out var v) ? (v as string ?? string.Empty) : string.Empty;

    public static DateRange GetDateRange(this IReadOnlyDictionary<string, object?> f, string key)
        => f.TryGetValue(key, out var v) && v is DateRange dr ? dr : new DateRange(null, null);

    public static DateTime? GetDate(this IReadOnlyDictionary<string, object?> f, string key)
        => f.TryGetValue(key, out var v) ? v as DateTime? : null;
}
