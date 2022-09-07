using ProjectDreamland.Data.GameFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDreamland.ExtensionClasses
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> collectionToAdd)
        {
            foreach(T item in collectionToAdd)
            {
                collection.Add(item);
            }
        }
    }
}
