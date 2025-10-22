using System.ComponentModel;
using System.Windows.Controls;

namespace CNandTMikuLab6.Attributes;

internal static class DataGridDecorator
{
    public static void Decorator(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        var desc = e.PropertyDescriptor as PropertyDescriptor;
        if (desc?.Attributes[typeof(ColumnNameAttribute)] is ColumnNameAttribute att)
        {
            e.Column.Header = att.Name;
        }
    }
}
