#include <iostream>
#include <time.h>
using namespace std;

struct Node {
	int data;
	Node* left, *right;
};

void add(int x, Node*&node) {
	if (node == NULL) {
		node = new Node;
		node->data = x;
		node->left = NULL;
		node->right = NULL;
	}
}

void printTree1(Node *node) {
	if (node != NULL) {
		cout << node->data << " ";
		printTree1(node->left);
		printTree1(node->right);
	}
}

void printTree2(Node *node) {
	if (node != NULL) {
		printTree2(node->left);
		cout << node->data << " ";
		printTree2(node->right);
	}
}

void printTree3(Node *node) {
	if (node != NULL) {
		printTree3(node->left);
		printTree3(node->right);
		cout << node->data << " ";
	}
}

void max_min(Node *node, int *max, int *min) {
	if (node != NULL && (node->left || node->right)) {
		if (node->data > *max)*max = node->data;
		if (node->data < *min)*min = node->data;
		max_min(node->left, max, min);
		max_min(node->right, max,  min);
	}
}


void randadd(int x, Node*&node) {
	if (rand() % 2) {
		add(rand() % 100, node->left);
		randadd(x - 1, node->left);
	}
	if (rand() % 2) {
		add(rand() % 100, node->right);
		randadd(x - 1, node->right);
	}
	if (x < 0)return;
}

void main() {
	srand(time(0));


		Node*root = NULL;
		int x = rand() % 100;
		int max = x, min = x;
		add(x, root);

		randadd(10, root);

		
		max_min(root, &max, &min);

		cout << "P: "; printTree1(root); cout << endl;
		cout << "S: "; printTree2(root); cout << endl;
		cout << "O: "; printTree3(root); cout << endl;

		cout << "Max: " << max << endl << "Min: " << min << endl;


	cout << endl;
	system("pause");
}
