#pragma once
#include "Appliances.h"
#include "Container_IAppliances.h"
#include "Vector_IAppliances.h"
#include "Refrigerator.h"
#include "Microwave.h"
#include "Compact_electric_stove.h"
#include "Bread_maker.h"
#include "Electric_stove.h"

#include <iostream>
#include <string>
#include <fstream>
#include <windows.h>

int main() {
	setlocale(LC_ALL, "ru");
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	int S = 0, s = 0;
	int num, price, power, number_of_modes, volume, number_of_burners, max_temperature, min_temp;
	std::string name1, series1, series2, price1, firm1, min_temp1, freezer1, power1, number_of_modes1, volume1, number_of_burners1, max_temperature1;
	Firm Firm1 = Firm::Undefined;
	bool Freezer1;

	Container_IAppliances<IAppliances>list;
	Vector_IAppliances<Refrigerator, Container_IAppliances<IAppliances>> v_refrigerator;
	Vector_IAppliances<Compact_electric_stove, Container_IAppliances<IAppliances>> v_compact_electric_stove;
	Vector_IAppliances<Bread_maker, Container_IAppliances<IAppliances>> v_bread_maker;
	Vector_IAppliances<Microwave, Container_IAppliances<IAppliances>> v_microwave;
	Vector_IAppliances<Electric_stove, Container_IAppliances<IAppliances>> v_electric_stove;

	std::ifstream fin("input.txt");

	while (fin.is_open())
	{
		switch (S)
		{
		case 0:
			std::cout << "\n�������� ����� ������:\n1. ������ �� �����\n2. ����� ������\n3. ���������� ������ � ����.\n4. ���������� �� ������� �� ...\n5. ����� ������� �� ��������...";
			std::cout << "\n6. ����� ������� �������...\n7. ������� ����� �� �������...\n8. �������� ������ � ������\n9. �������� ����\n10. ������� �����\n11. ����� ������\n";
			std::cin >> s;
			std::cout << std::endl;
			if (s == 1 && !fin.eof()) S = 1;//������ �� �����
			else if (s == 1 && fin.eof()) fin.seekg(0), S = 1;//������ �� �����
			else if (s == 2) S = 10;//�����
			else if (s == 3) {//������ � ����
				list.output_in_file();
				std::cout << "\n������ ��������� � ���� 'output.txt'\n";
			}
			else if (s == 4) S = 7;//���������� �� �����
			else if (s == 5) S = 8;//����� ������� ��������
			else if (s == 6) S = 9;//����� ������� �������
			else if (s == 7) S = 11;//������� ����� ��� �������
			else if (s == 8) S = 12;//�������� �����
			else if (s == 9)  S = 20;//������� ������
			else if (s == 10) S = 13;
			else if (s == 11) fin.close();
			else std::cout << "������: �������� ����� ������\n";
			break;
		case 1:
			if (fin.eof()) {
				S = 0;
				break;
			}
			std::getline(fin, name1);
			if (name1 == "�����������") S = 2;
			else if (name1 == "�������������") S = 3;
			else if (name1 == "���������� ������������") S = 4;
			else if (name1 == "���������") S = 5;
			else if (name1 == "������������") S = 6;
			else {
				std::cout << "������: ������������ ����\n";
				fin.close();
				list.print();
				break;
			}

			std::getline(fin, name1);
			std::getline(fin, firm1);
			std::getline(fin, series1);
			std::getline(fin, price1);
			price = std::stoi(price1);

			if (firm1 == "������") Firm1 = Firm::������;
			else if (firm1 == "Atlant") Firm1 = Firm::Atlant;
			else if (firm1 == "Dexp") Firm1 = Firm::Dexp;
			else if (firm1 == "Gefest") Firm1 = Firm::Gefest;
			else if (firm1 == "Midea") Firm1 = Firm::Midea;
			else if (firm1 == "Samsung") Firm1 = Firm::Samsung;
			else if (firm1 == "Weissgauff") Firm1 = Firm::Weissgauff;
			else Firm1 = Firm::Undefined;
			break;
		case 2:
			std::getline(fin, min_temp1);
			min_temp = std::stoi(min_temp1);
			std::getline(fin, freezer1);
			if (freezer1 == "true") Freezer1 = true;
			else Freezer1 = false;
			v_refrigerator.add_one_to_list(&list, new Refrigerator(name1.c_str(), Firm1, series1, price, min_temp, Freezer1));
			S = 1;
			break;
		case 3:
			std::getline(fin, power1);
			power = std::stoi(power1);
			v_microwave.add_one_to_list(&list, new Microwave(name1.c_str(), Firm1, series1, price, power));
			S = 1;
			break;
		case 4:
			std::getline(fin, power1);
			power = std::stoi(power1);
			std::getline(fin, number_of_modes1);
			number_of_modes = std::stoi(number_of_modes1);
			v_compact_electric_stove.add_one_to_list(&list, new Compact_electric_stove(name1.c_str(), Firm1, series1, price, power, number_of_modes));
			S = 1;
			break;
		case 5:
			std::getline(fin, power1);
			power = std::stoi(power1);
			std::getline(fin, number_of_modes1);
			number_of_modes = std::stoi(number_of_modes1);
			std::getline(fin, volume1);
			volume = std::stoi(volume1);
			v_bread_maker.add_one_to_list(&list, new Bread_maker(name1.c_str(), Firm1, series1, price, power, number_of_modes, volume));
			S = 1;
			break;
		case 6:
			std::getline(fin, power1);
			power = std::stoi(power1);
			std::getline(fin, number_of_modes1);
			number_of_modes = std::stoi(number_of_modes1);
			std::getline(fin, volume1);
			volume = std::stoi(volume1);
			std::getline(fin, number_of_burners1);
			number_of_burners = std::stoi(number_of_burners1);
			std::getline(fin, max_temperature1);
			max_temperature = std::stoi(max_temperature1);
			v_electric_stove.add_one_to_list(&list, new Electric_stove(name1.c_str(), Firm1, series1, price, power, number_of_modes, volume, number_of_burners, max_temperature));
			S = 1;
			break;
		case 7:
			std::cout << "\n1. Undefined\n2. Samsung\n3. Dexp\n4. Atlant\n5. Midea";
			std::cout << "\n6. Gefest\n7. ������\n8. Weissgauff\n�������� ��������: ";
			std::cin >> s;
			if (s == 1) Firm1 = Firm::Undefined;
			else if (s == 2) Firm1 = Firm::Samsung;
			else if (s == 3) Firm1 = Firm::Dexp;
			else if (s == 4) Firm1 = Firm::Atlant;
			else if (s == 5) Firm1 = Firm::Midea;
			else if (s == 6) Firm1 = Firm::Gefest;
			else if (s == 7) Firm1 = Firm::������;
			else if (s == 8) Firm1 = Firm::Weissgauff;
			else {
				std::cout << "\n������, �������� ��������";
				break;
			}
			list.sort_firm(Firm1);
			list.print();
			S = 0;
			break;
		case 8:
			std::cout << "\n1. Undefined\n2. Samsung\n3. Dexp\n4. Atlant\n5. Midea";
			std::cout << "\n6. Gefest\n7. ������\n8. Weissgauff\n�������� ��������: ";
			std::cin >> s;
			if (s == 1) Firm1 = Firm::Undefined;
			else if (s == 2) Firm1 = Firm::Samsung;
			else if (s == 3) Firm1 = Firm::Dexp;
			else if (s == 4) Firm1 = Firm::Atlant;
			else if (s == 5) Firm1 = Firm::Midea;
			else if (s == 6) Firm1 = Firm::Gefest;
			else if (s == 7) Firm1 = Firm::������;
			else if (s == 8) Firm1 = Firm::Weissgauff;
			else {
				std::cout << "\n������, �������� ��������";
				break;
			}
			std::cout << std::endl;
			list.search_firm(Firm1);
			S = 0;
			break;
		case 9:
			std::cout << "\n������ �������: ";
			std::cin >> s;
			list.search_price_is_less_than(s);
			std::cout << std::endl;
			S = 0;
			break;
		case 10:
			if (!list.empty())
				list.print();
			else std::cout << "������ ����\n";
			std::cout << std::endl;
			S = 0;
			break;
		case 11:
			if (!list.empty()) {
				list.print();
				std::cout << "\n������� ������� ��� �������: ";
				std::cin >> s;
				list.erase(s - 1);
			}
			else std::cout << "������ ����\n";
			S = 0;
			break;

		case 12:
			if (!list.empty()) {
				std::cout << std::endl;
				list.print();
				std::cout << "\n�������� ����� ��� �������: ";
				std::cin >> num;
				std::cout << "\n��������:\n1. ���\n2. �����\n3. �����\n4. ����\n";
				std::cin >> s;
				try {
					if (s == 1) {
						std::cout << "\n�������� ��� ��: ";
						std::cin >> name1;
						std::getline(std::cin, series1);
						list.return_object(num - 1)->set_name((name1 + series1).c_str());
					}
					else if (s == 2) {
						std::cout << "\n1. Undefined\n2. Samsung\n3. Dexp\n4. Atlant\n5. Midea";
						std::cout << "\n6. Gefest\n7. ������\n8. Weissgauff\n�������� ��������: ";
						std::cout << "\n�������� ����� ��: ";
						std::cin >> s;
						if (s == 1) Firm1 = Firm::Undefined;
						else if (s == 2) Firm1 = Firm::Samsung;
						else if (s == 3) Firm1 = Firm::Dexp;
						else if (s == 4) Firm1 = Firm::Atlant;
						else if (s == 5) Firm1 = Firm::Midea;
						else if (s == 6) Firm1 = Firm::Gefest;
						else if (s == 7) Firm1 = Firm::������;
						else if (s == 8) Firm1 = Firm::Weissgauff;
						list.return_object(num - 1)->set_firm(Firm1);
					}
					else if (s == 3) {
						std::cout << "\n�������� ����� ��: ";
						std::cin >> name1;
						std::getline(std::cin, series1);
						list.return_object(num - 1)->set_series((name1 + series1).c_str());
					}
					else if (s == 4) {
						std::cout << "\n�������� ���� ��: ";
						std::cin >> price;
						list.return_object(num - 1)->set_price(price);
					}
					else {
						std::cout << "\n�������� ��������";
						break;
					}
					list.print();
					S = 0;
					break;
				}
				catch (const std::exception& error_message) {
					std::cerr << error_message.what() << ", �������� ��������" << std::endl;
				}
				catch (...) {
					std::cerr << "_unexpected exception" << std::endl;
				}
			}
			else std::cout << "������ ����\n";
			break;

		case 13:
			std::cout << "\n1. �����������\n2. �������������\n3. ���������� ������������\n4. ���������\n5. ������������\n������� ����� ���������: ";
			std::cin >> s;
			if (s == 1) name1 = "�����������", S = 14;
			else if (s == 2) name1 = "�������������", S = 15;
			else if (s == 3) name1 = "���������� ������������", S = 16;
			else if (s == 4) name1 = "���������", S = 17;
			else if (s == 5) name1 = "������������", S = 18;
			else {
				std::cout << "\n������: ������������ �����\n";
				break;
			}

			std::cout << "\n1. Undefined\n2. Samsung\n3. Dexp\n4. Atlant\n5. Midea";
			std::cout << "\n6. Gefest\n7. ������\n8. Weissgauff\n�������� ��������: ";
			std::cin >> s;
			if (s == 1) Firm1 = Firm::Undefined;
			else if (s == 2) Firm1 = Firm::Samsung;
			else if (s == 3) Firm1 = Firm::Dexp;
			else if (s == 4) Firm1 = Firm::Atlant;
			else if (s == 5) Firm1 = Firm::Midea;
			else if (s == 6) Firm1 = Firm::Gefest;
			else if (s == 7) Firm1 = Firm::������;
			else if (s == 8) Firm1 = Firm::Weissgauff;
			else {
				std::cout << "\n������: ������������ �����\n";
				break;
			}

			std::cout << "\n������� �����: ";
			std::cin >> series1;
			std::getline(std::cin, series2);
			series1 += series2;

			std::cout << "\n������� ����: ";
			std::cin >> price;
			break;
		case 14:
			std::cout << "\n� ������������ ���� ���������?\n1. ��\n2. ���\n";
			std::cin >> s;
			if (s == 1) Freezer1 = true;
			else if (s == 2)Freezer1 = false;
			else {
				std::cout << "\n������: ������������ �����\n";
				break;
			}
			std::cout << "\n������� ����������� �����������: ";
			std::cin >> min_temp1;
			v_refrigerator.add_one_to_list(&list, new Refrigerator(name1.c_str(), Firm1, series1, price, min_temp, Freezer1));
			S = 0;
			break;
		case 15:
			std::cout << "\n������� ������������ ��������: ";
			std::cin >> power;
			v_microwave.add_one_to_list(&list, new Microwave(name1.c_str(), Firm1, series1, price, power));
			S = 0;
			break;
		case 16:
			std::cout << "\n������� ������������ ��������: ";
			std::cin >> power;
			std::cout << "\n������� ���������� �������: ";
			std::cin >> number_of_modes;
			v_compact_electric_stove.add_one_to_list(&list, new Compact_electric_stove(name1.c_str(), Firm1, series1, price, power, number_of_modes));
			S = 0;
			break;
		case 17:
			std::cout << "\n������� ������������ ��������: ";
			std::cin >> power;
			std::cout << "\n������� ���������� �������: ";
			std::cin >> number_of_modes;
			std::cout << "\n������� �����: ";
			std::cin >> volume;
			v_bread_maker.add_one_to_list(&list, new Bread_maker(name1.c_str(), Firm1, series1, price, power, number_of_modes, volume));
			S = 0;
			break;
		case 18:
			std::cout << "\n������� ������������ ��������: ";
			std::cin >> power;
			std::cout << "\n������� ���������� �������: ";
			std::cin >> number_of_modes;
			std::cout << "\n������� �����: ";
			std::cin >> volume;
			std::cout << "\n������� ���������� ��������: ";
			std::cin >> number_of_burners;
			std::cout << "\n������� ������������ �����������: ";
			std::cin >> max_temperature;
			v_electric_stove.add_one_to_list(&list, new Electric_stove(name1.c_str(), Firm1, series1, price, power, number_of_modes, volume, number_of_burners, max_temperature));
			S = 0;
			break;

		case 20:
			list.clear();
			std::cout << "������ ������\n";
			S = 0;
			break;
		}
	}


	std::cout << std::endl;
	system("pause");
	return 0;
}