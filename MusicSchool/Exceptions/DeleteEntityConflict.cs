using System.Net;

namespace MusicSchool.Exceptions;

public class DeleteEntityConflict : BaseException
{
    public DeleteEntityConflict(string message)
        : base(message, HttpStatusCode.Conflict)
    {
    }
}