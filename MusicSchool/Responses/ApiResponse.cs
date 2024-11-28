using Microsoft.AspNetCore.Http;

namespace MusicSchool.Responses;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }

    public ApiResponse(int statusCode, string message) : this(statusCode)
    {
        Message = message;
    }

    public ApiResponse(int statusCode, T data) : this(statusCode)
    {
        Data = data;
    }

    public ApiResponse(int statusCode)
    {
        StatusCode = statusCode;
    }
}
