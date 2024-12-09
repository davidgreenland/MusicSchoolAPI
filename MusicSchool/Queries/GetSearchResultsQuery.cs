using MediatR;
using MusicSchool.Responses;

namespace MusicSchool.Queries;

public record GetSearchResultsQuery(string Query) : IRequest<ApiResult<IEnumerable<SearchResponse>>>;
