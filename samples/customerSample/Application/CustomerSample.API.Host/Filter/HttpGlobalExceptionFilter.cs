using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CustomerSample.API.Host.Filter
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public HttpGlobalExceptionFilter(IHostingEnvironment env)
        {
            this._env = env ?? throw new ArgumentNullException(nameof(env));
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(Exception))
            {


                context.Result = new OkObjectResult(context.Exception.Message);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {


                context.Result = new OkObjectResult(context.Exception.Message);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }
}