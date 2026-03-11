using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CRM.ViewModels;
using CreditInTimeFront.WebApp.Modules.CRM.State;

namespace CreditInTimeFront.WebApp.Modules.CRM.Components;

public partial class InteraccionesTable
{
    [Parameter] public IReadOnlyList<InteraccionViewModel> Interacciones { get; set; } = [];
    [Parameter] public EventCallback OnNuevoCliente { get; set; }

    private string             _searchCliente      = string.Empty;
    private string             _tipoContactoFilter = string.Empty;
    private InteraccionStatus? _estadoFilter       = null;
    private string             _searchEjecutivo    = string.Empty;
    private DateTime?          _fechaFilter        = null;

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

    private static Color GetEstadoColor(InteraccionStatus estado) => estado switch
    {
        InteraccionStatus.Completado => Color.Success,
        InteraccionStatus.EnProceso  => Color.Info,
        InteraccionStatus.Pendiente  => Color.Warning,
        _                            => Color.Default
    };

    private bool FilterInteraccion(InteraccionViewModel r) =>
        (string.IsNullOrEmpty(_searchCliente) ||
         ContainsIgnoreAccents(r.ClienteNombre,  _searchCliente) ||
         ContainsIgnoreAccents(r.Identificacion, _searchCliente)) &&
        (string.IsNullOrEmpty(_tipoContactoFilter) || r.TipoContacto.ToLabel() == _tipoContactoFilter) &&
        (_estadoFilter == null || r.Estado == _estadoFilter) &&
        (string.IsNullOrEmpty(_searchEjecutivo) ||
         ContainsIgnoreAccents(r.EjecutivoAsignado, _searchEjecutivo)) &&
        (_fechaFilter == null || r.FechaCreacion.Date == _fechaFilter.Value.Date);
}
