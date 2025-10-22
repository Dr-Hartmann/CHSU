#include <iostream>
#include <fstream>
#include <ctime>

int main() {
	srand(time(NULL));
	std::ofstream file;
	file.open("int50000.txt");
	for (size_t i = 0; i < 50000; i++)
	{
		file << rand() % 100000 << " ";
	}

	/*file.open("int10000.txt");
	for (size_t i = 0; i < 10000; i++)
	{
		file << rand() * (rand() % 2 * rand() + 1) << " ";
	}*/

	/*file.open("int1000.txt");
	for (size_t i = 0; i < 1000; i++)
	{
		file << rand() * (rand() % 2 * rand() + 1) << " ";
	}*/

	/*file.open("int200.txt");
	for (size_t i = 0; i < 200; i++)
	{
		file << rand() << " ";
	}*/

	/*file.open("int100.txt");
	for (size_t i = 0; i < 100; i++)
	{
		file << rand() % 100000 << " ";
	}*/

	/*file.open("int10.txt");
	for (size_t i = 0; i < 10; i++)
	{
		file << rand()%1000 << " ";
	}*/

	file.close();
	return 0;
}