namespace MusicSchool.Requests;

public class StudentPut
{
    public required string NewFirstName { get; set; }
    public required string NewLastName { get; set; }
    public DateOnly? NewDateOfBirth { get; set; }
}
