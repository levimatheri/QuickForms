using AutoMapper;
using QuickForms.API.Database;
using QuickForms.API.Models;

namespace QuickForms.API.RequestHandlers;

public class CreateSurveyHandler : IRequestHandler<CreateSurveyRequest, SurveyDto>
{
    private readonly IMongoClientBuilder _mongoClientBuilder;
    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMapper _mapper;

    public CreateSurveyHandler(
        IMongoClientBuilder mongoClientBuilder,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
    {
        _mongoClientBuilder = mongoClientBuilder;
        _databaseSettings = databaseSettings;
        _mapper = mapper;
    }

    public async Task<SurveyDto> Handle(CreateSurveyRequest request, CancellationToken cancellationToken)
    {
        var mongoClient = _mongoClientBuilder.Build();
        var mongoDatabase = mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        var survey = _mapper.Map<Survey>(request.Survey);
        await mongoDatabase
            .GetCollection<Survey>(_databaseSettings.Value.SurveyCollectionName)
            .InsertOneAsync(survey, cancellationToken: cancellationToken);

        return _mapper.Map<SurveyDto>(survey);
    }
}

public record CreateSurveyRequest(NewSurveyDto Survey) : IRequest<SurveyDto>;
