#include <iostream>
#include <string>
#include <fstream>
#include <Windows.h>
using namespace std;

void SetColor(int text, int bg) {
	HANDLE hStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
	SetConsoleTextAttribute(hStdOut, (WORD)((bg << 4) | text));
}

int main() {
	int n1, n2;
	ifstream f("input.txt");
	f >> n1 >> n2;//Чтение размера массива: строки-столбцы
	cout << n1 << " " << n2 << endl;
	int** value = new int* [n1];
	int** bestValue = new int* [n1];
	int** direction = new int* [n1];

	for (int i = 0; i < n1; i++) {
		bestValue[i] = new int[n2];
		value[i] = new int[n2];
		direction[i] = new int[n2];
		for (int j = 0; j < n2; j++) {
			bestValue[i][j] = 0;
			f >> value[i][j];
			direction[i][j] = 0;
		}
	}

	for (int i = 0; i < n1; i++) bestValue[i][0] += value[i][0];//получили текущее значение столбца J при движении вправо

	for (int j = 0; j < n2 - 1; j++) {//n2 столбцов

		for (int i = 0; i < n1; i++)
			bestValue[i][j + 1] = bestValue[i][j] + value[i][j + 1];//получили следующее значение столбца J при движении вправо

		for (int i = 0; i < n1; i++) {//идём вправо-вверх

			if (i == 0)
				if (bestValue[i][j] + value[n1 - 1][j + 1] < bestValue[n1 - 1][j + 1]) {
					bestValue[n1 - 1][j + 1] = bestValue[i][j] + value[n1 - 1][j + 1];
					direction[n1 - 1][j + 1] = 1;
				}

			if (i != 0)
				if (bestValue[i][j] + value[i - 1][j + 1] < bestValue[i - 1][j + 1]) {
					bestValue[i - 1][j + 1] = bestValue[i][j] + value[i - 1][j + 1];
					direction[i - 1][j + 1] = 1;
				}
		}

		for (int i = n1 - 1; i >= 0; i--) {//идём вправо-вниз
			if (i == n1 - 1)
				if (bestValue[i][j] + value[0][j + 1] < bestValue[0][j + 1]) {
					bestValue[0][j + 1] = bestValue[i][j] + value[0][j + 1];
					direction[0][j + 1] = -1;
				}
			if (i != n1 - 1)
				if (bestValue[i][j] + value[i + 1][j + 1] < bestValue[i + 1][j + 1]) {
					bestValue[i + 1][j + 1] = bestValue[i][j] + value[i + 1][j + 1];
					direction[i + 1][j + 1] = -1;
				}
		}
	}
	cout << endl;

	int min;
	int* p = new int[n2];
	min = bestValue[0][n2 - 1];
	p[n2 - 1] = 0;
	for (int i = 1; i < n1; i++) {
		if (bestValue[i][n2 - 1] < min) {
			min = bestValue[i][n2 - 1];
			p[n2 - 1] = i;
		}
	}
	for (int j = n2 - 1; j > 0; j--) {
		p[j];
		if (direction[p[j]][j] == 1) {
			if (p[j] + 1 < n1)p[j - 1] = p[j] + 1;
			else p[j - 1] = 0;
		}
		else if (direction[p[j]][j] == 0) {
			p[j - 1] = p[j];
		}
		else if (direction[p[j]][j] == -1) {
			if (p[j] - 1 > 0)p[j - 1] = p[j] - 1;
			else p[j - 1] = n1-1;
		}
	}

	int width = 5;

	for (int i = 0; i < n1; i++) {
		for (int j = 0; j < n2; j++) {
			cout.width(width);
			if (p[j] == i) {
				SetColor(4, 0);
				cout << bestValue[i][j];
			}
			else {
				SetColor(7, 0);
				cout << bestValue[i][j];
			}
		}
		cout << endl;
	}
	cout << endl;
	SetColor(7, 0);

	for (int i = 0; i < n1; i++) {
		for (int j = 0; j < n2; j++) {
			cout.width(width);
			if (p[j] == i) {
				SetColor(4, 0);
				cout << value[i][j];
			}
			else {
				SetColor(7, 0);
			cout << value[i][j];
			}
		}
		cout << endl;
	}
	cout << endl;
	SetColor(7, 0);

	f.close();
	cout << endl;

	system("pause");
	return 0;
}