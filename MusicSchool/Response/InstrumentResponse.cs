namespace MusicSchool.Response;

public class InstrumentResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }

    public InstrumentResponse(int id, string name, string categoryName)
    {
        Id = id;
        Name = name;
        Category = categoryName;
    }
}
