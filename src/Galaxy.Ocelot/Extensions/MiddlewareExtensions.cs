using Galaxy.Ocelot.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Ocelot.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
        public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdMiddleware>();
        }
        public static IApplicationBuilder UseCircuitBreakerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CircuitBreakerMiddleware>();
        }
        
    }
}
