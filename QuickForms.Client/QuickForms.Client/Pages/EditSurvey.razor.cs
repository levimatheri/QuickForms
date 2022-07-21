using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace QuickForms.Client.Pages;

public partial class EditSurvey
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Parameter]
    public string? Id { get; set; }

    private IJSObjectReference _module;
    public bool IsSurveyJsRendered { get; set; } = false;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IsSurveyJsRendered = false;
            await JSRuntime.InvokeVoidAsync("setupSurveyJS");
            IsSurveyJsRendered = true;
        }

    }
}
