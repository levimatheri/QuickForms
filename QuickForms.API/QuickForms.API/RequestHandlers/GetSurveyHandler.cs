using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuickForms.API.Database;
using QuickForms.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.RequestHandlers
{
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
                .Find(s => s.Id == request.Id).FirstOrDefaultAsync();
        }
    }

    public record GetSurveyRequest(string Id) : IRequest<Survey>;
}
