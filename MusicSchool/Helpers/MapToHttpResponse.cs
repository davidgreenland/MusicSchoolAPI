using MusicSchool.Responses.Enums;
using System.Net;

namespace MusicSchool.Helpers;

public static class MapToHttpResponse
{
    public static HttpStatusCode Generate(ResponseStatus status)
    {
        return status switch
        {
            ResponseStatus.Success => HttpStatusCode.OK,
            ResponseStatus.Conflict => HttpStatusCode.Conflict,
            ResponseStatus.NotFound => HttpStatusCode.NotFound,
            ResponseStatus.Error => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }
}
