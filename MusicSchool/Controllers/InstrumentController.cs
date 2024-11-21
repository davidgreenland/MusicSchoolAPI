using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Responses;

namespace MusicSchool;

[Route("api/[controller]")]
[ApiController]
public class InstrumentController : ControllerBase
{
    private readonly MusicSchoolDBContext _context;

    public InstrumentController(MusicSchoolDBContext context)
    {
        _context = context;
    }

    // GET: api/Instrument
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InstrumentResponse>>> GetInstruments()
    {
        var instruments = await _context.Instrument
            .OrderBy(s => s.Name)
            .Select(x => new InstrumentResponse(x.Id, x.Name, x.Category.CategoryName))
            .ToListAsync();

        return Ok(instruments);
    }

    // GET: api/Instrument/5
    [HttpGet("{id}")]
    public async Task<ActionResult<InstrumentResponse>> GetInstrument(int id)
    {
        var instrument = await _context.Instrument
            .Where(x => x.Id == id)
            .Select(x => new InstrumentResponse(x.Id, x.Name, x.Category.CategoryName, x.Students))
            .ToListAsync();

        if (instrument == null)
        {
            return NotFound();
        }

        return Ok(instrument);
    }
}
