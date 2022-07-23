using Newtonsoft.Json;
using QuickForms.Client.Models;

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
}
