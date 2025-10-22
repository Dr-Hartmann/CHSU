#include "MyForm.h"
#include <iostream>
#include <msclr\marshal_cppstd.h>
using namespace System;
using namespace System::Windows::Forms;
using namespace System::IO;
[STAThreadAttribute]
int main(array<String^>^ args)
{
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);
	Lab4::MyForm form;
	Application::Run(% form);
}

//�������� ������
class Node {
public:
	std::string word;
	int count;
	Node* left, * right;
	static int size;
};

int Node::size = 0;

//������ ���� � ������
void insert(Node*& root, std::string* word) {
	if (root == NULL) {
		root = new Node;
		root->word = *word;
		root->count = 1;
		root->right = 0;
		root->left = 0;
		Node::size++;
	}
	else if (*word < root->word)
		insert(root->left, word);
	else if (*word > root->word)
		insert(root->right, word);
	else root->count++;
}

//����� ������ �� �������� (������������ �����)
void printTree2(Node* node, StreamWriter^% file, StreamWriter^% file_tree) {
	if (node != NULL) {
		printTree2(node->left, file, file_tree);
		file->Write(gcnew String(node->word.c_str()) + "\t\t-    " + node->count + "\n");//�������������� �� std::string � String^
		file_tree->WriteLine(gcnew String(node->word.c_str()));
		file_tree->WriteLine(node->count);
		printTree2(node->right, file, file_tree);
	}
}

//����� �����
void shell(Node** mass, int n) {
	int step = n / 2;
	while (step > 0) {
		for (int i = 0; i < n - step; i++) {
			int j = i;
			while (j >= 0 && mass[j]->count < mass[j + step]->count) {
				std::swap(mass[j], mass[j + step]);
				j -= step;
			}
		}
		step /= 2;
	}
}


//���������� ������� ������� ������
void searchTree(Node* node, Node** mass, int* counter) {
	if (node != NULL) {
		mass[*counter] = node;
		*counter = *counter + 1;
		searchTree(node->left, mass, counter);
		searchTree(node->right, mass, counter);
	}
}

//���������� �� �������
void printFrequency(Node* node, StreamWriter^% file) {
	Node** mass{ new Node * [node->size] };
	int counter = 0;
	searchTree(node, mass, &counter);

	shell(mass, counter);

	for (size_t i = 0; i < counter; i++)
	{
		file->Write(gcnew String(mass[i]->word.c_str()) + "\t\t-    " + mass[i]->count + "\n");//System::Convert::ToString(node->size)
	}

	//file->Write(System::Convert::ToString(counter));
	delete[] mass;
}

//�������� ������
void deleteTree(Node* node) {
	if (node != NULL) {
		deleteTree(node->left);
		deleteTree(node->right);
		delete node;
	}
}

void search_word(StreamReader^% file_tree, int* counter, String^% word1) {
	while (file_tree->Peek() > -1) {
		if (file_tree->ReadLine() == word1) {
			*counter = Convert::ToInt32(file_tree->ReadLine());
			break;
		}
		else file_tree->ReadLine();
	}
}

System::Void Lab4::MyForm::�������������ToolStripMenuItem_Click(System::Object^ sender, System::EventArgs^ e)
{
	OpenFileDialog^ openFile1 = gcnew OpenFileDialog;
	openFile1->DefaultExt = "*.txt";
	openFile1->Filter = "��������� ����� (*.txt)|*.txt|��� ����� (*.*)|*.*";
	if (openFile1->ShowDialog() == System::Windows::Forms::DialogResult::OK && openFile1->FileName->Length > 0)
		richTextBox1->Text = System::IO::File::ReadAllText(openFile1->FileName, System::Text::Encoding::Default);
}

System::Void Lab4::MyForm::start_Click(System::Object^ sender, System::EventArgs^ e)
{
	if (richTextBox1->Text != "") {
		Node* tree = NULL;//������ ������
		int row = 0;//���������� ���� � �������
		String^ word = "";//�����

		array <String^>^ seps = { "\t", "\n", "\r", " ", ".", ",", "!", "?", "-", ":", ";" , "�", "_" , "�", "[", "]", "(", ")", "/", "*", "\"", "�", "�", "�", "�"};
		array <String^>^ text = richTextBox1->Text->Split(seps, StringSplitOptions::RemoveEmptyEntries);

		for (int i = 0; i < text->Length; i++) {
			word = text[i];
			//������ ���� � ������ ������
			std::string word_std = msclr::interop::marshal_as<std::string>(word);//�������������� �� String^ � std::string
			insert(tree, &word_std);
			word = "";
		}
		//������ ������ � ��������
		StreamWriter^ file_tree = gcnew StreamWriter("tree.txt", false, System::Text::Encoding::Unicode);
		//������ ������ � ���������� �������
		StreamWriter^ file0 = gcnew StreamWriter("tree_alphabet.txt", false, System::Text::Encoding::Unicode);
		printTree2(tree, file0, file_tree);
		file0->Close();
		file_tree->Close();

		//������ ������ � ������� ������� ������������ ����
		StreamWriter^ file1 = gcnew StreamWriter("tree_frequency.txt", false, System::Text::Encoding::Unicode);
		printFrequency(tree, file1);
		file1->Close();

		//����� ������ � ��������� �������
		StreamReader^ fout = gcnew StreamReader("tree_alphabet.txt");
		richTextBox2->Text = fout->ReadToEnd();
		fout->Close();

		deleteTree(tree);//�������� ��������

		MessageBox::Show(L"���������� �������� � ���� 'tree_alphabet'", L"�����!", MessageBoxButtons::OK, MessageBoxIcon::Asterisk);
	}
	else MessageBox::Show(L"������ ������� ������", L"������!", MessageBoxButtons::OK, MessageBoxIcon::Error);
}

//���������� �� ��������
System::Void Lab4::MyForm::sort_alphabetically_Click(System::Object^ sender, System::EventArgs^ e)
{
	StreamReader^ fout = gcnew StreamReader("tree_alphabet.txt");
	richTextBox2->Text = fout->ReadToEnd();
	fout->Close();
}

//���������� �� �������
System::Void Lab4::MyForm::sort_frequency_Click(System::Object^ sender, System::EventArgs^ e)
{
	StreamReader^ fout = gcnew StreamReader("tree_frequency.txt");
	richTextBox2->Text = fout->ReadToEnd();
	fout->Close();
}

//����� �����
System::Void Lab4::MyForm::find_Click(System::Object^ sender, System::EventArgs^ e)
{
	StreamReader^ file_tree = gcnew StreamReader("tree.txt");
	int counter = -1;
	String^ word = textBox1->Text;
	search_word(file_tree, &counter, word);
	if (counter > 0) textBox2->Text = word + ", " + counter + " ���";
	else textBox2->Text = "����� �� �������";
	file_tree->Close();
}

//������� ����� ����������� �����
System::Void Lab4::MyForm::find_long_Click(System::Object^ sender, System::EventArgs^ e)
{
	if (numericUpDown1->Value > 0) {
		String^ word;
		StreamReader^ file_tree = gcnew StreamReader("tree.txt");
		StreamWriter^ file = gcnew StreamWriter("long.txt", false, System::Text::Encoding::Unicode);
		while (file_tree->Peek() > -1) {
			word = file_tree->ReadLine();
			if (word->Length == numericUpDown1->Value) {
				file->Write(word + "\t\t-    " + file_tree->ReadLine() + "\n");
			}
			else file_tree->ReadLine();
		}
		file->Close();
		file_tree->Close();
		StreamReader^ file1 = gcnew StreamReader("long.txt");
		richTextBox2->Text = file1->ReadToEnd();
		file1->Close();
	}
	else {
		StreamReader^ fout = gcnew StreamReader("tree_alphabet.txt");
		richTextBox2->Text = fout->ReadToEnd();
		fout->Close();
	}
}