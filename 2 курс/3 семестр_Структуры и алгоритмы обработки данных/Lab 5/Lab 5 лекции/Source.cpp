#include <iostream>
#include <string>
#include <fstream>
using namespace std;

int main() {
	int n, m;
	ifstream f("input.txt");
	f >> n >> m;//Чтение размера массива
	cout << n << m << endl;
	int* curentFloor = new int[m];//curentLine
	int* bestFloor = new int[m];//bestLine
	int** direction = new int* [n];

	for (int i = 0; i < n; i++) {
		direction[i] = new int[m];
		for (int j = 0; j < m; j++) {
			direction[i][j] = 0;
		}
	}
	for (int i = 0; i < m; i++) {
		bestFloor[i] = 0;
	}

	////////////////////////////////
	for (int i = 0; i < n; i++) {

		for (int j = 0; j < m; j++) {
			cout.width(3);
			f >> curentFloor[j];
			bestFloor[j] += curentFloor[j];//идём снизу
		}
		cout << endl;

		for (int j = 1; j < m; j++)
			if (bestFloor[j - 1] + curentFloor[j] < bestFloor[j]) {
				bestFloor[j] = bestFloor[j - 1] + curentFloor[j];//идём слева
				direction[i][j] = -1;
			}

		for (int j = m - 2; j >= 0; j--)
			if (bestFloor[j + 1] + curentFloor[j] < bestFloor[j]) {
				bestFloor[j] = bestFloor[j + 1] + curentFloor[j];
				direction[i][j] = 1;
			}

		for (int j = 0; j < m; j++) {
			cout.width(3);
			cout << bestFloor[j] ;
		}
		cout << endl;

	}

	int min = 1000000, nmin = -1;

	for (int j = 0; j < m; j++)
		if (min > bestFloor[j]) {
			min = bestFloor[j];
			nmin = j;
		}
	cout << nmin << " - " << min << endl;

	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			cout.width(3);
			cout << direction[i][j];
		}
		cout << endl;
	}

		f.close();
		cout << endl;
	

	system("pause");
	return 0;
}