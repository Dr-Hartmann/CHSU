using MahApps.Metro.Controls;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Concurrent;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace MikuDbLab3;

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
        Title = $"{Title}_{TableName}_DEBUG";
#endif
    }

    private string TableName => string.IsNullOrWhiteSpace(TableTextBox.Text) ? "LAB3" : TableTextBox.Text.Trim();
    private const string ID = "id";
    private const string LNAME = "LNAME";
    private const string FNAME = "FNAME";
    private const string STREET = "STREET";
    private const string HOUSE = "HOUSE";
    private const string APART = "APART";
    private const string SCHOLARSHIP = "SCHOLARSHIP";

    private string ConnectionString => $"""
        User Id={CheckField(SchemaTextBox, "STUDENT")};
        Password={CheckField(PasswordTextBox, "root")};
        Data Source={CheckField(HostTextBox, "localhost")}:{CheckField(PortTextBox, "9900")}/{CheckField(ServiceTextBox, "XE")};
     """;

    private static string CheckField(TextBox field, string defaultValue)
        => string.IsNullOrWhiteSpace(field.Text) ? defaultValue : field.Text.Trim();

    private readonly DataTable SourceTable = new();
    private readonly Random Rand = new();
    #endregion

    private async Task UpdateContext(IEnumerable<string> commands, CancellationToken token = default)
    {
        try
        {
            using var conn = new OracleConnection(ConnectionString);
            await conn.OpenAsync(token);

            foreach (var sql in commands)
            {
                using var cmd = new OracleCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync(token);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task UpdateContextParallel(IEnumerable<string> commands, CancellationToken token = default)
    {
        var errors = new ConcurrentBag<string>();
        var semaphore = new SemaphoreSlim(4);

        var tasks = commands.Chunk(500).Select(async batch =>
        {
            await semaphore.WaitAsync(token);
            try
            {
                using var conn = new OracleConnection(ConnectionString);
                await conn.OpenAsync(token);

                using var transaction = conn.BeginTransaction();
                try
                {
                    foreach (var sql in batch)
                    {
                        using var cmd = new OracleCommand(sql, conn) { Transaction = transaction };
                        await cmd.ExecuteNonQueryAsync(token);
                    }
                    await transaction.CommitAsync(token);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(token);
                    errors.Add(ex.Message);
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks);

        if (!errors.IsEmpty)
        {
            MessageBox.Show(string.Join(Environment.NewLine, errors), "Ошибки при обновлении", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task UpdateTable(CancellationToken token = default)
    {
        SourceTable.Clear();
        using var conn = new OracleConnection(ConnectionString);
        await conn.OpenAsync(token);
        using var cmd = new OracleCommand($"SELECT * FROM {TableName}", conn);
        using var adapter = new OracleDataAdapter(cmd);
        adapter.Fill(SourceTable);
        TableDataGrid.ItemsSource = SourceTable.DefaultView;
    }

    private void TableDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        e.Column.Header = e.PropertyName.Trim() switch
        {
            ID => "Ид.",
            LNAME => "Фамилия",
            FNAME => "Имя",
            STREET => "Улица",
            HOUSE => "н. Дома",
            APART => "Квартира",
            SCHOLARSHIP => "Стипендия",
            _ => "???"
        };
    }

    private async void Button_Click(object sender, RoutedEventArgs e) => await UpdateTable();

    private async void ReName_Click(object sender, RoutedEventArgs e)
    {
        var sql1 = $"""
            UPDATE {TableName}
            SET {HOUSE} = {HOUSE} - 50
            WHERE UPPER(TRIM({STREET})) = 'ЭНГЕЛЬСА'
                AND {HOUSE} > 50
         """;

        var sql2 = $"""
            UPDATE {TableName}
            SET {HOUSE} = 22
            WHERE UPPER(TRIM({STREET})) = 'ЭНГЕЛЬСА'
                AND ({HOUSE} IS NULL OR {HOUSE} = 0 
                OR {APART} IS NULL OR {APART} = 0)
         """;

        var sql = $"""
            UPDATE {TableName}
            SET {STREET} = 'Милютина'
            WHERE UPPER(TRIM({STREET})) = 'ЭНГЕЛЬСА'
         """;

        await UpdateContext([sql1, sql2, sql]);
        await UpdateTable();
    }

    private async void ReverseReName_Click(object sender, RoutedEventArgs e)
    {
        var sql2 = $"""
            UPDATE {TableName}
            SET {HOUSE} = NULL
            WHERE UPPER(TRIM({STREET})) = 'МИЛЮТИНА'
                AND {HOUSE} = 22
                AND ({APART} IS NULL OR {APART} = 0)
        """;

        var sql1 = $"""
            UPDATE {TableName}
            SET {HOUSE} = {HOUSE} + 50
            WHERE UPPER(TRIM({STREET})) = 'МИЛЮТИНА' 
                AND {HOUSE} > 0
         """;

        var sql = $"""
            UPDATE {TableName}
            SET {STREET} = 'Энгельса'
            WHERE UPPER(TRIM({STREET})) = 'МИЛЮТИНА'
         """;

        await UpdateContext([sql2, sql1, sql]);
        await UpdateTable();
    }

    private async void Lottery_Click(object sender, RoutedEventArgs e)
    {
        int GetScalar(string sql)
        {
            using var conn = new OracleConnection(ConnectionString);
            conn.Open();
            using var cmd = new OracleCommand(sql, conn);
            var x = cmd.ExecuteScalar();
            return Convert.ToInt32(x ?? 1);
        }

        try
        {
            var M = GetScalar($"SELECT MAX({HOUSE}) FROM {TableName}");
            var N = GetScalar($"SELECT MAX({APART}) FROM {TableName}");

            var arr = new int[M, N];
            for (var r1 = 0; r1 < M; ++r1)
            {
                for (var r2 = 0; r2 < N; ++r2)
                {
                    arr[r1, r2] = Rand.Next(1, 101);
                }
            }

            var cmds = new ConcurrentBag<string>();

            for (int i = 100000; i > 0; --i)
            {
                var m = Rand.Next(1, M + 1);
                var n = Rand.Next(1, N + 1);
                var value = arr[m - 1, n - 1];

                cmds.Add($"""
                    UPDATE {TableName}
                    SET {SCHOLARSHIP} = {SCHOLARSHIP} + {value}
                    WHERE (
                        EXISTS (
                            SELECT 1
                            FROM {TableName}
                            WHERE {HOUSE} = {m}
                                AND {APART} = {n}
                        )
                        AND {HOUSE} = {m}
                        AND {APART} = {n}
                    )
                    OR (
                        NOT EXISTS (
                            SELECT 1
                            FROM {TableName}
                            WHERE {HOUSE} = {m}
                              AND {APART} = {n}
                        )
                    )
                 """
                );
            }

            using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
            await UpdateContextParallel(cmds, cts.Token);
            await UpdateTable();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void DropLottery_Click(object sender, RoutedEventArgs e)
    {
        await UpdateContext([new($"UPDATE {TableName} SET {SCHOLARSHIP} = 0")]);
        await UpdateTable();
    }
}
