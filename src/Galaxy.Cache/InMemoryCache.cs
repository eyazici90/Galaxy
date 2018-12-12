using Galaxy.Exceptions;
using Galaxy.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Cache
{
    public sealed class InMemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISerializer _objectSerializer;
        private readonly ICacheDefaultSettings _cacheSettings;
        public InMemoryCache(IMemoryCache memoryCache
            , ISerializer objectSerializer
            , ICacheDefaultSettings cacheSettings)
        {
            this._memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            this._objectSerializer = objectSerializer ?? throw new ArgumentNullException(nameof(objectSerializer));
            this._cacheSettings = cacheSettings ?? throw new ArgumentNullException(nameof(cacheSettings));
        }

        public object Get(string key)
        {

            var cachedBytes = this._memoryCache.Get<string>(NormalizeKey(key));
            if (cachedBytes == null)
                return null;

            return this._objectSerializer.Deserialize(cachedBytes);
        }

        public TItem Get<TItem>(string key)
        {
            var cachedBytes = this._memoryCache.Get(NormalizeKey(key));
            if (cachedBytes == null)
                return default(TItem);
            return this._objectSerializer.Deserialize<TItem>(cachedBytes.ToString());
        }

        public Task<object> GetAsync(string key) =>
            Task.FromResult(this.Get(key));


        public Task<TItem> GetAsync<TItem>(string key) =>
            Task.FromResult(this.Get<TItem>(key));

        public object GetOrDefault(string key) =>
            this._memoryCache.Get(NormalizeKey(key));

        public TItem GetOrDefault<TItem>(string key) =>
            this._memoryCache.Get<TItem>(NormalizeKey(key));


        public Task<object> GetOrDefaultAsync(string key) =>
            Task.FromResult(this.GetOrDefault(key));

        public Task<TItem> GetOrDefaultAsync<TItem>(string key) =>
            Task.FromResult(this.GetOrDefault<TItem>(key));
        

        public void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            { 
                throw new GalaxyException($"Can not insert null values to the cache!");
            }

            this._memoryCache.Set(
              NormalizeKey(key),
              this._objectSerializer.Serialize(value),
              new MemoryCacheEntryOptions
              {
                  SlidingExpiration = slidingExpireTime ?? this._cacheSettings.DefaultSlidingExpireTime,
                  AbsoluteExpiration = absoluteExpireTime != default ? DateTimeOffset.Now.Add(absoluteExpireTime.Value)
                                                                     : DateTimeOffset.Now.Add(this._cacheSettings.DefaultAbsoluteExpireTime.Value)
              } 
          );
        }

        public Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null) {
            this.Set(key, value, slidingExpireTime, absoluteExpireTime);
            return Task.FromResult(true);
        }
           
        public void Remove(string key) =>
            this._memoryCache.Remove(NormalizeKey(key));
        
        public Task RemoveAsync(string key)
        {
            this.Remove(key);
            return Task.FromResult(true);
        }

        public string NormalizeKey(string key) =>
            $"cache:{this._cacheSettings.NameofCache},key:{key}";
        
        public void Dispose()
        {
            //Disposing
        }
    }
}
