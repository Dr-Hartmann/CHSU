#include "MyForm.h"
#include <iostream>
using namespace System;
using namespace System::Windows::Forms;
using namespace System::IO;
using namespace System::ComponentModel;
using namespace System::Drawing;
[STAThread]
int main(array<String^>^ args)
{
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);
	Lab5::MyForm form;
	Application::Run(% form);
}

class Hash_table {
public:
	Hash_table(int numericUpDown, int a, int c)
		: numericUpDown(numericUpDown), a(a), c(c) {
		collisions = 0; max_chain = 0; occupancy = 0;
	}
	~Hash_table() {
		for (size_t i = 0; i < numericUpDown; i++) {
			B[i].nullQueue();
		}
		delete[] B;
	}
	void add_element(int x) {
		h = (a * x + c) % numericUpDown;
		B[h].add(x);
	}
	void print_table() {
		StreamWriter^ file = gcnew StreamWriter("hash.txt", false);
		for (size_t i = 0; i < numericUpDown; i++) {
			file->Write(B[i].print());
			if (B[i].get_chain() > 0) occupancy++;
			if (B[i].get_chain() > 1) collisions++;
			if (B[i].get_chain() > max_chain) {
				max_chain = B[i].get_chain();
				index_max_chain = i;
			}
		}
		file->Close();
	}
	int get_collisions()const { return collisions; }
	int get_max_chain()const { return max_chain; }
	int get_index_max_chain()const { return index_max_chain; }
	double get_occupancy()const { return occupancy; }

private:
	long int h;
	int a; int c;
	int numericUpDown;
	double occupancy;
	int max_chain;
	int index_max_chain;
	int collisions;

	class Queue {
	public:
		Queue() {
			head = NULL;
			tail = NULL;
			chain = 0;
		}
		~Queue() {}

		//геттер длины цепочки
		int get_chain()const { return chain; }

		//проверка на пустоту
		bool empty() { return head == NULL; }

		//добавление элемента
		void add(int value) {
			if (empty()) {
				head = new Node;
				head->data = value;
				head->next = NULL;
				tail = head;
				chain = 1;
			}
			else {
				tail->next = new Node;
				tail = tail->next;
				tail->data = value;
				tail->next = NULL;
				chain++;
			}
		}

		//вывод очереди
		String^ print() {
			String^ line = "";
			if (!empty()) {
				Node* tmp = head;
				while (head != NULL) {
					line += head->data + " ";
					head = head->next;
				}
				head = tmp;
			}
			return line + "\n";
		}

		//удаление очереди
		void nullQueue() {
			Node* tmp;
			while (!empty()) {
				tmp = head;
				head = head->next;
				delete(tmp);
			}
		}
	private:
		struct Node {
			int data;
			Node* next;
		};
		Node* head, * tail;
		//жлина цепочки
		int chain;
	};
	Queue* B = new Queue[numericUpDown];
};

//загрузка текстового файла
System::Void Lab5::MyForm::file_open_Click(System::Object^ sender, System::EventArgs^ e)
{
	OpenFileDialog^ openFile1 = gcnew OpenFileDialog;
	openFile1->DefaultExt = "*.txt";
	openFile1->Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
	if (openFile1->ShowDialog() == System::Windows::Forms::DialogResult::OK && openFile1->FileName->Length > 0) {
		richTextBox1->Text = "";
		richTextBox3->Text = "";
		richTextBox_log->Text = "";
		richTextBox1->Text = System::IO::File::ReadAllText(openFile1->FileName, System::Text::Encoding::Default);
	}
}

System::Void Lab5::MyForm::run_Click(System::Object^ sender, System::EventArgs^ e)
{
	if (numericUpDown1->Value != 0 && richTextBox1->Text != "") {
		richTextBox3->Text = "";
		array <String^>^ int_file = richTextBox1->Text->Split(',', ' ', '\n', '\r', '\t', '\t');
		Hash_table hash(System::Convert::ToInt32(numericUpDown1->Value), System::Convert::ToInt32(numeric_a->Value), System::Convert::ToInt32(numeric_c->Value));
		for (int i = 0; i < int_file->Length; i++)
			if (int_file[i] != "" && int_file[i] != " ")
				hash.add_element(System::Convert::ToInt32(int_file[i]));
		//запись хеш-таблицы в текстовый файл "hash.txt"
		hash.print_table();
		//чтение хеш-таблицы из текстового файла в файл "hash_table.txt"
		StreamReader^ file = gcnew StreamReader("hash.txt");
		StreamWriter^ file_new = gcnew StreamWriter("hash_table.txt", false);
		for (size_t i = 0; i < numericUpDown1->Value; i++) {
			array <String^>^ hash = file->ReadLine()->Split(' ', '\n', '\r');
			file_new->Write("[" + i + "] ");
			for (int j = 0; j < hash->Length; j++)
			{
				if (hash[j] != "" && hash[j] != " ") {
					if (j != 0)file_new->Write("  ->  ");
					file_new->Write(hash[j]);
				}
			}
			file_new->Write("\n");
		}
		file->Close();
		file_new->Close();

		StreamReader^ file_table = gcnew StreamReader("hash_table.txt");
		richTextBox3->Text = file_table->ReadToEnd();
		file_table->Close();

		label_percent->Text = System::Convert::ToString(hash.get_occupancy() / System::Convert::ToInt32(numericUpDown1->Value) * 100);
		label_collisions->Text = System::Convert::ToString(hash.get_collisions());
		label_chain->Text = System::Convert::ToString(hash.get_max_chain()) + " (" + System::Convert::ToString(hash.get_index_max_chain()) + ")";
		richTextBox_log->Text += "B - " + numericUpDown1->Value + ", a - " + numeric_a->Value + ", c - " + numeric_c->Value + ": " + label_percent->Text + "%, collisions: "
			+ label_collisions->Text + ", max chain: " + label_chain->Text + Environment::NewLine;

		MessageBox::Show(L"Таблица записана в файл 'hash_table.txt'", L"Программа успешно выполнена", MessageBoxButtons::OK);
	}
	else {
		MessageBox::Show(L"Неверное количество классов или пустая входная строка", L"Ошибка!", MessageBoxButtons::OK, MessageBoxIcon::Error);
	}
}

System::Void Lab5::MyForm::button_search_Click(System::Object^ sender, System::EventArgs^ e)
{
	richTextBox4->Text = "";
	int h = (System::Convert::ToInt32(numeric_a->Value) * System::Convert::ToInt32(numeric_element->Value) + System::Convert::ToInt32(numeric_c->Value)) % System::Convert::ToInt32(numericUpDown1->Value);
	StreamReader^ file = gcnew StreamReader("hash.txt");
	for (size_t i = 0; i < h; i++)
		file->ReadLine();
	array <String^>^ number = file->ReadLine()->Split(' ', '\n', '\r');
	for (size_t i = 0; i < number->Length; i++)
	{
		if (number[i]!="" && System::Convert::ToInt32(numeric_element->Value) == System::Convert::ToInt32(number[i])) {
			richTextBox4->Text = h + " класс, " + i + " элемент";
			file->Close();
			return;
		}
		else if (i = number->Length - 1) {
			MessageBox::Show(L"Элемент не найден", L"Программа успешно выполнена", MessageBoxButtons::OK);
		}
	}
	file->Close();
}
