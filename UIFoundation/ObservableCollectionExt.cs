using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UIFoundation
{
    public static class ObservableCollectionExt
    {
        public static IEnumerable ObservableWhere<T>(this IEnumerable<T> c, Predicate<T> p)
        {
            var source = new CollectionViewSource { Source = c }.View;
            source.Filter = (t) => p((T)t);
            return source;
        }
    }
}
