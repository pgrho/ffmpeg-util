using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    internal static class CollectionHelper
    {
        public static Collection<T> GetCollection<T>(ref Collection<T> collection)
            => collection ?? (collection = new Collection<T>());

        public static void SetCollection<T>(ref Collection<T> collection, ICollection<T> value)
        {
            if (value != collection)
            {
                collection?.Clear();
                if (value?.Count > 0)
                {
                    if (collection == null)
                    {
                        collection = new Collection<T>();
                    }
                    foreach (var v in value)
                    {
                        collection.Add(v);
                    }
                }
            }
        }
    }
}
