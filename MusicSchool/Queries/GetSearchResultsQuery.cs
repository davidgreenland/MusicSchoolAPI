using MediatR;
using MusicSchool.Models;
using MusicSchool.Responses;

namespace MusicSchool.Queries;

public record GetSearchResultsQuery(string Query) : IRequest<IEnumerable<Student>>;
