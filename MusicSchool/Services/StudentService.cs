using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;
using MusicSchool.Requests.Student;
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

    public async Task<ApiResponse<IEnumerable<StudentResponse>>> GetAllCategoriesAsync()
    {
        var students = await _context.Student
            .OrderBy(s => s.LastName)
            .Select(x => new StudentResponse(x.Id, $"{x.FirstName} {x.LastName}", x.DateOfBirth))
            .ToListAsync();

        return new ApiResponse<IEnumerable<StudentResponse>>(HttpStatusCode.OK, students);
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

        var instruments = student.Instruments!.Count == 0
            ? "no instruments added"
            : string.Join(", ", student.Instruments.Select(x => x.Name));

        return new ApiResponse<StudentResponse>(HttpStatusCode.OK, new StudentResponse(student.Id, $"{student.FirstName} {student.LastName}", student.DateOfBirth, instruments));
    }

    public async Task<ApiResponse<Student>> UpdateInstrumentAsync(int id, [FromBody] UpdateStudentPut request)
    {
        var student = await _context.Student
            .SingleOrDefaultAsync(x => x.Id == id);

       if (student == null)
       {
            return new ApiResponse<Student>(HttpStatusCode.NotFound, "Id not found");

       }

       student.FirstName = request.NewFirstName;
       student.LastName = request.NewLastName;
       student.DateOfBirth = request.NewDateOfBirth;
       await _context.SaveChangesAsync();

       return new ApiResponse<Student>(HttpStatusCode.OK, student);
    }

    public async Task<ApiResponse<StudentResponse>> UpdateStudentInstrumentsAsync(int id, [FromBody] UpdateStudentInstrumentsPatch request)
    {
        var student = await _context.Student
                .Include(x => x.Instruments)
                .SingleOrDefaultAsync(x => x.Id == id);

       if (student == null)
       {
            return new ApiResponse<StudentResponse>(HttpStatusCode.NotFound, "student not found");
       }

       var validReqeustedInstruments = await _context.Instrument
           .Where(i => request.NewInstrumentIds.Contains(i.Id))
           .ToListAsync();

       if (validReqeustedInstruments.Count != request.NewInstrumentIds.Count())
       {
            var invalidInstrumentIds = request.NewInstrumentIds.Except(validReqeustedInstruments.Select(x => x.Id));
            return new  ApiResponse<StudentResponse>(HttpStatusCode.NotFound, $"Invalid instrument IDs: {string.Join(", ", invalidInstrumentIds)}");
       }

       student.Instruments = validReqeustedInstruments;
       await _context.SaveChangesAsync();

       return new ApiResponse<StudentResponse>(HttpStatusCode.OK, new StudentResponse(student.Id, $"{student.FirstName} {student.LastName}", student.DateOfBirth, string.Join(", ", student.Instruments.Select(x => x.Name))));
    }

    public async Task<ApiResponse<Student>> CreateStudentAsync([FromBody] CreateStudentRequest request)
    {
        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth
        };

        _context.Student.Add(student);
        await _context.SaveChangesAsync();

        return new ApiResponse<Student>(HttpStatusCode.Created, student);
    }

    public async Task<ApiResponse<Student>> DeleteStudentAsync(int id)
    {
        var student = await _context.Student
            .Include(s => s.Instruments)
            .SingleOrDefaultAsync(s => s.Id == id);

        if (student == null)
        {
            return new ApiResponse<Student>(HttpStatusCode.NotFound, $"Student: {id} not found");
        }

        _context.Student.Remove(student);
        await _context.SaveChangesAsync();

        return new ApiResponse<Student>(HttpStatusCode.NoContent, message: null);
    }
}
