using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands.StudentCommands;

public record UpdateStudentCommand(int Id, string NewFirstName, string NewLastName, DateOnly? NewDateOfBirth) : IRequest<ApiResult<Student>>;