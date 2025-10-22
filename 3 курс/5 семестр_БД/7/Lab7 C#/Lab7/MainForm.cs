using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Lab7
{
    public partial class MainForm : Form
    {
        public static string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/БдЛаб7.accdb;";
        private OleDbConnection _myConnection;

        public MainForm()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            InitializeComponent();
            _myConnection = new OleDbConnection(connectionString);
        }

        // загрузка формы
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "бдЛаб7DataSet.Table". При необходимости она может быть перемещена или удалена.
            this.tableTableAdapter.Fill(this.бдЛаб7DataSet.Table);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "бдЛаб7DataSet.Table". При необходимости она может быть перемещена или удалена.
            this.tableTableAdapter.Fill(this.бдЛаб7DataSet.Table);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "бдЛаб7DataSet.Table". При необходимости она может быть перемещена или удалена.
            this.tableTableAdapter.Fill(this.бдЛаб7DataSet.Table);
            _myConnection.Open();
            ProcessQuery();
        }

        // закрытие формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_myConnection != null && _myConnection.State == ConnectionState.Open)
            {
                this.tableTableAdapter.Update(this.бдЛаб7DataSet.Table);
                _myConnection.Close();
            }
        }

        private void ProcessQuery(string q = "SELECT * FROM [Table]")
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(q, _myConnection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        // создание записи
        private void insertButton_Click(object sender, EventArgs e)
        {
            var inputForm = new InputForm();
            if (inputForm.ShowDialog() != DialogResult.OK) return;

            string query = "INSERT INTO [Table] ([Фамилия], [Дата рождения], [Математика], [Информатика]," +
                "[Иностранный язык]) VALUES (@lastName, @birthdate, @math, @info, @forlan)";
            OleDbCommand command = new OleDbCommand(query, _myConnection);

            command.Parameters.AddWithValue("@lastName", inputForm.GetLastname);
            command.Parameters.AddWithValue("@birthdate", inputForm.GetBirthdate.ToOADate());
            command.Parameters.AddWithValue("@math", inputForm.GetMath);
            command.Parameters.AddWithValue("@info", inputForm.GetInfo);
            command.Parameters.AddWithValue("@forlan", inputForm.GetForlan);
            command.ExecuteNonQuery();

            ProcessQuery();
        }

        // изменение записи
        private void changingEntryButton_Click(object sender, EventArgs e)
        {
            var changingEntry = new ChangingEntry();
            if (changingEntry.ShowDialog() != DialogResult.OK) return;

            string queryReader = "SELECT * FROM [Table] WHERE [Код] = @code";
            OleDbCommand commandReader = new OleDbCommand(queryReader, _myConnection);
            commandReader.Parameters.AddWithValue("@code", changingEntry.GetCode);
            OleDbDataReader reader = commandReader.ExecuteReader();

            if (!reader.HasRows)
            {
                DialogResult error = MessageBox.Show("Такого кода не существует", "Ошибка ввода", MessageBoxButtons.OK);
                if (error == DialogResult.OK) return;
            }

            reader.Read();
            var lr2 = reader.GetString(1);
            var br2 = reader.GetDateTime(2);
            var m2 = reader.GetByte(3);
            var i2 = reader.GetByte(4);
            var fl2 = reader.GetByte(5);
            reader.Close();

            var inputForm2 = new InputForm(lr2, br2, m2, i2, fl2);
            if (inputForm2.ShowDialog() != DialogResult.OK) return;

            string query = $"UPDATE [Table] SET [Фамилия] = @lastName, [Дата рождения] = @birthdate," +
                $"[Математика] = @math, [Информатика] = @info, [Иностранный язык] = @forlan WHERE [Код] = @code";
            OleDbCommand command = new OleDbCommand(query, _myConnection);

            command.Parameters.AddWithValue("@lastName", inputForm2.GetLastname);
            command.Parameters.AddWithValue("@birthdate", inputForm2.GetBirthdate.ToOADate());
            command.Parameters.AddWithValue("@math", inputForm2.GetMath);
            command.Parameters.AddWithValue("@info", inputForm2.GetInfo);
            command.Parameters.AddWithValue("@forlan", inputForm2.GetForlan);
            command.Parameters.AddWithValue("@code", changingEntry.GetCode);
            command.ExecuteNonQuery();

            ProcessQuery();
        }

        // удаление записи
        private void deleteButton_Click(object sender, EventArgs e)
        {
            var deleteEntry = new ChangingEntry();
            if (deleteEntry.ShowDialog() != DialogResult.OK) return;

            string query = "DELETE FROM [Table] WHERE [Код] = @code";
            OleDbCommand command = new OleDbCommand(query, _myConnection);
            command.Parameters.AddWithValue("@code", deleteEntry.GetCode);
            command.ExecuteNonQuery();

            ProcessQuery();
        }

        // сортровка БД
        private void orderByCode_Click(object sender, EventArgs e)
        {
            ProcessQuery("SELECT * FROM [Table] ORDER BY [Код]");
        }
        
        private void orderByLastname_Click(object sender, EventArgs e)
        {
            ProcessQuery("SELECT * FROM [Table] ORDER BY [Фамилия]");
        }
        
        private void orderByBirthdate_Click(object sender, EventArgs e)
        {
            ProcessQuery("SELECT * FROM [Table] ORDER BY [Дата рождения]");
        }
        
        private void orderByMath_Click(object sender, EventArgs e)
        {
            ProcessQuery("SELECT * FROM [Table] ORDER BY [Математика]");
        }
        
        private void orderByInfo_Click(object sender, EventArgs e)
        {
            ProcessQuery("SELECT * FROM [Table] ORDER BY [Информатика]");
        }
        
        private void orderByForlan_Click(object sender, EventArgs e)
        {
            ProcessQuery("SELECT * FROM [Table] ORDER BY [Иностранный язык]");
        }

        private void ProcessSelectionQuery(string q = "SELECT * FROM [Table]")
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(q, _myConnection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        // запрос студентов с указанием фамилии и даты рождения
        private void queryStudentsLastnameBirthdate_Click(object sender, EventArgs e)
        {
            var inputLastnameBirthdateForm = new InputLastnameBirthdateForm();
            if (inputLastnameBirthdateForm.ShowDialog() != DialogResult.OK) return;

            string query = "SELECT * FROM [Table] WHERE [Фамилия] = @lastname AND [Дата рождения] = @birthdate";
            OleDbCommand command = new OleDbCommand(query, _myConnection);
            command.Parameters.AddWithValue("@lastName", inputLastnameBirthdateForm.GetLastname);
            command.Parameters.AddWithValue("@birthdate", inputLastnameBirthdateForm.GetBirthdate.ToOADate());

            OleDbDataAdapter adapter = new OleDbDataAdapter(query, _myConnection);
            DataTable dataTable = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        // запрос студентов-отличников по математике
        private void queryExcellentMathStudents_Click(object sender, EventArgs e)
        {
            ProcessSelectionQuery("SELECT [Фамилия], [Математика] FROM [Table] WHERE [Математика] = 5");
        }

        // запрос студентов-отличников по всем предметам
        private void queryExcellentStudents_Click(object sender, EventArgs e)
        {
            ProcessSelectionQuery("SELECT [Фамилия], [Математика], [Информатика], [Иностранный язык] FROM [Table]" +
                "WHERE [Математика] = 5 AND [Информатика] = 5 AND [Иностранный язык] = 5");
        }

        // запрос студентов, чей возраст старше 20 лет
        private void queryStudentsAgeMore20_Click(object sender, EventArgs e)
        {
            ProcessSelectionQuery("SELECT [Фамилия], [Дата рождения] FROM [Table] WHERE [Дата рождения] > 20");
        }

        // очистка БД
        private void clearDB_Click(object sender, EventArgs e)
        {
            ProcessQuery("DELETE * FROM [Table]");
        }

        // закрыть БД и форму
        private void closeDB_Click(object sender, EventArgs e)
        {
            this.tableTableAdapter.Update(this.бдЛаб7DataSet.Table);
            _myConnection.Close();
            this.Close();
        }

        // выравнивание по ширине всей таблицы
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}