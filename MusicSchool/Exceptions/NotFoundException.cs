using System.Net;

namespace MusicSchool.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message)
        : base(message, HttpStatusCode.NotFound)
    {
    }
}