using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UIFoundation
{
    public static class ObservableCollectionExt
    {
        public static IEnumerable<T> ObservableWhere<T>(this IEnumerable<T> c, Predicate<T> p)
        {
            var n = c as INotifyCollectionChanged;
            if (n != null)
            {
                return new ObserverCollection<T>(n, c, p);
            }
            throw new ArgumentNullException("n");
        }

        public class ObserverCollection<T> : IEnumerable<T>, INotifyCollectionChanged, IDisposable
        {
            private List<T> c;
            private Predicate<T> p;
            private FrameworkElement target;

            public ObserverCollection(INotifyCollectionChanged n, IEnumerable<T> c, Predicate<T> p)
            {
                n.CollectionChanged += N_CollectionChanged;
                this.c = c.Where(t => p(t)).ToList();
                this.p = p;
            }

            private void N_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        var newItems = e.NewItems.Cast<T>().Where(t => p(t));
                        if (newItems.Count() > 0)
                        {
                            this.InternalCollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems.ToList(), this.c.Count));
                            this.c.InsertRange(this.c.Count, newItems);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        var items = e.OldItems.Cast<T>().Where(t => c.Contains(t));
                        if (items.Count() > 0)
                        {
                            this.InternalCollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items.First(), c.IndexOf(items.First())));
                            foreach (var i in items)
                            {
                                this.c.Remove(i);
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Move:
                    case NotifyCollectionChangedAction.Replace:
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        break;
                }
            }

            public event NotifyCollectionChangedEventHandler CollectionChanged
            {
                add
                {
                    InternalCollectionChanged += value;
                    var target = value.Target as FrameworkElement;
                    if (target != null)
                    {
                        this.target = target;
                        target.Unloaded += Target_Unloaded;
                    }
                }
                remove
                {
                    InternalCollectionChanged -= value;
                }
            }

            private void Target_Unloaded(object sender, RoutedEventArgs e)
            {
                this.InternalCollectionChanged = null;
                target.Unloaded -= Target_Unloaded;
            }

            public event NotifyCollectionChangedEventHandler InternalCollectionChanged;

            public IEnumerator<T> GetEnumerator()
            {
                return c.Where(t => p(t)).ToList().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)c.Where(t => p(t)).ToArray()).GetEnumerator();
            }

            public void Dispose()
            {

            }

            ~ObserverCollection()
            {

            }
        }

    }
}
