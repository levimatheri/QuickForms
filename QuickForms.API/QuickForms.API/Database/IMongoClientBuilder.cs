using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.Database
{
    public interface IMongoClientBuilder
    {
        public IMongoClient Build();
    }
}
