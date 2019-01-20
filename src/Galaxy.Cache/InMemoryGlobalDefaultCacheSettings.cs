using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Cache
{
    public sealed class InMemoryGlobalDefaultCacheSettings : GlobalDefaultCacheSettingsBase
    {
        public override bool IsNormalizeKeyEnabled { get; set; } = true;

        public override TimeSpan DefaultSlidingExpireTime { get; set; } = TimeSpan.FromHours(1);
        
        public override TimeSpan? DefaultAbsoluteExpireTime { get; set; } = TimeSpan.FromHours(24);

        public override string NameofCache { get; set; } = $"default";

    }
}
