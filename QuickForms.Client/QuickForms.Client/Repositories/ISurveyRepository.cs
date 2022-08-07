using QuickForms.Client.Models;

namespace QuickForms.Client.Repositories;

public interface ISurveyRepository
{
    Task<List<Survey>> GetSurveys();
    Task<Survey> GetSurvey(string id);
    Task UpdateSurvey(Survey survey);
    Task SaveSurveyResults(SurveyResults surveyResult);
}
