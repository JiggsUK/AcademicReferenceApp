using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RefCatalogue.Controllers
{
    internal static class FormHelper
    {
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T t)
                    {
                        yield return t;
                    }

                    foreach (var childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static void ClearAllTextboxes(Grid gridToClear)
        {
            foreach (var item in FindVisualChildren<TextBox>(gridToClear))
            {
                item.Clear();
            }
        }

        public static List<string> ValidateTextboxes(TextBox[] requiredTextboxes)
        {
            return (from box in requiredTextboxes where string.IsNullOrEmpty(box.Text) select $"{box.Name} is required.").ToList();
        }
    }
}