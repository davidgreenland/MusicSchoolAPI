using MusicSchool.Models;
using MusicSchool.Requests.Category;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<ApiResponse<Category>> CreateCategoryAsync(CreateCategoryRequest request);
    Task<ApiResponse<Category>> DeleteCategoryAsync(int id);
    Task CommitAsync();
    Task<bool> CheckCategoryExistsAsync(string name);
}
