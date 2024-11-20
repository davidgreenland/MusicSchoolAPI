using System.Text.Json.Serialization;

namespace MusicSchool.Responses;

public class CategoryResponse
{
    public int Id { get; set; }

    [JsonPropertyName("category")]
    public string CategoryName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<InstrumentResponse>? Instruments { get; set; }

    public CategoryResponse(int id, string categoryName, List<Models.Instrument>? instruments)
    {
        Id = id;
        CategoryName = categoryName;

        if (instruments != null)
        {
            Instruments = [];
            foreach (var instrument in instruments)
            {
                Instruments.Add(new InstrumentResponse(instrument.Id, instrument.Name));
            }
        }
    }
}
