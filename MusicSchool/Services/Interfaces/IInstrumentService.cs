using MusicSchool.Models;

namespace MusicSchool.Services.Interfaces;

public interface IInstrumentService
{
    Task<IEnumerable<Instrument>> GetAllInstrumentsAsync();
    Task<Instrument?> GetInstrumentByIdAsync(int id);
    Task<bool> CategoryExistsAsync(int id);
    Task<bool> InstrumentExistsAsync(string name);
    Task<bool> InstrumentHasStudentsAsync(int id);
    Task InsertAsync(Instrument instrument);
    Task DeleteAsync(Instrument instrument);
    Task CommitAsync();
}
