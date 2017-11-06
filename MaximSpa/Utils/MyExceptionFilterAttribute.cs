using BlackBoxLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MaximSpa.Utils
{
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            BlackBox.e($@"
EXCEPTION!!!
{actionExecutedContext.Request.RequestUri}
{actionExecutedContext.Exception.ToString()}
            ");
        }
    }
}