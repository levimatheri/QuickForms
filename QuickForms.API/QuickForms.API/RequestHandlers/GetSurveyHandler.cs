using QuickForms.API.Database;
using QuickForms.API.Models;

namespace QuickForms.API.RequestHandlers;

public class GetSurveyHandler : IRequestHandler<GetSurveyRequest, Survey>
{
    private readonly IMongoClientBuilder _mongoClientBuilder;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public GetSurveyHandler(
        IMongoClientBuilder mongoClientBuilder,
        IOptions<DatabaseSettings> databaseSettings)
    {
        _mongoClientBuilder = mongoClientBuilder;
        _databaseSettings = databaseSettings;
    }

    public Task<Survey> Handle(GetSurveyRequest request, CancellationToken cancellationToken)
    {
        var mongoClient = _mongoClientBuilder.Build();
        var mongoDatabase = mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        return mongoDatabase
            .GetCollection<Survey>(_databaseSettings.Value.SurveyCollectionName)
            .Find(s => s.Id == request.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}

public record GetSurveyRequest(string Id) : IRequest<Survey>;
