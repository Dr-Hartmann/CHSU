#pragma once
#include "Compact_electric_stove.h"

class Bread_maker : public Compact_electric_stove {
public:
	Bread_maker(const char* name, Firm firm, std::string series, int price, int power, int mumber_of_modes, int volume)
		: Compact_electric_stove(name, firm, series, price, power, mumber_of_modes), volume(volume) {};
	void set_volume(int volume) {
		if (volume >= 0)
			this->volume = volume;
		else throw std::exception("_negative_volume");
	}
	int get_volume()const { return volume; }
	virtual void print()const {
		Compact_electric_stove::print();
		try {
			if (get_volume() >= 0)
				std::cout << " | " << get_volume() << "L";
			else throw "_negative_volume";
		}
		catch (const char* error_message) {
			if (error_message == "_negative_volume")
				std::cerr << " | " << error_message << ": " << get_volume() << "L";
		}
	}
	virtual void print_to_file(std::ofstream& out)const {
		Appliances::print_to_file(out);
		out << " | " << get_volume() << "L";
	}

protected:
	virtual std::string get_class_name() const {
		return "Хлебопечь";
	}
	int volume;
};