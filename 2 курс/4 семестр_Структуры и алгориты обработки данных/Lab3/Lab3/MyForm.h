#pragma once
namespace Lab3 {
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
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
	private: System::Windows::Forms::Button^ button_run;
	private: System::Windows::Forms::RichTextBox^ richTextBox1;
	private: System::Windows::Forms::NumericUpDown^ numericUpDown1;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::RichTextBox^ richTextBox2;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::RichTextBox^ richTextBox3;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::TextBox^ textBox1;
	private: System::Windows::Forms::TextBox^ textBox2;
	private: System::Windows::Forms::TextBox^ textBox3;
	private: System::Windows::Forms::Label^ label6;
	private: System::Windows::Forms::Label^ label7;
	private: System::Windows::Forms::TextBox^ textBox4;
	private: System::Windows::Forms::TextBox^ textBox5;
	private: System::Windows::Forms::TextBox^ textBox6;
	private: System::Windows::Forms::Label^ label8;
	private: System::Windows::Forms::Label^ label9;
	private: System::Windows::Forms::RichTextBox^ richTextBox4;
	private: System::Windows::Forms::Label^ label10;
	private: System::Windows::Forms::RichTextBox^ richTextBox5;
	private: System::Windows::Forms::Label^ label11;
	private: System::Windows::Forms::RichTextBox^ richTextBox6;
	private: System::Windows::Forms::Label^ label12;
	private: System::Windows::Forms::TextBox^ textBox7;
	private: System::Windows::Forms::TextBox^ textBox8;
	private: System::Windows::Forms::TextBox^ textBox9;
	private: System::Windows::Forms::Label^ label13;
	private: System::Windows::Forms::Label^ label14;
	private: System::Windows::Forms::RichTextBox^ richTextBox7;
	private: System::Windows::Forms::Label^ label15;
	private: System::Windows::Forms::RichTextBox^ richTextBox8;
	private: System::Windows::Forms::Label^ label16;
	private: System::Windows::Forms::RichTextBox^ richTextBox9;

	private: System::Windows::Forms::TextBox^ textBox10;
	private: System::Windows::Forms::TextBox^ textBox11;
	private: System::Windows::Forms::TextBox^ textBox12;
	private:
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		void InitializeComponent(void)
		{
			this->button_run = (gcnew System::Windows::Forms::Button());
			this->richTextBox1 = (gcnew System::Windows::Forms::RichTextBox());
			this->numericUpDown1 = (gcnew System::Windows::Forms::NumericUpDown());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->richTextBox2 = (gcnew System::Windows::Forms::RichTextBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->richTextBox3 = (gcnew System::Windows::Forms::RichTextBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->textBox3 = (gcnew System::Windows::Forms::TextBox());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->textBox4 = (gcnew System::Windows::Forms::TextBox());
			this->textBox5 = (gcnew System::Windows::Forms::TextBox());
			this->textBox6 = (gcnew System::Windows::Forms::TextBox());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->richTextBox4 = (gcnew System::Windows::Forms::RichTextBox());
			this->label10 = (gcnew System::Windows::Forms::Label());
			this->richTextBox5 = (gcnew System::Windows::Forms::RichTextBox());
			this->label11 = (gcnew System::Windows::Forms::Label());
			this->richTextBox6 = (gcnew System::Windows::Forms::RichTextBox());
			this->label12 = (gcnew System::Windows::Forms::Label());
			this->textBox7 = (gcnew System::Windows::Forms::TextBox());
			this->textBox8 = (gcnew System::Windows::Forms::TextBox());
			this->textBox9 = (gcnew System::Windows::Forms::TextBox());
			this->label13 = (gcnew System::Windows::Forms::Label());
			this->label14 = (gcnew System::Windows::Forms::Label());
			this->richTextBox7 = (gcnew System::Windows::Forms::RichTextBox());
			this->label15 = (gcnew System::Windows::Forms::Label());
			this->richTextBox8 = (gcnew System::Windows::Forms::RichTextBox());
			this->label16 = (gcnew System::Windows::Forms::Label());
			this->richTextBox9 = (gcnew System::Windows::Forms::RichTextBox());
			this->textBox10 = (gcnew System::Windows::Forms::TextBox());
			this->textBox11 = (gcnew System::Windows::Forms::TextBox());
			this->textBox12 = (gcnew System::Windows::Forms::TextBox());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numericUpDown1))->BeginInit();
			this->SuspendLayout();
			// 
			// button_run
			// 
			this->button_run->BackColor = System::Drawing::Color::DarkOrange;
			this->button_run->Location = System::Drawing::Point(834, 591);
			this->button_run->Name = L"button_run";
			this->button_run->Size = System::Drawing::Size(145, 44);
			this->button_run->TabIndex = 1;
			this->button_run->Text = L"Старт";
			this->button_run->UseVisualStyleBackColor = false;
			this->button_run->Click += gcnew System::EventHandler(this, &MyForm::button_run_Click);
			// 
			// richTextBox1
			// 
			this->richTextBox1->Location = System::Drawing::Point(12, 283);
			this->richTextBox1->Name = L"richTextBox1";
			this->richTextBox1->Size = System::Drawing::Size(311, 43);
			this->richTextBox1->TabIndex = 2;
			this->richTextBox1->Text = L"";
			// 
			// numericUpDown1
			// 
			this->numericUpDown1->Location = System::Drawing::Point(713, 605);
			this->numericUpDown1->Maximum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 50000, 0, 0, 0 });
			this->numericUpDown1->Name = L"numericUpDown1";
			this->numericUpDown1->Size = System::Drawing::Size(96, 20);
			this->numericUpDown1->TabIndex = 3;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(7, 255);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(218, 25);
			this->label1->TabIndex = 4;
			this->label1->Text = L"Случайный массив";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label2->Location = System::Drawing::Point(7, 329);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(293, 25);
			this->label2->TabIndex = 6;
			this->label2->Text = L"Отсортированный массив";
			// 
			// richTextBox2
			// 
			this->richTextBox2->Location = System::Drawing::Point(12, 357);
			this->richTextBox2->Name = L"richTextBox2";
			this->richTextBox2->Size = System::Drawing::Size(311, 43);
			this->richTextBox2->TabIndex = 5;
			this->richTextBox2->Text = L"";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label3->Location = System::Drawing::Point(7, 403);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(206, 25);
			this->label3->TabIndex = 8;
			this->label3->Text = L"Обратный массив";
			// 
			// richTextBox3
			// 
			this->richTextBox3->Location = System::Drawing::Point(12, 431);
			this->richTextBox3->Name = L"richTextBox3";
			this->richTextBox3->Size = System::Drawing::Size(311, 46);
			this->richTextBox3->TabIndex = 7;
			this->richTextBox3->Text = L"";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label4->Location = System::Drawing::Point(446, 594);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(252, 31);
			this->label4->TabIndex = 9;
			this->label4->Text = L"Размер массивов";
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 11.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label5->Location = System::Drawing::Point(9, 480);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(274, 18);
			this->label5->TabIndex = 10;
			this->label5->Text = L"Время сортировки (наносекунды)";
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(12, 501);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(145, 20);
			this->textBox1->TabIndex = 11;
			// 
			// textBox2
			// 
			this->textBox2->Location = System::Drawing::Point(12, 527);
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(145, 20);
			this->textBox2->TabIndex = 12;
			// 
			// textBox3
			// 
			this->textBox3->Location = System::Drawing::Point(12, 553);
			this->textBox3->Name = L"textBox3";
			this->textBox3->Size = System::Drawing::Size(145, 20);
			this->textBox3->TabIndex = 13;
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label6->Location = System::Drawing::Point(52, 34);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(218, 62);
			this->label6->TabIndex = 14;
			this->label6->Text = L"Метод прямого\r\nвключения";
			this->label6->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label7->Location = System::Drawing::Point(405, 50);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(195, 31);
			this->label7->TabIndex = 25;
			this->label7->Text = L"Метод Шелла";
			this->label7->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// textBox4
			// 
			this->textBox4->Location = System::Drawing::Point(329, 553);
			this->textBox4->Name = L"textBox4";
			this->textBox4->Size = System::Drawing::Size(145, 20);
			this->textBox4->TabIndex = 24;
			// 
			// textBox5
			// 
			this->textBox5->Location = System::Drawing::Point(329, 527);
			this->textBox5->Name = L"textBox5";
			this->textBox5->Size = System::Drawing::Size(145, 20);
			this->textBox5->TabIndex = 23;
			// 
			// textBox6
			// 
			this->textBox6->Location = System::Drawing::Point(329, 501);
			this->textBox6->Name = L"textBox6";
			this->textBox6->Size = System::Drawing::Size(145, 20);
			this->textBox6->TabIndex = 22;
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 11.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label8->Location = System::Drawing::Point(326, 480);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(274, 18);
			this->label8->TabIndex = 21;
			this->label8->Text = L"Время сортировки (наносекунды)";
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label9->Location = System::Drawing::Point(324, 403);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(206, 25);
			this->label9->TabIndex = 20;
			this->label9->Text = L"Обратный массив";
			// 
			// richTextBox4
			// 
			this->richTextBox4->Location = System::Drawing::Point(329, 431);
			this->richTextBox4->Name = L"richTextBox4";
			this->richTextBox4->Size = System::Drawing::Size(332, 46);
			this->richTextBox4->TabIndex = 19;
			this->richTextBox4->Text = L"";
			// 
			// label10
			// 
			this->label10->AutoSize = true;
			this->label10->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label10->Location = System::Drawing::Point(324, 329);
			this->label10->Name = L"label10";
			this->label10->Size = System::Drawing::Size(293, 25);
			this->label10->TabIndex = 18;
			this->label10->Text = L"Отсортированный массив";
			// 
			// richTextBox5
			// 
			this->richTextBox5->Location = System::Drawing::Point(329, 357);
			this->richTextBox5->Name = L"richTextBox5";
			this->richTextBox5->Size = System::Drawing::Size(332, 43);
			this->richTextBox5->TabIndex = 17;
			this->richTextBox5->Text = L"";
			// 
			// label11
			// 
			this->label11->AutoSize = true;
			this->label11->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label11->Location = System::Drawing::Point(324, 255);
			this->label11->Name = L"label11";
			this->label11->Size = System::Drawing::Size(218, 25);
			this->label11->TabIndex = 16;
			this->label11->Text = L"Случайный массив";
			// 
			// richTextBox6
			// 
			this->richTextBox6->Location = System::Drawing::Point(329, 283);
			this->richTextBox6->Name = L"richTextBox6";
			this->richTextBox6->Size = System::Drawing::Size(332, 43);
			this->richTextBox6->TabIndex = 15;
			this->richTextBox6->Text = L"";
			// 
			// label12
			// 
			this->label12->AutoSize = true;
			this->label12->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label12->Location = System::Drawing::Point(707, 34);
			this->label12->Name = L"label12";
			this->label12->Size = System::Drawing::Size(231, 62);
			this->label12->TabIndex = 36;
			this->label12->Text = L"Пирамидальная\r\nсортировка";
			this->label12->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// textBox7
			// 
			this->textBox7->Location = System::Drawing::Point(667, 553);
			this->textBox7->Name = L"textBox7";
			this->textBox7->Size = System::Drawing::Size(145, 20);
			this->textBox7->TabIndex = 35;
			// 
			// textBox8
			// 
			this->textBox8->Location = System::Drawing::Point(667, 527);
			this->textBox8->Name = L"textBox8";
			this->textBox8->Size = System::Drawing::Size(145, 20);
			this->textBox8->TabIndex = 34;
			// 
			// textBox9
			// 
			this->textBox9->Location = System::Drawing::Point(667, 501);
			this->textBox9->Name = L"textBox9";
			this->textBox9->Size = System::Drawing::Size(145, 20);
			this->textBox9->TabIndex = 33;
			// 
			// label13
			// 
			this->label13->AutoSize = true;
			this->label13->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 11.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label13->Location = System::Drawing::Point(664, 480);
			this->label13->Name = L"label13";
			this->label13->Size = System::Drawing::Size(274, 18);
			this->label13->TabIndex = 32;
			this->label13->Text = L"Время сортировки (наносекунды)";
			// 
			// label14
			// 
			this->label14->AutoSize = true;
			this->label14->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label14->Location = System::Drawing::Point(662, 403);
			this->label14->Name = L"label14";
			this->label14->Size = System::Drawing::Size(206, 25);
			this->label14->TabIndex = 31;
			this->label14->Text = L"Обратный массив";
			// 
			// richTextBox7
			// 
			this->richTextBox7->Location = System::Drawing::Point(667, 431);
			this->richTextBox7->Name = L"richTextBox7";
			this->richTextBox7->Size = System::Drawing::Size(312, 46);
			this->richTextBox7->TabIndex = 30;
			this->richTextBox7->Text = L"";
			// 
			// label15
			// 
			this->label15->AutoSize = true;
			this->label15->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label15->Location = System::Drawing::Point(662, 329);
			this->label15->Name = L"label15";
			this->label15->Size = System::Drawing::Size(293, 25);
			this->label15->TabIndex = 29;
			this->label15->Text = L"Отсортированный массив";
			// 
			// richTextBox8
			// 
			this->richTextBox8->Location = System::Drawing::Point(667, 357);
			this->richTextBox8->Name = L"richTextBox8";
			this->richTextBox8->Size = System::Drawing::Size(312, 43);
			this->richTextBox8->TabIndex = 28;
			this->richTextBox8->Text = L"";
			// 
			// label16
			// 
			this->label16->AutoSize = true;
			this->label16->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label16->Location = System::Drawing::Point(662, 255);
			this->label16->Name = L"label16";
			this->label16->Size = System::Drawing::Size(218, 25);
			this->label16->TabIndex = 27;
			this->label16->Text = L"Случайный массив";
			// 
			// richTextBox9
			// 
			this->richTextBox9->Location = System::Drawing::Point(667, 283);
			this->richTextBox9->Name = L"richTextBox9";
			this->richTextBox9->Size = System::Drawing::Size(312, 43);
			this->richTextBox9->TabIndex = 26;
			this->richTextBox9->Text = L"";
			// 
			// textBox10
			// 
			this->textBox10->Location = System::Drawing::Point(12, 99);
			this->textBox10->Multiline = true;
			this->textBox10->Name = L"textBox10";
			this->textBox10->Size = System::Drawing::Size(311, 145);
			this->textBox10->TabIndex = 38;
			// 
			// textBox11
			// 
			this->textBox11->Location = System::Drawing::Point(329, 99);
			this->textBox11->Multiline = true;
			this->textBox11->Name = L"textBox11";
			this->textBox11->Size = System::Drawing::Size(332, 145);
			this->textBox11->TabIndex = 39;
			// 
			// textBox12
			// 
			this->textBox12->Location = System::Drawing::Point(667, 99);
			this->textBox12->Multiline = true;
			this->textBox12->Name = L"textBox12";
			this->textBox12->Size = System::Drawing::Size(312, 145);
			this->textBox12->TabIndex = 40;
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(993, 644);
			this->Controls->Add(this->textBox12);
			this->Controls->Add(this->textBox11);
			this->Controls->Add(this->textBox10);
			this->Controls->Add(this->label12);
			this->Controls->Add(this->textBox7);
			this->Controls->Add(this->textBox8);
			this->Controls->Add(this->textBox9);
			this->Controls->Add(this->label13);
			this->Controls->Add(this->label14);
			this->Controls->Add(this->richTextBox7);
			this->Controls->Add(this->label15);
			this->Controls->Add(this->richTextBox8);
			this->Controls->Add(this->label16);
			this->Controls->Add(this->richTextBox9);
			this->Controls->Add(this->label7);
			this->Controls->Add(this->textBox4);
			this->Controls->Add(this->textBox5);
			this->Controls->Add(this->textBox6);
			this->Controls->Add(this->label8);
			this->Controls->Add(this->label9);
			this->Controls->Add(this->richTextBox4);
			this->Controls->Add(this->label10);
			this->Controls->Add(this->richTextBox5);
			this->Controls->Add(this->label11);
			this->Controls->Add(this->richTextBox6);
			this->Controls->Add(this->label6);
			this->Controls->Add(this->textBox3);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->richTextBox3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->richTextBox2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->numericUpDown1);
			this->Controls->Add(this->richTextBox1);
			this->Controls->Add(this->button_run);
			this->Name = L"MyForm";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"MyForm";
			this->Load += gcnew System::EventHandler(this, &MyForm::MyForm_Load);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->numericUpDown1))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
		//старт
	private: System::Void button_run_Click(System::Object^ sender, System::EventArgs^ e);
	private:
		//прямое включение
		System::Void insertion(int*, int*);
		//метод Шелла
		System::Void shell(int*, int*);
		//пирамидальная сортировка
		System::Void heap_sort(int*, int*);
	private: System::Void MyForm_Load(System::Object^ sender, System::EventArgs^ e) {
	}
};
}
