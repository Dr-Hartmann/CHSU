namespace Lab7
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.бдЛаб7DataSet = new Lab7.БдЛаб7DataSet();
            this.tableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableTableAdapter = new Lab7.БдЛаб7DataSetTableAdapters.TableTableAdapter();
            this.кодDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.фамилияDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.датаРожденияDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.математикаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.информатикаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.иностранныйЯзыкDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.бдЛаб7DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.кодDataGridViewTextBoxColumn,
            this.фамилияDataGridViewTextBoxColumn,
            this.датаРожденияDataGridViewTextBoxColumn,
            this.математикаDataGridViewTextBoxColumn,
            this.информатикаDataGridViewTextBoxColumn,
            this.иностранныйЯзыкDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tableBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(833, 348);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Yellow;
            this.button1.Location = new System.Drawing.Point(853, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Создать запись";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.insertButton_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.Location = new System.Drawing.Point(853, 634);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "Очистить БД";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.clearDB_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(853, 672);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(200, 30);
            this.button3.TabIndex = 3;
            this.button3.Text = "Закрыть БД";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.closeDB_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(13, 368);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(833, 335);
            this.dataGridView2.TabIndex = 4;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button4.Location = new System.Drawing.Point(853, 61);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(200, 40);
            this.button4.TabIndex = 5;
            this.button4.Text = "Изменить запись";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.changingEntryButton_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button5.Location = new System.Drawing.Point(853, 109);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(200, 40);
            this.button5.TabIndex = 6;
            this.button5.Text = "Удалить запись";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button6.Location = new System.Drawing.Point(853, 178);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(200, 30);
            this.button6.TabIndex = 7;
            this.button6.Text = "Коду";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.orderByCode_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(892, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Отсортровать по:";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button7.Location = new System.Drawing.Point(853, 216);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(200, 30);
            this.button7.TabIndex = 9;
            this.button7.Text = "Фамилии";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.orderByLastname_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button8.Location = new System.Drawing.Point(853, 254);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(200, 30);
            this.button8.TabIndex = 10;
            this.button8.Text = "Дата рождения";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.orderByBirthdate_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button9.Location = new System.Drawing.Point(853, 292);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(200, 30);
            this.button9.TabIndex = 11;
            this.button9.Text = "Математике";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.orderByMath_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button10.Location = new System.Drawing.Point(853, 330);
            this.button10.Margin = new System.Windows.Forms.Padding(4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(200, 30);
            this.button10.TabIndex = 12;
            this.button10.Text = "Информатике";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.orderByInfo_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button11.Location = new System.Drawing.Point(853, 367);
            this.button11.Margin = new System.Windows.Forms.Padding(4);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(200, 30);
            this.button11.TabIndex = 13;
            this.button11.Text = "Ин. языку";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.orderByForlan_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(918, 402);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Запросы:";
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button12.Location = new System.Drawing.Point(853, 581);
            this.button12.Margin = new System.Windows.Forms.Padding(4);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(200, 45);
            this.button12.TabIndex = 18;
            this.button12.Text = "студентов, чей возраст старше 20 лет";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.queryStudentsAgeMore20_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button13.Location = new System.Drawing.Point(853, 528);
            this.button13.Margin = new System.Windows.Forms.Padding(4);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(200, 45);
            this.button13.TabIndex = 17;
            this.button13.Text = "студентов-отличников по всем предметам";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.queryExcellentStudents_Click);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button14.Location = new System.Drawing.Point(853, 475);
            this.button14.Margin = new System.Windows.Forms.Padding(4);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(200, 45);
            this.button14.TabIndex = 16;
            this.button14.Text = "студентов-отличников по математике";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.queryExcellentMathStudents_Click);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button15.Location = new System.Drawing.Point(853, 422);
            this.button15.Margin = new System.Windows.Forms.Padding(4);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(200, 45);
            this.button15.TabIndex = 15;
            this.button15.Text = "с указанием фамилии и даты рождения\r\n";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.queryStudentsLastnameBirthdate_Click);
            // 
            // бдЛаб7DataSet
            // 
            this.бдЛаб7DataSet.DataSetName = "БдЛаб7DataSet";
            this.бдЛаб7DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableBindingSource
            // 
            this.tableBindingSource.DataMember = "Table";
            this.tableBindingSource.DataSource = this.бдЛаб7DataSet;
            // 
            // tableTableAdapter
            // 
            this.tableTableAdapter.ClearBeforeFill = true;
            // 
            // кодDataGridViewTextBoxColumn
            // 
            this.кодDataGridViewTextBoxColumn.DataPropertyName = "Код";
            this.кодDataGridViewTextBoxColumn.HeaderText = "Код";
            this.кодDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.кодDataGridViewTextBoxColumn.Name = "кодDataGridViewTextBoxColumn";
            this.кодDataGridViewTextBoxColumn.ReadOnly = true;
            this.кодDataGridViewTextBoxColumn.Width = 125;
            // 
            // фамилияDataGridViewTextBoxColumn
            // 
            this.фамилияDataGridViewTextBoxColumn.DataPropertyName = "Фамилия";
            this.фамилияDataGridViewTextBoxColumn.HeaderText = "Фамилия";
            this.фамилияDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.фамилияDataGridViewTextBoxColumn.Name = "фамилияDataGridViewTextBoxColumn";
            this.фамилияDataGridViewTextBoxColumn.ReadOnly = true;
            this.фамилияDataGridViewTextBoxColumn.Width = 125;
            // 
            // датаРожденияDataGridViewTextBoxColumn
            // 
            this.датаРожденияDataGridViewTextBoxColumn.DataPropertyName = "Дата рождения";
            this.датаРожденияDataGridViewTextBoxColumn.HeaderText = "Дата рождения";
            this.датаРожденияDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.датаРожденияDataGridViewTextBoxColumn.Name = "датаРожденияDataGridViewTextBoxColumn";
            this.датаРожденияDataGridViewTextBoxColumn.ReadOnly = true;
            this.датаРожденияDataGridViewTextBoxColumn.Width = 125;
            // 
            // математикаDataGridViewTextBoxColumn
            // 
            this.математикаDataGridViewTextBoxColumn.DataPropertyName = "Математика";
            this.математикаDataGridViewTextBoxColumn.HeaderText = "Математика";
            this.математикаDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.математикаDataGridViewTextBoxColumn.Name = "математикаDataGridViewTextBoxColumn";
            this.математикаDataGridViewTextBoxColumn.ReadOnly = true;
            this.математикаDataGridViewTextBoxColumn.Width = 125;
            // 
            // информатикаDataGridViewTextBoxColumn
            // 
            this.информатикаDataGridViewTextBoxColumn.DataPropertyName = "Информатика";
            this.информатикаDataGridViewTextBoxColumn.HeaderText = "Информатика";
            this.информатикаDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.информатикаDataGridViewTextBoxColumn.Name = "информатикаDataGridViewTextBoxColumn";
            this.информатикаDataGridViewTextBoxColumn.ReadOnly = true;
            this.информатикаDataGridViewTextBoxColumn.Width = 125;
            // 
            // иностранныйЯзыкDataGridViewTextBoxColumn
            // 
            this.иностранныйЯзыкDataGridViewTextBoxColumn.DataPropertyName = "Иностранный язык";
            this.иностранныйЯзыкDataGridViewTextBoxColumn.HeaderText = "Иностранный язык";
            this.иностранныйЯзыкDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.иностранныйЯзыкDataGridViewTextBoxColumn.Name = "иностранныйЯзыкDataGridViewTextBoxColumn";
            this.иностранныйЯзыкDataGridViewTextBoxColumn.ReadOnly = true;
            this.иностранныйЯзыкDataGridViewTextBoxColumn.Width = 125;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 715);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.бдЛаб7DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private БдЛаб7DataSet бдЛаб7DataSet;
        private System.Windows.Forms.BindingSource tableBindingSource;
        private БдЛаб7DataSetTableAdapters.TableTableAdapter tableTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn кодDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn фамилияDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn датаРожденияDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn математикаDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn информатикаDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn иностранныйЯзыкDataGridViewTextBoxColumn;
    }
}

