using System.Drawing;
using System.Windows.Forms;

namespace Lab7
{
    internal class ChangingEntry : Form
    {
        NumericUpDown numeric;

        public ChangingEntry()
        {
            this.Width = 250;
            this.Height = 100;

            var numl = new System.Windows.Forms.Label(); numl.Text = "Введите код студента";
            numl.Size = new Size(numl.PreferredWidth, numl.PreferredHeight);
            numeric = new NumericUpDown(); numeric.Top = 30;

            this.Controls.Add(numl);
            this.Controls.Add(numeric);

            var buttonOk = new Button() { Text = "OK", Left = 150, Top = 30 };
            this.Controls.Add(buttonOk);
            buttonOk.Click += (sender, args) => { this.DialogResult = DialogResult.OK; this.Close(); };
        }

        public decimal GetCode => numeric.Value;
    }
}
