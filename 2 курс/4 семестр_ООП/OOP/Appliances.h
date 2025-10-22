#pragma once
#include "IAppliances.h"
#include <typeinfo>
#include "Firm.h"
#include <iostream>
#include <string>

//�������� ������ ��� ������� �������, ����������� ������-����������
class Appliances :virtual public IAppliances {
public:
	//Appliances() {};//����������� �� ���������, �����?
	Appliances(const char* name, Firm firm, std::string series, int price);
	Appliances(const Appliances& m);//���������� �����������
	~Appliances() { amount_product--; std::cout << "Calling the destructor" << this << std::endl << std::endl; delete[]name; }//delete ��� ������������ ������

	void set_name(const char* name);
	void set_firm(Firm firm);
	void set_series(std::string series);
	void set_price(int price);
	char* get_name() const;
	Firm get_firm() const;
	std::string get_series() const;
	int get_price() const;//�� ������ �������� ����� - const
	virtual void print()const;//������� ������

	//���������� ���������
	Appliances& operator=(const char* name) {
		set_name(name);
		return *this;
	}
	Appliances& operator=(int price) {
		set_price(price);
		return *this;
	}
	bool operator ==(const Firm& firm)const {
		return this->get_firm() == firm;
	}
	friend std::ostream& operator << (std::ostream& os, const Appliances& m);

protected:
	virtual std::string get_class_name() const = 0;//����� ������

	//�������� �������� ������
private:
	char* name;
	std::string series;
	int price;
	Firm firm;
	static int amount_product;	
};