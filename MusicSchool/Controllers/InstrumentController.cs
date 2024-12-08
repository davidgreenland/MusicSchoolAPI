using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSchool.Commands.InstrumentCommands;
using MusicSchool.Models;
using MusicSchool.Queries;
using MusicSchool.Requests.Instrument;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstrumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public InstrumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/Instrument
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InstrumentResponse>>> GetInstruments()
    {
        var instruments = await _mediator.Send(new GetAllInstrumentsQuery());

        return Ok(instruments);
    }

    // GET: api/Instrument/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<InstrumentResponse>> GetInstrumentById(int id)
    {
        var instrument = await _mediator.Send(new GetInstrumentByIdQuery(id));

        return instrument == null
            ? NotFound("Id not found")
            : Ok(instrument);
    }

    // PUT: api/Instrument/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Instrument>> UpdateInstrument(int id, [FromBody] UpdateInstrumentPut request)
    {
        var response = await _mediator.Send(new UpdateInstrumentCommand(id, request.NewName, request.NewCategoryId));

        return HandleApiResponse(response);
    }

    // POST: api/Instrument
    [HttpPost]
    public async Task<ActionResult<Instrument>> CreateInstrument([FromBody] CreateInstrumentRequest request)
    {
        var response = await _mediator.Send(new CreateInstrumentCommand(request.Name, request.CategoryId));

        return HandleApiResponse(response);
    }

    //// DELETE: api/Instrument/5
    //[HttpDelete("{id:int}")]
    //public async Task<ActionResult> DeleteInstrument(int id)
    //{
    //    var response = await _instrumentService.DeleteInstrumentAsync(id);

    //    return HandleApiResponse(response);
    //}

    private ObjectResult HandleApiResponse<T>(ApiResult<T> response) where T : class
    {
        return response.IsSuccess
            ? StatusCode(response.StatusCode, response.Data)
            : StatusCode(response.StatusCode, response.Message);
    }
}
