using System.Net.NetworkInformation;
using System.Text;
using System.Windows;

namespace CNTCMikuLB4;

public partial class MainWindow : Window
{
    private const int Count = 4;
    private const int WaitingTime = 1 * 1000;

    public MainWindow() => InitializeComponent();

    private async void Check(object sender, RoutedEventArgs e)
    {
        string hostname = HostnameTextBox.Text.Trim();
        try
        {
            if (string.IsNullOrEmpty(hostname))
            {
                throw new ArgumentNullException(nameof(hostname), $"Поле для ввода IP-адреса пустое!");
            }

            using Ping pingSender = new();
            var options = new PingOptions { DontFragment = true };
            var buffer = Encoding.ASCII.GetBytes("daaaaata");

            for (int i = 0; i < Count; ++i)
            {
                var reply = await pingSender.SendPingAsync(hostname, WaitingTime, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    ResultsTextBox.Text
                        += $"Ping to {hostname} [{reply.Address}]: Успех{Environment.NewLine}"
                        + $"Задержка ответа: {reply.RoundtripTime} мс{Environment.NewLine}"
                        + $"Time-to-Live: {reply.Options?.Ttl ?? 0} прыжков{Environment.NewLine}";
                }
                else
                {
                    ResultsTextBox.Text += $"Подключение отсутствует. Статус: {reply.Status}{Environment.NewLine}";
                }
            }
        }
        catch (PingException pingEx)
        {
            MessageBox.Show($"Ошибка пинга {hostname}: {pingEx.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Clear(object sender, RoutedEventArgs e) => ResultsTextBox.Clear();
}
