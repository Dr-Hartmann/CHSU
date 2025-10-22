#pragma once
#include "Template.h"
#include <map>

class Statistics {
public:
	void add_data(Appliances* data, int x); //добавление элемента в словарь
	void uploading_data_from_mas(Template<Appliances>* mas); //загрузка всех данных в словарь из класса-контейнера
	void add_data_from_mas(Template<Appliances>* mas); //добавление элементов в словарь из класса-контейнера
	void changing_value(Appliances* data, int x); //изменение значения элемента по ключу в словаре
	void del(Appliances* data); //удаление элемента по ключу в словаре
	void print()const; //вывод всех элементов словаря
	void sum(); //вывод суммы стоимости всех элементов словаря
	void destruction(); //удаление словаря
private:
	std::map<Appliances*, int> quantity;//количество товаров на складе
};