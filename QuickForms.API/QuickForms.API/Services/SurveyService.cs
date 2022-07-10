using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuickForms.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.Services
{
    public class SurveyService
    {
        private readonly IMongoCollection<Survey> _surveys;

        public SurveyService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _surveys = mongoDatabase.GetCollection<Survey>(databaseSettings.Value.SurveyCollectionName);
        }

        public async Task<List<Survey>> GetAsync()
        {
            return await _surveys.Find(_ => true).ToListAsync();
        }

        public async Task<Survey?> GetAsync(string id)
        {
            return await _surveys.Find(s => s.Id == id).FirstOrDefaultAsync();
        }
    }
}
