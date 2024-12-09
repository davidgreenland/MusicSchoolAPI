using System.Net;

namespace MusicSchool.Exceptions;

public class EntityNameConflictException : BaseException
{
    public EntityNameConflictException(string name)
        : base($"Conflict: {name} is already in the database", HttpStatusCode.Conflict)
    {
    }
}