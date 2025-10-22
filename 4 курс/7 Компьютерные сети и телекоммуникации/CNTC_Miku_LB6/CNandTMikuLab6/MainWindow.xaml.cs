using CNandTMikuLab6.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static System.Diagnostics.Process;

namespace CNandTMikuLab6;

public partial class MainWindow : Window
{
    public ObservableCollection<Item> Items { get; set; } = [];

    #region init
    public MainWindow() => InitializeComponent();
    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        _windowReference.Owner = this;
    }
    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        Application.Current.Shutdown();
    }
    #endregion

    private void Grid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
    {
        if (e.Row.IsNewItem && Item.MaxCount < (SegmentsTable.Items.Count - 1))
        {
            e.Cancel = true;
        }
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            (var first, var last) = CheckItems();
            (var PDV, var PVV) = CalcPdvPvv(first, last);
            (var sbOut, var canBuild) = GetStringOut(PDV, PVV);
            MessageBox.Show(sbOut, "Результаты", MessageBoxButton.OK,
                canBuild ? MessageBoxImage.Information : MessageBoxImage.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private void ClearSegmentsTable(object sender, RoutedEventArgs e) => Items.Clear();
    private void OpenReference(object sender, RoutedEventArgs e) => _windowReference.Show();
    private static readonly Reference _windowReference = new();
    private void NavigateSource(object sender, RoutedEventArgs e)
    {
        Start(new System.Diagnostics.ProcessStartInfo(
            "https://www.ixbt.com/comm/net_work.html")
        { UseShellExecute = true, });
        Start(new System.Diagnostics.ProcessStartInfo(
            "https://kit68.ru/wp-content/uploads/2020/02/Metodichka_lab_rab_Komp_seti.pdf")
        { UseShellExecute = true, });
    }
    private void Exit(object sender, RoutedEventArgs e) => Close();

    #region PDVPVV
    private (Item?, Item?) CheckItems()
    {
        const string stopWord = "10Base-FB";
        if (Items is null || !Items.Any())
        {
            throw new ArgumentNullException(nameof(Items), "Сеть не должна быть пустой!");
        }

        var first = Items.FirstOrDefault();
        var last = Items.LastOrDefault();
        if (first?.Obj?.Item?.Name == stopWord
            || last?.Obj?.Item?.Name == stopWord)
        {
            throw new ArgumentException($"Крайний сегмент не должен быть '{stopWord}' !", nameof(Items));
        }

        return (first, last);
    }
    private (double, double) CalcPdvPvv(Item? first, Item? last)
    {
        var PDV = 0D;
        var PVV = 0D;

        foreach (var i in Items)
        {
            if (i.Length < Item.DefaultLength)
            {
                throw new ArgumentException($"Длина сегмента {i.Length} должна быть больше базового значения {Item.DefaultLength}!");
            }

            if (i.Length > i.Obj?.MaxLength_m)
            {
                throw new ArgumentException($"Сегмент '{i.Obj.Item?.ToString()}' " +
                    $"должен быть короче {i.Obj.MaxLength_m}м.!");
            }

            double? base_bt = i.Equals(first)
                ? i.Obj?.LeftBase_bt : (i.Equals(last)
                ? i.Obj?.RightBase_bt
                : i.Obj?.MidBase_bt);

            PDV += (base_bt + i.Length * i.Obj?.PropDelayPerMeter_bt) ?? 0D;
            PVV += (i.Equals(first)
                ? i.Obj?.Leading : (!i.Equals(last)
                ? i.Obj?.Intermediate
                : null)) ?? 0D;
        }

        return (PDV, PVV);
    }
    private (string, bool) GetStringOut(double PDV, double PVV)
    {
        var sbOut = new StringBuilder();
        var canBuild = true;

        sbOut.AppendLine($"PDV (время двойного оборота сигнала) = {PDV:F2}.{Environment.NewLine}");
        sbOut.AppendLine($"PVV (уменьшение межкадрового интервала) = {PVV:F2}.{Environment.NewLine}");

        if (PDV > 575)
        {
            sbOut.AppendLine($"{Environment.NewLine}PDV превышает допустимое значение {575} битовых интервала.");
            canBuild = false;
        }
        else
        {
            sbOut.Append($"{Environment.NewLine}PDV не превышает допустимое значение {575} битовых интервала");

            var length = Items.Sum(s => s.Length);
            if (length > 2500)
            {
                sbOut.Append($", и несмотря на то, что общая длина сети {length} превышает {2500}");
            }

            var count = Items.Count - 1;
            if (count > 4)
            {
                sbOut.Append($", а количество повторителей {count} больше {4}");
            }

            sbOut.AppendLine(", эта сеть проходит по критерию времени двойного оборота сигнала.");
        }

        if (PVV > 49)
        {
            sbOut.AppendLine($"{Environment.NewLine}PVV превышает допустимое значение {49} битовых интервала.");
            canBuild = false;
        }
        else
        {
            sbOut.AppendLine($"{Environment.NewLine}PVV не превышает допустимое значение {49} битовых интервала.");
        }

        if (!canBuild) sbOut.AppendLine($"{Environment.NewLine}Сеть не может быть построена.");
        else sbOut.AppendLine($"{Environment.NewLine}Сеть может быть построена.");

        return (sbOut.ToString(), canBuild);
    }
    #endregion
}
