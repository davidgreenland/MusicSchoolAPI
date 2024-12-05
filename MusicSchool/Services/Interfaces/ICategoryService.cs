using MusicSchool.Models;
using MusicSchool.Requests.Category;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task DeleteAsync(Category category);
    Task InsertAsync(Category category);
    Task CommitAsync();
    Task<bool> CheckCategoryExistsAsync(string name);
}
