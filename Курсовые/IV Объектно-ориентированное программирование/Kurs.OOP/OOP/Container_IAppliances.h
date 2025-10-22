#pragma once
#include "Container.h"
#include "Firm.h"

//шаблонный класс-контейнер для хранения указателей на абстрактный базовый класс-интерфейс
template<class T>
class Container_IAppliances : private Container
{
public:
	//добавление элемента
	void push_back(T* object) { Container::push_back(object); }

	//удаление элемента
	void erase(int n) { Container::erase(n); }

	//проверка пустоты
	bool empty()const { return Container::empty(); }

	//Очистка списка
	void clear() { Container::clear(); }

	//поиск по фирме
	void search_firm(Firm firm)
	{
		for (std::list<void*>::const_iterator it = Container::cbegin(); it != Container::cend(); ++it)
			if (((T*)(*it))->get_firm() == firm)
				((T*)(*it))->print(), std::cout << std::endl;
	}

	//сортировка по цене
	void search_price_is_less_than(int max_price)
	{
		for (std::list<void*>::const_iterator it = Container::cbegin(); it != Container::cend(); ++it)
			if (((T*)(*it))->get_price() <= max_price)
				((T*)(*it))->print(), std::cout << std::endl;
	}

	//сортировка по фирме
	void sort_firm(Firm firm)
	{
		std::list<void*>::iterator it_up = Container::begin();
		for (std::list<void*>::iterator it = ++Container::begin(); it != Container::end(); ++it)
			if (((T*)(*it))->get_firm() == firm) {
				while (((T*)(*it_up))->get_firm() == firm)
					it_up++;
				std::swap(*it_up, *it);
			}
	}

	//адрес объекта
	T* return_object(int n)const
	{
		std::list<void*>::const_iterator it = Container::cbegin();
		advance(it, n);
		return (T*)*it;
	}

	//вывод всех элементов из контейнера
	void print()const
	{
		int i = 0;
		for (std::list<void*>::const_iterator it = Container::cbegin(); it != Container::cend(); ++it) {
			std::cout << ++i << ". ";
			((T*)(*it))->print();
			std::cout << std::endl;
		}
	}

	void output_in_file()const
	{
		std::ofstream fout("output.txt");
		for (std::list<void*>::const_iterator it = Container::cbegin(); it != Container::cend(); ++it)
			((T*)(*it))->print_to_file(fout), fout << std::endl;
		fout.close();
	}
};