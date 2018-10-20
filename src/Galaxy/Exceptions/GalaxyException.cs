using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Exceptions
{
  
    public class GalaxyException : Exception
    {
        public GalaxyException()
        { }

        public GalaxyException(string message)
            : base(message)
        { }

        public GalaxyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
