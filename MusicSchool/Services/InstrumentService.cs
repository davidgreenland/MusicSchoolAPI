using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Requests.Instrument;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

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

    public async Task<ApiResult<Instrument>> CreateInstrumentAsync([FromBody] CreateInstrumentRequest request)
    {
        var existingInstrument = await _context.Instrument
            .Where(x => x.CategoryId == request.CategoryId)
            .SingleOrDefaultAsync(x => x.Name == request.Name);

        if (existingInstrument != null)
        {
            return new ApiResult<Instrument>(HttpStatusCode.Conflict, $"Instrument {request.Name} already exists");
        }

        if (!await CategoryExistsAsync(request.CategoryId))
        {
            return new ApiResult<Instrument>(HttpStatusCode.NotFound, $"Category: {request.CategoryId} not found");
        }

        var newInstrument = new Instrument
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
        };

        _context.Instrument.Add(newInstrument);
        await _context.SaveChangesAsync();

        return new ApiResult<Instrument>(HttpStatusCode.Created, newInstrument);
    }

    public async Task<ApiResult<Instrument>> DeleteInstrumentAsync(int id)
    {
        var instrument = await _context.Instrument
            .SingleOrDefaultAsync(x => x.Id == id);

        if (instrument == null)
        {
            return new ApiResult<Instrument>(HttpStatusCode.NotFound, $"Instrument ID {id} not found");
        }

        var studentHasInstrument = await _context.Student
            .Include(s => s.Instruments)
            .AnyAsync(x => x.Instruments!.Any(x => x.Id == id));

        if (studentHasInstrument)
        {
            return new ApiResult<Instrument>(HttpStatusCode.Conflict, "Unable to delete instrument");
        }

        _context.Instrument.Remove(instrument);
        await _context.SaveChangesAsync();

        return new ApiResult<Instrument>(HttpStatusCode.NoContent, data: null);
    }

    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _context.Category.AnyAsync(x => x.Id == categoryId);
    }

    public async Task<bool> InstrumentExistsAsync(string name)
    {
        return await _context.Instrument.AnyAsync(x => x.Name == name);
    }
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
