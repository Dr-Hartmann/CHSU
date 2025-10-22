#pragma once

namespace Автомат {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Сводка для MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
		}

	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::TextBox^ result;
	private: System::Windows::Forms::DataGridView^ dataGridView1;
	private: System::Windows::Forms::Button^ btn_auto;
	private: System::Windows::Forms::Button^ btn_step;
	private: System::Windows::Forms::Button^ reset;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::PictureBox^ pictureBox1;

	protected:

	private:
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		System::ComponentModel::Container^ components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			System::ComponentModel::ComponentResourceManager^ resources = (gcnew System::ComponentModel::ComponentResourceManager(MyForm::typeid));
			this->result = (gcnew System::Windows::Forms::TextBox());
			this->dataGridView1 = (gcnew System::Windows::Forms::DataGridView());
			this->btn_auto = (gcnew System::Windows::Forms::Button());
			this->btn_step = (gcnew System::Windows::Forms::Button());
			this->reset = (gcnew System::Windows::Forms::Button());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->pictureBox1 = (gcnew System::Windows::Forms::PictureBox());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->pictureBox1))->BeginInit();
			this->SuspendLayout();
			// 
			// result
			// 
			this->result->Location = System::Drawing::Point(25, 24);
			this->result->Multiline = true;
			this->result->Name = L"result";
			this->result->Size = System::Drawing::Size(653, 36);
			this->result->TabIndex = 0;
			// 
			// dataGridView1
			// 
			this->dataGridView1->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView1->Location = System::Drawing::Point(25, 134);
			this->dataGridView1->Name = L"dataGridView1";
			this->dataGridView1->Size = System::Drawing::Size(653, 125);
			this->dataGridView1->TabIndex = 1;
			// 
			// btn_auto
			// 
			this->btn_auto->Location = System::Drawing::Point(25, 75);
			this->btn_auto->Name = L"btn_auto";
			this->btn_auto->Size = System::Drawing::Size(257, 43);
			this->btn_auto->TabIndex = 2;
			this->btn_auto->Text = L"Автоматический";
			this->btn_auto->UseVisualStyleBackColor = true;
			this->btn_auto->Click += gcnew System::EventHandler(this, &MyForm::btn_auto_Click);
			// 
			// btn_step
			// 
			this->btn_step->Location = System::Drawing::Point(288, 75);
			this->btn_step->Name = L"btn_step";
			this->btn_step->Size = System::Drawing::Size(275, 43);
			this->btn_step->TabIndex = 3;
			this->btn_step->Text = L"Пошаговый";
			this->btn_step->UseVisualStyleBackColor = true;
			this->btn_step->Click += gcnew System::EventHandler(this, &MyForm::btn_step_Click);
			// 
			// reset
			// 
			this->reset->BackColor = System::Drawing::Color::IndianRed;
			this->reset->Location = System::Drawing::Point(569, 75);
			this->reset->Name = L"reset";
			this->reset->Size = System::Drawing::Size(109, 43);
			this->reset->TabIndex = 4;
			this->reset->Text = L"Сброс";
			this->reset->UseVisualStyleBackColor = false;
			this->reset->Click += gcnew System::EventHandler(this, &MyForm::reset_Click);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(22, 271);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(202, 325);
			this->label1->TabIndex = 5;
			this->label1->Text = resources->GetString(L"label1.Text");
			// 
			// pictureBox1
			// 
			this->pictureBox1->Image = (cli::safe_cast<System::Drawing::Image^>(resources->GetObject(L"pictureBox1.Image")));
			this->pictureBox1->Location = System::Drawing::Point(230, 271);
			this->pictureBox1->Name = L"pictureBox1";
			this->pictureBox1->Size = System::Drawing::Size(448, 329);
			this->pictureBox1->SizeMode = System::Windows::Forms::PictureBoxSizeMode::Zoom;
			this->pictureBox1->TabIndex = 6;
			this->pictureBox1->TabStop = false;
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(690, 612);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->reset);
			this->Controls->Add(this->btn_step);
			this->Controls->Add(this->btn_auto);
			this->Controls->Add(this->dataGridView1);
			this->Controls->Add(this->result);
			this->Controls->Add(this->pictureBox1);
			this->Name = L"MyForm";
			this->Text = L"MyForm";
			this->Load += gcnew System::EventHandler(this, &MyForm::MyForm_Load);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->pictureBox1))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion


		int S, r, size = 1;

		void action(int r, wchar_t symbol, String^ signal, int len) {
			switch (r)
			{
			case 0: {
				dataGridView1->Rows[0]->Cells[len]->Value = Convert::ToString(symbol);
				dataGridView1->Rows[1]->Cells[len + 1]->Value = "S0";
				dataGridView1->Rows[2]->Cells[len]->Value = Convert::ToString(signal);
				S = 0; break;
			}
			case 1: {
				dataGridView1->Rows[0]->Cells[len]->Value = Convert::ToString(symbol);
				dataGridView1->Rows[1]->Cells[len + 1]->Value = "S1";
				dataGridView1->Rows[2]->Cells[len]->Value = Convert::ToString(signal);
				S = 1; break;
			}
			case 2: {
				dataGridView1->Rows[0]->Cells[len]->Value = Convert::ToString(symbol);
				dataGridView1->Rows[1]->Cells[len + 1]->Value = "S2";
				dataGridView1->Rows[2]->Cells[len]->Value = Convert::ToString(signal);
				S = 2; break;
			}
			case 3: {
				dataGridView1->Rows[0]->Cells[len]->Value = Convert::ToString(symbol);
				dataGridView1->Rows[1]->Cells[len + 1]->Value = "Sр";
				dataGridView1->Rows[2]->Cells[len]->Value = Convert::ToString(signal);
				S = 3; break;
			}
			case 4: {
				dataGridView1->Rows[0]->Cells[len]->Value = Convert::ToString(symbol);
				dataGridView1->Rows[1]->Cells[len + 1]->Value = "Sп";
				dataGridView1->Rows[2]->Cells[len]->Value = Convert::ToString(signal);
				S = 4; break;
			}
			case 5: {
				MessageBox::Show(L"Ошибка!!! Введены неправильные данные. Измените последовательность!", L"Программа остановлена", MessageBoxButtons::OK, MessageBoxIcon::Error);
			}
			default: break;
			}
		}


		////////////////////////////////////////////////////


		void reading(String^ str, int len) {
			r = S;
			String^ signal;

			if (r == 0) {
				signal = "y0";
				switch (str[len])
				{
				case '0':;
				case '1':;
				case '2':;
				case '3':;
				case '4':;
				case '5':;
				case '6':;
				case '7':;
				case '8':;
				case '9': action(1, str[len], signal, len); break;
				default: action(5, str[len], "__", len); break;
				}
			}

			else if (r == 1) {
				signal = "y1";
				switch (str[len])
				{
				case '+':;
				case '-':;
				case '/':;
				case '*': action(2, str[len], "y2", len); break;
				case '=': action(3, str[len], "y3", len); break;
				case '0':;
				case '1':;
				case '2':;
				case '3':;
				case '4':;
				case '5':;
				case '6':;
				case '7':;
				case '8':;
				case '9': action(1, str[len], signal, len); break;
				default: action(5, str[len], "__", len); break;
				}
			}

			else if (r == 2) {
				signal = "y0";

				switch (str[len])
				{
				case '0':;
				case '1':;
				case '2':;
				case '3':;
				case '4':;
				case '5':;
				case '6':;
				case '7':;
				case '8':;
				case '9': action(1, str[len], signal, len); break;
				default: action(5, str[len], signal, len); break;
				}
			}

			else if (r == 3) {
				switch (str[len])
				{
				case 'M': action(4, str[len], "y4", len); break;
				case '=': action(0, str[len], "y6", len); break;
				case 'R': action(0, str[len], "y6", len); break;
				default: action(5, str[len], "__", len); break;
				}
			}

			else if (r == 4) {
				signal = "y5";
				switch (str[len])
				{
				case '+':;
				case '-':;
				case '/':;
				case '*': action(2, str[len], signal, len); break;
				default: action(5, str[len], "__", len); break;
				}
			}
		}


		////////////////////////////////////////////////////

		int steplen = 0;
	private: System::Void btn_auto_Click(System::Object^ sender, System::EventArgs^ e) {
		String^ str = result->Text;
		int strlen = str->Length;
		size = strlen + 1;
		steplen = strlen;

		dataGridView1->ColumnCount = size;
		dataGridView1->Rows[0]->Cells[0]->Value = "";
		dataGridView1->Rows[1]->Cells[0]->Value = "S0";
		dataGridView1->Rows[2]->Cells[0]->Value = "";
		dataGridView1->Columns[0]->HeaderCell->Value = "0";

		dataGridView1->Rows[0]->Cells[strlen]->Value = "";
		dataGridView1->Rows[2]->Cells[strlen]->Value = "";

		S = 0;
		for (int len = 0; len < strlen; len++) {
			dataGridView1->Rows[0]->Cells[len]->Value = " ";
			dataGridView1->Rows[2]->Cells[len]->Value = " ";
		}
		for (int len = 0; len < strlen; len++) {
			dataGridView1->Columns[len]->HeaderCell->Value = Convert::ToString(len);
			reading(str, len);
		}
		dataGridView1->Columns[strlen]->HeaderCell->Value = Convert::ToString(strlen);

		dataGridView1->AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode::AutoSizeToAllHeaders);
		dataGridView1->AutoResizeColumns();
	}

		   ////////////////////////////////////////////////////


	private: System::Void btn_step_Click(System::Object^ sender, System::EventArgs^ e) {
		String^ str = result->Text;
		int strlen = str->Length;
		size = strlen + 1;

		dataGridView1->ColumnCount = size;

		if (steplen == strlen) {
			MessageBox::Show(L"Конец последовательности, сбросьте таблицу.", L"Программа остановлена", MessageBoxButtons::OK, MessageBoxIcon::Error);
		}
		else if (steplen > strlen) {
			MessageBox::Show(L"Критическая ошибка!!! Сбросьте таблицу!", L"Программа остановлена", MessageBoxButtons::OK, MessageBoxIcon::Error);
		}
		else {
			dataGridView1->Columns[steplen + 1]->HeaderCell->Value = Convert::ToString(steplen + 1);
			reading(str, steplen);
			steplen++;
			size++;
		}

		dataGridView1->AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode::AutoSizeToAllHeaders);
		dataGridView1->AutoResizeColumns();
	}


		   ////////////////////////////////////////////////////


	private: System::Void reset_Click(System::Object^ sender, System::EventArgs^ e) {
		S = 0; size = 2; steplen = 0;
		dataGridView1->ColumnCount = 1;
		dataGridView1->Rows[0]->Cells[0]->Value = "";
		dataGridView1->Rows[1]->Cells[0]->Value = "S0";
		dataGridView1->Rows[2]->Cells[0]->Value = "";
		dataGridView1->Columns[0]->HeaderCell->Value = "0";


		dataGridView1->AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode::AutoSizeToAllHeaders);
		dataGridView1->AutoResizeColumns();
	}


		   ////////////////////////////////////////////////////


	private: System::Void MyForm_Load(System::Object^ sender, System::EventArgs^ e) {

		dataGridView1->ColumnCount = size;
		dataGridView1->RowCount = 3;

		dataGridView1->Rows[0]->HeaderCell->Value = "X";
		dataGridView1->Rows[1]->HeaderCell->Value = "S";
		dataGridView1->Rows[2]->HeaderCell->Value = "Y";
		dataGridView1->Rows[0]->Cells[0]->Value = "";
		dataGridView1->Rows[1]->Cells[0]->Value = "S0";
		dataGridView1->Rows[2]->Cells[0]->Value = "";
		dataGridView1->Columns[0]->HeaderCell->Value = "0";

		dataGridView1->AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode::AutoSizeToAllHeaders);
		dataGridView1->AutoResizeColumns();

	}
	};
}
