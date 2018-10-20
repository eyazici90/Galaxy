using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Galaxy.Extensions
{
   public static   class ObjectExtensions
    {
        public static T As<T>(this object obj)
          where T : class
        {
            return (T)obj;
        }
        public static T To<T>(this object obj)
          where T : struct
        {
            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
            {
                return;
            }

            foreach (T obj in items)
            {
                action(obj);
            }
        }
    }
}
