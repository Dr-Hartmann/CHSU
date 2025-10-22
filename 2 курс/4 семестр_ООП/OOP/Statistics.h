#pragma once
#include "Template.h"
#include <map>

class Statistics {
public:
	void add_data(Appliances* data, int x); //���������� �������� � �������
	void uploading_data_from_mas(Template<Appliances>* mas); //�������� ���� ������ � ������� �� ������-����������
	void add_data_from_mas(Template<Appliances>* mas); //���������� ��������� � ������� �� ������-����������
	void changing_value(Appliances* data, int x); //��������� �������� �������� �� ����� � �������
	void del(Appliances* data); //�������� �������� �� ����� � �������
	void print()const; //����� ���� ��������� �������
	void sum(); //����� ����� ��������� ���� ��������� �������
	void destruction(); //�������� �������
private:
	std::map<Appliances*, int> quantity;//���������� ������� �� ������
};