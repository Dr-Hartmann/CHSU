#include <iostream>
#include <fstream>
#include <time.h>
using namespace std;

int main() {
	setlocale(LC_ALL, "Ru");
	int count = 0;
	srand(time(0));
	int *n = new int;
	double x;

	cout << "n=";
	cin >> *n;

	fstream f("Chisla.bin", ios::binary | ios::out);
	f.write((char*)&*n, sizeof(n));
	for (int i = 0; i < *n; i++) {
		x = (rand() % 10000 - 1000)/10.0;
		f.write((char*)&x, sizeof(x));
	}
	f.close();

	int k;
	double a;
		f.open("Chisla.bin", ios::binary | ios::in);
		while (!f.eof()) {
		f.read((char*)&k, sizeof(k));
		for (int i = 0; i < k; i++) {
			f.read((char*)&a, sizeof(a));
			if (fmod(a, 5.0) == 0) count++;
		}
	}
	f.close();

	cout << endl;
	cout << "Кратных 5: " << count << endl;
	system("pause");
	return 0;
}
