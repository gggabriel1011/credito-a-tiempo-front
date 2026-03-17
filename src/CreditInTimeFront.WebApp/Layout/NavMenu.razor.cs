using MudBlazor;
using Microsoft.AspNetCore.Components.Routing;

namespace CreditInTimeFront.WebApp.Layout;

public partial class NavMenu
{
    // User info — replaceable by auth service injection
    private string _userInitials = "GA";
    private string _userRole = "Gerente Operativo";
    private string _userLocation = "Sede Central";

    // Nav items — order and visibility controllable from here
    private readonly List<NavItem> _navItems =
    [
        new("/",                     Icons.Material.Outlined.Dashboard,   "Dashboard Operacional",  NavLinkMatch.All),
        new("credit-factory",        Icons.Material.Outlined.Factory,     "Fábrica de Crédito"),
        new("crm",                   Icons.Material.Outlined.Payments,    "CRM"),
        new("collection-management", Icons.Material.Outlined.Payments,    "Gestión de Cobro"),
        new("jce",                   Icons.Material.Outlined.Fingerprint, "JCE"),
        new("credit-bureau",         Icons.Material.Outlined.CreditScore, "Buró de Crédito"),
        new("profile",               Icons.Material.Outlined.Person,      "Mi Perfil"),
        new("settings",              Icons.Material.Outlined.Settings,    "Configuración"),
        new("reports",               Icons.Material.Outlined.Description,  "Reportes"),
        new("components-playground", Icons.Material.Outlined.AutoAwesome,  "Prueba de Componentes"),
    ];

    private record NavItem(string Href, string Icon, string Label,
        NavLinkMatch Match = NavLinkMatch.Prefix);
}
