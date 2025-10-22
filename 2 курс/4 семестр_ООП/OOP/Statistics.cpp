#include "Statistics.h"

//���������� �������� � �������
void Statistics::add_data(Appliances* data, int x) {
	quantity[data] += x;
}

//�������� ���� ������ � ������� �� ������-����������
void Statistics::uploading_data_from_mas(Template<Appliances>* mas) {
	for (size_t i = 0; i < mas->get_n(); i++)
		for (size_t j = 0; j < mas->get_m(); j++)
			if (mas->read(i, j))
				add_data(mas->read(i, j), 1);
}

//���������� ��������� � ������� �� ������-����������
void Statistics::add_data_from_mas(Template<Appliances>* mas) {
	std::cout << "What object do you want to add?" << std::endl;
	mas->print();
	bool is_find = false;
	size_t a = 0, b = 0;
	do {
		std::string object;
		getline(std::cin, object);
		std::cin.clear();

		for (size_t i = 0; i < mas->get_n(); i++)
			for (size_t j = 0; j < mas->get_m(); j++)
				if (mas->read(i, j))
					if (object == mas->read(i, j)->get_name()) {
						std::cout << "How much?" << std::endl;
						int n;
						std::cin >> n;
						changing_value(mas->read(i, j), n);
						return;
					}
		std::cout << "The object was not found or an input error occurred, try again?\nyes\tor\tno?" << std::endl;
		std::string yes_no; getline(std::cin, yes_no);
		if (yes_no != "yes") return;
	} while (!is_find);
}

//��������� �������� �������� �� ����� � �������
void Statistics::changing_value(Appliances* data, int x) {
	if (quantity[data] + x > 0) add_data(data, x);
	else del(data);
}

//�������� �������� �� ����� � �������
void Statistics::del(Appliances* data) {
	quantity.erase(data);
}

//����� ���� ��������� �������
void Statistics::print()const {
	for (const auto& element : quantity)
		std::cout << element.first->get_name() << "\t" << element.second << std::endl;
}

//����� ����� ��������� ���� ��������� �������
void Statistics::sum() {
	size_t sum = 0;
	for (const auto& element : quantity)
		sum += element.second * element.first->get_price();
	std::cout << "The sum of the prices of all products is: " << sum << " rubles" << std::endl;
}

//�������� �������
void Statistics::destruction() {
	quantity.clear();
}