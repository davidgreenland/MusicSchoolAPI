using MediatR;
using MusicSchool.Responses;

namespace MusicSchool.Queries;

public record GetAllStudentsQuery() : IRequest<IEnumerable<StudentResponse>>;