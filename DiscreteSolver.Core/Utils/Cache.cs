﻿namespace DiscreteSolver.Core.Utils
{
    internal class Cache<TKey, TSecondaryKey, TValue>
    {
        private readonly Dictionary<TKey, Dictionary<TSecondaryKey, TValue>> cache = new Dictionary<TKey, Dictionary<TSecondaryKey, TValue>>();

        internal TValue GetCachedOrExecute(TKey key, TSecondaryKey secondaryKey, Func<TValue> func)
        {
            if (TryGetValue(key, secondaryKey, out var value))
                return value;

            var result = func();
            Store(key, secondaryKey, result);
            return result;
        }

        private bool TryGetValue(TKey key, TSecondaryKey secondaryKey, out TValue value)
        {
            if (!cache.ContainsKey(key) || !cache[key].ContainsKey(secondaryKey))
            {
                value = default;
                return false;
            }

            value = cache[key][secondaryKey];
            return true;
        }

        private void Store(TKey key, TSecondaryKey secondaryKey, TValue value)
        {
            if (!cache.TryGetValue(key, out _))
                cache[key] = new Dictionary<TSecondaryKey, TValue>();

            cache[key][secondaryKey] = value;
        }
    }
}