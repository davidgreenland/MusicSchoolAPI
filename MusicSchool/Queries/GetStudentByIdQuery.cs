using MediatR;
using MusicSchool.Responses;

namespace MusicSchool.Queries;

public record GetStudentByIdQuery(int Id) : IRequest<StudentResponse>;
