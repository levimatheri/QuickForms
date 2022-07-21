using QuickForms.Client.Models;

namespace QuickForms.Client.Repositories;

public interface ISurveyRepository
{
    Task<List<Survey>> GetSurveys();
}
