using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        var instruments = await _instrumentService.GetInstrumentsAsync();
        return Ok(instruments);
    }

    // GET: api/Instrument/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<InstrumentResponse>> GetInstrument(int id)
    {
        var instrument = await _instrumentService.GetInstrumentAsync(id);

        if (instrument == null)
        {
            return NotFound();
        }

        return Ok(instrument);
    }

    // PUT: api/Instrument/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Instrument>> UpdateInstrument(int id, [FromBody] UpdateInstrumentPut request)
    {
        var response = await _instrumentService.UpdateInstrumentAsync(id, request);

        if (response.StatusCode != 200)
        {
            return StatusCode(response.StatusCode, response.Message);
        }

        return Ok(response.Data);
    }

    // POST: api/Instrument
    [HttpPost]
    public async Task<ActionResult<Instrument>> CreateInstrument([FromBody] CreateInstrumentRequest request)
    {
        var response = await _instrumentService.CreateInstrumentAsync(request);

        if (response.StatusCode != 201)
        {
            return StatusCode(response.StatusCode, response.Message);
        }

        return CreatedAtAction(nameof(GetInstrument), new { id = response.Data?.Id }, response.Data);
    }

    // DELETE: api/Instrument/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteInstrument(int id)
    {
        var response = await _instrumentService.DeleteInstrumentAsync(id);

        if (response.StatusCode != 204)
        {
            return StatusCode(response.StatusCode, response.Message);
        }

        return NoContent();
    }
}
