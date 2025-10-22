#include <iostream>
using namespace std;

double fact(int n) {
	if (n == 1) return 1;
	if (n == 0) return 0;
	return n * fact(n - 1);
}
double first(double x, double e) {
	double rez = 0;
	int i = 2;
	while (abs(x) >= e) {
		rez += x;
		x = pow(2*x, 2*i) / (fact(2*i));
		i++;
	}
	return rez;
}

void second(double* x, double* rez, double *e) {
	*rez = 0;
	double a = *x;
	int i = 1;
	while (abs(a) >= *e) {
		*rez += a;
		a = pow(2 * x, 2 * i) / (fact(2 * i));
		i++;
	}
}
double& third(double& x, double& rez, double &e) {
	rez = 0;
	int i = 1;
	while (abs(x) >= e) {
		rez += x;
		x = pow(2 * x, 2 * i) / (fact(2 * i));
		i++;
	}
	return rez;
}
int main() {
	setlocale(LC_ALL, "rus");
	double res;
	double res2;
	double x1 = 0.2;

	int inp;
	cout << endl << "¬ведите номер механизма передачи параметров" << endl;
	cin >> inp; cout << endl;

	switch (inp)
	{
	case 1:
		cout << fu1(x1);
		break;

	case 2:
		fu2(&x1, &res);
		cout << endl << res << endl;
		break;

	case 3:
		cout << fu3(x1, res2) << endl;
		break;
	}




	system("pause");
	return 0;
}



