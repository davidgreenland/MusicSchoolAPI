using System.Net;

namespace MusicSchool.Exceptions;

public class DeleteEntityConflict : BaseException
{
    public DeleteEntityConflict()
        : base("Unable to delete", HttpStatusCode.Conflict)
    {
    }
}