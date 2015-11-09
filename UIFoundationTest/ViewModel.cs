using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIFoundation;

namespace UIFoundationTest
{
    public class ViewModel : BindableBase
    {
        ObservableCollection<int> collection = new ObservableCollection<int>();
        public ObservableCollection<int> Collection => collection;

        public ICommand Command => new Command(OnCommand);

        public IEnumerable<int> FilteredCollection => Collection.ObservableWhere(p => p % 2 == 0);

        private void OnCommand(object obj)
        {
            for (int i = 0; i < 1000; i++)
            {
                collection.Remove(i);
            }

            for (int i = 0; i < 1000; i++)
            {
                collection.Add(i);
            }

            GC.Collect();
        }

        public ViewModel()
        {
            for (int i = 0; i < 1000; i++)
            {
                collection.Add(i);
            }
        }
    }
}
