#include <iostream>
#include <clocale>
using namespace std;

int main() {
	setlocale(LC_ALL, "Russian");
	double a,c;
	char b;

	cout << "Введите число\n";
	cin >> a;

	do {
		cout << "Введите тип операции:\n'+'-сложение\n'-'-вычитание\n'*'-умножение\n'/'-деление\n";
	    cin >> b;
		if (b!='+' && b!='/' && b!='-' && b!='*') cout << "ОШИБКА!" << endl;
	} while (b != '+' && b != '/' && b != '-' && b != '*');


	   while (b!='=') {

	    cout << "Введите следующее число\n";
	    cin >> c;

		  switch (b) {
		   case '+': a += c; break;
		   case '-': a -= c; break;
		   case '*': a *= c; break;
		   case '/': a /= c; break;
		   case '=': break;
		   default: {
			   cout << "ОШИБКА!" << endl << endl;
			   b = 'stop';
		   }
		  }

		  if (b=='=' || b=='stop') continue;

			do {
				cout << "Введите тип операции:\n'+'-сложение\n'-'-вычитание\n'*'-умножение\n'/'-деление\n'='-результат\n";
				cin >> b;
				if (b != '+' && b != '/' && b != '-' && b != '*' && b != '=') cout << "ОШИБКА!" << endl;
			} while (b != '+' && b != '/' && b != '-' && b != '*' && b != '=');

			
	   }

	cout << a << endl;
    return 0;
}