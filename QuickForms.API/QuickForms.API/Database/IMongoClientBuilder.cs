namespace QuickForms.API.Database;

public interface IMongoClientBuilder
{
    public IMongoClient Build();
}
