#pragma once
#include "Appliances.h"

//производный класса Appliances
class Freezer : public Appliances {
public:
	Freezer(const char* name, Firm firm, std::string series, int price, int min_temp)
		: Appliances(name, firm, series, price), min_temp(min_temp) {};
	Freezer(const Freezer& pt)
		: Appliances(pt), min_temp(pt.min_temp) {};
	int get_min_temp()const { return min_temp; }
	virtual void print()const {
		Appliances::print();
		std::cout << "\t" << "Min temperature: " << get_min_temp() << "C";
	}

protected:
	virtual std::string get_class_name() const {
		return "Freezer";
	}//вывод класса
	int min_temp;
};