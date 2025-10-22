#include <iostream>
#include <string>
using namespace std;

int main() {
	setlocale(LC_ALL, "ru");
	const int n = 5;
	string x;

	//Структура поезда
	struct TRAIN {
		string lok;
		string date;
		string type; };

	//Массив поездов
	TRAIN rasp[n];
	for (int i = 0; i < n; i++) {

		cout << "Пункт назначения\n";
		cin >> rasp[i].lok; 
		cout << "Дата отправления\n";
		cin >> rasp[i].date;
		cout << "Тип поезда\n";
		cin >> rasp[i].type;
		cout << endl;
	}

	//Ввод нужного типа
	cout << "Введите нужный тип поездов\n";
	cin >> x;
	cout << endl;

	//Выдача всех совпадений
	for (int i = 0; i < n; i++) if (rasp[i].type == x) cout << i+1 << " " << rasp[i].lok << endl << rasp[i].date << endl << rasp[i].type << endl << endl;
	system("pause");
	return 0;
}
