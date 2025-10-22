#pragma once
#include <cmath>
#include <string>
#include <fstream>
#include <Windows.h>

std::string SystemStringToStdString(System::String^ str)
{
	char* pstr = static_cast<char*>(System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(str).ToPointer());
	std::string resultString(pstr);
	System::Runtime::InteropServices::Marshal::FreeHGlobal(System::IntPtr(pstr));
	return resultString;
}
const char endStr[] = { 13,10,0 };

namespace Feistel {
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	public ref class MyForm1 : public System::Windows::Forms::Form
	{
	public:
		MyForm1(void)
		{
			InitializeComponent();
		}
	protected:
		~MyForm1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^ button2;
	protected:
	private: System::Windows::Forms::Button^ button1;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::CheckBox^ checkBox3;
	private: System::Windows::Forms::CheckBox^ checkBox2;
	private: System::Windows::Forms::CheckBox^ checkBox1;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Button^ button3;
	private: System::Windows::Forms::Button^ button4;
	private: System::Windows::Forms::Button^ button5;
	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::Label^ label6;
	private: System::Windows::Forms::ToolStripMenuItem^ сохранитьВФайлToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ сохранитьToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ открытьToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ дляЗашифровкиToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ расшифровкиToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^ выходToolStripMenuItem;
	private: System::Windows::Forms::MenuStrip^ menuStrip2;
	private: System::Windows::Forms::OpenFileDialog^ openFileDialog1;
	private: System::Windows::Forms::SaveFileDialog^ saveFileDialog1;
	private: System::Windows::Forms::RichTextBox^ Message;
	private: System::Windows::Forms::RichTextBox^ Decrypted;
	private: System::Windows::Forms::RichTextBox^ Ciphertext;
	private: System::Windows::Forms::RichTextBox^ Encrypted;
	private: System::Windows::Forms::Button^ button6;
	private:
		System::ComponentModel::Container^ components;

#pragma region Windows Form Designer generated code
		void InitializeComponent(void)
		{
			System::ComponentModel::ComponentResourceManager^ resources = (gcnew System::ComponentModel::ComponentResourceManager(MyForm1::typeid));
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->checkBox3 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox2 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox1 = (gcnew System::Windows::Forms::CheckBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->button3 = (gcnew System::Windows::Forms::Button());
			this->button4 = (gcnew System::Windows::Forms::Button());
			this->button5 = (gcnew System::Windows::Forms::Button());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->сохранитьВФайлToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->сохранитьToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->открытьToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->дляЗашифровкиToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->расшифровкиToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->выходToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->menuStrip2 = (gcnew System::Windows::Forms::MenuStrip());
			this->openFileDialog1 = (gcnew System::Windows::Forms::OpenFileDialog());
			this->saveFileDialog1 = (gcnew System::Windows::Forms::SaveFileDialog());
			this->Message = (gcnew System::Windows::Forms::RichTextBox());
			this->Decrypted = (gcnew System::Windows::Forms::RichTextBox());
			this->Ciphertext = (gcnew System::Windows::Forms::RichTextBox());
			this->Encrypted = (gcnew System::Windows::Forms::RichTextBox());
			this->button6 = (gcnew System::Windows::Forms::Button());
			this->menuStrip2->SuspendLayout();
			this->SuspendLayout();
			// 
			// button2
			// 
			this->button2->FlatStyle = System::Windows::Forms::FlatStyle::System;
			this->button2->Location = System::Drawing::Point(857, 203);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(108, 42);
			this->button2->TabIndex = 31;
			this->button2->Text = L"Расшифровать";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &MyForm1::button2_Click);
			// 
			// button1
			// 
			this->button1->FlatStyle = System::Windows::Forms::FlatStyle::System;
			this->button1->Location = System::Drawing::Point(356, 335);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(108, 39);
			this->button1->TabIndex = 30;
			this->button1->Text = L"Зашифровать";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm1::button1_Click);
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Font = (gcnew System::Drawing::Font(L"Open Sans", 18, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label4->Location = System::Drawing::Point(513, 258);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(356, 33);
			this->label4->TabIndex = 28;
			this->label4->Text = L"Расшифрованное сообщение";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Font = (gcnew System::Drawing::Font(L"Open Sans", 18, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label3->Location = System::Drawing::Point(513, 24);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(445, 33);
			this->label3->TabIndex = 23;
			this->label3->Text = L"Введите зашифрованное сообщение\r\n";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Open Sans", 18, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label2->Location = System::Drawing::Point(6, 396);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(452, 33);
			this->label2->TabIndex = 21;
			this->label2->Text = L"Зашифрованная последовательность\r\n";
			// 
			// checkBox3
			// 
			this->checkBox3->AutoSize = true;
			this->checkBox3->Location = System::Drawing::Point(238, 600);
			this->checkBox3->Name = L"checkBox3";
			this->checkBox3->Size = System::Drawing::Size(85, 17);
			this->checkBox3->TabIndex = 20;
			this->checkBox3->Text = L"+2 - символ";
			this->checkBox3->UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this->checkBox2->AutoSize = true;
			this->checkBox2->Location = System::Drawing::Point(139, 600);
			this->checkBox2->Name = L"checkBox2";
			this->checkBox2->Size = System::Drawing::Size(85, 17);
			this->checkBox2->TabIndex = 19;
			this->checkBox2->Text = L"+1 - символ";
			this->checkBox2->UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this->checkBox1->AutoSize = true;
			this->checkBox1->Location = System::Drawing::Point(12, 600);
			this->checkBox1->Name = L"checkBox1";
			this->checkBox1->Size = System::Drawing::Size(121, 17);
			this->checkBox1->TabIndex = 18;
			this->checkBox1->Text = L"Обращение в ноль";
			this->checkBox1->UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Open Sans", 18, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(6, 24);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(317, 33);
			this->label1->TabIndex = 16;
			this->label1->Text = L"Введите Ваше сообщение";
			// 
			// button3
			// 
			this->button3->Location = System::Drawing::Point(12, 496);
			this->button3->Name = L"button3";
			this->button3->Size = System::Drawing::Size(137, 40);
			this->button3->TabIndex = 37;
			this->button3->Text = L"Скопировать в буфер обмена";
			this->button3->UseVisualStyleBackColor = true;
			this->button3->Click += gcnew System::EventHandler(this, &MyForm1::button3_Click);
			// 
			// button4
			// 
			this->button4->Location = System::Drawing::Point(873, 584);
			this->button4->Name = L"button4";
			this->button4->Size = System::Drawing::Size(92, 33);
			this->button4->TabIndex = 38;
			this->button4->Text = L"Очистить всё";
			this->button4->UseVisualStyleBackColor = true;
			this->button4->Click += gcnew System::EventHandler(this, &MyForm1::button4_Click);
			// 
			// button5
			// 
			this->button5->Location = System::Drawing::Point(519, 203);
			this->button5->Name = L"button5";
			this->button5->Size = System::Drawing::Size(137, 42);
			this->button5->TabIndex = 39;
			this->button5->Text = L"Вставить содержимое буфера обмена";
			this->button5->UseVisualStyleBackColor = true;
			this->button5->Click += gcnew System::EventHandler(this, &MyForm1::button5_Click);
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Font = (gcnew System::Drawing::Font(L"Open Sans", 9.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label5->Location = System::Drawing::Point(9, 560);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(228, 18);
			this->label5->TabIndex = 40;
			this->label5->Text = L"Дополнительные преобразования:";
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Font = (gcnew System::Drawing::Font(L"Open Sans", 18, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label6->Location = System::Drawing::Point(12, 9);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(0, 33);
			this->label6->TabIndex = 41;
			// 
			// сохранитьВФайлToolStripMenuItem
			// 
			this->сохранитьВФайлToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(3) {
				this->сохранитьToolStripMenuItem,
					this->открытьToolStripMenuItem, this->выходToolStripMenuItem
			});
			this->сохранитьВФайлToolStripMenuItem->Name = L"сохранитьВФайлToolStripMenuItem";
			this->сохранитьВФайлToolStripMenuItem->Size = System::Drawing::Size(48, 20);
			this->сохранитьВФайлToolStripMenuItem->Text = L"Файл";
			// 
			// сохранитьToolStripMenuItem
			// 
			this->сохранитьToolStripMenuItem->Name = L"сохранитьToolStripMenuItem";
			this->сохранитьToolStripMenuItem->Size = System::Drawing::Size(146, 22);
			this->сохранитьToolStripMenuItem->Text = L"Сохранить";
			this->сохранитьToolStripMenuItem->Click += gcnew System::EventHandler(this, &MyForm1::сохранитьToolStripMenuItem_Click);
			// 
			// открытьToolStripMenuItem
			// 
			this->открытьToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(2) {
				this->дляЗашифровкиToolStripMenuItem,
					this->расшифровкиToolStripMenuItem
			});
			this->открытьToolStripMenuItem->Name = L"открытьToolStripMenuItem";
			this->открытьToolStripMenuItem->Size = System::Drawing::Size(146, 22);
			this->открытьToolStripMenuItem->Text = L"Открыть для:";
			// 
			// дляЗашифровкиToolStripMenuItem
			// 
			this->дляЗашифровкиToolStripMenuItem->Name = L"дляЗашифровкиToolStripMenuItem";
			this->дляЗашифровкиToolStripMenuItem->Size = System::Drawing::Size(153, 22);
			this->дляЗашифровкиToolStripMenuItem->Text = L"Зашифровки";
			this->дляЗашифровкиToolStripMenuItem->Click += gcnew System::EventHandler(this, &MyForm1::дляЗашифровкиToolStripMenuItem_Click);
			// 
			// расшифровкиToolStripMenuItem
			// 
			this->расшифровкиToolStripMenuItem->Name = L"расшифровкиToolStripMenuItem";
			this->расшифровкиToolStripMenuItem->Size = System::Drawing::Size(153, 22);
			this->расшифровкиToolStripMenuItem->Text = L"Расшифровки";
			this->расшифровкиToolStripMenuItem->Click += gcnew System::EventHandler(this, &MyForm1::расшифровкиToolStripMenuItem_Click);
			// 
			// выходToolStripMenuItem
			// 
			this->выходToolStripMenuItem->Name = L"выходToolStripMenuItem";
			this->выходToolStripMenuItem->Size = System::Drawing::Size(146, 22);
			this->выходToolStripMenuItem->Text = L"Выход";
			this->выходToolStripMenuItem->Click += gcnew System::EventHandler(this, &MyForm1::выходToolStripMenuItem_Click);
			// 
			// menuStrip2
			// 
			this->menuStrip2->BackColor = System::Drawing::Color::WhiteSmoke;
			this->menuStrip2->GripStyle = System::Windows::Forms::ToolStripGripStyle::Visible;
			this->menuStrip2->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->сохранитьВФайлToolStripMenuItem });
			this->menuStrip2->Location = System::Drawing::Point(0, 0);
			this->menuStrip2->Name = L"menuStrip2";
			this->menuStrip2->RenderMode = System::Windows::Forms::ToolStripRenderMode::System;
			this->menuStrip2->Size = System::Drawing::Size(977, 24);
			this->menuStrip2->TabIndex = 43;
			this->menuStrip2->Text = L"menuStrip2";
			// 
			// openFileDialog1
			// 
			this->openFileDialog1->FileName = L"openFileDialog1";
			// 
			// Message
			// 
			this->Message->Location = System::Drawing::Point(12, 60);
			this->Message->Name = L"Message";
			this->Message->Size = System::Drawing::Size(452, 269);
			this->Message->TabIndex = 44;
			this->Message->Text = L"";
			// 
			// Decrypted
			// 
			this->Decrypted->Location = System::Drawing::Point(519, 294);
			this->Decrypted->Name = L"Decrypted";
			this->Decrypted->Size = System::Drawing::Size(446, 284);
			this->Decrypted->TabIndex = 45;
			this->Decrypted->Text = L"";
			// 
			// Ciphertext
			// 
			this->Ciphertext->Location = System::Drawing::Point(519, 60);
			this->Ciphertext->Name = L"Ciphertext";
			this->Ciphertext->Size = System::Drawing::Size(446, 137);
			this->Ciphertext->TabIndex = 46;
			this->Ciphertext->Text = L"";
			// 
			// Encrypted
			// 
			this->Encrypted->Location = System::Drawing::Point(12, 432);
			this->Encrypted->Name = L"Encrypted";
			this->Encrypted->Size = System::Drawing::Size(452, 58);
			this->Encrypted->TabIndex = 47;
			this->Encrypted->Text = L"";
			// 
			// button6
			// 
			this->button6->BackColor = System::Drawing::Color::Red;
			this->button6->ForeColor = System::Drawing::SystemColors::ButtonFace;
			this->button6->Location = System::Drawing::Point(967, 0);
			this->button6->Name = L"button6";
			this->button6->Size = System::Drawing::Size(10, 10);
			this->button6->TabIndex = 48;
			this->button6->UseVisualStyleBackColor = false;
			this->button6->Click += gcnew System::EventHandler(this, &MyForm1::button6_Click);
			// 
			// MyForm1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::Color::WhiteSmoke;
			this->ClientSize = System::Drawing::Size(977, 629);
			this->Controls->Add(this->button6);
			this->Controls->Add(this->Encrypted);
			this->Controls->Add(this->Ciphertext);
			this->Controls->Add(this->Decrypted);
			this->Controls->Add(this->Message);
			this->Controls->Add(this->label6);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->button5);
			this->Controls->Add(this->button4);
			this->Controls->Add(this->button3);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->checkBox3);
			this->Controls->Add(this->checkBox2);
			this->Controls->Add(this->checkBox1);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->menuStrip2);
			this->Icon = (cli::safe_cast<System::Drawing::Icon^>(resources->GetObject(L"$this.Icon")));
			this->Name = L"MyForm1";
			this->Text = L"Магма";
			this->menuStrip2->ResumeLayout(false);
			this->menuStrip2->PerformLayout();
			this->ResumeLayout(false);
			this->PerformLayout();
		}

#pragma endregion

		   //////////////////////////////////////////////////////////
		   //Преобразование кода строки в двоичный код
		   String^ Transformation(String^ str, int strlen) {
			   int n; String^ istr;
			   for (int len = 0; len < strlen; len++) {
				   String^ strl;
				   n = str[len] - 0;
				   do {
					   strl += n % 2;
					   n /= 2;
				   } while (n > 0);
				   while (strl->Length < 16) strl += 0;
				   for (int i = 0; i < strl->Length; i++) istr += strl[15 - i];
			   }
			   return istr;
		   }
		   //////////////////////////////////////////////////////////

		   //////////////////////////////////////////////////////////
		   //Дополнение кода до степени двойки
		   String^ Addition(String^ istr, int* block_count) {
			   int two = 1;
			   while (two < *block_count) two *= 2;
			   if (two > *block_count) *block_count = two;
			   for (int i = istr->Length; i < *block_count * 64; i++) istr += 1;
			   return istr;
		   }
		   //////////////////////////////////////////////////////////

		   //////////////////////////////////////////////////////////
		   //Зашифрование
		   String^ Encoding(String^ istr, int block_count) {
			   String^ res;
			   for (int n = 0; n < block_count; n++) {
				   String^ block64;
				   for (int i = 0; i < 64; i++) block64 += istr[n * 64 + i];
				   res += sum2(block64);
				   block64 = "";
			   }
			   return res;
		   }
		   //////////////////////////////////////////////////////////

		   //////////////////////////////////////////////////////////
		   //Расшифрование
		   String^ Decoding(String^ res, int block_count, int strlen) {
			   String^ dec; String^ code; String^ block64;
			   for (int n = 0; n < block_count; n++) {
				   for (int i = 0; i < 64; i++) block64 += res[n * 64 + i];
				   code += sum2(block64);
				   block64 = "";
			   }

			   for (int i = 0; i < strlen; i++) {
				   int pos = 0; int decode = 0;
				   for (int r = 15; r >= 0; r--) {
					   if (code[i * 16 + pos] == 49) decode += pow(2, r);
					   pos++;
				   }
				   dec += Convert::ToChar(decode);
			   }
			   return dec;
		   }
		   //////////////////////////////////////////////////////////

		   int F(int r, int n) {
			   if (checkBox1->Checked)r= 0;
			   if (checkBox2->Checked)r= n+1-r;
			   if (checkBox3->Checked)r = n + 2 - r;
			   return r;
		   }
		   //////////////////////////////////////////////////////////
		   //Сумма по модулю 2 левого и правого блоков
		   String^ sum2(String^ sum) {
			   for (int round = 0; round < 32; round++) {
				   String^ Lstr; String^ Rstr;
				   //sum = F(sum);
				   for (int i = 0; i < 32; i++) {
					   int l = sum[i];
					   int r = sum[i + 32];
					   Rstr += (l + F(r, i)) % 2;
					   Lstr += r - 48;
				   }
				   sum = Lstr + Rstr;
				   if (round == 31) sum = Rstr + Lstr;

			   }
			   return sum;
		   }
		   //////////////////////////////////////////////////////////


	private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e) {

		if (String::IsNullOrEmpty(Message->Text)) {
			MessageBox::Show("Нет исходных данных, заполните поле сообщения!!!",
				"Критическая ошибка!", MessageBoxButtons::OK, MessageBoxIcon::Error);
			return;
		};

		String^ str = Message->Text;
		int strlen = str->Length;
		int block_count = (strlen + 3) / 4;
		String^ istr = Transformation(str, strlen);
		istr = Addition(istr, &block_count);
		Encrypted->Text = Encoding(istr, block_count) + strlen;
	}


	private: System::Void button2_Click(System::Object^ sender, System::EventArgs^ e) {

		if (String::IsNullOrEmpty(Ciphertext->Text)) {
			MessageBox::Show("Нет исходных данных, заполните поле сообщения!!!",
				"Критическая ошибка!", MessageBoxButtons::OK, MessageBoxIcon::Error);
			return;
		};

		String^ res;
		res = Ciphertext->Text;
		int strlen = res->Length, block_count, meslen = 0;
		for (int i = 0; meslen == 0; i++) if (strlen - pow(2, 7 + i) < 0) meslen = pow(2, 7 + i - 1);

		for (int len = 0; len < meslen; len++) {
			if (res[len] != 48 && res[len] != 49) {
				MessageBox::Show("Неправильный тип исходных данных!!!",
					"Критическая ошибка!", MessageBoxButtons::OK, MessageBoxIcon::Error);
				return;
			}
		};

		block_count = meslen / 64;
		int x=0, p = 0;
		for (int n = strlen - meslen; n - 1 >= 0; n--) {
			int k = res[meslen + p] - 48;
			x += k * pow(10, n - 1);
			p++;
		}
		strlen = x;

		Decrypted->Text = Decoding(res, block_count, strlen);
	}


	private: System::Void button3_Click(System::Object^ sender, System::EventArgs^ e) {
		if (String::IsNullOrEmpty(Encrypted->Text)) {
			MessageBox::Show("Нет исходных данных, заполните поле для копирования!!!",
				"Критическая ошибка!", MessageBoxButtons::OK, MessageBoxIcon::Error);
			return;
		};
		Clipboard::SetText(Encrypted->Text);
	}

	private: System::Void button4_Click(System::Object^ sender, System::EventArgs^ e) {
		Encrypted->Text = "";
		Ciphertext->Text = "";
		Decrypted->Text = "";
		Message->Text = "";
		BackColor = Color::WhiteSmoke;
	}
	private: System::Void button5_Click(System::Object^ sender, System::EventArgs^ e) {
		Ciphertext->Text = Clipboard::GetText();
	}


	private: System::Void выходToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e) {
		Close();
	}


	private: System::Void сохранитьToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e) {
		if (saveFileDialog1->ShowDialog() != System::Windows::Forms::DialogResult::OK) return;
		std::string fileName = SystemStringToStdString(saveFileDialog1->FileName);
		if (fileName.empty()) return;
		std::ofstream out(fileName.c_str());
		std::string saveText = SystemStringToStdString(Encrypted->Text);
		out << saveText;
	}


	private: System::Void дляЗашифровкиToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e) {
		if (openFileDialog1->ShowDialog() != System::Windows::Forms::DialogResult::OK) return;
		std::string fileName = SystemStringToStdString(openFileDialog1->FileName);
		if (fileName.empty()) return;
		std::ifstream in(fileName.c_str());
		String^ systemEndStr = gcnew String(endStr);
		while (!in.eof()) {
			std::string s;
			getline(in, s);
			String^ myInfoDisp = gcnew String(s.c_str());
			Message->Text += (myInfoDisp + systemEndStr);
		}
	}


	private: System::Void расшифровкиToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e) {
		if (openFileDialog1->ShowDialog() != System::Windows::Forms::DialogResult::OK) return;
		std::string fileName = SystemStringToStdString(openFileDialog1->FileName);
		if (fileName.empty()) return;
		std::ifstream in(fileName.c_str());
		while (!in.eof()) {
			std::string s;
			getline(in, s);
			String^ myInfoDisp = gcnew String(s.c_str());
			Ciphertext->Text = myInfoDisp;
		}
	}


	private: System::Void button6_Click(System::Object^ sender, System::EventArgs^ e) {
		BackColor = Color::Red;
	}
};
}