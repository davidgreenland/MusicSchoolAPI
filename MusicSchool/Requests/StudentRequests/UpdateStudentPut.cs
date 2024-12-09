namespace MusicSchool.Requests.StudentRequests;

public class UpdateStudentPut
{
    public required string NewFirstName { get; set; }
    public required string NewLastName { get; set; }
    public DateOnly? NewDateOfBirth { get; set; }
}
