using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CreditFactory.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CreditFactory.Components;

public partial class CreditRequestsTable
{
    [Parameter] public IReadOnlyList<CreditRequestViewModel> Requests    { get; set; } = [];
    [Parameter] public EventCallback<CreditRequestViewModel> OnPrintRequest { get; set; }

    private string    _searchText    = string.Empty;
    private string    _productFilter = string.Empty;
    private DateRange _dateRange     = new(null, null);

    // Removes diacritical marks so "jose" matches "José", "garcia" matches "García", etc.
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

    // MudTable evaluates this on every render cycle — no manual re-filter needed
    private bool FilterRequest(CreditRequestViewModel r) =>
        (string.IsNullOrEmpty(_productFilter) || r.Product == _productFilter) &&
        (string.IsNullOrEmpty(_searchText)    ||
         ContainsIgnoreAccents(r.CaseNumber, _searchText) ||
         ContainsIgnoreAccents(r.ClientName, _searchText)) &&
        (_dateRange.Start == null || r.SubmittedAt.Date >= _dateRange.Start.Value.Date) &&
        (_dateRange.End   == null || r.SubmittedAt.Date <= _dateRange.End.Value.Date);

    private static string GetProgressColor(int progress) => progress switch
    {
        >= 80 => "primary",
        >= 40 => "warning",
        _     => "error"
    };
}
