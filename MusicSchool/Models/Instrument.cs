namespace MusicSchool.Models;

public class Instrument
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int CategoryId { get; set; }
    public List<Student> Students { get; } = [];

}
