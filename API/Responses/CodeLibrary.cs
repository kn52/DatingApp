using System;

namespace API.Responses;

public class CodeLibrary
{
    public static StatusInfo Success = new(_code: 200, _message: "Success.");
    public static StatusInfo BadRequest = new(_code: 400, _message: "Bad Request.");
    public static StatusInfo InternalServerError = new(_code: 500, _message: "Internal Server Error.");
    public static StatusInfo NotFound = new(_code: 404, _message: "Not Found.");
    public static StatusInfo AlreadyExists = new(_code: 409, _message: "Already Exists.");
    public static StatusInfo FailedToInsert = new(_code: 500, _message: "Failed To Insert.");
    public static StatusInfo FailedToUpdate = new(_code: 500, _message: "Failed To Update.");
    public static StatusInfo FailedToDelete = new(_code: 500, _message: "Failed To Delete.");
}
