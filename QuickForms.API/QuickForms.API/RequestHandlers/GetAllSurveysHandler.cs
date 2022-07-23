using AutoMapper;
using QuickForms.API.Database;
using QuickForms.API.Models;

namespace QuickForms.API.RequestHandlers;

public class GetAllSurveysHandler : IRequestHandler<GetAllSurveysRequest, List<SurveyDto>>
{
    private readonly IMongoClientBuilder _mongoClientBuilder;
    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMapper _mapper;

    public GetAllSurveysHandler(
        IMongoClientBuilder mongoClientBuilder,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
    {
        _mongoClientBuilder = mongoClientBuilder;
        _databaseSettings = databaseSettings;
        _mapper = mapper;
    }

    public async Task<List<SurveyDto>> Handle(GetAllSurveysRequest request, CancellationToken cancellationToken)
    {
        var mongoClient = _mongoClientBuilder.Build();
        var mongoDatabase = mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        var surveys = await mongoDatabase
                    .GetCollection<Survey>(_databaseSettings.Value.SurveyCollectionName)
                    .Find(_ => true).ToListAsync(cancellationToken: cancellationToken);

        return _mapper.Map<List<Survey>, List<SurveyDto>>(surveys);
    }
}

public record GetAllSurveysRequest : IRequest<List<SurveyDto>>;
