using Galaxy.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Redis
{ 
    public sealed class RedisGlobalDefaultCacheSettings : GlobalDefaultCacheSettingsBase
    {
        public override TimeSpan DefaultSlidingExpireTime { get; set; } = TimeSpan.FromHours(1);

        public override TimeSpan? DefaultAbsoluteExpireTime { get; set; } = TimeSpan.FromHours(24);

        public override string NameofCache { get; set; } = $"default";

    }
}
