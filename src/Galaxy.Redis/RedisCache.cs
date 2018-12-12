using Galaxy.Cache;
using Galaxy.Exceptions;
using Galaxy.Serialization;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Redis
{
    public class RedisCache : ICache
    {
        private readonly ISerializer _objectSerializer;
        private readonly ICacheDefaultSettings _cacheSettings;
        private readonly IDatabase _database;
        public RedisCache(ISerializer objectSerializer
            , ICacheDefaultSettings cacheSettings
            , IDatabase database)
        {
            _objectSerializer = objectSerializer ?? throw new ArgumentNullException(nameof(objectSerializer));
            _cacheSettings = cacheSettings ?? throw new ArgumentNullException(nameof(cacheSettings));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        } 

        public object Get(string key)
        { 
            var cachedBytes = this._database.StringGet(NormalizeKey(key));
            if (!cachedBytes.HasValue)
                return null;

            return this._objectSerializer.Deserialize(cachedBytes);
        }

        public TItem Get<TItem>(string key)
        {
            var cachedBytes = this._database.StringGet(NormalizeKey(key));
            if (!cachedBytes.HasValue)
                return default(TItem);
            return this._objectSerializer.Deserialize<TItem>(cachedBytes.ToString());
        }

        public async Task<object> GetAsync(string key)
        {
            var cachedBytes = await this._database.StringGetAsync(NormalizeKey(key));
            if (!cachedBytes.HasValue)
                return null;

            return this._objectSerializer.Deserialize(cachedBytes);
        }

        public async Task<TItem> GetAsync<TItem>(string key)
        {
            var cachedBytes = await this._database.StringGetAsync(NormalizeKey(key));
            if (!cachedBytes.HasValue)
                return default(TItem);
            return this._objectSerializer.Deserialize<TItem>(cachedBytes.ToString());
        }

        public object GetOrDefault(string key)
        {
            RedisValue objbyte = _database.StringGet(NormalizeKey(key));
            return objbyte.HasValue ? this._objectSerializer.Deserialize(objbyte) : null;
        }

        public TItem GetOrDefault<TItem>(string key)
        {
            RedisValue objbyte = _database.StringGet(NormalizeKey(key));
            return this._objectSerializer.Deserialize<TItem>(objbyte) ;
        }

        public async Task<object> GetOrDefaultAsync(string key)
        {
            RedisValue objbyte = await _database.StringGetAsync(NormalizeKey(key));
            return this._objectSerializer.Deserialize<object>(objbyte);
        }

        public async Task<TItem> GetOrDefaultAsync<TItem>(string key)
        {
            RedisValue objbyte = await _database.StringGetAsync(NormalizeKey(key));
            return this._objectSerializer.Deserialize<TItem>(objbyte);
        } 

        public void Remove(string key)
        {
            _database.KeyDelete(NormalizeKey(key));
        }

        public Task RemoveAsync(string key)
        {
            return _database.KeyDeleteAsync(NormalizeKey(key));
        }

        public void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                throw new GalaxyException($"Can not insert null values to the cache!");
            }

            this._database.StringSet(
              NormalizeKey(key),
              this._objectSerializer.Serialize(value),
              slidingExpireTime ?? absoluteExpireTime ?? this._cacheSettings.DefaultSlidingExpireTime
          );
        }

        public  Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                throw new GalaxyException($"Can not insert null values to the cache!");
            }

            return this._database.StringSetAsync(
              NormalizeKey(key),
              this._objectSerializer.Serialize(value),
              slidingExpireTime ?? absoluteExpireTime ?? this._cacheSettings.DefaultSlidingExpireTime
          );
        }  

        public string NormalizeKey(string key) =>
            $"cache:{this._cacheSettings.NameofCache},key:{key}";

        public void Dispose()
        {
            //Disposing
        }
    }
}
