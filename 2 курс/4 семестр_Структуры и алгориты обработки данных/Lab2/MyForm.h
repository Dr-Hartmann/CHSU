#pragma once
namespace Lab2 {
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;
	using namespace System::Text;
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
	private: System::Windows::Forms::Button^ button_start;
	private: System::Windows::Forms::TextBox^ textBox1;
	private: System::Windows::Forms::TextBox^ textBox2;
	private: System::Windows::Forms::TextBox^ textBox3;
	private: System::Windows::Forms::TextBox^ textBox4;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::RichTextBox^ textBox5;
	private: System::ComponentModel::IContainer^ components;
	private:

		void obhod(int a[][100], int* metka, int* out, int index, int num, int i, int n, int power, int * monsters_power) {
			metka[i] = ++num;
			out[++index] = i + 1;

			//буффер меток
			int* metka2{ new int[n] };
			//буффер вывода
			int* out2{ new int[n] };
			for (int r = 0; r < n; ++r)
				metka2[r] = metka[r], out2[r] = out[r];

			for (int j = 0; j < n; j++)
				if (a[i][j] == 1 && metka[j] == 0 && power >= monsters_power[j]) {
					obhod(a, metka, out, index, num, j, n, power + monsters_power[j], monsters_power);

					for (int r = 0; r < n; ++r)
						metka[r] = metka2[r], out[r] = out2[r];
				}

			delete[] metka2;
			if (index == n - 1) {
				for (int j = 0; j < n; ++j) {
					if (j) textBox5->Text += "->";
					textBox5->Text += out[j];
				}
				textBox5->Text += Environment::NewLine;
			}
		}

#pragma region Windows Form Designer generated code
		void InitializeComponent(void)
		{
			this->button_start = (gcnew System::Windows::Forms::Button());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->textBox3 = (gcnew System::Windows::Forms::TextBox());
			this->textBox4 = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->textBox5 = (gcnew System::Windows::Forms::RichTextBox());
			this->SuspendLayout();
			// 
			// button_start
			// 
			this->button_start->Location = System::Drawing::Point(612, 14);
			this->button_start->Name = L"button_start";
			this->button_start->Size = System::Drawing::Size(110, 46);
			this->button_start->TabIndex = 0;
			this->button_start->Text = L"Старт";
			this->button_start->UseVisualStyleBackColor = true;
			this->button_start->Click += gcnew System::EventHandler(this, &MyForm::button_start_Click);
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(211, 14);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(87, 20);
			this->textBox1->TabIndex = 1;
			// 
			// textBox2
			// 
			this->textBox2->Location = System::Drawing::Point(140, 40);
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(87, 20);
			this->textBox2->TabIndex = 2;
			// 
			// textBox3
			// 
			this->textBox3->Location = System::Drawing::Point(182, 80);
			this->textBox3->Name = L"textBox3";
			this->textBox3->Size = System::Drawing::Size(281, 20);
			this->textBox3->TabIndex = 3;
			// 
			// textBox4
			// 
			this->textBox4->Location = System::Drawing::Point(12, 145);
			this->textBox4->Multiline = true;
			this->textBox4->Name = L"textBox4";
			this->textBox4->Size = System::Drawing::Size(198, 266);
			this->textBox4->TabIndex = 4;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(12, 9);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(193, 25);
			this->label1->TabIndex = 8;
			this->label1->Text = L"Количество залов";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label2->Location = System::Drawing::Point(10, 34);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(124, 25);
			this->label2->TabIndex = 9;
			this->label2->Text = L"Сила героя";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label3->Location = System::Drawing::Point(12, 63);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(164, 50);
			this->label3->TabIndex = 10;
			this->label3->Text = L"Сила монстров\r\nпо залам";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label4->Location = System::Drawing::Point(10, 117);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(136, 25);
			this->label4->TabIndex = 11;
			this->label4->Text = L"Связи залов";
			// 
			// textBox5
			// 
			this->textBox5->Location = System::Drawing::Point(216, 145);
			this->textBox5->Name = L"textBox5";
			this->textBox5->Size = System::Drawing::Size(506, 266);
			this->textBox5->TabIndex = 15;
			this->textBox5->Text = L"";
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(734, 424);
			this->Controls->Add(this->textBox5);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->textBox4);
			this->Controls->Add(this->textBox3);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->button_start);
			this->Name = L"MyForm";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"MyForm";
			this->Load += gcnew System::EventHandler(this, &MyForm::MyForm_Load);
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion 

		//обход должен учитывать сторону двери, ее посещенность, силу монстра
	private: System::Void button_start_Click(System::Object^ sender, System::EventArgs^ e) {
		StreamReader^ f = gcnew StreamReader("input.txt");
		textBox5->Text = "";
		textBox4->Text = "";

		//массив наличия пути
		int a[100][100] = { 0 };

		//число залов
		textBox1->Text = f->ReadLine();
		int n = Convert::ToInt32(textBox1->Text);

		//стартовая сила
		textBox2->Text = f->ReadLine();
		int power = Convert::ToInt32(textBox2->Text);

		//сила монстров через пробел
		textBox3->Text = f->ReadLine();
		array <String^>^ monstrs = textBox3->Text->Split(' ');
		int* monsters_power{ new int[n] };

		//односторонние двери построчно
		String^ buffer = f->ReadToEnd();
		array <String^>^ doors = buffer->Split('\n', ' ');
		f->Close();

		//запись в массив наличия путей между дверьми и силы монстров
		for (int i = 0; i < doors->Length; i += 2) {
			textBox4->Text += doors[i] + " " + doors[i + 1] + Environment::NewLine;
			a[Convert::ToInt32(doors[i]) - 1][Convert::ToInt32(doors[i + 1]) - 1] = 1;
		}
		for (int i = 0; i < n; i++) {
			monsters_power[i] = Convert::ToInt32(monstrs[i]);
		}

		//вывод наличия путей
		for (int i = 0; i < n; i++) {
			textBox5->Text += (i + 1) + "\t";
			for (int j = 0; j < n; j++)
				textBox5->Text += a[i][j] + " ";
			textBox5->Text += Environment::NewLine;
		}
		for (int j = 0; j < n; j++)
			textBox5->Text += monsters_power[j] + " ";
		textBox5->Text += Environment::NewLine;

		//массив посещённых узлов
		int* metka{ new int[n] };

		//номер посещённого узла
		int num = 0;

		//запуск обхода
		textBox5->Text += Environment::NewLine;
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++)
				metka[j] = 0;

			//массив вывода
			int* out{ new int[n] };
			int index = -1;
			if (power >= monsters_power[i])
				obhod(a, metka, out, index, num, i, n, power + monsters_power[i], monsters_power);
			delete[] out;
		}
	}
	private: System::Void MyForm_Load(System::Object^ sender, System::EventArgs^ e) {
	}
	};
}
