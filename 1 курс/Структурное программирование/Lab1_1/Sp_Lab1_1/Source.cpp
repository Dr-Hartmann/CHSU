#include <iostream>
#include <string>
using namespace std;

int main() {
	setlocale(LC_ALL, "ru");
	const int n = 5;
	string x;

	//��������� ������
	struct TRAIN {
		string lok;
		string date;
		string type; };

	//������ �������
	TRAIN rasp[n];
	for (int i = 0; i < n; i++) {

		cout << "����� ����������\n";
		cin >> rasp[i].lok; 
		cout << "���� �����������\n";
		cin >> rasp[i].date;
		cout << "��� ������\n";
		cin >> rasp[i].type;
		cout << endl;
	}

	//���� ������� ����
	cout << "������� ������ ��� �������\n";
	cin >> x;
	cout << endl;

	//������ ���� ����������
	for (int i = 0; i < n; i++) if (rasp[i].type == x) cout << i+1 << " " << rasp[i].lok << endl << rasp[i].date << endl << rasp[i].type << endl << endl;
	system("pause");
	return 0;
}
