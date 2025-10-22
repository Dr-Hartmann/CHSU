#include <iostream>
#include <time.h>
#include <string>
using namespace std;

int manipulation(int n, int* ia) {
	int sum = 0;
	for (int i = 0; i < n; i++) {
		ia[i] = rand() % 10;
		cout << ia[i] << " ";
	}
	cout << endl;

	for (int i = 0; i < n; i++) {
		if (ia[i] == ia[0]) sum += ia[i];
	}
	return (sum);
};

float manipulation(int n, float* fa) {
	float sum = 0;
	for (int i = 0; i < n; i++) {
		fa[i] = (rand() % 1000) / 100.0;
		cout << fa[i] << "  ";
	}
	cout << endl;

	for (int i = 0; i < n; i++) {
		if (fa[i] == fa[0]) sum += fa[i];
	}
	return (sum);
};

int manipulation(int n, char* ca) {
	int sum = 0;
	for (int i = 0; i < n; i++) {
		ca[i] = 'c';
		cout << ca[i] << " ";
	}
	cout << endl;

	for (int i = 0; i < n; i++) {
		if (ca[i] == ca[0]) sum += ca[i];
	}
	return (sum);
};

string manipulation(int n, string* sa) {
	string sum = "";
	for (int i = 0; i < n; i++) {
		sa[i] = "s";
		cout << sa[i] << " ";
	}
	cout << endl;

	for (int i = 0; i < n; i++) {
		if (sa[i] == sa[0]) sum += sa[i];
	}
	return (sum);
};

int main() {
	srand(time(0));
	const int n = 10;
	int ia[n]; float fa[n]; char ca[n]; string sa[n];

	cout << manipulation(n, ia) << endl << manipulation(n, fa) << endl << manipulation(n, ca) << endl << manipulation(n, sa) << endl;
	system("pause");
	return 0;
}