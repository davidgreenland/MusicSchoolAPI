using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

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

    public async Task DeleteAsync(Category category)
    {
        _context.Remove(category);
        await CommitAsync();
    }

    public async Task InsertAsync(Category category)
    {
        _context.Add(category);
        await CommitAsync();
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

    public async Task<bool> CategoryHasInstrument(int id)
    {
        return await _context.Instrument
            .AnyAsync(x => x.CategoryId == id);
    }
}
