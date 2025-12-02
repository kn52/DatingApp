using System;

namespace API.Responses;

public class ApiResposne<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public T Data { get; set; }

    public static ApiResposne<T> PrepareResponse(Result<T, StatusInfo> source)
    {
        ApiResposne<T> response = new ApiResposne<T>();
        response.Success = source.IsSuccess ? true : false;
        response.Message = source.IsSuccess ? CodeLibrary.Success.Message : source.Status.Message;
        response.StatusCode = source.IsSuccess ? CodeLibrary.Success.Code : source.Status.Code;
        response.Data = source.Value;

        return response;
    }
}


public static class StatusHelper
{
    public static StatusInfo PrepareStatusInfo(int code, string message)
    {
        StatusInfo statusInfo = new
        (
            _code: code,
            _message: message
        );

        return statusInfo;
    }
}