using Microsoft.AspNetCore.Http;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Ocelot.Middlewares
{
    
    public class CircuitBreakerMiddleware
    {
        private readonly RequestDelegate _next;

        public CircuitBreakerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            var circuitBreaker = Policy
                 .Handle<Exception>()
                 .CircuitBreakerAsync(
                     exceptionsAllowedBeforeBreaking: 2,
                     durationOfBreak: TimeSpan.FromSeconds(10)
                 );
            await circuitBreaker.ExecuteAsync(async () =>
            {
                await _next(context);
            });
            
        }
    }
}
