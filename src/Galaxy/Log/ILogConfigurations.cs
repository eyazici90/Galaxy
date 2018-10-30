using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Log
{
    public interface ILogConfigurations
    {
        string Name { get; }

        bool IsEnabled { get; }
    }
}
