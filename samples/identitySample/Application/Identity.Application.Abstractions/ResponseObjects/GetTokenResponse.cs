using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Abstractions.ResponseObjects
{
    public class GetTokenResponse
    {
        public string Token { get; set; }

        public DateTimeOffset ExpiredDate { get; set; }
    }
}
