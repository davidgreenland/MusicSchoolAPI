using Microsoft.AspNetCore.Mvc;
using MusicSchool.Models;
using MusicSchool.Requests.Instrument;
using MusicSchool.Responses;

namespace MusicSchool.Services.Interfaces;

public interface IInstrumentService
{
    Task<IEnumerable<InstrumentResponse>> GetInstrumentsAsync();
    Task<InstrumentResponse?> GetInstrumentAsync(int id);
    Task<ApiResponse<Instrument>> UpdateInstrumentAsync(int id, UpdateInstrumentPut request);
    Task<ApiResponse<Instrument>> CreateInstrumentAsync([FromBody] CreateInstrumentRequest request);
    Task<ApiResponse<Instrument>> DeleteInstrumentAsync(int id);
}
