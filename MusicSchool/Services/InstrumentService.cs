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

    public async Task<ApiResponse<IEnumerable<InstrumentResponse>>> GetAllInstrumentsAsync()
    {
        var instruments = await _context.Instrument
            .OrderBy(s => s.Name)
            .Select(x => new InstrumentResponse(x.Id, x.Name, x.Category.CategoryName))
            .ToListAsync();

        return new ApiResponse<IEnumerable<InstrumentResponse>>(HttpStatusCode.OK, instruments);
    }

    public async Task<ApiResponse<InstrumentResponse>> GetInstrumentAsync(int id)
    {
        var instrument = await _context.Instrument
            .Include(i => i.Category)
            .Include(i => i.Students)
            .SingleOrDefaultAsync(i => i.Id == id);

        return instrument == null
            ? new ApiResponse<InstrumentResponse>(HttpStatusCode.NotFound, message: null)
            : new ApiResponse<InstrumentResponse>(HttpStatusCode.OK, new InstrumentResponse(instrument.Id, instrument.Name, instrument.Category.CategoryName, instrument.Students));
    }

    public async Task<ApiResponse<Instrument>> UpdateInstrumentAsync(int id, UpdateInstrumentPut request)
    {
        var instrument = await _context.Instrument
            .SingleOrDefaultAsync(x => x.Id == id);
        if (instrument == null)
        {
            return new ApiResponse<Instrument>(HttpStatusCode.NotFound, $"Instrument ID {id} not found");

        }

        if (!await CategoryExistsAsync(request.NewCategoryId)) // foreign key
        {
            return new ApiResponse<Instrument>(HttpStatusCode.NotFound, $"Category: {request.NewCategoryId} not found");
        }

        if (await InstrumentExistsAsync(request.NewInstrumentName))
        {
            return new ApiResponse<Instrument>(HttpStatusCode.Conflict, $"Instrument with name {request.NewInstrumentName}, is already in the database");
        }

        instrument.Name = request.NewInstrumentName;
        instrument.CategoryId = request.NewCategoryId;
        await _context.SaveChangesAsync();

        return new ApiResponse<Instrument>(HttpStatusCode.OK, instrument);
    }

    public async Task<ApiResponse<Instrument>> CreateInstrumentAsync([FromBody] CreateInstrumentRequest request)
    {
        var existingInstrument = await _context.Instrument
            .Where(x => x.CategoryId == request.CategoryId)
            .SingleOrDefaultAsync(x => x.Name == request.Name);

        if (existingInstrument != null)
        {
            return new ApiResponse<Instrument>(HttpStatusCode.Conflict, $"Instrument {request.Name} already exists");
        }

        if (!await CategoryExistsAsync(request.CategoryId))
        {
            return new ApiResponse<Instrument>(HttpStatusCode.NotFound, $"Category: {request.CategoryId} not found");
        }

        var newInstrument = new Instrument
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
        };

        _context.Instrument.Add(newInstrument);
        await _context.SaveChangesAsync();

        return new ApiResponse<Instrument>(HttpStatusCode.Created, newInstrument);
    }

    public async Task<ApiResponse<Instrument>> DeleteInstrumentAsync(int id)
    {
        var instrument = await _context.Instrument
            .SingleOrDefaultAsync(x => x.Id == id);

        if (instrument == null)
        {
            return new ApiResponse<Instrument>(HttpStatusCode.NotFound, $"Instrument ID {id} not found");
        }

        var studentHasInstrument = await _context.Student
            .Include(s => s.Instruments)
            .AnyAsync(x => x.Instruments.Any(x => x.Id == id));

        if (studentHasInstrument)
        {
            return new ApiResponse<Instrument>(HttpStatusCode.Conflict, "Unable to delete instrument");
        }

        _context.Instrument.Remove(instrument);
        await _context.SaveChangesAsync();

        return new ApiResponse<Instrument>(HttpStatusCode.NoContent, data: null);
    }

    private async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _context.Category.AnyAsync(x => x.Id == categoryId);
    }

    private async Task<bool> InstrumentExistsAsync(string name)
    {
        return await _context.Instrument.AnyAsync(x => x.Name == name);
    }
}
