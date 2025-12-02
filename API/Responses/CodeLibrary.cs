using System;

namespace API.Responses;

public class CodeLibrary
{
    public static StatusInfo Success = new(_code: 200, _message: "Success.");
    public static StatusInfo BadRequest = new(_code: 400, _message: "Bad Request.");
    public static StatusInfo InternalServerError = new(_code: 500, _message: "Internal Server Error.");
    public static StatusInfo NotFound = new(_code: 404, _message: "Not Found.");
}
