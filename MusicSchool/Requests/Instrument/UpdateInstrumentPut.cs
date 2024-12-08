namespace MusicSchool.Requests.Instrument;

public class UpdateInstrumentPut
{
    public required string NewName { get; set; }
    public required int NewCategoryId { get; set; }
}
