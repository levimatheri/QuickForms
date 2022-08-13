using QuickForms.API.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(
        this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(provider =>
        {
            var databaseSettings = provider.GetRequiredService<IOptions<DatabaseSettings>>();
            return new MongoClient(databaseSettings.Value.ConnectionString);
        });

        return services;
    }

    public static IServiceCollection AddMongoDb(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

        return services;
    }
}
