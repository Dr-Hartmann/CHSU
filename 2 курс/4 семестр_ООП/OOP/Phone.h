#pragma once
#include "Appliances.h"

//производный класса Appliances
class Phone : public Appliances {
public:
	Phone(const char* name, Firm firm, std::string series, int price, std::string type)
		: Appliances(name, firm, series, price), type(type) {}
	Phone(const Phone& pt)
		: Appliances(pt), type(pt.type) {}
	std::string get_type()const { return type; }
	virtual void print()const {
		Appliances::print();
		std::cout << "\t" << get_type();
	}

protected:
	virtual std::string get_class_name() const {
		return "Phone";
	}//вывод класса
	std::string type;
};