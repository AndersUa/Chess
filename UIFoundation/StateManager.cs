using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UIFoundation
{
    //is working outside control template 
    //TODO: create different method for two cases.(in data template and outside)
    public class StateManager : DependencyObject
    {
        public static string GetVisualStateProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(VisualStatePropertyProperty);
        }

        public static void SetVisualStateProperty(DependencyObject obj, string value)
        {
            obj.SetValue(VisualStatePropertyProperty, value);
        }

        public static readonly DependencyProperty VisualStatePropertyProperty =
            DependencyProperty.RegisterAttached(
            "VisualStateProperty",
            typeof(string),
            typeof(StateManager),
            new PropertyMetadata((s, e) =>
            {
                var propertyName = (string)e.NewValue;
                var ctrl = s as Control;
                if (ctrl == null)
                    throw new InvalidOperationException("This attached property only supports types derived from Control.");
                System.Windows.VisualStateManager.GoToElementState(ctrl, (string)e.NewValue, true);
            }));
    }
}
