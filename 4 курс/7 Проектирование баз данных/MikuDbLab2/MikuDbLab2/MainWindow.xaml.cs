using MahApps.Metro.Controls;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MikuDbLab2;

public partial class MainWindow : MetroWindow
{
    #region INIT
    private readonly DataTable SourceTable = new();

    public MainWindow()
    {
        InitializeComponent();
        Title = $"МикуцкихПрБД_{TableTextBox.Text}";
    }

    private string TableName => string.IsNullOrWhiteSpace(TableTextBox.Text) ? "STUDENTS" : TableTextBox.Text.Trim();
    private const string ID = "id";
    private const string LAST_NAME = "last_name";
    private const string BIRTHDAY = "birthday";
    private const string MATHEMATIC = "math";
    private const string INFORMATION_TECH = "info";
    private const string FOREIGN_LAN = "forlan";
    private const string GENDER = "GENDER";
    private const string GRADE = "GRADE";
    private const string BASEGRANT = "BASEGRANT";
    private const string GRANT = "GRANT";
    #endregion

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(NumberTextBox.Text))
            {
                throw new ArgumentNullException(nameof(NumberTextBox),
                    "Передано пустое значение новой базовой стипендии.");
            }

            string sqlSetNull = $"""
                UPDATE "{TableName}"
                SET "{GRADE}" = NULL, "{BASEGRANT}" = NULL, "{GRANT}" = NULL
             """;

            var sqlCalcGrade = $"""
                UPDATE "{TableName}"
                SET "{GRADE}" = ("{MATHEMATIC}" + "{INFORMATION_TECH}" + "{FOREIGN_LAN}") / 3
            """;

            if (!int.TryParse(NumberTextBox.Text, out var i))
            {
                throw new ArgumentException(nameof(NumberTextBox),
                        "Не удалось конвертировать значение новой базовой стипендии.");
            }

            var newBaseGrant = new OracleCommand($"UPDATE \"{TableName}\" SET \"{BASEGRANT}\" = :NEWBASEGRANT");
            newBaseGrant.Parameters.Add(new("NEWBASEGRANT", i));

            string sqlCalcGrant = $"""
                UPDATE "{TableName}" SET "{GRANT}" = CASE
                    WHEN "{GRADE}" >= 4.5 THEN :NEWBASEGRANT * 1.5 
                    WHEN "{GRADE}" >= 3.5 AND "{GRADE}" < 4.5 THEN :NEWBASEGRANT * 1.25
                    WHEN "{GRADE}" >= 2.5 AND "{GRADE}" < 3.5 THEN :NEWBASEGRANT
                    ELSE 0
                END
             """;

            var calcCmd = new OracleCommand(sqlCalcGrant)
            {
                BindByName = true
            };
            calcCmd.Parameters.Add(new("NEWBASEGRANT", i));

            UpdateContext([
                new(sqlSetNull),
                new(sqlCalcGrade),
                newBaseGrant,
                calcCmd,
            ]);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e) => UpdateContext();

    private void UpdateContext(IEnumerable<OracleCommand>? commands = null)
    {
        try
        {
            SchemaTextBox.Text = string.IsNullOrWhiteSpace(SchemaTextBox.Text) ? "STUDENT" : SchemaTextBox.Text.Trim();
            PasswordTextBox.Text = string.IsNullOrWhiteSpace(PasswordTextBox.Text) ? "root" : PasswordTextBox.Text.Trim();
            PortTextBox.Text = string.IsNullOrWhiteSpace(PortTextBox.Text) ? "9900" : PortTextBox.Text.Trim();
            HostTextBox.Text = string.IsNullOrWhiteSpace(HostTextBox.Text) ? "localhost" : HostTextBox.Text.Trim();
            ServiceTextBox.Text = string.IsNullOrWhiteSpace(ServiceTextBox.Text) ? "XE" : ServiceTextBox.Text.Trim();

            string connectionString =
                $"User Id={SchemaTextBox.Text};" +
                $"Password={PasswordTextBox.Text};" +
                $"Data Source={HostTextBox.Text}:{PortTextBox.Text}/{ServiceTextBox.Text};";

            using var conn = new OracleConnection(connectionString);
            conn.Open();

            if (commands is not null)
            {
                foreach (var command in commands)
                {
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }

            using var cmd = new OracleCommand($"""
                SELECT * FROM "{TableName}"
            """, conn);
                
            var adapter = new OracleDataAdapter(cmd);
            SourceTable.Clear();
            adapter.Fill(SourceTable);
            TableDataGrid.ItemsSource = SourceTable.DefaultView;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OnlyDigits(object sender, TextCompositionEventArgs e)
        => e.Handled = !TrueDigit().IsMatch(e.Text);

    private void TableDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        e.Column.Header = e.PropertyName.Trim() switch
        {
            ID => "Ид.",
            LAST_NAME => "Фамилия",
            BIRTHDAY => "День рождения",
            MATHEMATIC => "Математика",
            INFORMATION_TECH => "Информатика",
            FOREIGN_LAN => "Ин. яз.",
            GENDER => "Пол",
            GRADE => "Ср. успев.",
            BASEGRANT => "Базовая стип.",
            GRANT => "Стипендия",
            _ => "???"
        };

        if (e.PropertyType == typeof(DateTime) && e.Column is DataGridTextColumn c)
        {
            c.Binding.StringFormat = "dd.MM.yyyy";
        }
    }


    [GeneratedRegex("^[0-9]+$")]
    private static partial Regex TrueDigit();
}