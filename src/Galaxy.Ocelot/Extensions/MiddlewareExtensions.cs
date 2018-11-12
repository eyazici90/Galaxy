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
        public static IApplicationBuilder UseResponseConsistentMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseConsistentMiddleware>();
        }

        public static IApplicationBuilder UseJwtBearerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtBearerMiddleware>();
        }

        public static IApplicationBuilder UseIdempotencyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdempotencyMiddleware>();
        }

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
