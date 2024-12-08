using MusicSchool.Models;
using MusicSchool.Requests.Instrument;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface IInstrumentService
{
    Task<IEnumerable<Instrument>> GetAllInstrumentsAsync();
    Task<Instrument?> GetInstrumentByIdAsync(int id);
    Task<ApiResult<Instrument>> CreateInstrumentAsync(CreateInstrumentRequest request);
    Task<ApiResult<Instrument>> DeleteInstrumentAsync(int id);
    Task<bool> CategoryExistsAsync(int id);
    Task<bool> InstrumentExistsAsync(string name);
    Task CommitAsync();
}
