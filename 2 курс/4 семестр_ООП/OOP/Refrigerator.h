#pragma once
#include "Microwave.h"
#include "Freezer.h"

//производный класса Appliances
class Refrigerator : public Microwave, public Freezer {
public:
	Refrigerator(const char* name, Firm firm, std::string series, int price, int power, int min_temp, bool freezer)
		: Microwave(name, firm, series, price, power), Freezer(name, firm, series, price, min_temp), freezer(freezer) {};
	Refrigerator(const Refrigerator& pt)
		: Microwave(pt), Freezer(pt), freezer(pt.freezer) {};
	std::string get_freezer()const {
		return freezer ? "yes" : "no";
	}
	virtual void print()const {
		Microwave::print();
		std::cout << "\t" << "Freezer: " << get_freezer();
		std::cout << "\t" << "Min temperature: " << get_min_temp() << "C";
	}
protected:
	virtual std::string get_class_name() const {
		return "Refrigerator";
	}//вывод класса
	bool freezer;
};