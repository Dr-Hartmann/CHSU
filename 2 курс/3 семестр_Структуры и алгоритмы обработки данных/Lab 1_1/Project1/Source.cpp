#include <iostream>
#include <time.h>
using namespace std;

//Описание структуры массивом
struct Stack1 {
	int top;
	int* data;
	int stackStart;
	int size;
};

void InitStack(Stack1& st) {
	st.size = 0;
	st.data = new int[st.size];
	st.top = -1;
	st.stackStart = st.top;
}

void push(Stack1& st, int value) {
	if (st.top < st.size - 1) {
		st.data[++st.top] = value;
		st.stackStart = st.top;
	}
	else cout << "Stack is overflow" << endl;
}

bool empty(Stack1& st) {
	return st.top == -1;
}

int print(Stack1& st) {
	if (!empty(st)) {
		return st.data[st.top--];
	}
}

void backToTheRoots(Stack1& st) {
	st.top = st.stackStart;
}


//Описание очереди массивом
struct Queue1 {
	int head, tail, size;
	int* data;
	int queueStart;
};

void nullQueue(Queue1& q) {
	q.head = 0;
	q.queueStart = q.head;
	q.tail = q.size - 1;
}

void InitQueue(Queue1& q, int capacity) {
	q.size = capacity + 1;
	q.data = new int[q.size];
	nullQueue(q);
}

int next(Queue1& q, int n) {
	return (n + 1) % q.size;
}

bool empty(Queue1& q) {
	return next(q, q.tail) == q.head;
}

void add(Queue1& q, int value) {
	if (next(q, next(q, q.tail)) == q.head) cout << "Queue overflow" << endl;
	else {
		q.tail = next(q, q.tail);
		q.data[q.tail] = value;
	}
}

int print(Queue1& q) {
	if (!empty(q)) {
		int d = q.data[q.head];
		q.head = next(q, q.head);
		return d;
	}
}

void backToTheHead(Queue1& q) {
	q.head = q.queueStart;
}





int main() {
	srand(time(NULL));

	cout << "Creating a stack" << endl;
	Stack1 st;
	InitStack(st);

	//for (int i = 0; i < 0; i++) {
	for (int i = 0; i < 0; i++) {
		int n = -100;// rand() % 100;
		push(st, n);
	}

	//Чтение стека
	cout << "Reading values of Stack" << endl;
	if (!empty(st)) {
		while (!empty(st)) {
			int x;
			x = print(st);
			cout << x << " ";
		}
		backToTheRoots(st);
	}
	else cout << "Stack is empty" << endl;

	int maxStack = print(st);
	backToTheRoots(st);
	//Поиск максимума
	if (!empty(st)) {
		while (!empty(st)) {
			int x;
			x = print(st);
			if (x > maxStack)maxStack = x;
		}
		backToTheRoots(st);
	}
	else cout << "Stack is empty" << endl;

	cout << "\nMax Stack = " << maxStack;

	cout << "\nCreating Queue\n";
	Queue1 q;
	InitQueue(q, 0);
	//for (int i = 0; i < rand() % 100; i++) {
	for (int i = 0; i < 1; i++) {
		int n = -50;// rand() % 100;
		add(q, n);
	}

	//Чтение очереди
	cout << "Reading values of Queue" << endl;
	if (!empty(q)) {
		while (!empty(q)) {
			int x;
			x = print(q);
			cout << x << " ";
		}
	}
	else cout << "Queue is empty" << endl;
	backToTheHead(q);
	cout << endl;

	//Создание нового стека
	cout << "Creating a new Stack" << endl;
	Stack1 st2;
	InitStack(st2);

	//Наполнение нового стека
	while (!empty(q)) {
		int x;
		x = print(q);
		if (x < maxStack)push(st2, x);
	}
	backToTheHead(q);
	cout << endl;

	cout << "Reading values of new stack" << endl;
	if (!empty(st)) {
		while (!empty(st2)) {
			int x;
			x = print(st2);
			cout << x << " ";
		}
		backToTheRoots(st2);
	}
	else cout << "Stack is empty" << endl;

	cout << endl;
	system("pause");
	return 0;
}