import numpy as np
import matplotlib.pyplot as plt
from sympy import diff, exp
from sympy.abc import *

x1, x2 = symbols('x1 x2')

"""
Теория:
    Есть функции одной переменной f(x), а есть двух переменных f(x1, x2)

    ПРОИЗОДНАЯ - понятие дифференциального исчисления, характеризующее СКОРОСТЬ ИЗМЕНЕНИЯ ФУНКЦИИ В ДАННОЙ ТОЧКЕ.
    ГРАДИЕНТ - вектор, указывающий направление наибольшего возрастания функции в каждой точке.
    Если объединить эти понятия, то получается, что:
    ГРАДИЕНТ - набор частных производных.
    
    МЕТОД ГРАДИЕНТНОГО СПУСКА: это итеративный метод оптимизации,
    который применяется для нахождения МЕСТНЫХ МИНИМУМОВ ФУНКЦИЙ,
    так как найденый минимум может быть как локальным, так и глобальным.

    Минимум будет найден в том случае, когда градиент (все производные) будет меньше заданной точности эпсилон.

    Частные производные мы используем для приращений к исследуемым значениям (x или x1, x2), но обязательно
    предварительно умножаем на размер шага/скорость обучения.

    Задача куда проще, чем то, что пишет нейронка при прямом (тупом) вводе задания без понимания теории.

    РАУНД
"""

# исходная функция, выведет как формулу при передаче символов, так и результат её подсчёта
def func_test_1(x1, x2):
    return x1**3 - x1*x2 + x2**2 - 2*x1 + 3*x2 - 4

def func_test_2(x1, x2):
    return (x2 - x1**2)**2 + (1 - x1)**2

def func1(x1, x2):
    return (x2**2 + x1**2 - 1)**2 + (x1 + x2 - 1)**2

def func2(x1, x2):
    return (x1**2 + x2 -11)**2 + (x1 + x2**2 - 7)

def func3(x1, x2):
    return 4*(x1 - 5)**2 + (x2 - 6)**2

func = func_test_1

# вычисление функции
def calc_func(expr: exp, a, b):
    return expr.subs({x1: a, x2: b})

# вычисление градиента (ФОРМУЛ частных производных), фактически просто объединение производных
def gradient(expr: exp):
    return np.array([diff(expr, x1), diff(expr, x2)])
    # np.array для правильной работы с матрицами/векторами/списками/кортежами при арифметических операциях



# метод градиентного спуска
def gradient_descent(x_1: float, x_2: float, learning_rate, epsilon, max_iteration):
    trajectory = [(x_1, x_2)]
    grad = gradient(func(x1, x2)) # передаю здесь именно символы для получения формулы
    adaptive_grad = adagrad(grad, learning_rate, epsilon)

    for iteration in range(max_iteration):
        derivative_x1 = calc_func(adaptive_grad[0], x_1, x_2)
        derivative_x2 = calc_func(adaptive_grad[1], x_1, x_2)
        x_1 -= learning_rate * derivative_x1 # корректировка
        x_2 -= learning_rate * derivative_x2

        trajectory.append((x_1, x_2)) # запись траектории

        # Проверка условия остановки
        if derivative_x1 ** 2 < epsilon and derivative_x2 ** 2 < epsilon:
            break

    print(f'Минимум функции достигается за {iteration} итераций в точке: x1 = {x_1:.4f}, x2 = {x_2:.4f}')
    print(f'Значение функции: {calc_func(func(x1, x2), x_1, x_2):.4f}')
    return x_1, x_2, trajectory # фактически возвращается кортеж



# корректировка скорости обучения
def adagrad(grad, learning_rate, epsilon):
    accumulated_grad_squared = np.zeros_like(grad)
    accumulated_grad_squared += grad ** 2
    adjusted_lr = learning_rate / (accumulated_grad_squared**0.5 + epsilon)
    return adjusted_lr * grad
   


# Построение графика
def plot_function_and_trajectory(trajectory):
    x1_range = np.linspace(-4, 4, 100)
    x2_range = np.linspace(-4, 4, 100)
    X1, X2 = np.meshgrid(x1_range, x2_range)

    Z = np.zeros_like(X1)
    Z = func(X1, X2)

    plt.figure(figsize=(10, 7))
    plt.contour(X1, X2, Z, levels=50, cmap='viridis')  # Контурный график функции
    plt.colorbar()
    
    # Извлечение точек траектории
    trajectory = np.array(trajectory)
    plt.plot(trajectory[:, 0], trajectory[:, 1], marker='o', color='red', linewidth=1, markersize=1, label='Траектория')
    
    plt.title('Градиентный спуск')
    plt.xlabel('x1')
    plt.ylabel('x2')
    plt.legend()
    plt.grid()
    plt.autoscale()
    plt.show()



# НАЧАЛО ПРОГРАММЫ

x0 = ((0.0, 0.0), (0.0, 3.0), (3.0, 0.0), (8.0, 9.0))

learning_rate = 0.1
epsilon = 0.007
max_iteration = 1500

func = func_test_1
# * перед кортежем раскладывает его на несколько значений
x1_min, x2_min, trajectory = gradient_descent(*x0[0], learning_rate, epsilon, max_iteration)
plot_function_and_trajectory(trajectory)

func = func_test_2
x1_min, x2_min, trajectory = gradient_descent(*x0[0], learning_rate, epsilon, max_iteration)
plot_function_and_trajectory(trajectory)

func = func1
x1_min, x2_min, trajectory = gradient_descent(*x0[1], learning_rate, epsilon, max_iteration)
plot_function_and_trajectory(trajectory)

x1_min, x2_min, trajectory = gradient_descent(*x0[2], learning_rate, epsilon, max_iteration)
plot_function_and_trajectory(trajectory)

func = func2
x1_min, x2_min, trajectory = gradient_descent(*x0[0], learning_rate, epsilon, max_iteration)
plot_function_and_trajectory(trajectory)

func = func3
x1_min, x2_min, trajectory = gradient_descent(*x0[3], learning_rate, epsilon, max_iteration)
plot_function_and_trajectory(trajectory)