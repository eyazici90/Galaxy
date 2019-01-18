using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Identity.Domain
{
    public class GalaxyIdentityException : Exception
    {
        public GalaxyIdentityException()
        { }

        public GalaxyIdentityException(string message)
            : base(message)
        { }

        public GalaxyIdentityException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
