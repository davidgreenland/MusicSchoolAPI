using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;

namespace MusicSchool
{
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
        public async Task<ActionResult<IEnumerable<Instrument>>> GetInstruments()
        {
            return await _context.Instrument.ToListAsync();
        }

        // GET: api/Instrument/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instrument>> GetInstrument(int id)
        {
            var instrument = await _context.Instrument.Include(x => x.Students).SingleOrDefaultAsync(x => x.Id == id);

            if (instrument == null)
            {
                return NotFound();
            }

            return instrument;
        }
    }
}
