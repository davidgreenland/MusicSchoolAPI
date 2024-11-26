namespace MusicSchool.Requests.Instrument;

public class UpdateInstrumentPut
{
    public required string NewInstrumentName { get; set; }
    public required int NewCategoryId { get; set; }
}
