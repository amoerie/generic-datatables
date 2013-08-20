using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GenericDatatables.Core.Infrastructure.Utilities
{
    public class MultiDictionary<TKey, TValue> : Dictionary<TKey, ISet<TValue>>, IMultiDictionary<TKey, TValue>
    {
        public void Add(TKey key, TValue value)
        {
            ISet<TValue> container;
            if (!TryGetValue(key, out container))
            {
                container = new HashSet<TValue>();
                Add(key, container);
            }
            container.Add(value);
        }

        public bool Contains(TKey key, TValue value)
        {
            ISet<TValue> values;
            return TryGetValue(key, out values) && values.Contains(value);
        }

        public void Remove(TKey key, TValue value)
        {
            ISet<TValue> container;
            if (!TryGetValue(key, out container))
                return;

            container.Remove(value);
            if (!container.Any())
            {
                Remove(key);
            }
        }

        public void Merge(IMultiDictionary<TKey, TValue> other)
        {
            if (other == null)
            {
                return;
            }

            foreach (var pair in other)
            {
                foreach (var value in pair.Value)
                {
                    Add(pair.Key, value);
                }
            }
        }

        public ISet<TValue> Get(TKey key)
        {
            ISet<TValue> values;
            return TryGetValue(key, out values) ? values : new HashSet<TValue>();
        }
    }
}
