#pragma once
#include "Template.h"
#include "Appliances.h"
#include "Refrigerator.h"
#include "Freezer.h"
#include "Phone.h"
#include "Smartphone.h"
#include "Microwave.h"
#include <iostream>
#include <string>

int main() {
	const int n = 4, m = 3;
	Template<IAppliances>mas(n, m);
	
	/*Microwave object12("Microwave 5", Firm::Dexp, "90", 990, 700);
	Phone object7("Phone 1947", Firm::Undefined, "1947", 990, "Phone");
	Phone object8(object7);
	Freezer object11("Daewoo", Firm::Undefined, "661", 990, -20);
	Refrigerator object6("Daewoo", Firm::Undefined, "661", 990, 700, -18, true);
	Smartphone object9("IPhone 16 Pro Max Ultra", Firm::Undefined, "16", 990, "Iphone", 2);
	Smartphone object10(object7, 0);*/
	Microwave object13("Microwave 5", Firm::Dexp, "90", -990, 700);
	try {
		throw 1;
		object13.set_price(-990);
	}
	catch (const std::exception& error_message) {
		std::cerr << error_message.what();
	}
	catch (...) {
		std::cerr << "default exception";
	}
	

	std::cout << "Input is completed" << std::endl;
	
	//mas.add(0, 0, &object);
	/*mas.add(0, 1, &object2);
	mas.add(0, 2, &object3);
	mas.add(1, 0, &object4);*/
	mas.add(1, 1, &object13);
	/*mas.add(1, 2, &object6);
	mas.add(2, 0, &object7);
	mas.add(2, 1, &object8);
	mas.add(2, 2, &object9);
	mas.add(3, 0, &object10);
	mas.add(3, 1, &object11);
	mas.add(3, 2, &object12);*/
	mas.sort(); std::cout << "Sort is completed" << std::endl;
	mas.print(); std::cout << "\n\n\nOutput is completed" << std::endl << std::endl;

	system("pause");
	return 0;
}

	/*void* a = &object;
	std::cout << *(Appliances*)a << std::endl << std::endl;*/ //указатель на void

	/*Statistics stat;
	stat.uploading_data_from_mas(&mas);
	stat.add_data_from_mas(&mas);
	Appliances object6("Microwave 69", Firm::Samsung, "XXX", 977); mas.add(2, 2, &object6);
	stat.add_data(&object6, 3);
	stat.del(&object4);
	stat.changing_value(&object3, 1488);
	stat.print();
	stat.sum();

	stat.destruction();
	stat.print();*/