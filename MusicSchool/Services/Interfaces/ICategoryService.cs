using MusicSchool.Models;

namespace MusicSchool.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task InsertAsync(Category category);
    Task DeleteAsync(Category category);
    Task CommitAsync();
    Task<bool> CheckCategoryExistsAsync(string name);
    Task<bool> CategoryHasInstrument(int id);
}
