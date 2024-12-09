using System.Net;

namespace MusicSchool.Exceptions;

public class CategoryNotFoundException : BaseException
{
    public CategoryNotFoundException(int id)
        : base($"Category with id {id} not found", HttpStatusCode.NotFound)
    {
    }
}