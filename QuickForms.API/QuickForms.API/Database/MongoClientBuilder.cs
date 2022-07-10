using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.Database
{
    public class MongoClientBuilder : IMongoClientBuilder
    {
        private readonly IOptions<DatabaseSettings> _databaseSettings;

        public MongoClientBuilder(IOptions<DatabaseSettings> databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public IMongoClient Build()
        {
            return new MongoClient(_databaseSettings.Value.ConnectionString);
        }
    }
}
