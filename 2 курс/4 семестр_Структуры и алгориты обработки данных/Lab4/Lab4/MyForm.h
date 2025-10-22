#pragma once
namespace Lab4 {
	using namespace System;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
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

	private: System::Windows::Forms::MenuStrip^ menuStrip1;
	private: System::Windows::Forms::ToolStripMenuItem^ загрузитьФайлToolStripMenuItem;
	private: System::Windows::Forms::RichTextBox^ richTextBox1;
	private: System::Windows::Forms::Button^ start;


	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::OpenFileDialog^ openFileDialog1;
	private: System::Windows::Forms::NumericUpDown^ numericUpDown1;
	private: System::Windows::Forms::Button^ sort_alphabetically;
	private: System::Windows::Forms::Button^ sort_frequency;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::RichTextBox^ richTextBox2;
	private: System::Windows::Forms::TextBox^ textBox1;
	private: System::Windows::Forms::TextBox^ textBox2;
	private: System::Windows::Forms::Button^ find;
	private: System::Windows::Forms::Button^ find_long;

	private:

		System::ComponentModel::Container^ components;
#pragma region Windows Form Designer generated code
		void InitializeComponent(void)
		{
			this->menuStrip1 = (gcnew System::Windows::Forms::MenuStrip());
			this->загрузитьФайлToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->richTextBox1 = (gcnew System::Windows::Forms::RichTextBox());
			this->start = (gcnew System::Windows::Forms::Button());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->openFileDialog1 = (gcnew System::Windows::Forms::OpenFileDialog());
			this->numericUpDown1 = (gcnew System::Windows::Forms::NumericUpDown());
			this->sort_alphabetically = (gcnew System::Windows::Forms::Button());
			this->sort_frequency = (gcnew System::Windows::Forms::Button());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->richTextBox2 = (gcnew System::Windows::Forms::RichTextBox());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->find = (gcnew System::Windows::Forms::Button());
			this->find_long = (gcnew System::Windows::Forms::Button());
			this->menuStrip1->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numericUpDown1))->BeginInit();
			this->SuspendLayout();
			// 
			// menuStrip1
			// 
			this->menuStrip1->BackColor = System::Drawing::SystemColors::Control;
			this->menuStrip1->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->загрузитьФайлToolStripMenuItem });
			this->menuStrip1->Location = System::Drawing::Point(0, 0);
			this->menuStrip1->Name = L"menuStrip1";
			this->menuStrip1->Size = System::Drawing::Size(961, 24);
			this->menuStrip1->TabIndex = 1;
			this->menuStrip1->Text = L"menuStrip1";
			// 
			// загрузитьФайлToolStripMenuItem
			// 
			this->загрузитьФайлToolStripMenuItem->Name = L"загрузитьФайлToolStripMenuItem";
			this->загрузитьФайлToolStripMenuItem->Size = System::Drawing::Size(105, 20);
			this->загрузитьФайлToolStripMenuItem->Text = L"Загрузить файл";
			this->загрузитьФайлToolStripMenuItem->Click += gcnew System::EventHandler(this, &MyForm::загрузитьФайлToolStripMenuItem_Click);
			// 
			// richTextBox1
			// 
			this->richTextBox1->Location = System::Drawing::Point(12, 27);
			this->richTextBox1->Name = L"richTextBox1";
			this->richTextBox1->Size = System::Drawing::Size(400, 600);
			this->richTextBox1->TabIndex = 2;
			this->richTextBox1->Text = L"";
			// 
			// start
			// 
			this->start->BackColor = System::Drawing::Color::OrangeRed;
			this->start->Location = System::Drawing::Point(824, 383);
			this->start->Margin = System::Windows::Forms::Padding(1);
			this->start->Name = L"start";
			this->start->Size = System::Drawing::Size(131, 36);
			this->start->TabIndex = 3;
			this->start->Text = L"Выделить слова";
			this->start->UseVisualStyleBackColor = false;
			this->start->Click += gcnew System::EventHandler(this, &MyForm::start_Click);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 11.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(821, 270);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(138, 36);
			this->label1->TabIndex = 4;
			this->label1->Text = L"Выводить слова\r\nдлины (0 - все):";
			// 
			// openFileDialog1
			// 
			this->openFileDialog1->FileName = L"openFileDialog1";
			// 
			// numericUpDown1
			// 
			this->numericUpDown1->Location = System::Drawing::Point(824, 309);
			this->numericUpDown1->Name = L"numericUpDown1";
			this->numericUpDown1->Size = System::Drawing::Size(129, 20);
			this->numericUpDown1->TabIndex = 5;
			// 
			// sort_alphabetically
			// 
			this->sort_alphabetically->BackColor = System::Drawing::SystemColors::ActiveCaption;
			this->sort_alphabetically->FlatStyle = System::Windows::Forms::FlatStyle::Flat;
			this->sort_alphabetically->Location = System::Drawing::Point(824, 47);
			this->sort_alphabetically->Name = L"sort_alphabetically";
			this->sort_alphabetically->Size = System::Drawing::Size(129, 25);
			this->sort_alphabetically->TabIndex = 6;
			this->sort_alphabetically->Text = L"По алфавиту";
			this->sort_alphabetically->UseVisualStyleBackColor = false;
			this->sort_alphabetically->Click += gcnew System::EventHandler(this, &MyForm::sort_alphabetically_Click);
			// 
			// sort_frequency
			// 
			this->sort_frequency->BackColor = System::Drawing::SystemColors::ActiveCaption;
			this->sort_frequency->FlatStyle = System::Windows::Forms::FlatStyle::Flat;
			this->sort_frequency->Location = System::Drawing::Point(824, 78);
			this->sort_frequency->Name = L"sort_frequency";
			this->sort_frequency->Size = System::Drawing::Size(129, 25);
			this->sort_frequency->TabIndex = 7;
			this->sort_frequency->Text = L"По частоте";
			this->sort_frequency->UseVisualStyleBackColor = false;
			this->sort_frequency->Click += gcnew System::EventHandler(this, &MyForm::sort_frequency_Click);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label2->Location = System::Drawing::Point(828, 28);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(121, 16);
			this->label2->TabIndex = 8;
			this->label2->Text = L"Отсортировать";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label3->Location = System::Drawing::Point(828, 132);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(105, 16);
			this->label3->TabIndex = 9;
			this->label3->Text = L"Найти слово:";
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(822, 151);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(131, 20);
			this->textBox1->TabIndex = 10;
			// 
			// richTextBox2
			// 
			this->richTextBox2->Location = System::Drawing::Point(418, 27);
			this->richTextBox2->Name = L"richTextBox2";
			this->richTextBox2->Size = System::Drawing::Size(400, 600);
			this->richTextBox2->TabIndex = 11;
			this->richTextBox2->Text = L"";
			// 
			// textBox2
			// 
			this->textBox2->Location = System::Drawing::Point(822, 205);
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(131, 20);
			this->textBox2->TabIndex = 12;
			// 
			// find
			// 
			this->find->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(255)), static_cast<System::Int32>(static_cast<System::Byte>(255)),
				static_cast<System::Int32>(static_cast<System::Byte>(128)));
			this->find->FlatStyle = System::Windows::Forms::FlatStyle::Flat;
			this->find->Location = System::Drawing::Point(822, 174);
			this->find->Name = L"find";
			this->find->Size = System::Drawing::Size(131, 25);
			this->find->TabIndex = 13;
			this->find->Text = L"Найти";
			this->find->UseVisualStyleBackColor = false;
			this->find->Click += gcnew System::EventHandler(this, &MyForm::find_Click);
			// 
			// find_long
			// 
			this->find_long->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(0)), static_cast<System::Int32>(static_cast<System::Byte>(192)),
				static_cast<System::Int32>(static_cast<System::Byte>(192)));
			this->find_long->FlatStyle = System::Windows::Forms::FlatStyle::Flat;
			this->find_long->Location = System::Drawing::Point(824, 335);
			this->find_long->Name = L"find_long";
			this->find_long->Size = System::Drawing::Size(131, 25);
			this->find_long->TabIndex = 14;
			this->find_long->Text = L"Вывести";
			this->find_long->UseVisualStyleBackColor = false;
			this->find_long->Click += gcnew System::EventHandler(this, &MyForm::find_long_Click);
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(961, 637);
			this->Controls->Add(this->find_long);
			this->Controls->Add(this->find);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->richTextBox2);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->sort_frequency);
			this->Controls->Add(this->sort_alphabetically);
			this->Controls->Add(this->numericUpDown1);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->start);
			this->Controls->Add(this->richTextBox1);
			this->Controls->Add(this->menuStrip1);
			this->MainMenuStrip = this->menuStrip1;
			this->Name = L"MyForm";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"MyForm";
			this->menuStrip1->ResumeLayout(false);
			this->menuStrip1->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numericUpDown1))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
		//загрузка файла
	private: System::Void загрузитьФайлToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e);
		   //старт
	private: System::Void start_Click(System::Object^ sender, System::EventArgs^ e);
		   //сортировка по алфавиту
	private: System::Void sort_alphabetically_Click(System::Object^ sender, System::EventArgs^ e);
		   //сортировка по частоте
private: System::Void sort_frequency_Click(System::Object^ sender, System::EventArgs^ e);
private: System::Void find_Click(System::Object^ sender, System::EventArgs^ e);
private: System::Void find_long_Click(System::Object^ sender, System::EventArgs^ e);
};
}
