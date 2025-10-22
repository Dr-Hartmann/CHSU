#pragma once
#include "Appliances.h"

//производный класса Appliances
class Microwave : public Appliances {
public:
	Microwave(const char* name, Firm firm, std::string series, int price, int power)
		: Appliances(name, firm, series, price), power(power) {};
	Microwave(const Microwave& pt)
		: Appliances(pt), power(pt.power) {};
	void set_power() { this->power = power; }
	int get_power()const { return power; }
	virtual void print()const {
		Appliances::print();
		try {
			if (get_power() >= 0)
				std::cout << get_power() << "W" << "  ";
			else throw "_negative_wattage";
		}
		catch (const char* &error_message) {
			if (error_message == "_negative_wattage")std::cerr << error_message << ": " << get_power() << "W" << "  ";
		}
	}

protected:
	virtual std::string get_class_name() const {
		return "Microwave";
	}//вывод класса
	int power;
};