using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Log
{
    public interface ILog
    {
        void Info(string exception, string innerMessage = default);
        void Warning(string exception, string innerMessage = default);
        void Error(string exception, string innerMessage = default);
    }
}
