#include <iostream>
using namespace std;
double res = 0;

double f1(double x, int n) {
	double a;
	do {
		a = 1 / ((2 * n + 1) * pow(x, 2 * n + 1));
		res += a;
		n++;
	} while (abs(a) >= 0.0005);
	return res; 1 / ((2 * n + 1) * pow(x, 2 * n + 1));
};

double f2(double *x, int *n) {
	double a;
	do {
		a = 1 / ((*n * 2 + 1) * pow(*x, *n * 2 + 1));
		res += a;
		(*n)++;
	} while (abs(a) >= 0.0005);
	return res;
};

double f3(double &a, int &b) {
	double c;
	do {
		c = 1 / ((b * 2 + 1) * pow(a, b * 2 + 1));
		res += c;
		b++;
	} while (abs(c) >= 0.0005);
	return res;
};


int main() {
	int b, n = 1;
	double x = 0.2;
	cout << "f(1, 2, 3)?\n";
	cin >> b;
	switch (b) {
	case 1: f1(x, n);
		break;
	case 2:f2(&x, &n);
		break;
	case 3: f3(x, n);
		break;
	default:;
	}
	cout << res << endl;

	system("pause");
	return 0;
}