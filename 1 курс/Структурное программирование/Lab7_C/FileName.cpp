#include <iostream>

int main() {
   
    int k = 0;
    FILE* f;
    char* buf = new char[100];
    fopen_s(&f, "NePust.txt", "rt");
    while (!feof(f)) {
        fgets(buf, 99, f);
        if (buf[0] == 'c') {
            printf("%s", buf);
            k++;
        }
    }
    fclose(f);
    
    printf("\n%d\n", k);
    system("pause");
    return 0;
}