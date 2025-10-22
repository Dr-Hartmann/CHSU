using System.Reflection;
using System.Windows.Markup;

namespace CNandTMikuLab6.Attributes;

public class ColumnHeaderExtension(Type type, string property) : MarkupExtension
{
    public Type Type { get; set; } = type;
    public string Property { get; set; } = property;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var prop = Type.GetProperty(Property);
        if (prop == null) return Property;
        var attr = prop.GetCustomAttribute<ColumnNameAttribute>();
        return attr?.Name ?? Property;
    }
}
