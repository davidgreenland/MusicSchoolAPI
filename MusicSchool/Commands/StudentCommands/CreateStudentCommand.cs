using MediatR;
using MusicSchool.Models;

namespace MusicSchool.Commands.StudentCommands;

public record CreateStudentCommand(string FirstName, string LastName, DateOnly? DateOfBirth) : IRequest<Student>;