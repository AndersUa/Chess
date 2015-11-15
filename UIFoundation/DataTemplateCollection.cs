using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace UIFoundation
{
    [ContentProperty("Items")]
    public class DataTemplateCollection : DataTemplateSelector
    {
        public DataTemplateCollection()
        {

        }

        private ObservableCollection<DataTemplate> _items = new ObservableCollection<DataTemplate>();
        public ObservableCollection<DataTemplate> Items
        {
            get { return this._items; }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return Items.First(p => p.DataType.ToString().ToLower() == item.GetType().Name.ToLower());
        }
    }
}
