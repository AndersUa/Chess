using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public static class Extensions
    {
        public static IEnumerable<Tuple<T, T>> AllCombinations<T>(this IEnumerable<T> collection)
        { 
            return collection.SelectMany(e1=>collection.Select(e2=>Tuple.Create(e1,e2)));
        }
    }
}
