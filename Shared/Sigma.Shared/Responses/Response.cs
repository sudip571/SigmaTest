using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sigma.Shared.Responses;

public class Response<T>
{
    public bool Success
    {
        get { return Errors is  null; }
    }
    public string Message { get; set; }
    public object? Errors { get; set; }
    public T Data { get; set; }   
    public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
}

#region Response Extension Methods
public static class ResponseExtension
{
    public static Response<T> OKResponse<T>(this Response<T> response, string message = "")
    {
        response.StatusCode = (int)HttpStatusCode.OK;
        response.Message = message;

        return response;
    }
    public static Response<T> OKResponse<T>(this Response<T> response, T data, string message = "")
    {
        response.StatusCode = (int)HttpStatusCode.OK;
        response.Message = message;
        response.Data = data;

        return response;
    }
    
    public static Response<T> ErrorResponse<T>(this Response<T> response, Exception ex)
    {
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.Message = "Error occured while processing the request.";
        response.Errors = new 
        {
            ErrorType = ex.GetType().Name,
            Message = ex.Message
        };
        return response;
    }
    public static Response<T> ResultNotFoundResponse<T>(this Response<T> response)
    {
        response.StatusCode = (int)HttpStatusCode.NotFound;
        response.Message = "Data is not available";
        response.Errors = new
        {
            Message = "Data is not available",
            ErrorType = "NotFound"
        };
        return response;
    }    
}
#endregion
