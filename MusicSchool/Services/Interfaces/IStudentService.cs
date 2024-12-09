using MusicSchool.Models;

namespace MusicSchool.Services.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int id);
    Task DeleteAsync(Student student);
    Task InsertAsync(Student student);
    Task CommitAsync();
}
