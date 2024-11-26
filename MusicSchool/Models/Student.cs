using System.Text.Json.Serialization;

namespace MusicSchool.Models;

public class Student
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Instrument> Instruments { get; set; } = null!;
}
