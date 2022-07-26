using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using QuickForms.Client.Models;
using QuickForms.Client.Repositories;

namespace QuickForms.Client.Pages;

public partial class EditSurvey
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public ISurveyRepository SurveyRepository { get; set; }

    [Parameter]
    public string Id { get; set; }

    public bool IsSurveyJsRendered { get; set; } = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IsSurveyJsRendered = false;
            using var dotnetObjRef = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("setupSurveyJS", dotnetObjRef);
            IsSurveyJsRendered = true;
        }
    }

    [JSInvokable]
    public async Task<string> GetCurrentSurvey()
    {
        var survey = await SurveyRepository.GetSurvey(Id);
        return JsonConvert.SerializeObject(survey);
    }
}
