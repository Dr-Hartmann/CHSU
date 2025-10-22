#pragma once
#include "Bread_maker.h"

class Electric_stove : public Bread_maker {
public:
	Electric_stove(const char* name, Firm firm, std::string series, int price, int power, int mumber_of_modes, int volume, int number_of_burners, int max_temperature)
		: Bread_maker(name, firm, series, price, power, mumber_of_modes, volume), number_of_burners(number_of_burners), max_temperature(max_temperature) {};
	void set_max_temperature(int max_temperature) { this->max_temperature = max_temperature; }
	void set_number_of_burners(int number_of_burners) {
		if (number_of_burners >= 0)
			this->number_of_burners = number_of_burners;
		else throw std::exception("_negative_number_of_burners");
	}
	int get_max_temperature()const { return max_temperature; }
	int get_number_of_burners()const { return number_of_burners; }
	virtual void print()const {
		Bread_maker::print();
		try {
			if (get_number_of_burners() >= 0)
				std::cout << " | " << get_number_of_burners();
			else throw "_negative_temperature";
		}
		catch (const char* error_message) {
			if (error_message == "_negative_temperature")
				std::cerr << " | " << error_message << ": " << get_max_temperature() << "C";
		}
		try {
			if (get_max_temperature() >= 0)
				std::cout << " | " << get_max_temperature() << "C";
			else throw "_negative_number_of_burners";
		}
		catch (const char* error_message) {
			if (error_message == "_negative_number_of_burners")
				std::cerr << " | " << error_message << ": " << get_number_of_burners();
		}
	}
	virtual void print_to_file(std::ofstream& out)const {
		Appliances::print_to_file(out);
		out << " | " << get_number_of_burners() << " | " << get_max_temperature() << "C";
	}

protected:
	virtual std::string get_class_name() const {
		return "Ёлектрическа€ плита";
	}
	int number_of_burners;
	int max_temperature;
};