using System.Net;

namespace MusicSchool.Exceptions;

public class InstrumentNotFoundException : BaseException
{
    public InstrumentNotFoundException(int id)
        : base($"Instrument with id {id} not found", HttpStatusCode.NotFound)
    {
    }
}