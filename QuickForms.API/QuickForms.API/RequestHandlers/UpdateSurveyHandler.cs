using AutoMapper;
using QuickForms.API.Database;
using QuickForms.API.Models;

namespace QuickForms.API.RequestHandlers;
public class UpdateSurveyHandler : IRequestHandler<UpdateSurveyRequest>
{
    private readonly IMongoClientBuilder _mongoClientBuilder;
    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMapper _mapper;

    public UpdateSurveyHandler(
        IMongoClientBuilder mongoClientBuilder,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
    {
        _mongoClientBuilder = mongoClientBuilder;
        _databaseSettings = databaseSettings;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateSurveyRequest request, CancellationToken cancellationToken)
    {
        var mongoClient = _mongoClientBuilder.Build();
        var mongoDatabase = mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        var survey = _mapper.Map<Survey>(request.UpdatedSurvey);

        await mongoDatabase
            .GetCollection<Survey>(_databaseSettings.Value.SurveyCollectionName)
            .ReplaceOneAsync(s => s.Id == survey.Id, survey, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}

public record UpdateSurveyRequest(string Id, UpdateSurveyDto UpdatedSurvey) : IRequest;