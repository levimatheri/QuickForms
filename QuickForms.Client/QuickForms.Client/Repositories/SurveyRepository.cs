using Newtonsoft.Json;
using QuickForms.Client.Models;
using QuickForms.Client.Models.Serialization;
using System.Text;
using System.Text.Json;

namespace QuickForms.Client.Repositories;

public class SurveyRepository : ISurveyRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SurveyRepository> _logger;

    private readonly static JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public SurveyRepository(IHttpClientFactory httpClientFactory, ILogger<SurveyRepository> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<List<Survey>> GetSurveys()
    {
        var httpClient = _httpClientFactory.CreateClient(Constants.QuickFormsApi.Name);

        try
        {
            var httpResponseMessage = await httpClient.GetAsync("/api/surveys");

            httpResponseMessage.EnsureSuccessStatusCode();

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return (await System.Text.Json.JsonSerializer.DeserializeAsync(contentStream, SurveyJsonContext.Default.ListSurvey))!;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to retrieve surveys from API");
            throw;
        }
    }

    public async Task<Survey> GetSurvey(string id)
    {
        var httpClient = _httpClientFactory.CreateClient(Constants.QuickFormsApi.Name);

        try
        {
            var httpResponseMessage = await httpClient.GetAsync($"/api/surveys/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return (await System.Text.Json.JsonSerializer.DeserializeAsync(contentStream, SurveyJsonContext.Default.Survey))!;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to retrieve survey with {id} from API", id);
            throw;
        }
    }

    public async Task UpdateSurvey(Survey survey)
    {
        var httpClient = _httpClientFactory.CreateClient(Constants.QuickFormsApi.Name);

        try
        {
            var surveyString = System.Text.Json.JsonSerializer.Serialize(survey, SurveyJsonContext.Default.Survey);
            var httpResponseMessage = await httpClient.PutAsync(
                    $"/api/surveys/{survey.Id}", new StringContent(surveyString, Encoding.UTF8, "application/json"));

            httpResponseMessage.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to update survey {survey}", survey);
            throw;
        }
    }

    public async Task SaveSurveyResults(SurveyResults surveyResults)
    {
        var httpClient = _httpClientFactory.CreateClient(Constants.QuickFormsApi.Name);

        try
        {
            var surveyResultString = System.Text.Json.JsonSerializer.Serialize(surveyResults, SurveyResultsJsonContext.Default.SurveyResults);
            var httpResponseMessage = await httpClient.PostAsync(
                    $"/api/surveyResults/{surveyResults.SurveyId}", new StringContent(surveyResultString, Encoding.UTF8, "application/json"));

            httpResponseMessage.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to save survey results {surveyResults}", surveyResults);
            throw;
        }
    }
}
