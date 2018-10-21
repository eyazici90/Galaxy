using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Cache
{
    public interface ICacheDefaultSettings
    {
        string NameofCache { get; }
        TimeSpan DefaultSlidingExpireTime { get; }
        TimeSpan? DefaultAbsoluteExpireTime { get;  }
    }
}
