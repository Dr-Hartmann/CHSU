#pragma once
#include <list>

class Container//���������� ������� �����-���������
{
public:
	//���������� ��������
	void push_back(void* object) { list.push_back(object); }

	//��������
	void erase(int n) {
		if (n < list.size()) {
			std::list<void*>::const_iterator it = Container::cbegin();
			advance(it, n);
			list.erase(it);
			std::cout << "���� " << n +1 << " �����" << std::endl;
		}
		else std::cout << "�������� �� ����������" << std::endl;
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