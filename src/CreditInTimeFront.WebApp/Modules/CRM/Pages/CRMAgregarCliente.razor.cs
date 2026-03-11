using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CRM.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CRM.Pages;

public partial class CRMAgregarCliente
{
    [Inject] private NavigationManager NavManager { get; set; } = default!;

    private MudForm _form = default!;

    // Sección 1 — Información Básica
    private string _nombre         = string.Empty;
    private string _identificacion = string.Empty;
    private string _email          = string.Empty;
    private string _telefono       = string.Empty;

    // Sección 2 — Información Empresarial
    private string          _nombreEmpresa = string.Empty;
    private string          _rnc           = string.Empty;
    private SegmentoCliente _segmento      = SegmentoCliente.Individual;

    // Sección 3 — Información de Contacto
    private string _direccion    = string.Empty;
    private string _provincia    = string.Empty;
    private string _municipio    = string.Empty;
    private string _codigoPostal = string.Empty;

    // Sección 4 — Notas Adicionales
    private string _observaciones = string.Empty;

    private readonly Func<string, string?> _emailValidator = value =>
        !string.IsNullOrWhiteSpace(value) && !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$")
            ? "Ingrese un correo válido"
            : null;

    private void Cancelar() => NavManager.NavigateTo("/crm");

    private async Task Guardar()
    {
        await _form.ValidateAsync();
        if (!_form.IsValid) return;

        // TODO: reemplazar con llamada a servicio real (CrmState.CrearClienteAsync) al conectar backend
        NavManager.NavigateTo("/crm");
    }
}
