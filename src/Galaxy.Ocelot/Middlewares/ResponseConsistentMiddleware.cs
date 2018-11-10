using Galaxy.Ocelot.Infrastructure;
using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Galaxy.Ocelot.Middlewares
{
    public class ResponseConsistentMiddleware
    {
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public ResponseConsistentMiddleware(RequestDelegate next
            , ISerializer serializer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }

        private async Task<ResponseMessage> HandleResponse(HttpResponse response)
        {
            using (var reader = new StreamReader(response.Body))
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var results = await reader.ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
                return new ResponseMessage {
                    Result = results
                };
            }
        }
    }
}
