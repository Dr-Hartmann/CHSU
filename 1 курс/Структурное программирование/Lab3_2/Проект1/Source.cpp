#include <iostream>
#include <time.h>
using namespace std;

void vvod(int n, int *a) {
	for (int i = 0; i < n; i++) {
		a[i] = rand() % 100;
	}
};

void vivod(int n, int *a) {
	for (int i = 0; i < n; i++) {
		cout << a[i] << " ";
	}
};

int main() {
	setlocale(LC_ALL, "Ru");
	srand(time(0));
	int n, x;
	cout << "n="; cin >> n;
	cout << "x="; cin >> x;
	int *a = new int[n];

	vvod(n, a);
	vivod(n, a);
	cout << endl;

	for (int i = 0; i < n; i++) {
		if (a[i] > x) a[i] = x;
	}

	vivod(n, a);
	cout << endl;

	system("pause");
	return 0;
}
