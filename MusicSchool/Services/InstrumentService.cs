using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Services;

public class InstrumentService : IInstrumentService
{
    private readonly MusicSchoolDBContext _context;

    public InstrumentService(MusicSchoolDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Instrument>> GetAllInstrumentsAsync()
    {
        return await _context.Instrument
            .ToListAsync();
    }

    public async Task<Instrument?> GetInstrumentByIdAsync(int id)
    {
        return await _context.Instrument
            .Include(i => i.Category)
            .Include(i => i.Students)
            .SingleOrDefaultAsync(i => i.Id == id);
    }

    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _context.Category.AnyAsync(x => x.Id == categoryId);
    }

    public async Task<bool> InstrumentExistsAsync(string name)
    {
        return await _context.Instrument.AnyAsync(x => x.Name == name);
    }

    public async Task<bool> InstrumentHasStudentsAsync(int id)
    {
        return await _context.Student
            .Include(s => s.Instruments)
            .AnyAsync(x => x.Instruments!.Any(x => x.Id == id));
    }

    public async Task DeleteAsync(Instrument instrument)
    {
        _context.Remove(instrument);
        await CommitAsync();
    }

    public async Task InsertAsync(Instrument instrument)
    {
        _context.Add(instrument);
        await CommitAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
