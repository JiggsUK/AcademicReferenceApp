using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RefCatalogue
{
    internal class FormHelper
    {
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T t)
                    {
                        yield return t;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static void ClearAllTextboxes(Grid gridToClear)
        {
            foreach (TextBox item in FindVisualChildren<TextBox>(gridToClear))
            {
                item.Clear();
            }
        }

        public static List<string> ValidateTextboxes(TextBox[] requiredTextboxes)
        {
            List<string> errors = new List<string>();
            foreach (TextBox box in requiredTextboxes)
            {
                if (string.IsNullOrEmpty(box.Text))
                {
                    errors.Add($"{box.Name} is required.");
                }
            }

            return errors;
        }
    }
}