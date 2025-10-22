#include <iostream>
#include <string>
#include <fstream>
#include <time.h>
using namespace std;

void pluss(int* bin, int i) {
	while (bin[i - 1] == 1) bin[i-- - 1] = 0;
	bin[i - 1] = 1;
}

int main() {
	string s; int a, count = 0, sum = 0, array[21]; array[0] = 0;
	cout << "A="; cin >> a;
	ifstream f("NePust.txt");
	while (!f.eof()) {
		getline(f, s);
		array[count + 1] = stoi(s);
		cout << s << ", ";
		count++;
	}
	f.close();
	cout << endl;

	ofstream fout;
	fout.open("Result.txt", ios_base::out | ios_base::app);
	int bin[20] = { 0 };
	while (bin[0] == 0) {
		int result = 0;
		for (int i = 1; i <= count; i++) {
			if (i == 1) result = array[1];
			else {
				if (bin[i - 1] == 0) result += array[i];
				else result -= array[i];
			}
		}
		if (result == a) {
			//время
			/*time_t tim = time(NULL);
			char str[26];
			ctime_s(str, sizeof str, &tim);
			fout << endl << str;*/

			fout << array[1];
			for (int j = 2; j <= count; j++) {
				if (bin[j - 1] == 0) fout << "+" << array[j];
				else fout << "-" << array[j];
			}
			fout << "=" << a << endl;
			sum++;
		}
		pluss(bin, count);
	}

	cout << "Number of options: " << sum << endl;
	fout.close();
	
	cout << "The result is written to the file 'Result.txt'" << endl;

	cout << endl;
	system("pause");
	return 0;
}