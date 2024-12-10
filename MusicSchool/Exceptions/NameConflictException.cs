using System.Net;

namespace MusicSchool.Exceptions;

public class NameConflictException : BaseException
{
    public NameConflictException(string name)
        : base($"Conflict: {name} is already in the database", HttpStatusCode.Conflict)
    {
    }
}