using Microsoft.AspNetCore.Components;
using QuickForms.Client.Models;
using QuickForms.Client.Repositories;

namespace QuickForms.Client.Pages;

public partial class ActiveSurveys
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public ISurveyRepository SurveyRepository { get; set; }
    public List<Survey> Surveys { get; set; } = new List<Survey>();

    protected override async Task OnInitializedAsync()
    {
        Surveys = await SurveyRepository.GetSurveys();
    }

    private void EditSurvey(string id)
    {
        NavigationManager.NavigateTo($"editSurvey/{id}");
    }
}
