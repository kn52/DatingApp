using System;

namespace API.Responses;

public interface IResult<T, E>
{
    bool IsSuccess { get; set; }
    bool IsFailed { get; }
    bool IsException { get; }
    E Status { get; set; }
    T Value { get; set; }
}


public class Result<T, E> : IResult<T, E>
{
    public bool IsSuccess { get; set; }
    public bool IsFailed => !IsSuccess && !IsException;
    public bool IsException { get; set; }
    public E Status { get; set; }
    public T Value { get; set; }

    // Success implicit operator (T → Result<T,E>)
    public static implicit operator Result<T, E>(T value)
    {
        return new Result<T, E>
        {
            IsSuccess = true,
            Value = value
        };
    }

    // Error implicit operator (E → Result<T,E>)
    public static implicit operator Result<T, E>(E status)
    {
        return new Result<T, E>
        {
            IsSuccess = false,
            Status = status
        };
    }
}

