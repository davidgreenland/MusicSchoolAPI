using Microsoft.AspNetCore.Mvc;
using MusicSchool.Models;
using MusicSchool.Requests.Category;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
    Task<ApiResponse<CategoryResponse>> GetCategoryAsync(int id);
    Task<ApiResponse<Category>> UpdateCategoryAsync(int id, [FromBody] UpdateCategory request);
    Task<ApiResponse<Category>> CreateCategoryAsync([FromBody] CreateCategoryRequest request);
    Task<ApiResponse<Category>> DeleteCategoryAsync(int id);
}
