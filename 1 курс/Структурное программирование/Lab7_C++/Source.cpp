#include <iostream>
#include <string>
#include <fstream>
using namespace std;

int main() {
    setlocale(LC_ALL, "ru"); 
    string s;
    ifstream f;
    f.open("NePust.txt");
    while (!f.eof()) {
        getline(f, s);
        if(s[0]=='c') cout << s[0] << endl;
    }
    f.close();

    system("pause");
    return 0;
}