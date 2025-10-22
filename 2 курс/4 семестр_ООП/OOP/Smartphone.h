#pragma once
#include "Phone.h"

//производный класса Phone
class Smartphone : public Phone {
public:
	Smartphone(const char* name, Firm firm, std::string series, int price, std::string type, int num_sim)
		: Phone(name, firm, series, price, type), num_sim(num_sim) {}
	Smartphone(const Smartphone& pt)
		: Phone(pt), num_sim(pt.num_sim) {}
	Smartphone(const Phone& pt, int num_sim)
		: Phone(pt), num_sim(num_sim) {}
	int get_num_sim()const { return num_sim; }
	virtual void print()const {
		Phone::print();
		std::cout << "\t" << get_num_sim();
	}

protected:
	virtual std::string get_class_name() const {
		return "Smartphone";
	}//вывод класса
	int num_sim;
};