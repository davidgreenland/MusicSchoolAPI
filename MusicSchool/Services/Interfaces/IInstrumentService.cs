using MusicSchool.Models;
using MusicSchool.Requests.Instrument;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface IInstrumentService
{
    Task<ApiResult<IEnumerable<InstrumentResponse>>> GetAllInstrumentsAsync();
    Task<ApiResult<InstrumentResponse>> GetInstrumentAsync(int id);
    Task<ApiResult<Instrument>> UpdateInstrumentAsync(int id, UpdateInstrumentPut request);
    Task<ApiResult<Instrument>> CreateInstrumentAsync(CreateInstrumentRequest request);
    Task<ApiResult<Instrument>> DeleteInstrumentAsync(int id);
}
