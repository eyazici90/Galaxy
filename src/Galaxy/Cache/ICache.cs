using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Cache
{
   public interface ICache : IDisposable
    {

        object Get(string key);

        Task<object> GetAsync(string key);

        object GetOrDefault(string key);

        Task<object> GetOrDefaultAsync(string key);

        TItem Get<TItem>(string key);
 
        Task<TItem> GetAsync<TItem>(string key);

        TItem GetOrDefault<TItem>(string key);

        Task<TItem> GetOrDefaultAsync<TItem>(string key);

        string GetString(string key);

        Task<string> GetStringAsync(string key);

        void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        void Remove(string key);

        Task RemoveAsync(string key);
  
        string NormalizeKey(string key);
    }
}
