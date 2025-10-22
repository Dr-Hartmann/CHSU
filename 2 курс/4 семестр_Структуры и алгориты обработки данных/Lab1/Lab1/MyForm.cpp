#include "MyForm.h"
#include <cstdint>
#include <cmath>
#include <iostream>
using namespace ::System::IO;
using namespace System;
using namespace System::Windows::Forms;
[STAThreadAttribute]
void main(array<String^>^ arg) {
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);
	Lab1::MyForm form;
	Application::Run(% form);
}

//класс дерева Хаффмана
class HaffmanTree {
private:
	struct Node {
		double p;
		uint8_t c;
		Node* left, * right, * parent;
	};
	Node* trees[256], * symbols[256];
	int size;

public:
	HaffmanTree(int col) : size(col) {}

	//чтение байтов
	void readInfo(double* byte_mass) {
		int real_size = 0;
		for (int i = 0; i < 256; i++)
			if (byte_mass[i] != 0) {
				trees[real_size] = new Node;
				symbols[real_size] = trees[real_size];
				trees[real_size]->c = i;
				trees[real_size]->p = byte_mass[i] / byte_mass[256];
				trees[real_size]->left = NULL;
				trees[real_size]->right = NULL;
				trees[real_size]->parent = NULL;
				real_size++;
			}
	}

	//создание дерева
	void makeTree(int col) {
		if (col > 1) {
			double minp1 = 1;
			int n1 = 0;
			for (int i = 0; i < size; i++)
				if (trees[i] != NULL && trees[i]->p < minp1) {
					minp1 = trees[i]->p;
					n1 = i;
				}
			double minp2 = 1;
			int n2 = 0;
			for (int i = 0; i < size; i++)
				if (trees[i] != NULL && trees[i]->p < minp2 && i != n1) {
					minp2 = trees[i]->p;
					n2 = i;
				}
			Node* tmp = new Node;
			tmp->left = trees[n1];
			tmp->right = trees[n2];
			trees[n1]->parent = tmp;
			trees[n2]->parent = tmp;
			tmp->p = trees[n1]->p + trees[n2]->p;
			tmp->parent = NULL;
			trees[n1] = tmp;
			trees[n2] = NULL;
			makeTree(col - 1);
		}
	}

	void showCodes() {
		StreamWriter^ file = gcnew StreamWriter("tree.txt", false);
		if (size == 1)
			file->Write(symbols[0]->c + "\n" + 0 + "\n");
		else {
			Node* tmp;
			String^ code;
			for (int i = 0; i < size; i++) {
				tmp = symbols[i];
				code = "";
				while (tmp->parent) {
					if (tmp->parent->left == tmp)
						code = "0" + code;
					else code = "1" + code;
					tmp = tmp->parent;
				}
				file->Write(symbols[i]->c + "\n" + code + "\n");
			}
		}
		file->Close();
	}
};

//открытие файла + архивация
System::Void Lab1::MyForm::архивацииToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e)
{
	//открытие файла
	OpenFileDialog^ openFile = gcnew OpenFileDialog;
	openFile->Filter = "Все файлы (*.*)|*.*";
	if (openFile->ShowDialog() == System::Windows::Forms::DialogResult::OK && openFile->FileName->Length > 0)
	{
		BinaryReader^ fout_size = gcnew BinaryReader(gcnew FileStream(openFile->FileName, FileMode::Open), System::Text::Encoding::Default);//чтение входного файла
		this->richTextBox1->Text = "";
		int counter = 512;

		double byte_mass[257] = { 0 };
		while (fout_size->PeekChar() > -1) {
			uint8_t b = fout_size->ReadByte();
			byte_mass[b]++;//частота байта
			byte_mass[256]++;//общее число байтов
			if (counter-- > 0)
				this->richTextBox1->Text += b + " ";
		}
		fout_size->Close();

		int size = 0;
		for (int i = 0; i < 256; i++)
			if (byte_mass[i] != 0)
				size++;//определение реального размера массива

		//создание дерева
		HaffmanTree tree(size);
		tree.readInfo(byte_mass);
		tree.makeTree(size);
		tree.showCodes();//запись дерева в файл

		//составление выходного кода
		StreamReader^ file_tree = gcnew StreamReader("tree.txt", false);
		BinaryReader^ fout_bin = gcnew BinaryReader(gcnew FileStream(openFile->FileName, FileMode::Open), System::Text::Encoding::Default);//чтение входного файла
		BinaryWriter^ fsave = gcnew BinaryWriter(gcnew FileStream(openFile->FileName + ".archive", FileMode::Create));//создание нового файла
		fsave->Write(byte_mass[256] + "\n");//запись общего числа байтов
		fsave->Write(size + "\n");//запись количества уникальных байтов

		uint8_t word;//байт
		array<String^>^ code = gcnew array<String^>(256);//массив бинарных кодов байтов

		while (file_tree->Peek() > -1) {
			word = System::Convert::ToByte(file_tree->ReadLine());
			code[word] = file_tree->ReadLine();
			fsave->Write(word + "\n");//запись байта
		}

		while (fout_bin->PeekChar() > -1) {
			fsave->Write(code[fout_bin->PeekChar()]);
			this->richTextBox1->Text += code[fout_bin->ReadByte()] + " ";
		}
		fsave->Close(); fout_bin->Close(); file_tree->Close();

		StreamReader^ file_out = gcnew StreamReader("tree.txt", false);
		this->richTextBox1->Text += "\n" + file_out->ReadToEnd();
		file_out->Close();

		String^ file_name = Path::GetFileNameWithoutExtension(openFile->FileName);//чтение имени файла без расширения
		String^ extension = Path::GetExtension(file_name);//чтение расширения файла
		String^ directoryName = Path::GetDirectoryName(openFile->FileName) + "\134";//чтение директории, восьмиричная система oct
		MessageBox::Show(L"Запись выполнена в файл '" + file_name + extension + ".archive" + "' по пути" + directoryName, L"Успех!", MessageBoxButtons::OK, MessageBoxIcon::Asterisk);
	}
}

//распаковка
System::Void Lab1::MyForm::распаковкиToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e)
{
	OpenFileDialog^ openFile = gcnew OpenFileDialog;
	openFile->Filter = "Все файлы (*.*)|*.*";
	if (openFile->ShowDialog() == System::Windows::Forms::DialogResult::OK && openFile->FileName->Length > 0)
	{
		BinaryReader^ fout = gcnew BinaryReader(gcnew FileStream(openFile->FileName, FileMode::Open), System::Text::Encoding::Default);//открытие архивного файла
		String^ file_name = Path::GetFileNameWithoutExtension(openFile->FileName);//чтение имени файла без расширения
		String^ extension = Path::GetExtension(file_name);//чтение расширения файла
		if (extension)
			file_name = Path::GetFileNameWithoutExtension(file_name);
		String^ directoryName = Path::GetDirectoryName(openFile->FileName) + "\134";//чтение директории, восьмиричная система oct
		BinaryWriter^ fsave = gcnew BinaryWriter(gcnew FileStream(directoryName + file_name + "_archive" + extension, FileMode::Create));//создание нового файла
		this->richTextBox2->Text = "";
		int counter = 512;

		while (fout->PeekChar() > -1)
		{
			uint8_t b = fout->ReadByte();
			if (counter-- > 0)
				this->richTextBox2->Text += b + " ";

			fsave->Write(b);
		}
		if (counter <= 0)
			this->richTextBox2->Text += "... ... ...";
		fout->Close();
		fsave->Close();
		MessageBox::Show(L"Запись выполнена в файл '" + file_name + "_archive" + "' по пути" + directoryName, L"Успех!", MessageBoxButtons::OK, MessageBoxIcon::Asterisk);
	}
}

//сохранение архива
System::Void Lab1::MyForm::сохранитьАрхивToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e)
{
	/*if (richTextBox1->Text != "")
	{*/
		//SaveFileDialog^ saveFile = gcnew SaveFileDialog;
		//saveFile->Filter = "Все файлы (*.*)|*.*";
		/*if (saveFile->ShowDialog() == System::Windows::Forms::DialogResult::OK && saveFile->FileName->Length > 0)
		{*/
			BinaryWriter^ fsave = gcnew BinaryWriter(gcnew FileStream("tect.txt"/*saveFile->FileName + ".archive"*/, FileMode::Create));
			String^ str = "";
			for (int i = 0; i < std::pow(128, 2)*2-1; i++) {
				str += "q";
			}
			fsave->Write(str);
			fsave->Close();

			BinaryReader^ fout = gcnew BinaryReader(gcnew FileStream("tect.txt"/*saveFile->FileName + ".archive"*/, FileMode::Open), System::Text::Encoding::Default);//открытие архивного файла
			String^ str1 = "";
			while (fout->PeekChar() > -1)
			{
				str1 += fout->ReadByte() + "\n";
			}
			fout->Close();
			this->richTextBox2->Text = str1;


		//}

		

	/*}
	else MessageBox::Show(L"Входная последовательность пуста", L"Ошибка!", MessageBoxButtons::OK, MessageBoxIcon::Asterisk);*/
}