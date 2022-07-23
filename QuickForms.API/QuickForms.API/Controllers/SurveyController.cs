using Microsoft.AspNetCore.Mvc;
using QuickForms.API.Models;
using QuickForms.API.RequestHandlers;

namespace QuickForms.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SurveysController : ControllerBase
{
    private readonly IMediator _mediator;

    public SurveysController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<SurveyDto>> GetSurveysAsync()
    {
        return await _mediator.Send(new GetAllSurveysRequest());
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<SurveyDto>> GetSurveyAsync(string id)
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
    public async Task<IActionResult> UpdateSurvey(string id, UpdateSurveyDto updatedSurvey)
    {
        var survey = await _mediator.Send(new GetSurveyRequest(id));

        if (survey is null)
        {
            return NotFound();
        }

        updatedSurvey.Id = survey.Id!;
        updatedSurvey.Name = survey.Name!;

        await _mediator.Send(new UpdateSurveyRequest(id, updatedSurvey));

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteSurvey(string id)
    {
        var survey = await _mediator.Send(new GetSurveyRequest(id));

        if (survey is null)
        {
            return NotFound();
        }

        await _mediator.Send(new DeleteSurveyRequest(id));

        return NoContent();
    }
}
