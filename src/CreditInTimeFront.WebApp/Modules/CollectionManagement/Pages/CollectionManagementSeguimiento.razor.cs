using Microsoft.AspNetCore.Components;
using CreditInTimeFront.WebApp.Modules.CollectionManagement.State;
using CreditInTimeFront.WebApp.Modules.CollectionManagement.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CollectionManagement.Pages;

public partial class CollectionManagementSeguimiento
{
    [Parameter] public string? ClienteId { get; set; }

    [Inject] private NavigationManager NavManager { get; set; } = default!;

    // TODO: reemplazar con [Inject] private CollectionManagementState _state { get; set; } al conectar servicios reales
    private readonly CollectionManagementState _state = new();

    private string _tituloCliente       = string.Empty;
    private string _subtitleSeguimiento = string.Empty;
    private string _volverHref          = "/collection-management";

    // ── Form state ────────────────────────────────────────────────────────────
    private TipoSeguimiento    _tipoSeguimiento = TipoSeguimiento.LlamadaCobro;
    private PrioridadSeguimiento _prioridad     = PrioridadSeguimiento.Media;
    private DateTime?          _fechaProgramada;
    private TimeSpan?          _horaProgramada;
    private string             _descripcion     = string.Empty;
    private bool               _notificarCorreo = false;
    private bool               _notificarSistema = true;

    protected override void OnParametersSet()
    {
        if (string.IsNullOrEmpty(ClienteId))
        {
            _tituloCliente       = string.Empty;
            _subtitleSeguimiento = string.Empty;
            _volverHref          = "/collection-management";
            return;
        }

        var caso = _state.Casos.FirstOrDefault(c => c.ClienteId == ClienteId) ?? _state.Casos[0];

        _tituloCliente       = $"- {caso.ClienteNombre}";
        _subtitleSeguimiento = $"ID Cliente: {caso.ClienteId} • {caso.TipoCredito}";
        _volverHref          = $"/collection-management/detalle/{caso.ClienteId}";
    }

    private void Cancelar() =>
        NavManager.NavigateTo(string.IsNullOrEmpty(ClienteId)
            ? "/collection-management"
            : $"/collection-management/detalle/{ClienteId}");

    private void ProgramarAccion()
    {
        // TODO: reemplazar con llamada a servicio real al conectar backend
        NavManager.NavigateTo("/collection-management");
    }
}
