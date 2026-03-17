using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Components;
using CreditInTimeFront.WebApp.Components;
using CreditInTimeFront.WebApp.Modules.CRM.ViewModels;
using CreditInTimeFront.WebApp.Modules.CRM.State;

namespace CreditInTimeFront.WebApp.Modules.CRM.Components;

public partial class InteraccionesTable
{
    [Parameter] public IReadOnlyList<InteraccionViewModel> Interacciones { get; set; } = [];
    [Parameter] public EventCallback OnNuevoCliente { get; set; }

    // ── Filter definitions ────────────────────────────────────────────────────
    private static readonly IReadOnlyList<FilterDef> _filters =
    [
        new SearchFilter("cliente",      "Cliente",       "Buscar cliente..."),
        new SelectFilter("tipoContacto", "Tipo Contacto",
            CrmState.ContactTypes.Select(t => new SelectOption(t, t)).ToList()),
        new SelectFilter("estado",       "Estado",
            Enum.GetValues<InteraccionStatus>()
                .Select(s => new SelectOption(s.ToString(), s.ToLabel()))
                .ToList()),
        new SearchFilter("ejecutivo",    "Ejecutivo",     "Nombre..."),
        new DateFilter  ("fecha",        "Fecha"),
    ];

    // ── Filter predicate ──────────────────────────────────────────────────────
    private bool FilterInteraccion(InteraccionViewModel r, IReadOnlyDictionary<string, object?> f)
    {
        var cliente      = f.GetString("cliente");
        var tipoContacto = f.GetString("tipoContacto");
        var estadoStr    = f.GetString("estado");
        var ejecutivo    = f.GetString("ejecutivo");
        var fecha        = f.GetDate("fecha");

        return (string.IsNullOrEmpty(cliente) ||
                ContainsIgnoreAccents(r.ClienteNombre,  cliente) ||
                ContainsIgnoreAccents(r.Identificacion, cliente)) &&
               (string.IsNullOrEmpty(tipoContacto) || r.TipoContacto.ToLabel() == tipoContacto) &&
               (string.IsNullOrEmpty(estadoStr)    || r.Estado.ToString()      == estadoStr)    &&
               (string.IsNullOrEmpty(ejecutivo) ||
                ContainsIgnoreAccents(r.EjecutivoAsignado, ejecutivo)) &&
               (fecha == null || r.FechaCreacion.Date == fecha.Value.Date);
    }

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
