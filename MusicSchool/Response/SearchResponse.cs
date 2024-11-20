namespace MusicSchool.Resources;

public class SearchResponse
{
    public string FullName { get; set; }
    public string Instrument {  get; set; }

    public SearchResponse(string fullName, string instrument)
    {
        FullName = fullName;
        Instrument = instrument;
    }
}
