using Microsoft.AspNetCore.Components;

namespace QuickForms.Client.Shared;

public partial class AppBar
{
    [Parameter]
    public EventCallback OnSidebarToggled { get; set; }
}
