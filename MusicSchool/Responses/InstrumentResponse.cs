using MusicSchool.Models;
using System.Text.Json.Serialization;

namespace MusicSchool.Responses;

public class InstrumentResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonPropertyName("category")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CategoryName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<StudentResponse>? Students { get; set; }

    public InstrumentResponse(int id, string name, string categoryName, IEnumerable<Student>? students = null) : this(id, name, categoryName)
    {
        Students = students?.Select(x => new StudentResponse(x.Id, x.FirstName, x.LastName, x.DateOfBirth));
    }

    public InstrumentResponse(int id, string name, string categoryName) : this(id, name)
    {
        CategoryName = categoryName;
    }

    public InstrumentResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
