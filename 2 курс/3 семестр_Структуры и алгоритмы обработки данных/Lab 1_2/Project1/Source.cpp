#include<iostream>
#include <time.h>
using namespace std;

//Стек динамическим списком
struct Node {
	int data;
	Node* next;
};

void InitStack(Node*& top) {
	top = NULL;
}

bool empty(Node*& top) {
	return top == NULL;
}

void nullStack(Node*& top) {
	Node* tmp;
	while (!empty(top)) {
		tmp = top;
		top = top->next;
		delete(tmp);
	}
}

void push(Node*& top, int value) {
	Node* tmp = new Node;
	tmp->next = top;
	top = tmp;
	top->data = value;
}

int print(Node*& top) {
	if (!empty(top)) {
		Node* tmp = top;
		int d = top->data;
		top = top->next;
		return d;
	}
	else cout << "Stack is empty" << endl;
}



//Очередь динамическим списком
class Queue {
private:
	struct Node {
		int data;
		Node* next;
	};
	Node* head, * tail;

public:
	Queue() {
		head = NULL;
		tail = NULL;
	}

	bool empty() {
		return head == NULL;
	}

	void add(int value) {
		if (empty()) {
			head = new Node;
			head->data = value;
			head->next = NULL;
			tail = head;
		}
		else {
			tail->next = new Node;
			tail = tail->next;
			tail->data = value;
			tail->next = NULL;
		}
	}

	void print() {
		if (!empty()) {
			Node* tmp = head;
			while (tmp != NULL) {
				cout << head->data << " ";
				head = head->next;
			}
			cout << endl;
		}
		else cout << "Queue is empty" << endl;
	}

	void nullQueue() {
		Node* tmp;
		while (!empty()) {
			tmp = head;
			head = head->next;
			delete(tmp);
		}
	}

};

int main() {
	srand(time(0));

	cout << "Creating a stack" << endl;
	Node* top;
	InitStack(top);

	cout << "Entering values of Stack" << endl;
	for (int i = 0; i < 0; i++) {
		int n = -100;//rand() % 1000;
		push(top, n);
		cout << n << " ";
	}
	cout << endl;

	Node* stackStart = top;
	cout << "Reading values of Stack" << endl;
	while (!empty(top)) {
		int x;
		x = print(top);
		cout << x << " ";
	}
	top = stackStart;

	int maxStack = print(top);
	top = stackStart;

	//Поиск максимума
	while (!empty(top)) {
		int x;
		x = print(top);
		if (x > maxStack) maxStack = x;
	}
	top = stackStart;

	cout << endl;
	cout << "Max Stack = " << maxStack << endl;
	cout << endl;

	cout << "Creating a new Stack" << endl;
	Node* top1;
	InitStack(top1);

	cout << "Creating Queue" << endl;
	Queue q;

	cout << "Entering values of Queue" << endl;
	for (int i = 0; i < 0; i++) {
		int n = -100;//rand() % 1000;
		if (n < maxStack) push(top1, n);
		q.add(n);
		cout << n << " ";
	}
	cout << endl;

	cout << "Reading values of Queue" << endl;
	q.print();

	cout << "Reading values of Queue" << endl;
	q.print();

	cout << "Reading values of new stack" << endl;
	stackStart = top1;
	while (!empty(top1)) {
		int x;
		x = print(top1);
		cout << x << " ";
	}
	top1 = stackStart;
	cout << endl;

	system("pause");
	return 0;
}