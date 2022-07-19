using QuickForms.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.Client.Repositories
{
    public interface ISurveyRepository
    {
        Task<List<Survey>> GetSurveys();
    }
}
