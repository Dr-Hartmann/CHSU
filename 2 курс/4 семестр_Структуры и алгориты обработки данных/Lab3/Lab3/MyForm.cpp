#include "MyForm.h"
#include <chrono>
#include <ctime>
#include <iostream>
using namespace System;
using namespace System::Windows::Forms;
[STAThreadAttribute]
int main(array<String^>^ args)
{
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);
	Lab3::MyForm form;
	Application::Run(% form);
}

//метод прямого включения
System::Void Lab3::MyForm::insertion(int* mass, int* n) {
	for (int i = 1; i < *n; i++)
	{
		int x = mass[i], j = i;
		while (j != 0 && x < mass[j - 1]) {
			mass[j] = mass[j - 1];
			j--;
		}
		mass[j] = x;
	}
}

//метод Шелла
System::Void Lab3::MyForm::shell(int* mass, int* n) {
	int step = *n / 2;
	while (step > 0) {
		for (int i = 0; i < *n - step; i++) {
			int j = i;
			while (j >= 0 && mass[j] > mass[j + step]) {
				std::swap(mass[j], mass[j + step]);
				j -= step;
			}
		}
		step /= 2;
	}
}

//пирамидальная сортировка
void push_down(int* mass, int root, int bottom) {
	bool done = false;
	int max_child;
	while (2 * root + 1 <= bottom && !done) {
		if (2 * root + 1 == bottom)max_child = 2 * root + 1;
		else if (mass[2 * root + 1] > mass[2 * root + 2])max_child = 2 * root + 1;
		else max_child = 2 * root + 2;

		if (mass[root] < mass[max_child]) {
			std::swap(mass[root], mass[max_child]);
			root = max_child;
		}
		else done = true;
	}
}
System::Void Lab3::MyForm::heap_sort(int* mass, int* n) {
	for (int i = *n / 2 - 1; i >= 0; i--)
		push_down(mass, i, *n - 1);
	for (int i = *n - 1; i >= 0; i--) {
		std::swap(mass[0], mass[i]);
		push_down(mass, 0, i - 1);
	}
}

System::Void Lab3::MyForm::button_run_Click(System::Object^ sender, System::EventArgs^ e)
{
	if (numericUpDown1->Value > 0) {
		srand(time(NULL));

		int m = System::Convert::ToInt32(numericUpDown1->Value);
		int* random_array{ new int[m] };
		int* sorted_array{ new int[m] };
		int* reverse_array{ new int[m] };

		int count = 20;
		if (count > numericUpDown1->Value) count = System::Convert::ToInt32(numericUpDown1->Value);

		//изолированный блок теста
		{
			this->textBox10->Text = ""; this->textBox11->Text = ""; this->textBox12->Text = "";
			int k = 10;
			//тестовый массив 1
			int* small_array1{ new int[k] };
			for (int i = 0; i < k; i++)
				small_array1[i] = rand() % 100;
			for (int i = 0; i < k; i++)
				this->textBox10->Text += small_array1[i] + " ";
			this->textBox10->Text += Environment::NewLine;
			for (int i = 1; i < k; i++)
			{
				int x = small_array1[i], j = i;
				while (j != 0 && x < small_array1[j - 1]) {
					small_array1[j] = small_array1[j - 1];
					j--;
				}
				small_array1[j] = x;

				for (int u = 0; u < k; u++)
					this->textBox10->Text += small_array1[u] + " ";
				this->textBox10->Text += Environment::NewLine;
			}

			//тестовый массив 2
			int* small_array2{ new int[k] };
			for (int i = 0; i < k; i++)
				small_array2[i] = rand() % 100;
			for (int i = 0; i < k; i++)
				this->textBox11->Text += small_array2[i] + " ";
			this->textBox11->Text += Environment::NewLine;
			int step = k / 2;
			while (step > 0) {
				for (int i = 0; i < k - step; i++) {
					int j = i;
					while (j >= 0 && small_array2[j] > small_array2[j + step]) {
						std::swap(small_array2[j], small_array2[j + step]);
						j -= step;
					}
				}
				step /= 2;

				for (int u = 0; u < k; u++)
					this->textBox11->Text += small_array2[u] + " ";
				this->textBox11->Text += Environment::NewLine;
			}

			//тестовый массив 3
			int* small_array3{ new int[k] };
			for (int i = 0; i < k; i++)
				small_array3[i] = rand() % 100;
			for (int i = 0; i < k; i++)
				this->textBox12->Text += small_array3[i] + " ";
			this->textBox12->Text += Environment::NewLine;
			for (int i = k / 2 - 1; i >= 0; i--) {
				push_down(small_array3, i, k - 1);
				for (int u = 0; u < k; u++)
					this->textBox12->Text += small_array3[u] + " ";
				this->textBox12->Text += Environment::NewLine;
			}
			for (int i = k - 1; i >= 0; i--) {
				std::swap(small_array3[0], small_array3[i]);
				push_down(small_array3, 0, i - 1);
				for (int u = 0; u < k; u++)
					this->textBox12->Text += small_array3[u] + " ";
				this->textBox12->Text += Environment::NewLine;
			}
		}

		//изолированный блок метода прямого включения
		{
			this->richTextBox1->Text = ""; this->richTextBox2->Text = ""; this->richTextBox3->Text = "";
			this->textBox1->Text = ""; this->textBox2->Text = ""; this->textBox3->Text = "";

			//случайный массив
			for (int i = 0; i < m; i++)
				random_array[i] = rand() % 10000;
			//отсортированный массив
			for (int i = 0; i < m; i++)
				sorted_array[i] = i;
			//обратный массив
			for (int i = 0; i < m; i++)
				reverse_array[i] = m - i;

			//вывод массивов
			for (int i = 0; i < count; i++)
				this->richTextBox1->Text += random_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox1->Text += "...";
			this->richTextBox1->Text += "\n\n";
			for (int i = 0; i < count; i++)
				this->richTextBox2->Text += sorted_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox2->Text += "...";
			this->richTextBox2->Text += "\n\n";
			for (int i = 0; i < count; i++)
				this->richTextBox3->Text += reverse_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox3->Text += "...";
			this->richTextBox3->Text += "\n\n";

			//измерение скорости сортировки прямым включением
			auto begin_random_array = std::chrono::steady_clock::now();
			insertion(random_array, &m);
			auto end_random_array = std::chrono::steady_clock::now();
			auto elapsed_ms_random_array = std::chrono::duration_cast<std::chrono::nanoseconds>(end_random_array - begin_random_array);
			for (int i = 0; i < count; i++)
				this->richTextBox1->Text += random_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox1->Text += "...";
			this->textBox1->Text += elapsed_ms_random_array.count();

			auto begin_sorted_array = std::chrono::steady_clock::now();
			insertion(sorted_array, &m);
			auto end_sorted_array = std::chrono::steady_clock::now();
			auto elapsed_ms_sorted_array = std::chrono::duration_cast<std::chrono::nanoseconds>(end_sorted_array - begin_sorted_array);
			for (int i = 0; i < count; i++)
				this->richTextBox2->Text += sorted_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox2->Text += "...";
			this->textBox2->Text += elapsed_ms_sorted_array.count();

			auto begin_reverse_array = std::chrono::steady_clock::now();
			insertion(reverse_array, &m);
			auto end_reverse_array = std::chrono::steady_clock::now();
			auto elapsed_ms_reverse_array = std::chrono::duration_cast<std::chrono::nanoseconds>(end_reverse_array - begin_reverse_array);
			for (int i = 0; i < count; i++)
				this->richTextBox3->Text += reverse_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox3->Text += "...";
			this->textBox3->Text += elapsed_ms_reverse_array.count();
		}

		//изолированный блок метода Шелла
		{
			this->richTextBox6->Text = ""; this->richTextBox5->Text = ""; this->richTextBox4->Text = "";
			this->textBox6->Text = ""; this->textBox5->Text = ""; this->textBox4->Text = "";

			//случайный массив
			for (int i = 0; i < m; i++)
				random_array[i] = rand() % 10000;
			//отсортированный массив
			for (int i = 0; i < m; i++)
				sorted_array[i] = i;
			//обратный массив
			for (int i = 0; i < m; i++)
				reverse_array[i] = m - i;

			//вывод массивов
			for (int i = 0; i < count; i++)
				this->richTextBox6->Text += random_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox6->Text += "...";
			this->richTextBox6->Text += "\n\n";
			for (int i = 0; i < count; i++)
				this->richTextBox5->Text += sorted_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox5->Text += "...";
			this->richTextBox5->Text += "\n\n";
			for (int i = 0; i < count; i++)
				this->richTextBox4->Text += reverse_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox4->Text += "...";
			this->richTextBox4->Text += "\n\n";

			//измерение скорости сортировки прямым включением
			auto begin_random_array = std::chrono::steady_clock::now();
			shell(random_array, &m);
			auto end_random_array = std::chrono::steady_clock::now();
			auto elapsed_ms_random_array = std::chrono::duration_cast<std::chrono::nanoseconds>(end_random_array - begin_random_array);
			for (int i = 0; i < count; i++)
				this->richTextBox6->Text += random_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox6->Text += "...";
			this->textBox6->Text += elapsed_ms_random_array.count();

			auto begin_sorted_array = std::chrono::steady_clock::now();
			shell(sorted_array, &m);
			auto end_sorted_array = std::chrono::steady_clock::now();
			auto elapsed_ms_sorted_array = std::chrono::duration_cast<std::chrono::nanoseconds>(end_sorted_array - begin_sorted_array);
			for (int i = 0; i < count; i++)
				this->richTextBox5->Text += sorted_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox5->Text += "...";
			this->textBox5->Text += elapsed_ms_sorted_array.count();

			auto begin_reverse_array = std::chrono::steady_clock::now();
			shell(reverse_array, &m);
			auto end_reverse_array = std::chrono::steady_clock::now();
			auto elapsed_ms_reverse_array = std::chrono::duration_cast<std::chrono::nanoseconds>(end_reverse_array - begin_reverse_array);
			for (int i = 0; i < count; i++)
				this->richTextBox4->Text += reverse_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox4->Text += "...";
			this->textBox4->Text += elapsed_ms_reverse_array.count();
		}

		//изолированный блок пирамидальной сортировки
		{
			this->richTextBox9->Text = ""; this->richTextBox8->Text = ""; this->richTextBox7->Text = "";
			this->textBox9->Text = ""; this->textBox8->Text = ""; this->textBox7->Text = "";

			//случайный массив
			for (int i = 0; i < m; i++)
				random_array[i] = rand() % 10000;
			//отсортированный массив
			for (int i = 0; i < m; i++)
				reverse_array[i] = i;
			//обратный массив
			for (int i = 0; i < m; i++)
				sorted_array[i] = m - i;

			//вывод массивов
			for (int i = 0; i < count; i++)
				this->richTextBox9->Text += random_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox9->Text += "...";
			this->richTextBox9->Text += "\n\n";
			for (int i = 0; i < count; i++)
				this->richTextBox7->Text += sorted_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox7->Text += "...";
			this->richTextBox7->Text += "\n\n";
			for (int i = 0; i < count; i++)
				this->richTextBox8->Text += reverse_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox8->Text += "...";
			this->richTextBox8->Text += "\n\n";

			//измерение скорости сортировки прямым включением
			auto begin_random_array = std::chrono::steady_clock::now();
			heap_sort(random_array, &m);
			auto end_random_array = std::chrono::steady_clock::now();
			auto elapsed_ms_random_array = std::chrono::duration_cast<std::chrono::nanoseconds>(end_random_array - begin_random_array);
			for (int i = 0; i < count; i++)
				this->richTextBox9->Text += random_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox9->Text += "...";
			this->textBox9->Text += elapsed_ms_random_array.count();

			auto begin_sorted_array = std::chrono::steady_clock::now();
			heap_sort(sorted_array, &m);
			auto end_sorted_array = std::chrono::steady_clock::now();
			auto elapsed_ms_sorted_array = std::chrono::duration_cast<std::chrono::nanoseconds>(end_sorted_array - begin_sorted_array);
			for (int i = 0; i < count; i++)
				this->richTextBox8->Text += sorted_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox8->Text += "...";
			this->textBox8->Text += elapsed_ms_sorted_array.count();

			auto begin_reverse_array = std::chrono::steady_clock::now();
			heap_sort(reverse_array, &m);
			auto end_reverse_array = std::chrono::steady_clock::now();
			auto elapsed_ms_reverse_array = std::chrono::duration_cast<std::chrono::nanoseconds>(end_reverse_array - begin_reverse_array);
			for (int i = 0; i < count; i++)
				this->richTextBox7->Text += reverse_array[i] + " ";
			if (count < numericUpDown1->Value)this->richTextBox7->Text += "...";
			this->textBox7->Text += elapsed_ms_reverse_array.count();
		}
	}
	else MessageBox::Show(L"Неверный размер массива", L"Ошибка!", MessageBoxButtons::OK, MessageBoxIcon::Error);
}