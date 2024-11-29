using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Responses;
using MusicSchool.Services.Interfaces;
using System.Net;

namespace MusicSchool.Services;

public class StudentService : IStudentService
{
    private readonly MusicSchoolDBContext _context;

    public StudentService(MusicSchoolDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentResponse>> GetAllCategoriesAsync()
    {
        return await _context.Student
            .OrderBy(s => s.LastName)
            .Select(x => new StudentResponse(x.Id, $"{x.FirstName} {x.LastName}", x.DateOfBirth))
            .ToListAsync();
    }

    public async Task<ApiResponse<StudentResponse>> GetStudentAsync(int id)
    {
        var student = await _context.Student
            .Include(x => x.Instruments)
            .SingleOrDefaultAsync(x => x.Id == id);
 
        if (student == null)
        {
            return new ApiResponse<StudentResponse>(HttpStatusCode.NotFound, message: null);
        }

        var instruments = student.Instruments.Count == 0
            ? "no instruments added"
            : string.Join(", ", student.Instruments.Select(x => x.Name));

        return new ApiResponse<StudentResponse>(HttpStatusCode.OK, (new StudentResponse(student.Id, $"{student.FirstName} {student.LastName}", student.DateOfBirth, instruments));
    }
}
