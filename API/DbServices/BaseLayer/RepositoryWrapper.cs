using System;

namespace API.DbServices.BaseLayer;

public class RepositoryWrapper<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public Exception Exp { get; set; }


    public static RepositoryWrapper<T> PrepareRepositoryWrapper(bool success, T? data = default, Exception? e = null)
    {
        RepositoryWrapper<T> wrapperResponse = new RepositoryWrapper<T>();
        wrapperResponse.Success = success;
        wrapperResponse.Data = data;
        wrapperResponse.Exp = e;

        return wrapperResponse;
    }
}
