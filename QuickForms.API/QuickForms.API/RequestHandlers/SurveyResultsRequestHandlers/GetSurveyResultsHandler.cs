using AutoMapper;
using QuickForms.API.Database;
using QuickForms.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.RequestHandlers.SurveyResultsRequestHandlers;
public class GetSurveyResultsHandler : IRequestHandler<GetSurveyResultsRequest, List<SurveyResultsDto>>
{
    private readonly IMongoClient _mongoClient;
    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMapper _mapper;

    public GetSurveyResultsHandler(
        IMongoClient mongoClient,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
    {
        _mongoClient = mongoClient;
        _databaseSettings = databaseSettings;
        _mapper = mapper;
    }

    public async Task<List<SurveyResultsDto>> Handle(GetSurveyResultsRequest request, CancellationToken cancellationToken)
    {
        var mongoDatabase = _mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        var surveyResults = await mongoDatabase
                    .GetCollection<SurveyResults>(_databaseSettings.Value.SurveyResultsCollectionName)
                    .Find(s => s.SurveyId == request.SurveyId).ToListAsync(cancellationToken: cancellationToken);

        return _mapper.Map<List<SurveyResults>, List<SurveyResultsDto>>(surveyResults);
    }
}

public record GetSurveyResultsRequest(string SurveyId) : IRequest<List<SurveyResultsDto>>;