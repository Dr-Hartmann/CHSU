using System;
using System.Windows.Forms;

namespace Lab7
{
    internal class InputForm : Form
    {
        private TextBox _lastname = new TextBox();
        private DateTimePicker _birthdate = new DateTimePicker();
        private NumericUpDown _math = new NumericUpDown();
        private NumericUpDown _info = new NumericUpDown();
        private NumericUpDown _forlan = new NumericUpDown();

        private Label _lnl = new Label();
        private Label _bdl = new Label();
        private Label _ml = new Label();
        private Label _il = new Label();
        private Label _fl = new Label();

        public InputForm()
        {
            setSize();
            formatFields();
            createExitButton();
        }

        public InputForm(string lastname, DateTime birthdate, byte math, byte info, byte forlan)
        {
            setSize();
            formatFields();

            _lastname.Text = lastname;
            _birthdate.Value = birthdate.Date;
            _math.Value = math;
            _info.Value = info;
            _forlan.Value = forlan;

            createExitButton();
        }

        private void setSize()
        {
            this.Width = 400;
            this.Height = 200;
        }

        private void formatFields()
        {
            _lastname.Left = 120;
            _birthdate.Top = 30; _birthdate.Left = 120;
            _math.Top = 60; _math.Left = 120;
            _info.Top = 90; _info.Left = 120;
            _forlan.Top = 120; _forlan.Left = 120;

            _lastname.Text = "Фамилия";
            _birthdate.Value = _birthdate.MinDate;
            _math.Maximum = 5; _math.Minimum = 1;
            _info.Maximum = 5; _info.Minimum = 1;
            _forlan.Maximum = 5; _forlan.Minimum = 1;

            _lnl.Text = "Фамилия";
            _bdl.Text = "Дата рождения"; _bdl.Top = 30;
            _ml.Text = "Математика"; _ml.Top = 60;
            _il.Text = "Информатика"; _il.Top = 90;
            _fl.Text = "Ин. язык"; _fl.Top = 120;

            this.Controls.Add(_lastname);
            this.Controls.Add(_birthdate);
            this.Controls.Add(_math);
            this.Controls.Add(_info);
            this.Controls.Add(_forlan);
            this.Controls.Add(_lnl);
            this.Controls.Add(_bdl);
            this.Controls.Add(_ml);
            this.Controls.Add(_il);
            this.Controls.Add(_fl);
        }

        private void createExitButton()
        {
            var buttonOk = new Button() { Text = "OK", Left = 300 };
            this.Controls.Add(buttonOk);
            buttonOk.Click += (sender, args) =>
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
        }

        public string GetLastname => _lastname.Text;
        public DateTime GetBirthdate => _birthdate.Value.Date;
        public decimal GetMath => _math.Value;
        public decimal GetInfo => _info.Value;
        public decimal GetForlan => _forlan.Value;
    }
}