using API.Responses;
using System;

namespace API.DbServices.BaseLayer;

public class RepositoryWrapper<T>
{
    private static Result<T, StatusInfo> PrepareRepositoryWrapper(bool success, bool exception, StatusInfo status, T? data = default, Exception? e = null)
    {
        return new Result<T, StatusInfo>
        {
            IsSuccess = success,
            IsException = exception,
            Value = data,
            Status = status
        };
    }

    public static Result<T, StatusInfo> PrepareErrorResponse(StatusInfo status, T? data = default, Exception? e = null)
    {
        bool exception = e != null;
        Result<T, StatusInfo> wrapperResponse = PrepareRepositoryWrapper(false, exception, status, data, e);

        return wrapperResponse;
    }
    public static Result<T, StatusInfo> PrepareSuccessResponse(StatusInfo status, T? data = default, Exception? e = null)
    {
        Result<T, StatusInfo> wrapperResponse = PrepareRepositoryWrapper(true, false, status, data, e);

        return wrapperResponse;
    }
}
