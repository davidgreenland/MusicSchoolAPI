using System.Text.Json.Serialization;

namespace MusicSchool.Models;

public class Category
{
    public required int Id { get; set; }
    public required string CategoryName { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<Instrument>? Instruments { get; }
}
