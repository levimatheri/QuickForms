using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.Database;
public interface IMongoClientBuilder
{
    IMongoClient Build(string connectionString);
}
