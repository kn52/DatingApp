using System;

namespace API.Responses;

public class StatusInfo
{
    public int Code { get; set; }
    public string Message { get; set; }

    public StatusInfo(int _code, string _message = "")
    {
        Code = _code;
        Message = _message;
    }
}

