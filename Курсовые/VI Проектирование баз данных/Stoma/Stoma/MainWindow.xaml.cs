using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Stoma;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private const string _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=StomaDB.mdb;";
    private DataTable? _currentDataTable;
    private string? _selectedTableName;

    private const string CREATE = "Создать";
    private const string DELETE = "Удалить";
    private const string CHANGE = "Изменить";

    private readonly Dictionary<(string Table, string Action), string> _spesialQueries = new()
    {
        { ("Личности", CREATE), "INSERT INTO [Личности] " +
                                "([Фамилия], [Имя], [Отчество], [Паспорт], [СНИЛС], [ИНН], [Номер телефона], [Электронная почта]) " +
                                "VALUES (?, ?, ?, ?, ?, ?, ?, ?)" },
        { ("Личности", DELETE), "DELETE FROM [Личности] WHERE [Id личности] = ?" },
        { ("Личности", CHANGE), "UPDATE [Личности] " +
                                "SET [Фамилия] = ?, [Имя] = ?, [Отчество] = ?, [Паспорт] = ?, [СНИЛС] = ?, [ИНН] = ?, [Номер телефона] = ?, [Электронная почта] = ? " +
                                "WHERE [Id личности] = ?" },

        { ("Должности", CREATE), "INSERT INTO [Должности] ([Название]) VALUES (?)" },
        { ("Должности", DELETE), "DELETE FROM [Должности] WHERE [Id должности] = ?" },
        { ("Должности", CHANGE), "UPDATE [Должности] SET [Название] = ? WHERE [Id должности] = ?" },

        { ("Кабинеты", CREATE), "INSERT INTO [Кабинеты] ([Номер кабинета], [Название]) VALUES (?, ?)" },
        { ("Кабинеты", DELETE), "DELETE FROM [Кабинеты] WHERE [Номер кабинета] = ?" },
        { ("Кабинеты", CHANGE), "UPDATE [Кабинеты] SET [Номер кабинета] = ?, [Название] = ? WHERE [Номер кабинета] = ?" },

        { ("Прейскурант", CREATE), "INSERT INTO [Прейскурант] ([Название], [Стоимость], [Единица измерения]) VALUES (?, ?, ?)" },
        { ("Прейскурант", DELETE), "DELETE FROM [Прейскурант] WHERE [Id услуги] = ?" },

        { ("Приёмы", CREATE), "INSERT INTO [Приёмы] ([Id сотрудника], [Id пациента], [Дата и время], [Заключение], [Номер кабинета]) VALUES (?, ?, ?, ?, ?)" },
        { ("Приёмы", DELETE), "DELETE FROM [Приёмы] WHERE [Id приёма] = ?" },
        { ("Приёмы", CHANGE), "UPDATE [Приёмы] " +
                              "SET [Id сотрудника] = ?, [Id пациента] = ?, [Дата и время] = ?, [Заключение] = ? " +
                              "WHERE [Id приёма] = ?" },

        { ("Сотрудники", CREATE), "INSERT INTO [Сотрудники] " +
                                  "([Id личности], [Id должности], [Id формы оклада], [Оклад], [Дата заключения], [Договор]) " +
                                  "VALUES (?, ?, ?, ?, ?, ?)" },
        { ("Сотрудники", DELETE), "DELETE FROM [Сотрудники] WHERE [Id сотрудника] = ?" },
        { ("Сотрудники", CHANGE), "UPDATE [Сотрудники] " +
                                  "SET [Id личности] = ?, [Id должности] = ?, [Id формы оклада] = ?, [Оклад] = ?, [Дата заключения] = ?, [Договор] = ? " +
                                  "WHERE [Id сотрудника] = ?" },

        { ("Пациенты", CREATE), "INSERT INTO [Пациенты] " +
                                "([Id личности], [Дата заключения], [Дата окончания], [Противопоказания], " +
                                "[Согласие на обработку персональных данных], [Согласие на медицинское вмешательство]) " +
                                "VALUES (?, ?, ?, ?, ?, ?)" },
        { ("Пациенты", DELETE), "DELETE FROM [Пациенты] WHERE [Id пациента] = ?" },

        { ("Поставки", CREATE), "INSERT INTO [Поставки] ([Название поставщика], [Дата поставки], [Номер телефона]) VALUES (?, ?, ?)" },
        { ("Поставки", DELETE), "DELETE FROM [Поставки] WHERE [Id поставки] = ?" },

        { ("Медицинские карты", CREATE), "INSERT INTO [Медицинские карты] ([Id приёма], [Id услуги]) VALUES (?, ?)" },
    };

    // Инициализация списка всех доступных таблиц
    private void InitTableDefault(object? sender, RoutedEventArgs? e)
    {
        TablesDefault.Items.Clear();

        try
        {
            using var connection = new OleDbConnection(_connectionString);
            connection.Open();
            var tables = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new string?[] { null, null, null, "TABLE" })!;

            if (tables is null)
            {
                MessageBox.Show("Пустое соединение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var tableName in from DataRow? row in tables.Rows
                                      let tableName = row?["TABLE_NAME"].ToString()
                                      where !tableName.StartsWith("MSys")
                                      select tableName)
            {
                TablesDefault.Items.Add(tableName);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка подключения к базе данных:\n\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private readonly Dictionary<string, string> _defaultQueries = new()
    {
        ["Личности"] = "SELECT * FROM [Личности]",
        ["Должности"] = "SELECT * FROM [Должности]",
        ["Прейскурант"] = "SELECT * FROM [Прейскурант]",
        ["Приёмы"] = "SELECT * FROM [Приёмы]",
        ["Стоматологические материалы"] = @$"SELECT * FROM [Стоматологические материалы]",
        ["Инструменты"] = "SELECT * FROM [Инструменты]",
        ["Оборудование"] = "SELECT * FROM [Оборудование]",
        ["Хозяйственные принадлежности"] = "SELECT * FROM [Хозяйственные принадлежности]",
        ["Сотрудники"] = "SELECT * FROM [Сотрудники]",
        ["Медицинские карты"] = "SELECT * FROM [Медицинские карты]",
        ["Кабинеты"] = "SELECT * FROM [Кабинеты]",
        ["Пациенты"] = "SELECT * FROM [Пациенты]",
        ["Статусы ресурсов"] = "SELECT * FROM [Статусы ресурсов]",
        ["Формы оклада"] = "SELECT * FROM [Формы оклада]",
        ["Поставки"] = "SELECT * FROM [Поставки]",
        ["Планы лечения"] = "SELECT * FROM [Планы лечения]",
    };

    // Реакция на выбор запроса таблицы
    private void TablesDefault_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TablesDefault.SelectedItem is not string selectedItem) return;

        try
        {
            if (!_defaultQueries.TryGetValue(_selectedTableName = selectedItem, out var query))
            {
                MessageBox.Show("Запрос не реализован...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            LoadTableData(query);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при загрузке:\n\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void LoadTableData(string query)
    {
        try
        {
            using var connection = new OleDbConnection(_connectionString);
            connection.Open();

            var adapter = new OleDbDataAdapter(query, connection);
            adapter.Fill(_currentDataTable = new());

            Tables.ItemsSource = null;
            Tables.Columns.Clear();

            var foreignTables = LoadDataset(connection);

            foreach (DataColumn column in _currentDataTable.Columns)
            {
                string columnName = column.ColumnName;

                if (foreignTables.ContainsKey((_selectedTableName!, columnName)))
                {
                    var (source, displayMember) = foreignTables[(_selectedTableName!, columnName)];

                    var comboBoxColumn = new DataGridComboBoxColumn
                    {
                        Header = new TextBlock { Text = displayMember },
                        SelectedValuePath = columnName,
                        DisplayMemberPath = displayMember,
                        SelectedValueBinding = new Binding($"[{columnName}]"),
                        ItemsSource = source.DefaultView
                    };

                    Tables.Columns.Add(comboBoxColumn);
                }
                else if (column.DataType == typeof(DateTime))
                {
                    var binding = new Binding($"[{columnName}]");

                    if (columnName.ToLower().Contains("дата")) binding.StringFormat = "dd.MM.yyyy";
                    else binding.StringFormat = "HH:mm";

                    var dateColumn = new DataGridTextColumn
                    {
                        Header = new TextBlock { Text = columnName },
                        Binding = binding
                    };

                    Tables.Columns.Add(dateColumn);
                }
                else
                {
                    var textColumn = new DataGridTextColumn
                    {
                        Header = new TextBlock { Text = columnName },
                        Binding = new Binding($"[{columnName}]")
                    };

                    Tables.Columns.Add(textColumn);
                }
            }

            Tables.ItemsSource = _currentDataTable.DefaultView;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при загрузке таблицы:\n\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private Dictionary<(string Table, string Column), (DataTable Source, string DisplayMember)>
        LoadDataset(OleDbConnection connection)
    {
        var dict = new Dictionary<(string, string), (DataTable, string)>();

        Add("Приёмы", "Id сотрудника",
            "SELECT [Id сотрудника], [Личности].[Фамилия] & ' ' &  [Личности].[Имя] & ' ' &  [Личности].[Отчество] & ': ' & [Должности].[Название] AS [Сотрудник] " +
            "FROM ([Сотрудники] " +
            "INNER JOIN [Личности] ON [Сотрудники].[Id личности] = [Личности].[Id личности]) " +
            "INNER JOIN [Должности] ON [Сотрудники].[Id должности] = [Должности].[Id должности]", "Сотрудник");
        
        Add("Приёмы", "Id пациента",
            "SELECT [Id пациента], [Личности].[Фамилия] & ' ' &  [Личности].[Имя] & ' ' &  [Личности].[Отчество] " +
            "AS [Пациент] FROM [Пациенты] " +
            "INNER JOIN [Личности] ON [Пациенты].[Id личности] = [Личности].[Id личности]", "Пациент");
        
        Add("Приёмы", "Номер кабинета", 
            "SELECT [Номер кабинета], [Номер кабинета] & '\t' &  [Название] AS [Кабинет] FROM [Кабинеты]", "Кабинет");
        
        Add("Сотрудники", "Id личности", 
            "SELECT [Id личности], [Фамилия] & ' ' & [Имя] & ' ' & [Отчество] AS [ФИО] " +
            "FROM [Личности]", "ФИО");
        
        Add("Пациенты", "Id личности", 
            "SELECT [Id личности], [Фамилия] & ' ' & [Имя] & ' ' & [Отчество] AS [ФИО] " +
            "FROM [Личности]", "ФИО");
        
        Add("Сотрудники", "Id должности", 
            "SELECT [Id должности], [Название] AS [Должность] " +
            "FROM [Должности]", "Должность");
        
        Add("Сотрудники", "Id формы оклада", 
            "SELECT [Id формы оклада], [Название] AS [Тип оклада] " +
            "FROM [Формы оклада]", "Тип оклада");

        Add("Медицинские карты", "Id приёма",
            "SELECT [Id приёма], [Личности].[Фамилия] & ' ' &  [Личности].[Имя] & ' ' &  [Личности].[Отчество] & '\t' & [Приёмы].[Дата и время] & '\t' & [Приёмы].[Номер кабинета] AS [Приём] " +
            "FROM ([Приёмы] " +
            "INNER JOIN [Пациенты] ON [Приёмы].[Id пациента] = [Пациенты].[Id пациента]) " +
            "INNER JOIN [Личности] ON [Пациенты].[Id личности] = [Личности].[Id личности]", "Приём");

        Add("Медицинские карты", "Id услуги",
            "SELECT [Id услуги], [Название] & ' - ' & [Стоимость] AS [Услуга]" +
            "FROM [Прейскурант]", "Услуга");

        return dict;

        void Add(string table, string column, string query, string display)
        {
            var tableData = new DataTable();
            var adapter = new OleDbDataAdapter(query, connection);
            adapter.Fill(tableData);

            if (tableData.Rows.Count <= 0)
            {
                MessageBox.Show($"!!! Нет данных для {table}.{column} !!!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            dict[(table, column)] = (tableData, display);
        }
    }

    private readonly Dictionary<string, int> _countOfPrKeys = new()
    {
        ["Личности"] = 1,
        ["Должности"] = 1,
        ["Прейскурант"] = 1,
        ["Приёмы"] = 1,
        ["Стоматологические материалы"] = 1,
        ["Сотрудники"] = 1,
        ["Медицинские карты"] = 0,
        ["Кабинеты"] = 0,
        ["Пациенты"] = 1,
        ["Статусы ресурсов"] = 1,
        ["Формы оклада"] = 1,
        ["Поставки"] = 1,
        ["Планы лечения"] = 1,
    };

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedTableName) || _currentDataTable is null)
        {
            MessageBox.Show("Сначала выберите таблицу!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var changes = _currentDataTable.GetChanges(DataRowState.Added);

            if (changes is null || changes.Rows.Count <= 0)
            {
                MessageBox.Show($"Не введены строки чтобы добавить...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var connection = new OleDbConnection(_connectionString);
            connection.Open();

            foreach (DataRow row in changes.Rows)
            {
                if (!_spesialQueries.TryGetValue((_selectedTableName, CREATE), out var query))
                {
                    MessageBox.Show("Запрос не реализован...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var command = new OleDbCommand(query, connection);

                for (int i = _countOfPrKeys[_selectedTableName]; i < changes.Columns.Count; ++i)
                {
                    command.Parameters.AddWithValue("?", row[i] ?? "");
                }

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении:\n\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        LoadTableData($"SELECT * FROM [{_selectedTableName}]");
    }

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedTableName) || _currentDataTable is null)
        {
            MessageBox.Show("Сначала выберите таблицу!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var changes = _currentDataTable.GetChanges(DataRowState.Modified);

            if (changes is null || changes.Rows.Count <= 0)
            {
                MessageBox.Show("Не выбраны строки чтобы изменить...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var connection = new OleDbConnection(_connectionString);
            connection.Open();

            foreach (DataRow row in changes.Rows)
            {
                if (!_spesialQueries.TryGetValue((_selectedTableName, CHANGE), out var query))
                {
                    MessageBox.Show("Запрос не реализован...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var command = new OleDbCommand(query, connection);

                for (int i = _countOfPrKeys[_selectedTableName]; i < changes.Columns.Count; ++i)
                {
                    command.Parameters.AddWithValue("?", row[i] ?? "");
                }
                command.Parameters.AddWithValue("?", row[0, DataRowVersion.Original] ?? "");

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при изменении:\n\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        LoadTableData($"SELECT * FROM [{_selectedTableName}]");
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedTableName) || _currentDataTable is null)
        {
            MessageBox.Show("Сначала выберите таблицу!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var changes = _currentDataTable.GetChanges(DataRowState.Deleted);

            if (changes is null || changes.Rows.Count <= 0)
            {
                MessageBox.Show("Не выбраны строки чтобы удалить...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var connection = new OleDbConnection(_connectionString);
            connection.Open();

            foreach (DataRow row in changes.Rows)
            {
                if (!_spesialQueries.TryGetValue((_selectedTableName, DELETE), out var query))
                {
                    MessageBox.Show("Запрос не реализован...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("?", row[0, DataRowVersion.Original]);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при удалении:\n\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        LoadTableData($"SELECT * FROM [{_selectedTableName}]");
    }
}