#include "Appliances.h"


int Appliances::amount_product = 0;

//Конструктор
Appliances::Appliances(const char* name, Firm firm, std::string series, int price)
	: series(series), firm(firm), price(price)
{
	set_name(name);
	Appliances::amount_product++;
}

//Копирующий конструктор
Appliances::Appliances(const Appliances& pt)
	: Appliances(pt.name, pt.firm, pt.series, pt.price) {}

//Сеттеры
void Appliances::set_name(const char* name)
{
	size_t x = strlen(name) + 1;
	delete[] this->name;
	this->name = new char[x];
	strcpy_s(this->name, x, name);
}
void Appliances::set_firm(Firm firm) { this->firm = firm; }
void Appliances::set_series(std::string series) { this->series = series; }
void Appliances::set_price(int price) {
	if (price >= 0)
		this->price = price; 
	else throw std::exception("_negative_price");
}

//Геттеры
char* Appliances::get_name()const { return name; }
Firm Appliances::get_firm()const { return firm; }
std::string Appliances::get_series()const { return series; }
int Appliances::get_price()const {	return price;}

//Функция вывода
void Appliances::print()const {
	std::cout << "\n\n" << get_class_name() << "\t" << get_name() << " " << get_series() << " ";
	std::cout << get_price() << " rubles" << " ";
	std::cout << static_cast<int>(get_firm());
}

std::ostream& operator << (std::ostream& os, const Appliances& m) {
	return os << m.get_name();
}