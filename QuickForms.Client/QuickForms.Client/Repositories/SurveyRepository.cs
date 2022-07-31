using Newtonsoft.Json;
using QuickForms.Client.Models;
using System.Text;

namespace QuickForms.Client.Repositories;

public class SurveyRepository : ISurveyRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SurveyRepository> _logger;

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

            var contentString = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Survey>>(contentString)!;
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

            var contentString = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Survey>(contentString)!;
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
            var surveyString = JsonConvert.SerializeObject(survey);
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
}
