using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickForms.API.Models;
using QuickForms.API.RequestHandlers;

namespace QuickForms.API.Controllers;

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

    [HttpPost]
    public async Task<IActionResult> CreateNewSurvey(NewSurveyDto survey)
    {
        var createdSurvey = await _mediator.Send(new CreateSurveyRequest(survey));
        return CreatedAtAction(nameof(CreateNewSurvey), new { id = createdSurvey.Id }, createdSurvey);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, UpdateSurveyDto updatedSurvey)
    {
        var survey = await _mediator.Send(new GetSurveyRequest(id));

        if (survey is null)
        {
            return NotFound();
        }

        updatedSurvey.Id = survey.Id!;

        await _mediator.Send(new UpdateSurveyRequest(id, updatedSurvey));

        return NoContent();
    }
}
