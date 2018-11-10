using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Ocelot.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }


        public async Task Invoke(HttpContext context)
        {
            var correlationId = Guid.NewGuid().ToString();

            //context.Response.OnStarting(() =>
            //{
                context.Response.Headers.Add("CorrelationId", correlationId);
                //return Task.CompletedTask;
            //});

            await _next(context);

        }
    }
}
