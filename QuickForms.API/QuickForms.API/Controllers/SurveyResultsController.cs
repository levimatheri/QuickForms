using Microsoft.AspNetCore.Mvc;
using QuickForms.API.Models;
using QuickForms.API.RequestHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickForms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SurveyResultsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SurveyResultsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{id:length(24)}")]
    public async Task<IActionResult> SaveSurveyResults(string id, SurveyResultsDto surveyResults)
    {
        var survey = await _mediator.Send(new GetSurveyRequest(id));

        if (survey is null)
        {
            return NotFound();
        }

        surveyResults.SurveyId = id;

        await _mediator.Send(new SaveSurveyResultsRequest(id, surveyResults));

        return NoContent();
    }
}
