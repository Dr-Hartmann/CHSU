#include <iostream>
#include <string>
using namespace std;

int main() {
	setlocale(LC_ALL, "ru");

	char str[100];
	cout << "������� ������ � ��������" << endl;
	gets_s(str);
	
	int x;
	cout << "����� ����� ������..." << endl;
	cin >> x;

	char seps[] = " ,";
	char* token = strtok(str, seps);

	while (token != NULL) {
		if (strlen(token) > x) cout << token << endl;
		token = strtok(NULL, seps);
	}
	system("pause");
	return 0;
}