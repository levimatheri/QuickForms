using AutoMapper;
using QuickForms.API.Database;
using QuickForms.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.RequestHandlers;
public class SaveSurveyResultsHandler : IRequestHandler<SaveSurveyResultsRequest>
{
    private readonly IMongoClient _mongoClient;
    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMapper _mapper;

    public SaveSurveyResultsHandler(
        IMongoClient mongoClient,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
    {
        _mongoClient = mongoClient;
        _databaseSettings = databaseSettings;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(SaveSurveyResultsRequest request, CancellationToken cancellationToken)
    {
        var mongoDatabase = _mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        var surveyResults = _mapper.Map<SurveyResults>(request.SurveyResults);
        await mongoDatabase
            .GetCollection<SurveyResults>(_databaseSettings.Value.SurveyResultsCollectionName)
            .InsertOneAsync(surveyResults, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}

public record SaveSurveyResultsRequest(string Id, SurveyResultsDto SurveyResults) : IRequest;

