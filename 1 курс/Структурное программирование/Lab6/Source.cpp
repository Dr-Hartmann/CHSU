#include <iostream>
using namespace std;

int nerecurs(int n) {
	int sum = 0;
	for (int i = 1; i <= n; i++) {
		sum += pow(2 * i, 2);
	}
	return (sum);
};

int recurs(int n) {
	if (n=0) return (0);
	else recurs(n-1)+ pow(2 * n, 2);
};

void recurss(int n, int *sum) {
	*sum += pow(2 * n, 2);
	if (n!=0) recurss(n-1, sum);
};

int main() {
	int n;
	int sum = 0;
	cout << "n="; cin >> n;
	recurss(n, &sum);
	cout << nerecurs(n) << endl << recurs(n) << endl << sum << endl;
	system("pause");
	return 0;
}
