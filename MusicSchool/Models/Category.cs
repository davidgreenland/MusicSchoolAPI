using Microsoft.Build.Framework;
using System.Text.Json.Serialization;

namespace MusicSchool.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    public required string CategoryName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<Instrument> Instruments { get; } = null!;
}
