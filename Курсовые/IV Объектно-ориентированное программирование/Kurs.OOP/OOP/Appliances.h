#pragma once
#include "IAppliances.h"
#include "Firm.h"

#include <iostream>
#include <string>

//Создание класса для бытовой техники, производный класса-интерфейса
class Appliances :virtual public IAppliances {
public:
	Appliances(const char*, Firm, std::string, int);
	Appliances(const Appliances&);
	~Appliances() { std::cout << "Вызов деструктора" << this << std::endl; delete[]name; }

	void set_name(const char*);
	void set_firm(Firm);
	void set_series(std::string);
	void set_price(int);

	char* get_name() const;
	Firm get_firm() const;
	std::string get_series() const;
	int get_price() const;

	virtual void print()const;
	virtual void print_to_file(std::ofstream&)const;

private:
	char* name;
	std::string series;
	int price;
	Firm firm;
};