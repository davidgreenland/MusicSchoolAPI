namespace MusicSchool.Models;

public class Category
{
    public required int Id { get; set; }
    public required string CategoryName { get; set; }
    public List<Instrument> Instruments { get; } = [];
}
