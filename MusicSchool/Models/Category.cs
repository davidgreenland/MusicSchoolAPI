using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MusicSchool.Models;

public class Category
{
    public int Id { get; set; }

    [MaxLength(255)]    
    public required string Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<Instrument>? Instruments { get; set; }
}
