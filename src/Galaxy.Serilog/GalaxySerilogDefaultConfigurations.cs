using Galaxy.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Serilog
{
    public sealed class GalaxySerilogDefaultConfigurations : ILogConfigurations
    {
        public string Name => $"Default";

        public bool IsEnabled => true;
    }
}
