using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CreditFactory.State;
using CreditInTimeFront.WebApp.Modules.CreditFactory.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CreditFactory.Components;

public partial class DocumentsModal
{
    [Parameter] public bool IsOpen                              { get; set; }
    [Parameter] public CreditRequestViewModel? Request          { get; set; }
    [Parameter] public EventCallback OnClose                    { get; set; }
    [Parameter] public EventCallback<string> OnOpenReport       { get; set; }

    private readonly List<DocItem> _documents =
    [
        new(Icons.Material.Outlined.Assessment, "blue",   "Formulario de Evaluación Crediticia",     "Análisis y Evaluación de Riesgo",         CreditFactoryState.ReportUrls.EvaluacionCrediticia),
        new(Icons.Material.Outlined.VerifiedUser, "purple", "Autorización Consulta Buró de Crédito", "",                                        CreditFactoryState.ReportUrls.AutorizacionBuro),
        new(Icons.Material.Outlined.Person,      "green",  "Perfil del Cliente - Persona Física",    "Formulario de Perfil del Cliente",         CreditFactoryState.ReportUrls.PerfilCliente),
        new(Icons.Material.Outlined.Apartment,   "teal",   "Perfil del Cliente - Persona Jurídica",  "Formulario de Perfil del Cliente",         CreditFactoryState.ReportUrls.PerfilClienteJuridica),
        new(Icons.Material.Outlined.TaskAlt,     "amber",  "Reporte de Preaprobación de Crédito",    "Resumen Ejecutivo para Comité Crediticio", CreditFactoryState.ReportUrls.PreAprobacion),
        new(Icons.Material.Outlined.Badge,       "cyan",   "Conozca a su Cliente - Persona Física",  "Formulario de Identificación del Cliente", CreditFactoryState.ReportUrls.ConozcaSuClienteFisico),
        new(Icons.Material.Outlined.Storefront,  "teal",   "Conozca a su Cliente - Persona Jurídica","Formulario de Identificación del Cliente", CreditFactoryState.ReportUrls.ConozcaSuClienteJuridico),
    ];

    private record DocItem(string Icon, string Color, string Title, string Subtitle, string Url)
    {
        public string ColorClass => $"doc-icon--{Color}";
        public string BgClass    => $"doc-icon-bg--{Color}";
    }
}
