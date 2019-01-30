using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Cache
{
    public interface ICacheDefaultSettings
    {
        bool IsNormalizeKeyEnabled { get; set; }
        string NameofCache { get; set; }
        TimeSpan DefaultSlidingExpireTime { get; set; }
        TimeSpan? DefaultAbsoluteExpireTime { get; set; }
    }
}
