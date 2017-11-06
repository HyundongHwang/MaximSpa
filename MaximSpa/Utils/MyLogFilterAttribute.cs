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
    public class MyLogFilterAttribute : ActionFilterAttribute
    {
        const bool _SIMPLE = true;


        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var headerJson = "";
            var reqContentStr = "";

            if (_SIMPLE)
            {
                headerJson = JsonConvert.SerializeObject(actionContext.Request.Headers, Formatting.None);

                if (headerJson.Length > 50)
                {
                    headerJson = $"{headerJson.Substring(0, 50)} ...";
                }
            }
            else
            {
                headerJson = JsonConvert.SerializeObject(actionContext.Request.Headers, Formatting.Indented);
            }

            try
            {
                reqContentStr = JsonConvert.SerializeObject(actionContext.ActionArguments, Formatting.Indented);

                if (_SIMPLE)
                {
                    if (reqContentStr.Length > 200)
                    {
                        reqContentStr = $"{reqContentStr.Substring(0, 200)} ...";
                    }
                }
            }
            catch (Exception)
            {
            }

            BlackBox.i($@"
HTTP REQ <<< {actionContext.Request.Method} {actionContext.Request.RequestUri.ToString()}
    HEADER : {headerJson}
    CONTENT : {reqContentStr}



");
        }


        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var headerJson = "";
            var resContentStr = "";
            var resStatusCode = actionExecutedContext.Response.StatusCode;

            if (_SIMPLE)
            {
                headerJson = JsonConvert.SerializeObject(actionExecutedContext.Request.Headers, Formatting.None);

                if (headerJson.Length > 50)
                {
                    headerJson = $"{headerJson.Substring(0, 50)} ...";
                }
            }
            else
            {
                headerJson = JsonConvert.SerializeObject(actionExecutedContext.Request.Headers, Formatting.Indented);
            }

            try
            {
                resContentStr = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;

                if (_SIMPLE)
                {
                    if (resContentStr.Length > 200)
                    {
                        resContentStr = $"{resContentStr.Substring(0, 200)} ...";
                    }
                }
            }
            catch (Exception)
            {
            }

            BlackBox.i($@"
HTTP RES >>> {resStatusCode}({(int)resStatusCode}) {actionExecutedContext.Request.RequestUri.ToString()}
    HEADER : {headerJson}
    CONTENT : {resContentStr}



");




        }
    }
}