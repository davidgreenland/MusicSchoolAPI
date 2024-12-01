using Microsoft.AspNetCore.Mvc;
using MusicSchool.Models;
using MusicSchool.Requests.Student;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentResponse>> GetAllCategoriesAsync();
    Task<ApiResponse<StudentResponse>> GetStudentAsync(int id);
    Task<ApiResponse<Student>> UpdateInstrumentAsync(int id, UpdateStudentPut request);
    Task<ApiResponse<StudentResponse>> UpdateStudentInstrumentsAsync(int id, UpdateStudentInstrumentsPatch request);
    Task<ApiResponse<Student>> CreateStudentAsync(CreateStudentRequest request);
}
