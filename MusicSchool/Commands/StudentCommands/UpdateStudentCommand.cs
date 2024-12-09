using MediatR;
using MusicSchool.Models;

namespace MusicSchool.Commands.StudentCommands;

public record UpdateStudentCommand(int Id, string NewFirstName, string NewLastName, DateOnly? NewDateOfBirth) : IRequest<Student>;