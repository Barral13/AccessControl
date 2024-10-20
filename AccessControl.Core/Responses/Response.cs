using System.Text.Json.Serialization;

namespace AccessControl.Core.Responses;

public class Response<TData>
{
    private readonly int _code;

    [JsonConstructor]
    public Response() => _code = Configuration.DefaultStatusCode;

    public Response(
        TData? data,
        int code = Configuration.DefaultStatusCode,
        string? message = null)
    {
        Data = data;
        Message = message;
        _code = code;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }

    [JsonIgnore]
    public int Code => _code;

}
