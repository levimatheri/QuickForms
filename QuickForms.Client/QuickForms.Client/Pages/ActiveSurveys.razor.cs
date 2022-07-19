using Microsoft.AspNetCore.Components;
using QuickForms.Client.Models;
using QuickForms.Client.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.Client.Pages
{
    public partial class ActiveSurveys
    {
        [Inject]
        public ISurveyRepository SurveyRepository { get; set; }
        public List<Survey> Surveys { get; set; } = new List<Survey>();

        protected override async Task OnInitializedAsync()
        {
            Surveys = await SurveyRepository.GetSurveys();
        }
    }
}
