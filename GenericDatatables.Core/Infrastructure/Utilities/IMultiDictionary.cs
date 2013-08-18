using System.Collections.Generic;

namespace GenericDatatables.Core.Infrastructure.Utilities
{
    public interface IMultiDictionary <TKey, TValue> : IDictionary<TKey, ISet<TValue>>
    {
        /// <summary>
        ///     Adds a value to the <see cref="IEnumerable{TValue}"/> associated with the <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value to associate with the key</param>
        void Add(TKey key, TValue value);

        /// <summary>
        ///     Returns a value indicating whether the <paramref name="value"/> is associated with the <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <returns>A value indicating whether the <paramref name="value"/> is associated with the <paramref name="key"/></returns>
        bool Contains(TKey key, TValue value);

        /// <summary>
        ///     Removes the <paramref name="value"/> for the specified <paramref name="key"/> if it was associated with it.
        ///     If this <paramref name="value"/> was the last value associated with the <paramref name="key"/>, then the <paramref name="key"/> is also deleted from this <see cref="IMultiDictionary{TKey,TValue}"/>
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        void Remove(TKey key, TValue value);

        /// <summary>
        ///     Merges all keys and corresponding values from this <see cref="IMultiDictionary{TKey,TValue}"/> with all keys and corresponding values from the <paramref name="other"/> <see cref="IMultiDictionary{TKey,TValue}"/>
        /// </summary>
        /// <param name="other">The other <see cref="IMultiDictionary{TKey,TValue}"/></param>
        void Merge(IMultiDictionary<TKey, TValue> other);

        /// <summary>
        ///     Gets the associated set of values for the <paramref name="key"/> or an empty <see cref="ISet{TValue}"/> if the key was not present in this <see cref="IMultiDictionary{TKey,TValue}"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ISet<TValue> Get(TKey key);
    }
}