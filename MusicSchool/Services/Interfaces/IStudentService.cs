using MusicSchool.Models;
using MusicSchool.Requests.Student;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface IStudentService
{
    Task<ApiResult<IEnumerable<StudentResponse>>> GetAllCategoriesAsync();
    Task<ApiResult<StudentResponse>> GetStudentAsync(int id);
    Task<ApiResult<Student>> UpdateInstrumentAsync(int id, UpdateStudentPut request);
    Task<ApiResult<StudentResponse>> UpdateStudentInstrumentsAsync(int id, UpdateStudentInstrumentsPatch request);
    Task<ApiResult<Student>> CreateStudentAsync(CreateStudentRequest request);
    Task<ApiResult<Student>> DeleteStudentAsync(int id);
}
