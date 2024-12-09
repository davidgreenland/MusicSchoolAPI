using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands.StudentCommands;

public record DeleteStudentByIdCommand(int Id) : IRequest<ApiResult<Student>>;
