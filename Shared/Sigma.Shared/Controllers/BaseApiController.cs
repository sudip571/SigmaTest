using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Shared.Controllers;


[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public abstract  class BaseApiController : ControllerBase
{

}

