#include <iostream>

class HaffmanTree {
private:
	struct Node {
		double p;
		char c;
		Node* left, * right, * parent;
	};
	Node* trees[256], * symbols[256];
	int size;

public:
	HaffmanTree(int col) : size(col) {}

	void makeTree(int col) {
		if (col > 1) {
			double minp1 = 1;
			int n1 = 0;
			for (int i = 0; i < size; i++)//ищем первый минимум
				if (trees[i] != NULL && trees[i]->p < minp1) {
					minp1 = trees[i]->p;
					n1 = i;
				}
			double minp2 = 1;
			int n2 = 0;
			for (int i = 0; i < size; i++)//ищем второй минимум
				if (trees[i] != NULL && trees[i]->p < minp2 && i != n1) {
					minp2 = trees[i]->p;
					n2 = i;
				}
			Node* tmp = new Node;//новое дерево
			tmp->left = trees[n1];
			tmp->right = trees[n2];
			trees[n1]->parent = tmp;
			trees[n2]->parent = tmp;
			tmp->p = trees[n1]->p + trees[n2]->p;
			tmp->parent = NULL;
			trees[n1] = tmp;
			trees[n2] = NULL;
			makeTree(col - 1);//опять в лес по дрова
		}
	}

	void readInfo() {
		for (int i = 0; i < size; i++)
		{
			trees[i] = new Node;
			symbols[i] = trees[i];
			std::cout << "Enter symbol: ";
			std::cin >> trees[i]->c;
			std::cout << "p = ";
			std::cin >> trees[i]->p;
			trees[i]->left = NULL;
			trees[i]->right = NULL;
			trees[i]->parent = NULL;
		}
	}

	void showCodes() {
		if (size == 1)
			std::cout << symbols[0]->c << " - " << 0 << std::endl;
		else {
			Node* tmp;
			std::string code;
			for (int i = 0; i < size; i++) {
				tmp = symbols[i];
				code = "";
				while (tmp->parent) {
					if (tmp->parent->left == tmp)
						code = "0" + code;
					else code = "1" + code;
					tmp = tmp->parent;
				}
				std::cout << symbols[i]->c << " - " << code << std::endl;
			}
		}
	}
};

int main() {
	int size = 5;
	HaffmanTree tree (size);
	tree.readInfo();
	tree.makeTree(size);
	tree.showCodes();
	return 0;
}