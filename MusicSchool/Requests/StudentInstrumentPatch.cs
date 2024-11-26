namespace MusicSchool.Requests;

public class StudentInstrumentPatch
{
    public IEnumerable<int> NewInstrumentIds { get; set; } = default!;
}
