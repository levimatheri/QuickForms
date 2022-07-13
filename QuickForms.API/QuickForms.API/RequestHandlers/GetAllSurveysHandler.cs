using QuickForms.API.Database;
using QuickForms.API.Models;

namespace QuickForms.API.RequestHandlers;

public class GetAllSurveysHandler : IRequestHandler<GetAllSurveysRequest, List<Survey>>
{
    private readonly IMongoClientBuilder _mongoClientBuilder;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public GetAllSurveysHandler(
        IMongoClientBuilder mongoClientBuilder,
        IOptions<DatabaseSettings> databaseSettings)
    {
        _mongoClientBuilder = mongoClientBuilder;
        _databaseSettings = databaseSettings;
    }

    public Task<List<Survey>> Handle(GetAllSurveysRequest request, CancellationToken cancellationToken)
    {
        var mongoClient = _mongoClientBuilder.Build();
        var mongoDatabase = mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        return mongoDatabase
            .GetCollection<Survey>(_databaseSettings.Value.SurveyCollectionName)
            .Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
    }
}

public record GetAllSurveysRequest : IRequest<List<Survey>>;
