using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;
using QuickForms.Client.Models;
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

    [Inject]
    public ISnackbar Snackbar { get; set; }

    [Inject]
    public ILogger<RunSurvey> Logger { get; set; }

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
        try
        {
            var survey = await SurveyRepository.GetSurvey(Id);
            return JsonConvert.SerializeObject(survey);
        }
        catch (HttpRequestException httpEx) when (httpEx.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Logger.LogError(httpEx, "Survey not found");
            Snackbar.Add("Survey not found", Severity.Error);
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error loading survey");
            Snackbar.Add("Error occurred while loading survey", Severity.Error);
            throw;
        }
    }

    [JSInvokable]
    public async Task SaveCurrentSurveyResults(SurveyResults surveyResults)
    {
        try
        {
            await SurveyRepository.SaveSurveyResults(surveyResults);
            Snackbar.Add("Successfully saved survey results!", Severity.Success);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating survey");
            Snackbar.Add("Error occurred while saving survey results", Severity.Error);
            throw;
        }
    }
}


