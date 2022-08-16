using AutoMapper;
using QuickForms.API.Database;
using QuickForms.API.Models;

namespace QuickForms.API.RequestHandlers.SurveyRequestHandlers;

public class GetAllSurveysHandler : IRequestHandler<GetAllSurveysRequest, List<SurveyDto>>
{
    private readonly IMongoClient _mongoClient;
    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMapper _mapper;

    public GetAllSurveysHandler(
        IMongoClient mongoClient,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
    {
        _mongoClient = mongoClient;
        _databaseSettings = databaseSettings;
        _mapper = mapper;
    }

    public async Task<List<SurveyDto>> Handle(GetAllSurveysRequest request, CancellationToken cancellationToken)
    {
        var mongoDatabase = _mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        var surveys = await mongoDatabase
                    .GetCollection<Survey>(_databaseSettings.Value.SurveyCollectionName)
                    .Find(_ => true).ToListAsync(cancellationToken: cancellationToken);

        return _mapper.Map<List<Survey>, List<SurveyDto>>(surveys);
    }
}

public record GetAllSurveysRequest : IRequest<List<SurveyDto>>;
