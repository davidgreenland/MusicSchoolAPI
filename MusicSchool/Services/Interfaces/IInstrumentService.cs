using MusicSchool.Models;
using MusicSchool.Requests.Instrument;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface IInstrumentService
{
    Task<ApiResponse<IEnumerable<InstrumentResponse>>> GetAllInstrumentsAsync();
    Task<ApiResponse<InstrumentResponse>> GetInstrumentAsync(int id);
    Task<ApiResponse<Instrument>> UpdateInstrumentAsync(int id, UpdateInstrumentPut request);
    Task<ApiResponse<Instrument>> CreateInstrumentAsync(CreateInstrumentRequest request);
    Task<ApiResponse<Instrument>> DeleteInstrumentAsync(int id);
}
