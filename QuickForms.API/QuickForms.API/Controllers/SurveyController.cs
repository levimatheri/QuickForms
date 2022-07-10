using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickForms.API.Models;
using QuickForms.API.RequestHandlers;

namespace QuickForms.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SurveyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Survey>> GetSurveysAsync()
        {
            return await _mediator.Send(new GetAllSurveysRequest());
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Survey>> GetSurveyAsync(string id)
        {
            var survey = await _mediator.Send(new GetSurveyRequest(id));

            if (survey is null)
            {
                return NotFound();
            }

            return survey;
        }
    }
}
