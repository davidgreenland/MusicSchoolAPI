using System.Net;

namespace MusicSchool.Exceptions;

public class InstrumentNameConflictException : BaseException
{
    public InstrumentNameConflictException(string name)
        : base($"Instrument with name: {name}, is already in the database", HttpStatusCode.Conflict)
    {
    }
}