#pragma once
#include "Appliances.h"

class Matrix
{
public:
	Matrix(int n, int m);
	~Matrix();

	void add(int n, int m, Appliances* q) {
		mas[n][m] = q;
	}
	void del(int n, int m) {
		mas[n][m] = 0;
	}
	void search(Firm firm)const;
	void sort();
	void print()const;

private:
	Appliances*** mas;
	int n, m;
};