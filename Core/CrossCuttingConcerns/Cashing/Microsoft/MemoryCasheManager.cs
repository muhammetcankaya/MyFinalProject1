using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Cashing.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memorycache;

        public MemoryCacheManager()
        {
            _memorycache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public T Get<T>(string key)
        {
            return _memorycache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memorycache.Get(key);
        }

        public void Add(string key, object data, int duration)
        {
            _memorycache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public bool IsAdd(string key)
        {
            return _memorycache.TryGetValue(key, out _);// out _ zahmet olmasın bir şey verme
        }

        public void Remove(string key)
        {
            _memorycache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var fieldInfo = typeof(MemoryCache).GetField("_coherentState", BindingFlags.Instance | BindingFlags.NonPublic);
            var propertyInfo = fieldInfo.FieldType.GetProperty("EntriesCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = fieldInfo.GetValue(_memorycache);
            var dict = propertyInfo.GetValue(value) as dynamic;


            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in dict)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memorycache.Remove(key);
            }
        }
    }
}
