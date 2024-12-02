namespace MusicSchool.Requests.Instrument;

public class CreateInstrumentRequest
{
    public string Name { get; set; } = null!;
    public int CategoryId { get; set; }
}
