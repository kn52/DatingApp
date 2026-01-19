using API.Responses;

namespace API.DbRepository.BaseLayer
{
    public class RepositoryWrapper<T>
    {
        public bool Success { get; set; }
        public string StatusCode { get; set; }
        public T Data { get; set; }
        public Exception? exception { get; set; }

        private static RepositoryWrapper<T> PrepareRepositoryResponse(
            bool success, 
            string statusCode, 
            T? data = default, 
            Exception? e = null)
        {
            RepositoryWrapper<T> wrapperResponse = new RepositoryWrapper<T>()
            {
                Success = success,
                StatusCode = statusCode,
                Data = data,
                exception = e
            };

            return wrapperResponse;
        }
        public static RepositoryWrapper<T> PrepareErrorResponse(string statusCode, T? data = default, Exception? e = null)
        {
            RepositoryWrapper<T> wrapperResponse = PrepareRepositoryResponse(false, statusCode, data, e);
            return wrapperResponse;
        }
        public static RepositoryWrapper<T> PrepareSuccessResponse(string statusCode, T? data = default, Exception? e = null)
        {
            RepositoryWrapper<T> wrapperResponse = PrepareRepositoryResponse(true, statusCode, data, e);
            return wrapperResponse;
        }
    }
}
