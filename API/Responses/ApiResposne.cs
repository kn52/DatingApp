namespace API.Responses;

public class ApiResposne<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; } = 200;
    public T? Data { get; set; }
}