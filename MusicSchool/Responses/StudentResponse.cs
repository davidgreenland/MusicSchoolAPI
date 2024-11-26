using System.Text.Json.Serialization;

namespace MusicSchool.Responses;

public class StudentResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Instrument { get; set; }

    public StudentResponse(int id, string name, DateOnly? dateOfBirth, string instrument) : this(id, name, dateOfBirth)
    {
        Instrument = instrument;
    }

    public StudentResponse(int id, string name, DateOnly? dateOfBirth)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
    }
}
