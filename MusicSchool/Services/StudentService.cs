using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Services.Interfaces;

namespace MusicSchool.Services;

public class StudentService : IStudentService
{
    private readonly MusicSchoolDBContext _context;

    public StudentService(MusicSchoolDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _context.Student
            .OrderBy(s => s.LastName)
            .ToListAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _context.Student
            .Include(x => x.Instruments)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteAsync(Student student)
    {
        _context.Remove(student);
        await CommitAsync();
    }

    public async Task InsertAsync(Student student)
    {
        _context.Add(student);
        await CommitAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
