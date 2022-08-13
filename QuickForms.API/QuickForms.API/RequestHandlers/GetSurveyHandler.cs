using AutoMapper;
using QuickForms.API.Database;
using QuickForms.API.Models;

namespace QuickForms.API.RequestHandlers;

public class GetSurveyHandler : IRequestHandler<GetSurveyRequest, SurveyDto>
{
    private readonly IMongoClient _mongoClient;
    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMapper _mapper;

    public GetSurveyHandler(
        IMongoClient mongoClient,
        IOptions<DatabaseSettings> databaseSettings,
        IMapper mapper)
    {
        _mongoClient = mongoClient;
        _databaseSettings = databaseSettings;
        _mapper = mapper;
    }

    public async Task<SurveyDto> Handle(GetSurveyRequest request, CancellationToken cancellationToken)
    {
        var mongoDatabase = _mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        return _mapper.Map<SurveyDto>(await mongoDatabase
            .GetCollection<Survey>(_databaseSettings.Value.SurveyCollectionName)
            .Find(s => s.Id == request.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken));
    }
}

public record GetSurveyRequest(string Id) : IRequest<SurveyDto>;
