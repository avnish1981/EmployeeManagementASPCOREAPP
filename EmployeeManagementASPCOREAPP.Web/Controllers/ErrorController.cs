using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementASPCOREAPP.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statuscode)
        {
            switch (statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry , the resource you are requested could not be found";
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
            ViewBag.ExceptioPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrack = exceptionDetails.Error.StackTrace;
            return View("Error");
        }
    }
}