using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicator
{
    public static class SmallHepler
    {
        public static T LastElement<T>(this IEnumerable<T> a)
        {
            List<T> list = a.ToList();

            int count = list.Count;

            return list[count - 1];
        }

        public static bool IsExistFieldwithValue<T>(this IEnumerable<T> a, Func<T, bool> predicate)
        {
            List<T> list = a.ToList();

            for (int i = 0; i < list.Count; i++)
                if (predicate(list[i]))
                    return true;

            return false;
            
        }
    }
}
