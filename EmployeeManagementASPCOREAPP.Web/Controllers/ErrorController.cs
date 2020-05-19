using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementASPCOREAPP.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statuscode)
        {
            var statuscodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry , the resource you are requested could not be found";
                    //ViewBag.Path = statuscodeResult.OriginalPath;
                    //ViewBag.QS = statuscodeResult.OriginalQueryString;
                    logger.LogWarning($"404 Error Occured .Path ={statuscodeResult.OriginalPath }" +

                        $"and QueryString={statuscodeResult.OriginalQueryString }");
                   
                    break;
            }
            return View("NotFound");
            //return View("Views/Shared/NotFound.cshtml");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"The Path : {exceptionDetails.Path } threw an exception {exceptionDetails.Error}");
            //ViewBag.ExceptioPath = exceptionDetails.Path;
            //ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            //ViewBag.StackTrack = exceptionDetails.Error.StackTrace;
            return View("Error");
           
        }
    }
}