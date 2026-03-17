using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Components;
using CreditInTimeFront.WebApp.Modules.CreditFactory.ViewModels;
using CreditInTimeFront.WebApp.Modules.CreditFactory.State;

namespace CreditInTimeFront.WebApp.Modules.CreditFactory.Components;

public partial class CreditRequestsTable
{
    [Parameter] public IReadOnlyList<CreditRequestViewModel> Requests       { get; set; } = [];
    [Parameter] public EventCallback<CreditRequestViewModel> OnPrintRequest { get; set; }

    // ── Filter definitions ────────────────────────────────────────────────────
    // DataTable renders these controls and owns the filter state.
    private static readonly IReadOnlyList<FilterDef> _filters =
    [
        new DateRangeFilter("dates",   "Fecha"),
        new SelectFilter   ("product", "Producto",
            CreditFactoryState.ProductTypes.Select(p => new SelectOption(p, p)).ToList()),
        new SearchFilter   ("search",  "Buscar", "Buscar expediente..."),
    ];

    // ── Filter predicate ──────────────────────────────────────────────────────
    // DataTable calls this on every row, passing the current filter values.
    private bool FilterRequest(CreditRequestViewModel r, IReadOnlyDictionary<string, object?> f)
    {
        var product   = f.GetString("product");
        var search    = f.GetString("search");
        var dateRange = f.GetDateRange("dates");

        return (string.IsNullOrEmpty(product) || r.Product == product) &&
               (string.IsNullOrEmpty(search)  ||
                ContainsIgnoreAccents(r.CaseNumber,  search) ||
                ContainsIgnoreAccents(r.ClientName,  search)) &&
               (dateRange.Start == null || r.SubmittedAt.Date >= dateRange.Start.Value.Date) &&
               (dateRange.End   == null || r.SubmittedAt.Date <= dateRange.End.Value.Date);
    }

    // ── Cell helpers ──────────────────────────────────────────────────────────
    private static string GetProgressColor(int progress) => progress switch
    {
        >= 80 => "primary",
        >= 40 => "warning",
        _     => "error"
    };

    // ── Accent-insensitive search helpers ─────────────────────────────────────
    private static string StripAccents(string input)
    {
        var normalized = input.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalized.Length);
        foreach (var c in normalized)
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    private static bool ContainsIgnoreAccents(string source, string term) =>
        StripAccents(source).Contains(StripAccents(term), StringComparison.OrdinalIgnoreCase);
}
