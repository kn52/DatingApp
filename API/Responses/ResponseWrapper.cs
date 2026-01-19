namespace API.Responses;

public class ResponseWrapper<T>
{
    private static Result<T, StatusInfo> PrepareResponseWrapper(bool success, bool exception, StatusInfo status, T? data = default, Exception? e = null)
    {
        Result<T, StatusInfo> wrapperResponse = new Result<T, StatusInfo>()
        {
            IsSuccess = success,
            IsException = exception,
            Value = data,
            Status = status
        };
        

        return wrapperResponse;
    }
    public static Result<T, StatusInfo> PrepareResponse(bool success, string statusCode, T? data = default, Exception? e = null)
    {
        bool exception = false;
        StatusInfo status = CodeLibrary.InternalServerError;

        if (!string.IsNullOrEmpty(statusCode))
        {
            status = CodeLibrary.Get(statusCode);
        }

        if (e != null)
        {
            exception = true;
            status.Message = e.Message;
        }
        Result<T, StatusInfo> wrapperResponse = PrepareResponseWrapper(success, exception, status, data, e);

        return wrapperResponse;
    }
}
