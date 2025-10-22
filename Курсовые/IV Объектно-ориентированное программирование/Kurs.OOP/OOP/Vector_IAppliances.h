#pragma once
#include <vector>

//шаблонный класс-контейнер для хранения объектов
template<class T, class TCA>
class Vector_IAppliances
{
public:

	void add_one_to_list(TCA* list, T* object)
	{
		vector_IAppliances.push_back(object);
		list->push_back(vector_IAppliances[vector_IAppliances.size() - 1]);
	}

	void add_from_vector_to_list(TCA* list, int n)
	{
		list->push_back(vector_IAppliances[n]);
	}

	void push_back(T* object) { vector_IAppliances.push_back(object); }

	void del_object(int n)
	{
		typename std::vector<T*>::iterator it = vector_IAppliances.begin();
		advance(it, n);
		vector_IAppliances.erase(it);
	}

	T* return_object(int n)const
	{
		try {
			if (n < vector_IAppliances.size())
				return vector_IAppliances[n];
			else throw "_index_outside_size_of_vector";
		}
		catch (const char* error_message) {
			if (error_message == "_index_outside_size_of_vector") {
				std::cerr << error_message << ": Выход за пределы индекса\n";
				return vector_IAppliances[vector_IAppliances.size() - 1];
			}
		}
	}

private:
	std::vector<T*> vector_IAppliances;
};