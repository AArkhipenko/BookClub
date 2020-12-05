using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("error")]
        public IActionResult Error([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = context.Error;
            int statusCode = 500; // Internal Server Error by default
            string title = "";
            string details = "";
            if (ex is Exceptions.AuthorizationException)
            {
                statusCode = 401; //Unauthorized
                title = (ex as Exceptions.AuthorizationException).FullMessage;
                details = (ex as Exceptions.AuthorizationException).FullStack;
            }
            else if (ex is Exceptions.GeneralException)
            {
                statusCode = 400; // Bad Request
                title = (ex as Exceptions.GeneralException).FullMessage;
                details = (ex as Exceptions.GeneralException).FullStack;
            }
            else
            {
                var tempException = new Exceptions.GeneralException("Неконтроллируемая ошибка.");
                title = tempException.FullMessage;
                details = tempException.FullStack;
            }

            return Problem(
                statusCode: statusCode,
                detail: details,
                title: title);
        }
    }
}
