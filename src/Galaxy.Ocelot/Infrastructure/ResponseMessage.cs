using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Ocelot.Infrastructure
{
    public class ResponseMessage
    {
        public string Result { get; set; }
        public bool Success { get; set; } = true;
        public bool Error { get; set; }

    }
}
