#include <iostream>
#include <time.h>
using namespace std;

int vk(int a) { return (a*a*a); };

int main() {
	setlocale(LC_ALL, "Ru");
	int a;
	cout << "������� ����� ����\n";
	cin >> a;

	cout << vk(a);

	system("pause");
	return 0;
}