#pragma once
#include "Appliances.h"
#include "Firm.h"
#include <iostream>
#include <string>

//класс-контейнер
template<typename T>
class Template
{
public:
	Template(int n, int m)
	{
		this->n = n; this->m = m;
		mas = new T * *[n];
		for (size_t i = 0; i < n; i++)
		{
			mas[i] = new T * [m];
			for (size_t j = 0; j < m; j++)
			{
				mas[i][j] = 0;
			}
		}
	}

	~Template()
	{
		for (size_t i = 0; i < n; i++)
		{
			delete[] mas[i];
		}
		delete[] mas;
	}

	size_t get_n() {
		return n;
	}

	size_t get_m() {
		return m;
	}

	void add(int n, int m, T* q) {
		mas[n][m] = q;
	}

	void del(int n, int m) {
		mas[n][m] = 0;
	}

	void search(Firm firm)const
	{
		for (size_t i = 0; i < n; i++) {
			for (size_t j = 0; j < m; j++) {
				if (mas[i][j]->get_firm() == firm) mas[i][j]->print();
			}
		}
	}

	void sort()
	{
		int a = 0, b = 0;
		for (size_t i = 0; i < n; i++) {
			for (size_t j = 0; j < m; j++) {
				if (mas[i][j]) {
					if (mas[i][j]->get_firm() == Firm::Lada)
						std::swap(mas[i][j], mas[a][b++]);
					if (b == m) {
						b = 0; a++;
					}
				}
			}
		}
	}

	void print()const
	{
		for (size_t i = 0; i < n; i++) {
			for (size_t j = 0; j < m; j++) {
				if (mas[i][j])
					mas[i][j]->print();
			}
		}
	}

	T* read(int i, int j)const
	{
		return mas[i][j];
	}



private:
	T*** mas;
	int n, m;
};