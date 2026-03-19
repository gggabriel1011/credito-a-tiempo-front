using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Components;
using CreditInTimeFront.WebApp.Components;
using CreditInTimeFront.WebApp.Modules.CollectionManagement.State;
using CreditInTimeFront.WebApp.Modules.CollectionManagement.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CollectionManagement.Components;

public partial class CobroAlertsTable
{
    [Parameter] public IReadOnlyList<CobroCasoViewModel> Casos          { get; set; } = [];
    [Parameter] public EventCallback                     OnNuevaGestion { get; set; }

    // ── Filter definitions ────────────────────────────────────────────────────

    private static readonly IReadOnlyList<FilterDef> _filters =
    [
        new SearchFilter("search", "Cliente", "Nombre de cliente..."),
        new SelectFilter("diasAtraso", "Rango de Atraso",
        [
            new SelectOption("1-30",  "1 a 30 días"),
            new SelectOption("31-90", "31 a 90 días"),
            new SelectOption("90+",   "Más de 90 días"),
        ]),
        new SelectFilter("monto", "Monto (RD$)",
        [
            new SelectOption("menos100k", "Menos de 100k"),
            new SelectOption("100k-500k", "100k - 500k"),
            new SelectOption("mas500k",   "Más de 500k"),
        ]),
        new SelectFilter("ultimaGestion", "Última Gestión",
        [
            new SelectOption("Llamada",         "Llamada"),
            new SelectOption("Visita",          "Visita"),
            new SelectOption("Correo",          "Correo"),
            new SelectOption("Promesa de Pago", "Promesa de Pago"),
            new SelectOption("Judicial",           "Judicial"),
        ]),
    ];

    // ── Filter predicate ──────────────────────────────────────────────────────

    private bool FilterCaso(CobroCasoViewModel c, IReadOnlyDictionary<string, object?> f)
    {
        var search        = f.GetString("search");
        var diasAtraso    = f.GetString("diasAtraso");
        var monto         = f.GetString("monto");
        var ultimaGestion = f.GetString("ultimaGestion");

        return (string.IsNullOrEmpty(search)        || ContainsIgnoreAccents(c.ClienteNombre, search)) &&
               (string.IsNullOrEmpty(diasAtraso)    || MatchDiasAtraso(c.DiasAtraso, diasAtraso))      &&
               (string.IsNullOrEmpty(monto)         || MatchMonto(c.Monto, monto))                     &&
               (string.IsNullOrEmpty(ultimaGestion) || c.TipoUltimaGestion == ultimaGestion);
    }

    // ── Cell helpers ──────────────────────────────────────────────────────────

    private static string GetDiasClass(int dias) => dias switch
    {
        > 90 => "critico",
        > 30 => "alerta",
        _    => "normal"
    };

    private static string FormatMonto(decimal amount) =>
        CollectionManagementState.FormatMonto(amount);

    // ── Filter range helpers ──────────────────────────────────────────────────

    private static bool MatchDiasAtraso(int dias, string range) => range switch
    {
        "1-30"  => dias >= 1  && dias <= 30,
        "31-90" => dias >= 31 && dias <= 90,
        "90+"   => dias > 90,
        _       => true
    };

    private static bool MatchMonto(decimal monto, string range) => range switch
    {
        "menos100k" => monto < 100_000m,
        "100k-500k" => monto >= 100_000m && monto <= 500_000m,
        "mas500k"   => monto > 500_000m,
        _           => true
    };

    // ── Accent-insensitive search ─────────────────────────────────────────────

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
