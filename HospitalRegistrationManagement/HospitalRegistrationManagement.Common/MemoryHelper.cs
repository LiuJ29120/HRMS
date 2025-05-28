using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRegistrationManagement.Common
{
    public class MemoryHelper
    {
        private static readonly Lazy<IMemoryCache> _memoryCache = new Lazy<IMemoryCache>(() => new MemoryCache(new MemoryCacheOptions()));

        public static void SetMemory<T>(string key, T value, TimeSpan expiration)
        {
            _memoryCache.Value.Set(key, value, expiration);
        }

        public static T GetMemory<T>(string key)
        {
            if (_memoryCache.Value.TryGetValue(key, out object value))
            {
                return (T)value;
            }
            return default;
        }
    }

}
