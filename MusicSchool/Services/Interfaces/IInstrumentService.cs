using MusicSchool.Models;
using MusicSchool.Requests.Instrument;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface IInstrumentService
{
    Task<IEnumerable<Instrument>> GetAllInstrumentsAsync();
    Task<Instrument?> GetInstrumentByIdAsync(int id);
    Task<bool> CategoryExistsAsync(int id);
    Task<bool> InstrumentExistsAsync(string name);
    Task InsertAsync(Instrument instrument);
    Task DeleteAsync(Instrument instrument);
    Task CommitAsync();
}
