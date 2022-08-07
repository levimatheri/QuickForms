using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
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

    [Inject]
    public ILogger<EditSurvey> Logger { get; set; }

    [Inject]
    public ISnackbar Snackbar { get; set; } 

    [Parameter]
    public string Id { get; set; }

    public bool IsSurveyJsRendered { get; set; } = false;

    private DotNetObjectReference<EditSurvey>? _dotNetObjectReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IsSurveyJsRendered = false;
            _dotNetObjectReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("setupSurveyJS", _dotNetObjectReference);
            IsSurveyJsRendered = true;
        }
    }

    [JSInvokable]
    public async Task<Survey> GetCurrentSurvey()
    {
        try
        {
            return await SurveyRepository.GetSurvey(Id);
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
    public async Task UpdateCurrentSurvey(Survey survey)
    {
        try
        {
            await SurveyRepository.UpdateSurvey(survey);
            Snackbar.Add("Successfully saved changes!", Severity.Success);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating survey");
            Snackbar.Add("Error occurred while saving changes", Severity.Error);
            throw;
        }
    }
}
