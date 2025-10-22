#pragma once

namespace Lab1 {

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
	private: System::Windows::Forms::DataGridView^ dataGridView1;
	protected:
	private: System::Windows::Forms::OpenFileDialog^ openFileDialog1;
	private: System::Windows::Forms::SaveFileDialog^ saveFileDialog1;
	private: System::Windows::Forms::MenuStrip^ menuStrip1;
	private: System::Windows::Forms::ToolStripMenuItem^ загрузитьФайлToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ открытьToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ сохранитьАрхивToolStripMenuItem;
	private: System::Windows::Forms::RichTextBox^ richTextBox1;

	private: System::Windows::Forms::ToolStripMenuItem^ архивацииToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ распаковкиToolStripMenuItem;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::RichTextBox^ richTextBox2;
	private: System::Windows::Forms::Button^ button1;


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
			this->dataGridView1 = (gcnew System::Windows::Forms::DataGridView());
			this->openFileDialog1 = (gcnew System::Windows::Forms::OpenFileDialog());
			this->saveFileDialog1 = (gcnew System::Windows::Forms::SaveFileDialog());
			this->menuStrip1 = (gcnew System::Windows::Forms::MenuStrip());
			this->загрузитьФайлToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->открытьToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->архивацииToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->распаковкиToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->сохранитьАрхивToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->richTextBox1 = (gcnew System::Windows::Forms::RichTextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->richTextBox2 = (gcnew System::Windows::Forms::RichTextBox());
			this->button1 = (gcnew System::Windows::Forms::Button());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->BeginInit();
			this->menuStrip1->SuspendLayout();
			this->SuspendLayout();
			// 
			// dataGridView1
			// 
			this->dataGridView1->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView1->Location = System::Drawing::Point(12, 401);
			this->dataGridView1->Name = L"dataGridView1";
			this->dataGridView1->Size = System::Drawing::Size(324, 150);
			this->dataGridView1->TabIndex = 0;
			// 
			// openFileDialog1
			// 
			this->openFileDialog1->FileName = L"openFileDialog1";
			// 
			// menuStrip1
			// 
			this->menuStrip1->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->загрузитьФайлToolStripMenuItem });
			this->menuStrip1->Location = System::Drawing::Point(0, 0);
			this->menuStrip1->Name = L"menuStrip1";
			this->menuStrip1->Size = System::Drawing::Size(676, 24);
			this->menuStrip1->TabIndex = 2;
			this->menuStrip1->Text = L"menuStrip1";
			// 
			// загрузитьФайлToolStripMenuItem
			// 
			this->загрузитьФайлToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(2) {
				this->открытьToolStripMenuItem,
					this->сохранитьАрхивToolStripMenuItem
			});
			this->загрузитьФайлToolStripMenuItem->Name = L"загрузитьФайлToolStripMenuItem";
			this->загрузитьФайлToolStripMenuItem->Size = System::Drawing::Size(48, 20);
			this->загрузитьФайлToolStripMenuItem->Text = L"Файл";
			// 
			// открытьToolStripMenuItem
			// 
			this->открытьToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(2) {
				this->архивацииToolStripMenuItem,
					this->распаковкиToolStripMenuItem
			});
			this->открытьToolStripMenuItem->Name = L"открытьToolStripMenuItem";
			this->открытьToolStripMenuItem->Size = System::Drawing::Size(180, 22);
			this->открытьToolStripMenuItem->Text = L"Открыть для...";
			// 
			// архивацииToolStripMenuItem
			// 
			this->архивацииToolStripMenuItem->Name = L"архивацииToolStripMenuItem";
			this->архивацииToolStripMenuItem->Size = System::Drawing::Size(138, 22);
			this->архивацииToolStripMenuItem->Text = L"Архивации";
			this->архивацииToolStripMenuItem->Click += gcnew System::EventHandler(this, &MyForm::архивацииToolStripMenuItem_Click);
			// 
			// распаковкиToolStripMenuItem
			// 
			this->распаковкиToolStripMenuItem->Name = L"распаковкиToolStripMenuItem";
			this->распаковкиToolStripMenuItem->Size = System::Drawing::Size(138, 22);
			this->распаковкиToolStripMenuItem->Text = L"Распаковки";
			this->распаковкиToolStripMenuItem->Click += gcnew System::EventHandler(this, &MyForm::распаковкиToolStripMenuItem_Click);
			// 
			// сохранитьАрхивToolStripMenuItem
			// 
			this->сохранитьАрхивToolStripMenuItem->Name = L"сохранитьАрхивToolStripMenuItem";
			this->сохранитьАрхивToolStripMenuItem->Size = System::Drawing::Size(180, 22);
			this->сохранитьАрхивToolStripMenuItem->Text = L"Сохранить архив";
			this->сохранитьАрхивToolStripMenuItem->Click += gcnew System::EventHandler(this, &MyForm::сохранитьАрхивToolStripMenuItem_Click);
			// 
			// richTextBox1
			// 
			this->richTextBox1->Location = System::Drawing::Point(12, 52);
			this->richTextBox1->Name = L"richTextBox1";
			this->richTextBox1->Size = System::Drawing::Size(324, 282);
			this->richTextBox1->TabIndex = 3;
			this->richTextBox1->Text = L"";
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(12, 24);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(128, 25);
			this->label1->TabIndex = 4;
			this->label1->Text = L"Архивация";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label2->Location = System::Drawing::Point(342, 24);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(140, 25);
			this->label2->TabIndex = 6;
			this->label2->Text = L"Распаковка";
			// 
			// richTextBox2
			// 
			this->richTextBox2->Location = System::Drawing::Point(342, 52);
			this->richTextBox2->Name = L"richTextBox2";
			this->richTextBox2->Size = System::Drawing::Size(324, 282);
			this->richTextBox2->TabIndex = 5;
			this->richTextBox2->Text = L"";
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(551, 511);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(113, 75);
			this->button1->TabIndex = 7;
			this->button1->Text = L"Очистить";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm::button1_Click);
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(676, 598);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->richTextBox2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->richTextBox1);
			this->Controls->Add(this->menuStrip1);
			this->Controls->Add(this->dataGridView1);
			this->MainMenuStrip = this->menuStrip1;
			this->Name = L"MyForm";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"MyForm";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->EndInit();
			this->menuStrip1->ResumeLayout(false);
			this->menuStrip1->PerformLayout();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

	private: System::Void сохранитьАрхивToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e);
	private: System::Void архивацииToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e);
	private: System::Void распаковкиToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e);
private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e) {
	this->richTextBox2->Text = "";
}
};
}
