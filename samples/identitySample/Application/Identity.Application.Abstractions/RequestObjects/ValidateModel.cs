using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Abstractions.RequestObjects
{
    public class ValidateModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
