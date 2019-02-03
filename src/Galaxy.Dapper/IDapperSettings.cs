using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Dapper
{
    public interface IDapperSettings
    {
        int CommandTimeout { get; set; }
    }
}
