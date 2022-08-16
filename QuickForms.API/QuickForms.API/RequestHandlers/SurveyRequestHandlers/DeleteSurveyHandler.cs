using QuickForms.API.Database;
using QuickForms.API.Models;

namespace QuickForms.API.RequestHandlers.SurveyRequestHandlers;
public class DeleteSurveyHandler : IRequestHandler<DeleteSurveyRequest>
{
    private readonly IMongoClient _mongoClient;
    private readonly IOptions<DatabaseSettings> _databaseSettings;

    public DeleteSurveyHandler(
        IMongoClient mongoClient,
        IOptions<DatabaseSettings> databaseSettings)
    {
        _mongoClient = mongoClient;
        _databaseSettings = databaseSettings;
    }

    public async Task<Unit> Handle(DeleteSurveyRequest request, CancellationToken cancellationToken)
    {
        var mongoDatabase = _mongoClient.GetDatabase(_databaseSettings.Value.DatabaseName);

        await mongoDatabase
            .GetCollection<Survey>(_databaseSettings.Value.SurveyCollectionName)
            .DeleteOneAsync(s => s.Id == request.Id, cancellationToken: cancellationToken);

        return Unit.Value;
    }
}

public record DeleteSurveyRequest(string Id) : IRequest;