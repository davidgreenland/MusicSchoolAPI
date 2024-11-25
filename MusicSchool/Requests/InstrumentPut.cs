using System.ComponentModel.DataAnnotations;

namespace MusicSchool.Requests;

public class InstrumentPut
{
    public required string NewInstrumentName { get; set; }
    public required int NewCategoryId { get; set; }
}
