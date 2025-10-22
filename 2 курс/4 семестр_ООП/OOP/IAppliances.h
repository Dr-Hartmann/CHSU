#pragma once
#include "Firm.h"
#include <iostream>
#include <string>

//класс-интерфейс для бытовой техники
class IAppliances {
public:
	virtual void set_name(const char* name) = 0;
	virtual void set_firm(Firm firm) = 0;
	virtual void set_series(std::string series) = 0;
	virtual void set_price(int price) = 0;

	virtual char* get_name() const = 0;
	virtual Firm get_firm() const = 0;
	virtual std::string get_series() const = 0;
	virtual int get_price() const = 0;

	virtual void print()const = 0;

protected:
	virtual ~IAppliances() {};
};
