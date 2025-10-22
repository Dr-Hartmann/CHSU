#include "Appliances.h"

Appliances::Appliances(const char* name, Firm firm, std::string series, int price)
	: series(series), firm(firm), price(price)
{
	set_name(name);
	//std::cout << "Объект " << this << std::endl;
}

Appliances::Appliances(const Appliances& pt)
	: Appliances(pt.name, pt.firm, pt.series, pt.price) {}

void Appliances::set_name(const char* name)
{
	int x = strlen(name) + 1;
	delete[] this->name;
	this->name = new char[x];
	strcpy_s(this->name, x, name);
}
void Appliances::set_firm(Firm firm) { this->firm = firm; }
void Appliances::set_series(std::string series) { this->series = series; }
void Appliances::set_price(int price)
{
	if (price >= 0)
		this->price = price;
	else throw std::exception("_negative_price");
}

char* Appliances::get_name()const { return name; }
Firm Appliances::get_firm()const { return firm; }
std::string Appliances::get_series()const { return series; }
int Appliances::get_price()const { return price; }

void Appliances::print()const {
	std::cout << get_class_name() << "  |  " << get_name() << " | " << get_series() << " | "
		<< get_price() << " рублей" << " |  Код фирмы: " << static_cast<int>(get_firm());
}

void Appliances::print_to_file(std::ofstream& out)const
{
	out << get_class_name() << "  |  " << get_name() << " | " << get_series() << " | "
		<< get_price() << " рублей" << " |  Код фирмы: " << static_cast<int>(get_firm());
}