using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStockMarket
{
    /// <summary>
    /// Modifier to the compare used with the SortedList for trades so the trades are ordered most reent first
    /// 
    /// Simply switches x and y.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class DecendingCompare<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            return Comparer<T>.Default.Compare(y, x);
        }
    }

}
