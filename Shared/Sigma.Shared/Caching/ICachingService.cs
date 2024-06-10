using Microsoft.Extensions.Caching.Memory;
using Sigma.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Shared.Caching;

public interface ICachingService : ITransientService
{
    void AddorUpdateData<T>(string key, T item, MemoryCacheEntryOptions options = null);
    T GetData<T>(string key);
    void RemoveCache<T>(string key);
    MemoryCacheEntryOptions SetCachingOption(int cacheSlidingTimeInMinute = 10, int cacheExpiringTimeInMinute = 60);
}
public class CachingService : ICachingService
{
    private IMemoryCache _cache;
    public CachingService(IMemoryCache cache)
    {
        _cache = cache;
    }
    public T GetData<T>(string key)
    {
        if (_cache.TryGetValue(key, out T item))
        {
            return item;
        }

        return default(T);
    }

    public void AddorUpdateData<T>(string key, T item, MemoryCacheEntryOptions options = null)
    {
        if (options == null)
        {
            options = SetCachingOption();
        }
        _cache.Set(key, item, options);
    }
    public void RemoveCache<T>(string key)
    {
        if (_cache.TryGetValue(key, out T item))
        {
            _cache.Remove(key);
        }
    }
    public MemoryCacheEntryOptions SetCachingOption(int cacheSlidingTimeInMinute = 10, int cacheExpiringTimeInMinute = 60)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(cacheSlidingTimeInMinute))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheExpiringTimeInMinute))
                    .SetPriority(CacheItemPriority.Normal);
        return cacheEntryOptions;

    }
    public string GenerateCacheId(params object[] values)
    {
        return string.Join("", values);
    }
}
