#pragma once
#include "Microwave.h"

class Compact_electric_stove : public Microwave {
public:
	Compact_electric_stove(const char* name, Firm firm, std::string series, int price, int power, int mumber_of_modes)
		: Microwave(name, firm, series, price, power), mumber_of_modes(mumber_of_modes) {};
	void set_mumber_of_modes(int mumber_of_modes) {
		if (mumber_of_modes >= 0)
			this->mumber_of_modes = mumber_of_modes;
		else throw std::exception("_negative_mumber_of_modes");
	}
	int get_mumber_of_modes()const { return mumber_of_modes; }
	virtual void print()const {
		Microwave::print();
		try {
			if (get_mumber_of_modes() >= 0)
				std::cout << " | " << get_mumber_of_modes();
			else throw "_negative_mumber_of_modes";

		}
		catch (const char* error_message) {
			if (error_message == "_negative_mumber_of_modes")
				std::cerr << " | " << error_message << ": " << get_mumber_of_modes();
		}
	}

	virtual void print_to_file(std::ofstream& out)const {
		Appliances::print_to_file(out);
		out << " | " << get_mumber_of_modes();
	}

protected:
	virtual std::string get_class_name() const {
		return "Компактная электрическая плита";
	}
	int mumber_of_modes;
};