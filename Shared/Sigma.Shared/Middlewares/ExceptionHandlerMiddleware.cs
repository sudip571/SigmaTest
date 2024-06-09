using Microsoft.AspNetCore.Http;
using Sigma.Shared.Exceptions;
using Sigma.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sigma.Shared.Middlewares;


public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var responseModel = new ResponseV2<string>(); // Use ResponseV2<T> here
            responseModel.Errors = new 
            {
                Message = error?.Message,
                ErrorType = error?.GetType().Name
            };

            switch (error)
            {               

                case ValidationFailedException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = ex.ErrorsDictionary;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await response.WriteAsync(result);
        }
    }
}
