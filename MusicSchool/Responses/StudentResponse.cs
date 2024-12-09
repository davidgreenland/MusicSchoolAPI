using System.Text.Json.Serialization;

namespace MusicSchool.Responses;

public class StudentResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Instrument { get; set; }

    public StudentResponse(int id, string firstName, string lastName, DateOnly? dateOfBirth, string instrument) : this(id, firstName, lastName, dateOfBirth)
    {
        Instrument = instrument;
    }

    public StudentResponse(int id, string firstName, string lastName, DateOnly? dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }
}
