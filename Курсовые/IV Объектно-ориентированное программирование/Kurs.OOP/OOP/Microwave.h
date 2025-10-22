#pragma once
#include "Appliances.h"

class Microwave : public Appliances {
public:
	Microwave(const char* name, Firm firm, std::string series, int price, int power)
		: Appliances(name, firm, series, price), power(power) {};
	void set_power(int power) {
		if (power >= 0)
			this->power = power;
		else throw std::exception("_negative_wattage");
	}
	int get_power()const { return power; }
	virtual void print()const {
		Appliances::print();
		try {
			if (get_power() >= 0)
				std::cout << " | " << get_power() << "W";
			else throw "_negative_wattage";
		}
		catch (const char* error_message) {
			if (error_message == "_negative_wattage")
				std::cerr << " | " << error_message << ": " << get_power() << "W";
		}
	}
	virtual void print_to_file(std::ofstream& out)const {
		Appliances::print_to_file(out);
		out << " | " << get_power() << "W";
	}

protected:
	virtual std::string get_class_name() const {
		return "Микроволновка";
	}
	int power;
};