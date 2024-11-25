using MusicSchool.Models;
using System.Text.Json.Serialization;

namespace MusicSchool.Responses;

public class CategoryResponse
{
    public int Id { get; set; }

    [JsonPropertyName("category")]
    public string CategoryName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<InstrumentResponse>? Instruments { get; set; }

    public CategoryResponse(int id, string categoryName, IEnumerable<Instrument> instruments) : this(id, categoryName)
    {
        Instruments = instruments.Select(x => new InstrumentResponse(x.Id, x.Name));
    }

    public CategoryResponse(int id, string categoryName)
    {
        Id = id;
        CategoryName = categoryName;
    }
}
