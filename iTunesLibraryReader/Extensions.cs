using System;
using System.Collections.Generic;
using System.Linq;

namespace iTunesLibraryReader
{
    /// <summary>
    /// A few utility extensions
    /// </summary>
    public static class Extensions
    {
        #region Methods
        /// <summary>
        /// Finds the mode of a list. Slow, but works. Could probably rewrite this to not loop four times the collection
        /// </summary>
        /// <typeparam name="T">Type of the collection</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="list">List to get the mode from</param>
        /// <param name="selector">Selector to get the values to find the mode of</param>
        /// <returns>The value with the most occurences in the list</returns>
        public static TResult Mode<T, TResult>(this List<T> list, Func<T, TResult> selector)
        {
            return list.Select(selector).GroupBy(t => t).OrderByDescending(g => g.Count()).First().Key;
        }

        /// <summary>
        /// automatically sets a TimeSpan to return the smallest string possible in matters of days/hours/minutes/seconds
        /// </summary>
        /// <param name="ts">TimesSpan to get the string from</param>
        /// <returns>Shortest string representation possible</returns>
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
        #endregion
    }
}
