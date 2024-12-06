using Microsoft.AspNetCore.Mvc;
using MusicSchool.Models;
using MusicSchool.Requests.Instrument;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstrumentController : ControllerBase
{
    private readonly IInstrumentService _instrumentService;

    public InstrumentController(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    // GET: api/Instrument
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InstrumentResponse>>> GetInstruments()
    {
        var instruments = await _instrumentService.GetAllInstrumentsAsync();

        return HandleApiResponse(instruments);
    }

    // GET: api/Instrument/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<InstrumentResponse>> GetInstrument(int id)
    {
        var response = await _instrumentService.GetInstrumentAsync(id);

        return HandleApiResponse(response);
    }

    // PUT: api/Instrument/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Instrument>> UpdateInstrument(int id, [FromBody] UpdateInstrumentPut request)
    {
        var response = await _instrumentService.UpdateInstrumentAsync(id, request);

        return HandleApiResponse(response);
    }

    // POST: api/Instrument
    [HttpPost]
    public async Task<ActionResult<Instrument>> CreateInstrument([FromBody] CreateInstrumentRequest request)
    {
        var response = await _instrumentService.CreateInstrumentAsync(request);

        return HandleApiResponse(response);
    }

    // DELETE: api/Instrument/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteInstrument(int id)
    {
        var response = await _instrumentService.DeleteInstrumentAsync(id);

        return HandleApiResponse(response);
    }

    private ObjectResult HandleApiResponse<T>(ApiResult<T> response) where T : class
    {
        return response.IsSuccess
            ? StatusCode(response.StatusCode, response.Data)
            : StatusCode(response.StatusCode, response.Message);
    }
}
