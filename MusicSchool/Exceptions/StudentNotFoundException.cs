using System.Net;

namespace MusicSchool.Exceptions;

public class StudentNotFoundException : BaseException
{
    public StudentNotFoundException(int id)
        : base($"Student with id {id} not found", HttpStatusCode.NotFound)
    {
    }
}