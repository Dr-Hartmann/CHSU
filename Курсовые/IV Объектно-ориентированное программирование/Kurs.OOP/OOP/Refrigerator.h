#pragma once
#include "Appliances.h"

class Refrigerator : public Appliances {
public:
	Refrigerator(const char* name, Firm firm, std::string series, int price, int min_temp, bool freezer)
		: Appliances(name, firm, series, price), min_temp(min_temp), freezer(freezer) {};
	void set_min_temp(int min_temp) { this->min_temp = min_temp; }
	void set_freezer(int freezer) { this->freezer = freezer; }
	std::string get_freezer()const { return freezer ? "да" : "нет"; }
	int get_min_temp()const { return min_temp; }
	virtual void print()const {
		Appliances::print();
		std::cout << " | " << "Минимальная температура: " << get_min_temp() << "C"
			<< " | " << "Наличие морозильной камеры: " << get_freezer();
	}
	virtual void print_to_file(std::ofstream& out)const {
		Appliances::print_to_file(out);
		out << " | " << "Минимальная температура: " << get_min_temp() << "C"
			<< " | " << "Наличие морозильной камеры: " << get_freezer();
	}

protected:
	virtual std::string get_class_name() const {
		return "Холодильник";
	}
	int min_temp;
	bool freezer;
};