using Galaxy.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.NLog
{ 
    public sealed class GalaxyNLogDefaultConfigurations : ILogConfigurations
    {
        public string Name => $"Default";

        public bool IsEnabled => true;
    }
}
