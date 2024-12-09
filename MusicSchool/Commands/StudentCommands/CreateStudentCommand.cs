using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands.StudentCommands;

public record CreateStudentCommand(string FirstName, string LastName, DateOnly? DateOfBirth) : IRequest<ApiResult<Student>>;