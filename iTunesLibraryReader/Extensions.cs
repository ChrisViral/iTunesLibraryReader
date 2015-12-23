using System;
using System.Collections.Generic;
using System.Linq;

namespace iTunesLibraryReader
{
    public static class Extensions
    {
        public static TResult Mode<T, TResult>(this List<T> list, Func<T, TResult> selector)
        {
            return list.Select(selector).GroupBy(t => t).OrderByDescending(g => g.Count()).First().Key;
        }

        public static string AutoString(this TimeSpan ts)
        {
            if (ts.Days > 0)
            {
                return ts.ToString(@"d\.hh\:mm\:ss");
            }

            else if (ts.Hours > 0)
            {
                return ts.ToString(@"h\:mm\:ss");
            }

            return ts.ToString(@"m\:ss");
        }
    }
}
