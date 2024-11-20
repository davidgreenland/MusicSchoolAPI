namespace MusicSchool.Response;

public class StudentResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public StudentResponse(int id, string name, DateOnly? dateOfBirth)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
    }
}
