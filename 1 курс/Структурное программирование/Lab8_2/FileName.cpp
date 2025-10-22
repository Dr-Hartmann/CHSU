#include <iostream>
#include <fstream>
#include <time.h>
using namespace std;

int main() {
    //���������� �������
    srand(time(0));
    const int n = 3;
    int a[n][n];
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            a[i][j] = rand()%100;
            cout << a[i][j] << " ";
        }
        cout << endl;
    }

    //�������� ����� f, ������ ������� � ����
    fstream f ("Chisla.bin", ios::binary | ios::out);
    f.write((char*)&a, sizeof(a));
    f.close();
    cout << endl;

    //�������� ����������� �����
    f.open("Chisla.bin", ios::binary | ios::in);
    f.read((char*)&a, sizeof(a));
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            cout << a[i][j] << " ";
        }
        cout << endl;
    }
    f.close();

    //�������� ����� g, ������ �������� ����� � ����
    fstream g ("Nechot.bin", ios::binary | ios::out);
    int p = 0;
    int* b = new int[p];
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            if (a[i][j] % 2 != 0) {
                b[p] = a[i][j];
                p++;
            }
        }
    }
    g.write((char*)&b, sizeof(b));
    g.close();
    cout << endl;
    
    //�������� ����������� g
    g.open("Nechot.bin", ios::binary | ios::in);
    g.read((char*)&b[p], sizeof(b));
    for (int i = 0; i < p; i++) {
        cout << b[i] << " ";
    }
    g.close();

    system("pause");
    return 0;
}