using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MusicSchool.Models;

public class Student
{
    public int Id { get; set; }

    [MaxLength(255)]
    public required string FirstName { get; set; }

    [MaxLength(255)]
    public required string LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Instrument>? Instruments { get; set; }
}
