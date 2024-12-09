using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Commands.StudentCommands;

public record UpdateStudentInstrumentsCommand(int Id, IEnumerable<int> NewInstrumentIds) : IRequest<StudentResponse>;
