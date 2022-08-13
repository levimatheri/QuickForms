namespace QuickForms.API.Database;

public class MongoClientBuilder : IMongoClientBuilder
{
    public IMongoClient Build(string connectionString)
    {
        return new MongoClient(connectionString);
    }
}
