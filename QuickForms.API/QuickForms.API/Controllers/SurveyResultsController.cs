using Microsoft.AspNetCore.Mvc;
using QuickForms.API.Models;
using QuickForms.API.RequestHandlers.SurveyRequestHandlers;
using QuickForms.API.RequestHandlers.SurveyResultsRequestHandlers;
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

    [HttpGet("{surveyId:length(24)}")]
    public async Task<ActionResult<List<SurveyResultsDto>>> GetSurveyResults(string surveyId)
    {
        var survey = await _mediator.Send(new GetSurveyRequest(surveyId));

        if (survey is null)
        {
            return NotFound($"Survey id: {surveyId} not found");
        }

        return await _mediator.Send(new GetSurveyResultsRequest(surveyId));
    }

    [HttpPost("{surveyId:length(24)}")]
    public async Task<IActionResult> SaveSurveyResults(string surveyId, SurveyResultsDto surveyResults)
    {
        var survey = await _mediator.Send(new GetSurveyRequest(surveyId));

        if (survey is null)
        {
            return NotFound($"Survey id: {surveyId} not found");
        }

        surveyResults.SurveyId = surveyId;

        await _mediator.Send(new SaveSurveyResultsRequest(surveyId, surveyResults));

        return NoContent();
    }
}
