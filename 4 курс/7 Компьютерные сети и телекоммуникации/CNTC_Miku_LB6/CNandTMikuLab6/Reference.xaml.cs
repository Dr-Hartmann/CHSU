using CNandTMikuLab6.Attributes;
using CNandTMikuLab6.Views;
using System.ComponentModel;
using System.Windows;

namespace CNandTMikuLab6;

public partial class Reference : Window
{
    public Reference()
    {
        InitializeComponent();
        SegmentsTable.ItemsSource = Item.ListData;
        SegmentsTable.AutoGeneratingColumn += DataGridDecorator.Decorator;

        About.Text =
            $"Ethernet (англ. Ethernet [ˈiːθəˌnɛt] от ether [ˈiːθə] «эфир» + network «сеть, цепь») — семейство технологий пакетной передачи данных между устройствами для компьютерных и промышленных сетей. Протоколы Ethernet работают на канальном и физическом уровне модели OSI, предоставляя средства для передачи данных между устройствами.{Environment.NewLine}{Environment.NewLine}" +
            $"PDV (Path Delay Value) – время удвоенной задержки распространения сигнала между двумя самыми удалёнными друг от друга станциями сети (не больше 575 битовых интервалов).{Environment.NewLine}{Environment.NewLine}" +
            $"Расчёт PDV был обязателен только для ранних спецификаций 10 Мбит/с Ethernet (IEEE 802.3), которые работали в режиме полудуплекса (Half-Duplex) и использовали повторители (repeater).{Environment.NewLine}{Environment.NewLine}" +
            $"PVV (Path Variability Value) – уменьшение межкадрового интервала повторителями. Для расчета PVV также можно воспользоваться значениями макс. величин уменьшения межкадрового интервала при прохождении повторителей различных физических сред.{Environment.NewLine}{Environment.NewLine}" +
            $"PVV помогает системным администраторам и проектировщикам убедиться, что физическая топология сети (длина кабелей, количество и тип активного оборудования, такого как повторители) соответствует установленным стандартам и не приведет к сбоям в работе сети.";
    }
    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        Width = SegmentsTable.Columns.Sum(c => c.ActualWidth) + 50;
    }
    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        e.Cancel = true;
        Hide();
    }
}
