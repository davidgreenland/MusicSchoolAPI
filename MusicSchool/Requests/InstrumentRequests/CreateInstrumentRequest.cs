namespace MusicSchool.Requests.InstrumentRequests;

public class CreateInstrumentRequest
{
    public string Name { get; set; } = null!;
    public int CategoryId { get; set; }
}
