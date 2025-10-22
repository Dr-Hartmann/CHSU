using MahApps.Metro.Controls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MikuDbLab4;

public partial class MainWindow : MetroWindow
{
    #region INIT
    public MainWindow() => InitializeComponent();
    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
#if !DEBUG
        ReverseReName.Visibility = System.Windows.Visibility.Hidden;
        Title = $"{Title}_{TableName}_RELEASE";
#else
        Title = $"{Title}_DEBUG";
#endif
    }

    private string TableName => string.IsNullOrWhiteSpace(TableTextBox.Text) ? "MIKULAB4" : TableTextBox.Text.Trim();
    private const string ID = "ID";
    private const string LNAME = "LAST_NAME";
    private const string BIRTHDAY = "BIRTHDAY";
    private const string MATH = "MATH";
    private const string INFO = "INFO";
    private const string FORLAN = "FORLAN";
    private const string GENDER = "GENDER";
    private const string GRADE = "GRADE";
    private const string BASESCHOLARSHIP = "BASESCHOLARSHIP";
    private const string SCHOLARSHIP = "SCHOLARSHIP";

    private string ConnectionString => $"""
        User Id={Check(SchemaTextBox, "STUDENT")};
        Password={Check(PasswordTextBox, "root")};
        Data Source={Check(HostTextBox, "localhost")}:{Check(PortTextBox, "9900")}/{Check(ServiceTextBox, "XE")};
     """;

    private static string Check(TextBox field, string defaultValue)
        => string.IsNullOrWhiteSpace(field.Text) ? defaultValue : field.Text.Trim();

    private async void UpdateTable_Click(object sender, RoutedEventArgs e) => await UpdateTable();

    private readonly DataTable SourceTable = new();
    #endregion

    private void TableDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        e.Column.Header = e.PropertyName.Trim() switch
        {
            ID => "Ид.",
            LNAME => "Фамилия",
            BIRTHDAY => "День рожд.",
            MATH => "Математика",
            INFO => "Информатика",
            FORLAN => "Ин. яз.",
            GENDER => "Пол",
            GRADE => "Ср. усп.",
            BASESCHOLARSHIP => "Базовая стип.",
            SCHOLARSHIP => "Стипендия",
            _ => "???"
        };

        if (e.PropertyType == typeof(DateTime) && e.Column is DataGridTextColumn c)
        {
            c.Binding.StringFormat = "dd.MM.yyyy";
        }
    }

    //private void DataGrid_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
    //{
    //    try
    //    {
    //        if (e.EditAction == DataGridEditAction.Commit)
    //        {
    //            Dispatcher.BeginInvoke(() =>
    //            {
    //                using var conn = new OracleConnection(ConnectionString);
    //                conn.Open();

    //                using var adapter = new OracleDataAdapter($"SELECT * FROM {TableName}", conn);
    //                using var builder = new OracleCommandBuilder(adapter);

    //                adapter.Update(SourceTable);
    //            }, DispatcherPriority.Background);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    //    }
    //}

    private void DataGrid_RowEditEnding(object? sender, DataGridRowEditEndingEventArgs e)
    {
        try
        {
            Dispatcher.BeginInvoke(() =>
            {
                using var conn = new OracleConnection(ConnectionString);
                conn.Open();

                using var adapter = new OracleDataAdapter($"SELECT * FROM {TableName}", conn);
                using var builder = new OracleCommandBuilder(adapter);

                adapter.Update(SourceTable);
            }, DispatcherPriority.Background);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void DataGrid_PreviewKeyDown(object? sender, KeyEventArgs e)
    {
        try
        {
            if (e.Key != Key.Delete || sender is not DataGrid g) return;
            if (g.ItemContainerGenerator.ContainerFromItem(g.CurrentCell.Item) is DataGridRow row && row.IsEditing) return;

            Dispatcher.BeginInvoke(() =>
            {
                using var conn = new OracleConnection(ConnectionString);
                conn.Open();

                using var adapter = new OracleDataAdapter($"SELECT * FROM {TableName}", conn);
                using var builder = new OracleCommandBuilder(adapter);

                adapter.Update(SourceTable);
            }, DispatcherPriority.Background);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task UpdateTable(CancellationToken token = default)
    {
        try
        {
            //TableDataGrid.CellEditEnding -= DataGrid_CellEditEnding;
            TableDataGrid.PreviewKeyDown -= DataGrid_PreviewKeyDown;
            TableDataGrid.RowEditEnding -= DataGrid_RowEditEnding;

            using var conn = new OracleConnection(ConnectionString);
            await conn.OpenAsync(token);
            using var adapter = new OracleDataAdapter($"SELECT * FROM {TableName}", conn);
            SourceTable.Clear();
            adapter.Fill(SourceTable);

            SourceTable.PrimaryKey = [SourceTable.Columns[ID]!];
            TableDataGrid.ItemsSource = SourceTable.DefaultView;

            //TableDataGrid.CellEditEnding += DataGrid_CellEditEnding;
            TableDataGrid.PreviewKeyDown += DataGrid_PreviewKeyDown;
            TableDataGrid.RowEditEnding += DataGrid_RowEditEnding;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка подключения:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}