using QuickForms.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.Client.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SurveyRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<List<Survey>> GetSurveys()
        {
            var surveys = new List<Survey>
            {
                new Survey {Id = "563ab", Name="Test Survey 1"},
                new Survey {Id = "563ac", Name="Test Survey 2"},
                new Survey {Id = "563ad", Name="Test Survey 3"},
            };

            return Task.FromResult(surveys);
        }
    }
}
