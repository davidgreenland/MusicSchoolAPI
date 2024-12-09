namespace MusicSchool.Requests.StudentRequests;

public class UpdateStudentInstrumentsPatch
{
    public IEnumerable<int> NewInstrumentIds { get; set; } = default!;
}
