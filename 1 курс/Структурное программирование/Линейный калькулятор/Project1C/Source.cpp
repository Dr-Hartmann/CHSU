#include <iostream>
#include <clocale>
using namespace std;

int main() {
	setlocale(LC_ALL, "Russian");
	double a,c;
	char b;

	cout << "������� �����\n";
	cin >> a;

	do {
		cout << "������� ��� ��������:\n'+'-��������\n'-'-���������\n'*'-���������\n'/'-�������\n";
	    cin >> b;
		if (b!='+' && b!='/' && b!='-' && b!='*') cout << "������!" << endl;
	} while (b != '+' && b != '/' && b != '-' && b != '*');


	   while (b!='=') {

	    cout << "������� ��������� �����\n";
	    cin >> c;

		  switch (b) {
		   case '+': a += c; break;
		   case '-': a -= c; break;
		   case '*': a *= c; break;
		   case '/': a /= c; break;
		   case '=': break;
		   default: {
			   cout << "������!" << endl << endl;
			   b = 'stop';
		   }
		  }

		  if (b=='=' || b=='stop') continue;

			do {
				cout << "������� ��� ��������:\n'+'-��������\n'-'-���������\n'*'-���������\n'/'-�������\n'='-���������\n";
				cin >> b;
				if (b != '+' && b != '/' && b != '-' && b != '*' && b != '=') cout << "������!" << endl;
			} while (b != '+' && b != '/' && b != '-' && b != '*' && b != '=');

			
	   }

	cout << a << endl;
    return 0;
}