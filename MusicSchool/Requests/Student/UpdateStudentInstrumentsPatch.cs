namespace MusicSchool.Requests.Student;

public class UpdateStudentInstrumentsPatch
{
    public IEnumerable<int> NewInstrumentIds { get; set; } = default!;
}
