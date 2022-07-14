using QuickForms.API.Database;
using QuickForms.API.Models;

namespace QuickForms.API.RequestHandlers;
public class DeleteSurveyHandler : IRequestHandler<DeleteSurveyRequest>
{
    private readonly IMongoClientBuilder _mongoClientBuilder;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public DeleteSurveyHandler(
        IMongoClientBuilder mongoClientBuilder,
        IOptions<DatabaseSettings> databaseSettings)
    {
        _mongoClientBuilder = mongoClientBuilder;
        _databaseSettings = databaseSettings;
    }

    public async Task<Unit> Handle(DeleteSurveyRequest request, CancellationToken cancellationToken)
    {
        var mongoClient = _mongoClientBuilder.Build();
        var mongoDatabase = mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        await mongoDatabase
            .GetCollection<Survey>(_databaseSettings.Value.SurveyCollectionName)
            .DeleteOneAsync(s => s.Id == request.Id, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}

public record DeleteSurveyRequest(string Id) : IRequest;