using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Requests.Category;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Diagnostics.Metrics;
using System.Net;

namespace MusicSchool.Services;

public class CategoryService : ICategoryService
{
    private readonly MusicSchoolDBContext _context;

    public CategoryService(MusicSchoolDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Category
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Category
                .Include(c => c.Instruments)
                .SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task DeleteAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<Category>> DeleteCategoryAsync(int id)
    {
        var category = await _context.Category
            .SingleOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return new ApiResponse<Category>(HttpStatusCode.NotFound, $"Instrument {id} not found");
        }

        var categoryHasInstrument = await _context.Instrument
            .AnyAsync(x => x.CategoryId == id);

        if (categoryHasInstrument)
        {
            return new ApiResponse<Category>(HttpStatusCode.Conflict, "Unable to delete category");
        }

        _context.Category.Remove(category);
        await _context.SaveChangesAsync();

        return new ApiResponse<Category>(HttpStatusCode.NoContent, data: null);
    }

    public async Task InsertAsync(Category category)
    {
        await _context.AddAsync(category);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckCategoryExistsAsync(string name)
    {
        return await _context.Category
            .AnyAsync(c => c.Name == name);
    }

}
