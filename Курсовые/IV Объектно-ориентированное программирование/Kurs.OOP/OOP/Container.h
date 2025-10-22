#pragma once
#include <list>

class Container//обобщенный базовый класс-контейнер
{
public:
	//добавление элемента
	void push_back(void* object) { list.push_back(object); }

	//удаление
	void erase(int n) {
		if (n < list.size()) {
			std::list<void*>::const_iterator it = Container::cbegin();
			advance(it, n);
			list.erase(it);
			std::cout << "‘айл " << n +1 << " удалЄн" << std::endl;
		}
		else std::cout << "Ёлемента не существует" << std::endl;
	}

	int size()const { return list.size(); }
	bool empty()const { return list.empty(); }
	void clear() { list.clear(); }

	std::list<void*>::const_iterator cbegin()const { return list.cbegin(); }
	std::list<void*>::const_iterator cend()const { return list.cend(); }
	std::list<void*>::iterator begin() { return list.begin(); }
	std::list<void*>::iterator end() { return list.end(); }

private:
	std::list<void*> list;
};