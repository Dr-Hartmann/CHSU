#pragma once
#include "Firm.h"

#include <iostream>
#include <string>
#include <fstream>

//класс-интерфейс для бытовой техники
class IAppliances {
public:
	virtual void set_name(const char*) = 0;
	virtual void set_firm(Firm) = 0;
	virtual void set_series(std::string) = 0;
	virtual void set_price(int) = 0;

	virtual char* get_name() const = 0;
	virtual Firm get_firm() const = 0;
	virtual std::string get_series() const = 0;
	virtual int get_price() const = 0;

	virtual void print()const = 0;
	virtual void print_to_file(std::ofstream&)const = 0;
	virtual std::string get_class_name() const = 0;

protected:
	virtual ~IAppliances() {};
};