using Microsoft.AspNetCore.Mvc;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentResponse>> GetAllCategoriesAsync();
    Task<ApiResponse<StudentResponse>> GetStudentAsync(int id);
}
