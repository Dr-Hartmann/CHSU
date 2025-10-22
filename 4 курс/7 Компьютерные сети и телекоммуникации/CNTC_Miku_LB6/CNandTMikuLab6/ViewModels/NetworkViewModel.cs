using CNandTMikuLab6.Attributes;
using CNandTMikuLab6.Models;
using System.ComponentModel;

namespace CNandTMikuLab6.ViewModels;

public class NetworkViewModel : INotifyPropertyChanged
{
    [ColumnName("Тип сегмента")]
    public Network? Item
    {
        get => _item;
        set
        {
            if (_item == null || !_item.Equals(value))
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
                OnPropertyChanged(nameof(CableType));
                OnPropertyChanged(nameof(LeftBase_bt));
                OnPropertyChanged(nameof(MidBase_bt));
                OnPropertyChanged(nameof(RightBase_bt));
                OnPropertyChanged(nameof(PropDelayPerMeter_bt));
                OnPropertyChanged(nameof(MaxLength_m));
                OnPropertyChanged(nameof(Leading));
                OnPropertyChanged(nameof(Intermediate));
            }
        }
    }

    [ColumnName("Тип кабеля")]
    public string? CableType => Item?.CableType;

    [ColumnName("База левого\r\nсегмента (bt)")]
    public double? LeftBase_bt => Item?.LeftBase_bt;

    [ColumnName("База промеж.\r\nсегмента (bt)")]
    public double? MidBase_bt => Item?.MidBase_bt;

    [ColumnName("База правого\r\nсегмента (bt)")]
    public double? RightBase_bt => Item?.RightBase_bt;

    [ColumnName("Задержка среды\r\nна 1м. (bt)")]
    public double? PropDelayPerMeter_bt => Item?.PropDelayPerMeter_bt;

    [ColumnName("Макс. длина\r\nсегмента (м)")]
    public double? MaxLength_m => Item?.MaxLength_m;

    [ColumnName("PVV передающего\r\nсегмента, bt")]
    public double? Leading => Item?.Leading;

    [ColumnName("PVV промежуточного\r\nсегмента, bt")]
    public double? Intermediate => Item?.Intermediate;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    public override string? ToString() => Item?.ToString();

    private Network? _item;
}
