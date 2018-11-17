using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Abstractions.RequestObjects
{
    public class GetTokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
