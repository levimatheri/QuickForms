using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using QuickForms.Client.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.Client.Pages;
public partial class RunSurvey
{
    [Parameter]
    public string Id { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public ISurveyRepository SurveyRepository { get; set; }

    public bool IsSurveyJsRendered { get; set; } = false;

    private DotNetObjectReference<RunSurvey>? _dotNetObjectReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IsSurveyJsRendered = false;
            _dotNetObjectReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("runSurvey", _dotNetObjectReference);
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


