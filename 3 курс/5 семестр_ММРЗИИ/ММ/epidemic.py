# Python
import numpy as np
from scipy.integrate import odeint
import matplotlib.pyplot as plt

# Параметры
N = 1000       # Общее население
I0 = 1         # Начальное число инфицированных
R0 = 0         # Начальное число выздоровевших
S0 = N - I0 - R0  # Начальное число восприимчивых
beta = 0.3     # Коэффициент передачи
gamma = 0.1    # Коэффициент выздоровления

# Функция SIR
def sir(y, t, N, beta, gamma):
    S, I, R = y
    dS_dt = -beta * S * I / N
    dI_dt = beta * S * I / N - gamma * I
    dR_dt = gamma * I
    return dS_dt, dI_dt, dR_dt

# Временной интервал
t = np.linspace(0, 160, 160)

# Начальные условия
y0 = S0, I0, R0

# Решение ОДУ
ret = odeint(sir, y0, t, args=(N, beta, gamma))
S, I, R = ret.T

# Построение графика
plt.plot(t, S, label='Восприимчивые')
plt.plot(t, I, label='Инфицированные')
plt.plot(t, R, label='Выздоровевшие')
plt.xlabel('Время / дни')
plt.ylabel('Количество')
plt.legend()
plt.show()