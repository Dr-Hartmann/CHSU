#include "Matrix.h"
Matrix::Matrix(int n, int m)
{
	this->n = n; this->m = m;
	mas = new Appliances * *[n];
	for (size_t i = 0; i < n; i++)
	{
		mas[i] = new Appliances * [m];
		for (size_t j = 0; j < m; j++)
		{
			mas[i][j] = 0;
		}
	}
}


Matrix::~Matrix()
{
	for (size_t i = 0; i < n; i++)
	{
		delete[] mas[i];
	}
	delete[] mas;
}

void Matrix::search(Firm firm) const
{
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			if (mas[i][j]->get_firm() == firm) mas[i][j]->print();
		}
	}
}

void Matrix::sort()
{
	int a = 0, b = 0;
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			if (mas[i][j]) {
				if (*mas[i][j] == Firm::Lada) {
					std::swap(mas[i][j], mas[a][b++]);
				}
				if (b == m) {
					b = 0; a++;
				}
			}
		}
	}
}



void Matrix::print() const
{
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			if (mas[i][j]) {
				std::cout << *mas[i][j] << std::endl;
			}
		}
	}
}