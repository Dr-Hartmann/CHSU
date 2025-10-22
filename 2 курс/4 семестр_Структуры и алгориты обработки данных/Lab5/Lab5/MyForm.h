#pragma once
namespace Lab5 {
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
		}
	protected:
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^ file_open;
	private: System::Windows::Forms::OpenFileDialog^ openFileDialog1;
	private: System::Windows::Forms::RichTextBox^ richTextBox1;
	private: System::Windows::Forms::Button^ run;
	private: System::Windows::Forms::NumericUpDown^ numericUpDown1;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::RichTextBox^ richTextBox3;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::NumericUpDown^ numeric_a;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::NumericUpDown^ numeric_c;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::Label^ label_percent;
	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::Label^ label_collisions;
	private: System::Windows::Forms::Label^ label_chain;
	private: System::Windows::Forms::Label^ label7;
	private: System::Windows::Forms::RichTextBox^ richTextBox_log;
	private: System::Windows::Forms::Label^ label8;
	private: System::Windows::Forms::Label^ label6;
	private: System::Windows::Forms::RichTextBox^ richTextBox4;
	private: System::Windows::Forms::Label^ label9;
	private: System::Windows::Forms::Button^ button_search;
	private: System::Windows::Forms::NumericUpDown^ numeric_element;
	protected:
	private:
		System::ComponentModel::Container^ components;

#pragma region Windows Form Designer generated code
		void InitializeComponent(void)
		{
			this->file_open = (gcnew System::Windows::Forms::Button());
			this->openFileDialog1 = (gcnew System::Windows::Forms::OpenFileDialog());
			this->richTextBox1 = (gcnew System::Windows::Forms::RichTextBox());
			this->run = (gcnew System::Windows::Forms::Button());
			this->numericUpDown1 = (gcnew System::Windows::Forms::NumericUpDown());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->richTextBox3 = (gcnew System::Windows::Forms::RichTextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->numeric_a = (gcnew System::Windows::Forms::NumericUpDown());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->numeric_c = (gcnew System::Windows::Forms::NumericUpDown());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->label_percent = (gcnew System::Windows::Forms::Label());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->label_collisions = (gcnew System::Windows::Forms::Label());
			this->label_chain = (gcnew System::Windows::Forms::Label());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->richTextBox_log = (gcnew System::Windows::Forms::RichTextBox());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->richTextBox4 = (gcnew System::Windows::Forms::RichTextBox());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->button_search = (gcnew System::Windows::Forms::Button());
			this->numeric_element = (gcnew System::Windows::Forms::NumericUpDown());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numericUpDown1))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numeric_a))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numeric_c))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numeric_element))->BeginInit();
			this->SuspendLayout();
			// 
			// file_open
			// 
			this->file_open->BackColor = System::Drawing::SystemColors::ActiveCaption;
			this->file_open->Location = System::Drawing::Point(970, 612);
			this->file_open->Name = L"file_open";
			this->file_open->Size = System::Drawing::Size(131, 37);
			this->file_open->TabIndex = 0;
			this->file_open->Text = L"Открыть файл";
			this->file_open->UseVisualStyleBackColor = false;
			this->file_open->Click += gcnew System::EventHandler(this, &MyForm::file_open_Click);
			// 
			// openFileDialog1
			// 
			this->openFileDialog1->FileName = L"openFileDialog1";
			// 
			// richTextBox1
			// 
			this->richTextBox1->Location = System::Drawing::Point(12, 12);
			this->richTextBox1->Name = L"richTextBox1";
			this->richTextBox1->Size = System::Drawing::Size(425, 189);
			this->richTextBox1->TabIndex = 2;
			this->richTextBox1->Text = L"";
			// 
			// run
			// 
			this->run->BackColor = System::Drawing::SystemColors::ButtonFace;
			this->run->Location = System::Drawing::Point(1105, 612);
			this->run->Name = L"run";
			this->run->Size = System::Drawing::Size(123, 37);
			this->run->TabIndex = 6;
			this->run->Text = L"Хеширование";
			this->run->UseVisualStyleBackColor = false;
			this->run->Click += gcnew System::EventHandler(this, &MyForm::run_Click);
			// 
			// numericUpDown1
			// 
			this->numericUpDown1->Location = System::Drawing::Point(730, 20);
			this->numericUpDown1->Maximum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 50000, 0, 0, 0 });
			this->numericUpDown1->Name = L"numericUpDown1";
			this->numericUpDown1->Size = System::Drawing::Size(74, 20);
			this->numericUpDown1->TabIndex = 7;
			this->numericUpDown1->Value = System::Decimal(gcnew cli::array< System::Int32 >(4) { 100, 0, 0, 0 });
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(441, 9);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(287, 31);
			this->label1->TabIndex = 8;
			this->label1->Text = L"Количество классов";
			// 
			// richTextBox3
			// 
			this->richTextBox3->Location = System::Drawing::Point(12, 207);
			this->richTextBox3->Name = L"richTextBox3";
			this->richTextBox3->Size = System::Drawing::Size(804, 442);
			this->richTextBox3->TabIndex = 9;
			this->richTextBox3->Text = L"";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label2->Location = System::Drawing::Point(443, 40);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(30, 31);
			this->label2->TabIndex = 11;
			this->label2->Text = L"a";
			// 
			// numeric_a
			// 
			this->numeric_a->Location = System::Drawing::Point(479, 51);
			this->numeric_a->Maximum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 40000, 0, 0, 0 });
			this->numeric_a->Minimum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 40000, 0, 0, System::Int32::MinValue });
			this->numeric_a->Name = L"numeric_a";
			this->numeric_a->Size = System::Drawing::Size(74, 20);
			this->numeric_a->TabIndex = 10;
			this->numeric_a->Value = System::Decimal(gcnew cli::array< System::Int32 >(4) { 1, 0, 0, 0 });
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label3->Location = System::Drawing::Point(573, 40);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(29, 31);
			this->label3->TabIndex = 13;
			this->label3->Text = L"c";
			// 
			// numeric_c
			// 
			this->numeric_c->Location = System::Drawing::Point(608, 51);
			this->numeric_c->Maximum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 40000, 0, 0, 0 });
			this->numeric_c->Minimum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 40000, 0, 0, System::Int32::MinValue });
			this->numeric_c->Name = L"numeric_c";
			this->numeric_c->Size = System::Drawing::Size(74, 20);
			this->numeric_c->TabIndex = 12;
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label4->Location = System::Drawing::Point(443, 167);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(285, 31);
			this->label4->TabIndex = 14;
			this->label4->Text = L"Заполняемость (%):";
			// 
			// label_percent
			// 
			this->label_percent->AutoSize = true;
			this->label_percent->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label_percent->Location = System::Drawing::Point(734, 167);
			this->label_percent->Name = L"label_percent";
			this->label_percent->Size = System::Drawing::Size(30, 31);
			this->label_percent->TabIndex = 15;
			this->label_percent->Text = L"0";
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label5->Location = System::Drawing::Point(443, 74);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(153, 31);
			this->label5->TabIndex = 17;
			this->label5->Text = L"Коллизий:";
			// 
			// label_collisions
			// 
			this->label_collisions->AutoSize = true;
			this->label_collisions->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label_collisions->Location = System::Drawing::Point(602, 74);
			this->label_collisions->Name = L"label_collisions";
			this->label_collisions->Size = System::Drawing::Size(30, 31);
			this->label_collisions->TabIndex = 18;
			this->label_collisions->Text = L"0";
			// 
			// label_chain
			// 
			this->label_chain->AutoSize = true;
			this->label_chain->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label_chain->Location = System::Drawing::Point(588, 136);
			this->label_chain->Name = L"label_chain";
			this->label_chain->Size = System::Drawing::Size(24, 31);
			this->label_chain->TabIndex = 20;
			this->label_chain->Text = L"-";
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label7->Location = System::Drawing::Point(443, 105);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(224, 62);
			this->label7->TabIndex = 19;
			this->label7->Text = L"Самая длинная\r\nцепочка:";
			// 
			// richTextBox_log
			// 
			this->richTextBox_log->Location = System::Drawing::Point(822, 207);
			this->richTextBox_log->Name = L"richTextBox_log";
			this->richTextBox_log->Size = System::Drawing::Size(406, 399);
			this->richTextBox_log->TabIndex = 21;
			this->richTextBox_log->Text = L"";
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label8->Location = System::Drawing::Point(964, 170);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(127, 31);
			this->label8->TabIndex = 22;
			this->label8->Text = L"История";
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label6->Location = System::Drawing::Point(972, 9);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(176, 50);
			this->label6->TabIndex = 23;
			this->label6->Text = L"Найти элемент\r\nв таблице";
			// 
			// richTextBox4
			// 
			this->richTextBox4->Location = System::Drawing::Point(970, 93);
			this->richTextBox4->Name = L"richTextBox4";
			this->richTextBox4->Size = System::Drawing::Size(258, 28);
			this->richTextBox4->TabIndex = 24;
			this->richTextBox4->Text = L"";
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label9->Location = System::Drawing::Point(972, 65);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(189, 25);
			this->label9->TabIndex = 25;
			this->label9->Text = L"Адрсе элемента";
			this->label9->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// button_search
			// 
			this->button_search->BackColor = System::Drawing::SystemColors::ActiveCaption;
			this->button_search->Location = System::Drawing::Point(970, 127);
			this->button_search->Name = L"button_search";
			this->button_search->Size = System::Drawing::Size(258, 28);
			this->button_search->TabIndex = 26;
			this->button_search->Text = L"Поиск";
			this->button_search->UseVisualStyleBackColor = false;
			this->button_search->Click += gcnew System::EventHandler(this, &MyForm::button_search_Click);
			// 
			// numeric_element
			// 
			this->numeric_element->Location = System::Drawing::Point(1154, 22);
			this->numeric_element->Maximum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 2147483647, 0, 0, 0 });
			this->numeric_element->Minimum = System::Decimal(gcnew cli::array< System::Int32 >(4) { System::Int32::MinValue, 0, 0, System::Int32::MinValue });
			this->numeric_element->Name = L"numeric_element";
			this->numeric_element->Size = System::Drawing::Size(74, 20);
			this->numeric_element->TabIndex = 27;
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::SystemColors::Control;
			this->ClientSize = System::Drawing::Size(1240, 661);
			this->Controls->Add(this->numeric_element);
			this->Controls->Add(this->button_search);
			this->Controls->Add(this->label9);
			this->Controls->Add(this->richTextBox4);
			this->Controls->Add(this->label6);
			this->Controls->Add(this->label8);
			this->Controls->Add(this->richTextBox_log);
			this->Controls->Add(this->label_chain);
			this->Controls->Add(this->label7);
			this->Controls->Add(this->label_collisions);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->label_percent);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->numeric_c);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->numeric_a);
			this->Controls->Add(this->richTextBox3);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->numericUpDown1);
			this->Controls->Add(this->run);
			this->Controls->Add(this->richTextBox1);
			this->Controls->Add(this->file_open);
			this->ForeColor = System::Drawing::SystemColors::ControlText;
			this->Name = L"MyForm";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"MyForm";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numericUpDown1))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numeric_a))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numeric_c))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numeric_element))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
		//загрузка текстового файла
	private: System::Void file_open_Click(System::Object^ sender, System::EventArgs^ e);
		   //начало обработки
	private: System::Void run_Click(System::Object^ sender, System::EventArgs^ e);
		   //поиск элемента по таблице
	private: System::Void button_search_Click(System::Object^ sender, System::EventArgs^ e);

	};
}
