using MusicSchool.Models;
using MusicSchool.Requests.Category;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface ICategoryService
{
    Task<ApiResponse<IEnumerable<CategoryResponse>>> GetAllCategoriesAsync();
    Task<ApiResponse<CategoryResponse>> GetCategoryAsync(int id);
    Task<ApiResponse<Category>> UpdateCategoryAsync(int id, UpdateCategory request);
    Task<ApiResponse<Category>> CreateCategoryAsync(CreateCategoryRequest request);
    Task<ApiResponse<Category>> DeleteCategoryAsync(int id);
}
