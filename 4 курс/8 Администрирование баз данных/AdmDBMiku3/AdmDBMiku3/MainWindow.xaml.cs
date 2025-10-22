using MahApps.Metro.Controls;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace AdmDBMiku3;

public partial class MainWindow : MetroWindow
{
    #region INIT
    public MainWindow() => InitializeComponent();
    
    private string ConnectionString => $"""
        User Id={CheckAndReplace(SchemaTextBox, "MIKU8ADMDB")};
        Password={CheckAndReplace(PasswordTextBox, "system")};
        Data Source={CheckAndReplace(HostTextBox, "192.168.100.69")}:{CheckAndReplace(PortTextBox, "1521")}/{CheckAndReplace(ServiceTextBox, "XE")};
        Connection Timeout=5;
     """;
    
    private string TableName => TableTextBox.Text.Trim();

    private readonly DataTable SourceTable = new();
    
    private void TableDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyType == typeof(DateTime) && e.Column is DataGridTextColumn c)
        {
            c.Binding.StringFormat = "dd.MM.yyyy";
        }
    }
    #endregion

    #region BUTTONS
    private void UpdateTable_Click(object sender, RoutedEventArgs e) => UpdateTable();
    
    private void TestConnection_Click(object sender, RoutedEventArgs e)
    {
        if (!ValidateConnectionParameters(HostTextBox, PortTextBox, ServiceTextBox, SchemaTextBox, PasswordTextBox, TableTextBox))
        {
            UpdateStatus("Ошибка: заполните все параметры подключения", Brushes.Red);
            return;
        }

        UpdateStatus("Тестирование подключения...", Brushes.Blue);
        try
        {
            using var conn = new OracleConnection(ConnectionString);
            conn.Open();
            UpdateStatus("Подключение успешно", Brushes.Green);
        }
        catch (Exception ex)
        {
            UpdateStatus($"Ошибка подключения: {ex.Message}", Brushes.Red);
        }
    }
    #endregion

    private void UpdateTable()
    {
        try
        {
            TableDataGrid.ItemsSource = null;
            SourceTable.Constraints.Clear();
            SourceTable.Columns.Clear();
            SourceTable.Rows.Clear();

            using var conn = new OracleConnection(ConnectionString);
            conn.Open();

            using var adapter = new OracleDataAdapter($"SELECT * FROM {TableName}", conn);
            adapter.Fill(SourceTable);

            TableDataGrid.ItemsSource = SourceTable.DefaultView;
            UpdateStatus($"Загружено строк: {SourceTable.Rows.Count}", Brushes.Green);
        }
        catch (Exception ex)
        {
            UpdateStatus($"Ошибка загрузки данных: {ex.Message}", Brushes.Red);
        }
    }

    private void UpdateStatus(string message, Brush? color = null)
    {
        Dispatcher.Invoke(() =>
        {
            StatusTextBlock.Text = message;
            StatusTextBlock.Foreground = color ?? Brushes.Gray;
        });
    }

    #region UTILS
    private static string CheckAndReplace(TextBox field, string defaultValue)
    {
        if (string.IsNullOrWhiteSpace(field.Text))
            field.Text = defaultValue.Trim();
        return field.Text.Trim();
    }
    
    private static bool ValidateConnectionParameters(params TextBox[] fields)
    {
        foreach (var f in fields)
            if (string.IsNullOrWhiteSpace(f.Text))
                return false;
        return true;
    }
    #endregion
}
