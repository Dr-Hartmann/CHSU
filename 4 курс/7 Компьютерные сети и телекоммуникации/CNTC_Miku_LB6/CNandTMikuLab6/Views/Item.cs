using CNandTMikuLab6.Attributes;
using CNandTMikuLab6.ViewModels;
using System.Collections.ObjectModel;

namespace CNandTMikuLab6.Views;

public class Item
{
    [ColumnName("Сегмент")]
    public NetworkViewModel? Obj { get; set; }

    [ColumnName("Длина сегмента")]
    public double Length { get; set; } = DefaultLength;

    public static int MaxCount => 10; // 1024

    public static ObservableCollection<NetworkViewModel> ListData { get; } =
    [
        new() { Item = new("10Base-5", "Толстый коаксиальный кабель RG-8/11 (\"жёлтый\" Ethernet)", 11.8, 46.5, 169.5, 0.0866, 500, 16, 11) },
        new() { Item = new("10Base-2", "Тонкий коаксиальный кабель RG-58/U или RG-58A/U", 11.8, 46.5, 169.5, 0.1026, 185, 16, 11) },
        new() { Item = new("10Base-T", "Витая пара UTP Cat3/4/5", 15.3, 42.0, 165.0, 0.1130, 100, 10.5, 8) },
        new() { Item = new("10Base-FB", "Многомодовое оптоволокно", 0, 24.0, 0, 0.1000, 2000, null, 2) },
        new() { Item = new("10Base-FL", "Многомодовое оптоволокно", 12.3, 33.5, 156.5, 0.1000, 2000, 10.5, 8) },
        //new() { Item = new("FOIRL","Серийный оптоволоконный интерфейс", 7.8, 29.0, 152.0, 0.1000, 1000) },
        //new() { Item = new("AUI (>2м.)","Attachment Unit Interface", 0, 0, 0, 0.1026, 48) },
    ];

    public static double DefaultLength => 0.01D;
}
