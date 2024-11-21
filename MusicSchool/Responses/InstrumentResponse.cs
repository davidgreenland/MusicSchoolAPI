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
    public List<StudentResponse>? Students { get; set; }

    public InstrumentResponse(int id, string name, string categoryName, List<Student> students) : this(id, name, categoryName)
    {
        Students = [];
        foreach (var student in students)
        {
            Students.Add(new StudentResponse(student.Id, $"{student.FirstName} {student.LastName}", student.DateOfBirth));
        }
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
