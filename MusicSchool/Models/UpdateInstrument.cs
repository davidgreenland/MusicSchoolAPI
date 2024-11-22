namespace MusicSchool.Models;

public class UpdateInstrument
{
    public required string NewInstrumentName { get; set; }
    public required int NewCategoryId { get; set; }
}
