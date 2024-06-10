
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Shared.Responses;

namespace Sigma.Shared.Responses;

public static class CustomResponses
{
    public static ObjectResult HttpResponses<T>(this Response<T> response) => new Status().StatusCode(response.StatusCode, response);

}
public class Status : ControllerBase
{

}
