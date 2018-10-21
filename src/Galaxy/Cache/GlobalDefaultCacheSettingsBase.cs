using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Cache
{
    public abstract class GlobalDefaultCacheSettingsBase : ICacheDefaultSettings
    {
        public abstract TimeSpan DefaultSlidingExpireTime { get; set; }

        public abstract TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        public abstract string NameofCache { get; set; }
    }
}
