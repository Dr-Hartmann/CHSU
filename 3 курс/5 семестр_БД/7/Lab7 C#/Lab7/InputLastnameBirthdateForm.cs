using System.Windows.Forms;
using System;

namespace Lab7
{
    internal class InputLastnameBirthdateForm : Form
    {
        private TextBox _lastname = new TextBox();
        private DateTimePicker _birthdate = new DateTimePicker();

        private Label _lnl = new Label();
        private Label _bdl = new Label();

        public InputLastnameBirthdateForm()
        {
            setSize();
            formatFields();
            createExitButton();
        }

        private void setSize()
        {
            this.Width = 400;
            this.Height = 200;
            this.CenterToScreen();
        }

        private void formatFields()
        {
            _lastname.Left = 120; 
            _lastname.Text = "Фамилия";
            _birthdate.Top = 30; _birthdate.Left = 120;
            _birthdate.Value = _birthdate.MinDate;

            _lnl.Text = "Фамилия";
            _bdl.Text = "Дата рождения"; _bdl.Top = 30;

            this.Controls.Add(_lastname);
            this.Controls.Add(_birthdate);
            this.Controls.Add(_lnl);
            this.Controls.Add(_bdl);
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
    }
}