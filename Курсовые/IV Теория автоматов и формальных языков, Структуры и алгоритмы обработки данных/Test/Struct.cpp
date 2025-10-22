#include <iostream>
#define _USE_MATH_DEFINES
#include <math.h>
using namespace std;

/*
	multi-line comment
*/
struct Person {//creating a structure
	union Height {//creating a union
		int int_height;
		size_t size_t_height;
		double double_height;
	} height = { -1 };
	string name = "noname";
	int age = -1;
};
//
int main() {
	Person man;
	cout << "Enter your name, age, height\nname\tage\theight" << endl;
	cin >> man.name >> man.age >> man.height.double_height;
	cout << "Hi " + man.name + ", " << man.age << " years old, " << man.height.double_height << " centimeters!" << endl;
	system("pause");
	return 0;
}