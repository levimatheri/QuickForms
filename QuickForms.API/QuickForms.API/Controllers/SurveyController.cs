using Microsoft.AspNetCore.Mvc;
using QuickForms.API.Models;
using QuickForms.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly SurveyService _surveyService;

        public SurveyController(SurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet]
        public async Task<List<Survey>> GetSurveysAsync() => await _surveyService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Survey>> GetSurveyAsync(string id)
        {
            var survey = await _surveyService.GetAsync(id);

            if (survey is null)
            {
                return NotFound();
            }

            return survey;
        }
    }
}
