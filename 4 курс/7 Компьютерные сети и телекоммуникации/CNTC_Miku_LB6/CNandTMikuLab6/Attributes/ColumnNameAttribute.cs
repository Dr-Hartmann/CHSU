namespace CNandTMikuLab6.Attributes;

[AttributeUsage(AttributeTargets.All)]
internal class ColumnNameAttribute(string Name) : Attribute
{
    public string Name { get; set; } = Name;
}
