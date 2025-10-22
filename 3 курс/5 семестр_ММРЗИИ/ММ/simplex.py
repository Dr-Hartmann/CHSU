
import matplotlib.pyplot as plt
from prettytable import PrettyTable
from fractions import Fraction
import numpy as np

from warnings import warn


class Simplex(object):
    def __init__(self, num_vars, constraints, objective_function):
        """
        num_vars: Number of variables

        equations: A list of strings representing constraints
        each variable should be start with x followed by a underscore
        and a number
        eg of constraints
        ['1x_1 + 2x_2 >= 4', '2x_3 + 3x_1 <= 5', 'x_3 + 3x_2 = 6']
        Note that in x_num, num should not be more than num_vars.
        Also single spaces should be used in expressions.

        objective_function: should be a tuple with first element
        either 'min' or 'max', and second element be the equation
        eg
        ('min', '2x_1 + 4x_3 + 5x_2')

        For solution finding algorithm uses two-phase simplex method
        """
        self.num_vars = num_vars
        self.constraints = constraints
        self.objective = objective_function[0]
        self.objective_function = objective_function[1]
        self.coeff_matrix, self.r_rows, self.num_s_vars, self.num_r_vars = self.construct_matrix_from_constraints()
        del self.constraints
        self.basic_vars = [0 for i in range(len(self.coeff_matrix))]
        self.phase1()
        r_index = self.num_r_vars + self.num_s_vars

        for i in self.basic_vars:
            if i > r_index:
                raise ValueError("Infeasible solution")

        self.delete_r_vars()

        if 'min' in self.objective.lower():
            self.solution = self.objective_minimize()

        else:
            self.solution = self.objective_maximize()
        self.optimize_val = self.coeff_matrix[0][-1]

    def print_simplex_table(self):
        """Print current simplex table"""
        table = PrettyTable()
        
        # Add column headers
        headers = ['Basic'] + [f'x_{i+1}' for i in range(self.num_vars)] + \
                 [f's_{i+1}' for i in range(self.num_s_vars)] + ['RHS']
        table.field_names = headers
        
        # Add rows
        for i, row in enumerate(self.coeff_matrix):
            basic_var = 'Z' if i == 0 else f'x_{self.basic_vars[i]}'
            table.add_row([basic_var] + [float(x) for x in row])
            
        print("\nТаблица симплекс-метода:")
        print(table)

    def plot_solution(self):
        """Plot graphical representation for 2D problems"""
        j= 0
        newL = list()
        for i, var in enumerate(self.basic_vars[1:]):
            if var < self.num_vars:
                j += 1
                newL.append(float(self.coeff_matrix[i+1][-1]))
        if j != 2:
            print("Графическое представление доступно только для двумерных задач")
            return

        fig, ax = plt.subplots(figsize=(10, 6))

        # Построить ограничения
        x1 = np.linspace(0, 10, 100)
         # Из первого ограничения: x₁ + 2x₂ + x₃ = 8
        x2_1 = (8 - x1) / 2
        # Из второго ограничения: x₁ + 3x₃ + x₄ = 6
        x2_2 = (6 - x1)


        plt.plot(x1, x2_1, 'b-', label='x₁ + 2x₂ + x₃ = 8')
        plt.plot(x1, x2_2, 'r-', label='x₁ + 3x₃ + x₄ = 6')
        plt.plot(newL[1], newL[0], 'go', label='Оптимальная точка')


        # Добавляем неотрицательность переменных
        plt.axhline(y=0, color='k', linestyle='-')
        plt.axvline(x=0, color='k', linestyle='-')

        # Построить допустимую область (пересечение всех ограничений)
        feasible_x2 = np.minimum(x2_1, x2_2)
        feasible_x2 = np.maximum(feasible_x2, 0)  # учитываем x₂ ≥ 0

        # Построить допустимую область
        plt.fill_between(x1, np.minimum(x2_1, x2_2), 0, alpha=0.4, color='green')

        plt.grid(True)
        plt.xlabel('x₁')
        plt.ylabel('x₂')
        plt.title('Графическое решение (проекция на плоскость x₁-x₂)')
        plt.legend()
        plt.xlim(left=0)
        plt.ylim(bottom=0)
        plt.show()

    def print_basic_solution(self):
        """Print the basic solution"""
        print("\nБазисное решение:")
        for i, var in enumerate(self.basic_vars[1:]):
            if var < self.num_vars:
                print(f"x_{var+1} = {float(self.coeff_matrix[i+1][-1]):.3f}")

    def print_optimal_solution(self):
        """Print the optimal solution with formatting"""
        print("\nОптимальное решение:")
        print("-" * 40)
        for var, value in self.solution.items():
            print(f"{var} = {float(value):.3f}")
        print(f"Оптимальное значение = {float(self.optimize_val):.3f}")
        print("-" * 40)
    def construct_matrix_from_constraints(self):
        num_s_vars = 0  # number of slack and surplus variables
        num_r_vars = 0  # number of additional variables to balance equality and less than equal to
        for expression in self.constraints:
            if '>=' in expression:
                num_s_vars += 1

            elif '<=' in expression:
                num_s_vars += 1
                num_r_vars += 1

            elif '=' in expression:
                num_r_vars += 1
        total_vars = self.num_vars + num_s_vars + num_r_vars
        self.right_part = [constraint[-1] for constraint in self.constraints]
        self.x2 = []
        for constraint in self.constraints:
            parts = constraint.split()
            for i, part in enumerate(parts):
                if 'x_2' in part:
                    coeff = part[0]
                    self.x2.append(int(coeff))
                    break
        print(self.x2)
        coeff_matrix = [[Fraction("0") for i in range(total_vars+1)] for j in range(len(self.constraints)+1)]
        s_index = self.num_vars
        r_index = self.num_vars + num_s_vars
        r_rows = [] # stores the non -zero index of r
        for i in range(1, len(self.constraints)+1):
            constraint = self.constraints[i-1].split(' ')

            for j in range(len(constraint)):

                if '_' in constraint[j]:
                    coeff, index = constraint[j].split('_')
                    if constraint[j-1] == '-':
                        coeff_matrix[i][int(index)-1] = Fraction("-" + coeff[:-1] + "")
                    else:
                        coeff_matrix[i][int(index)-1] = Fraction(coeff[:-1] + "")

                elif constraint[j] == '<=':
                    coeff_matrix[i][s_index] = Fraction("1")  # add surplus variable
                    s_index += 1

                elif constraint[j] == '>=':
                    coeff_matrix[i][s_index] = Fraction("-1")  # slack variable
                    coeff_matrix[i][r_index] = Fraction("1")   # r variable
                    s_index += 1
                    r_index += 1
                    r_rows.append(i)

                elif constraint[j] == '=':
                    coeff_matrix[i][r_index] = Fraction("1")  # r variable
                    r_index += 1
                    r_rows.append(i)

            coeff_matrix[i][-1] = Fraction(constraint[-1] + "")

        return coeff_matrix, r_rows, num_s_vars, num_r_vars

    def phase1(self):
        # Objective function here is minimize r1+ r2 + r3 + ... + rn
        r_index = self.num_vars + self.num_s_vars
        for i in range(r_index, len(self.coeff_matrix[0])-1):
            self.coeff_matrix[0][i] = Fraction("-1")
        coeff_0 = 0
        for i in self.r_rows:
            self.coeff_matrix[0] = add_row(self.coeff_matrix[0], self.coeff_matrix[i])
            self.basic_vars[i] = r_index
            r_index += 1
        s_index = self.num_vars
        for i in range(1, len(self.basic_vars)):
            if self.basic_vars[i] == 0:
                self.basic_vars[i] = s_index
                s_index += 1

        # Run the simplex iterations
        key_column = max_index(self.coeff_matrix[0])
        condition = self.coeff_matrix[0][key_column] > 0

        while condition is True:

            key_row = self.find_key_row(key_column = key_column)
            self.basic_vars[key_row] = key_column
            pivot = self.coeff_matrix[key_row][key_column]
            self.normalize_to_pivot(key_row, pivot)
            self.make_key_column_zero(key_column, key_row)

            key_column = max_index(self.coeff_matrix[0])
            condition = self.coeff_matrix[0][key_column] > 0

    def find_key_row(self, key_column):
        min_val = float("inf")
        min_i = 0
        for i in range(1, len(self.coeff_matrix)):
            if self.coeff_matrix[i][key_column] > 0:
                val = self.coeff_matrix[i][-1] / self.coeff_matrix[i][key_column]
                if val <  min_val:
                    min_val = val
                    min_i = i
        if min_val == float("inf"):
            raise ValueError("Unbounded solution")
        if min_val == 0:
            warn("Dengeneracy")
        return min_i

    def normalize_to_pivot(self, key_row, pivot):
        for i in range(len(self.coeff_matrix[0])):
            self.coeff_matrix[key_row][i] /= pivot

    def make_key_column_zero(self, key_column, key_row):
        num_columns = len(self.coeff_matrix[0])
        for i in range(len(self.coeff_matrix)):
            if i != key_row:
                factor = self.coeff_matrix[i][key_column]
                for j in range(num_columns):
                    self.coeff_matrix[i][j] -= self.coeff_matrix[key_row][j] * factor

    def delete_r_vars(self):
        for i in range(len(self.coeff_matrix)):
            non_r_length = self.num_vars + self.num_s_vars + 1
            length = len(self.coeff_matrix[i])
            while length != non_r_length:
                del self.coeff_matrix[i][non_r_length-1]
                length -= 1

    def update_objective_function(self):
        objective_function_coeffs = self.objective_function.split()
        for i in range(len(objective_function_coeffs)):
            if '_' in objective_function_coeffs[i]:
                coeff, index = objective_function_coeffs[i].split('_')
                if objective_function_coeffs[i-1] == '-':
                    self.coeff_matrix[0][int(index)-1] = Fraction(coeff[:-1] + "")
                else:
                    self.coeff_matrix[0][int(index)-1] = Fraction("-" + coeff[:-1] + "")

    def check_alternate_solution(self):
        for i in range(len(self.coeff_matrix[0])):
            if self.coeff_matrix[0][i] and i not in self.basic_vars[1:]:
                warn("Alternate Solution exists")
                break

    def objective_minimize(self):
        self.update_objective_function()

        for row, column in enumerate(self.basic_vars[1:]):
            if self.coeff_matrix[0][column] != 0:
                self.coeff_matrix[0] = add_row(self.coeff_matrix[0], multiply_const_row(-self.coeff_matrix[0][column], self.coeff_matrix[row+1]))

        key_column = max_index(self.coeff_matrix[0])
        condition = self.coeff_matrix[0][key_column] > 0

        while condition is True:

            key_row = self.find_key_row(key_column = key_column)
            self.basic_vars[key_row] = key_column
            pivot = self.coeff_matrix[key_row][key_column]
            self.normalize_to_pivot(key_row, pivot)
            self.make_key_column_zero(key_column, key_row)

            key_column = max_index(self.coeff_matrix[0])
            condition = self.coeff_matrix[0][key_column] > 0

        solution = {}
        for i, var in enumerate(self.basic_vars[1:]):
            if var < self.num_vars:
                solution['x_'+str(var+1)] = self.coeff_matrix[i+1][-1]

        for i in range(0, self.num_vars):
            if i not in self.basic_vars[1:]:
                solution['x_'+str(i+1)] = Fraction("0")
        self.check_alternate_solution()
        return solution

    def objective_maximize(self):
        self.update_objective_function()

        for row, column in enumerate(self.basic_vars[1:]):
            if self.coeff_matrix[0][column] != 0:
                self.coeff_matrix[0] = add_row(self.coeff_matrix[0], multiply_const_row(-self.coeff_matrix[0][column], self.coeff_matrix[row+1]))

        key_column = min_index(self.coeff_matrix[0])
        condition = self.coeff_matrix[0][key_column] < 0

        while condition is True:

            key_row = self.find_key_row(key_column = key_column)
            self.basic_vars[key_row] = key_column
            pivot = self.coeff_matrix[key_row][key_column]
            self.normalize_to_pivot(key_row, pivot)
            self.make_key_column_zero(key_column, key_row)

            key_column = min_index(self.coeff_matrix[0])
            condition = self.coeff_matrix[0][key_column] < 0

        solution = {}
        for i, var in enumerate(self.basic_vars[1:]):
            if var < self.num_vars:
                solution['x_'+str(var+1)] = self.coeff_matrix[i+1][-1]

        for i in range(0, self.num_vars):
            if i not in self.basic_vars[1:]:
                solution['x_'+str(i+1)] = Fraction("0")

        self.check_alternate_solution()

        return solution

def add_row(row1, row2):
    row_sum = [0 for i in range(len(row1))]
    for i in range(len(row1)):
        row_sum[i] = row1[i] + row2[i]
    return row_sum

def max_index(row):
    max_i = 0
    for i in range(0, len(row)-1):
        if row[i] > row[max_i]:
            max_i = i

    return max_i

def multiply_const_row(const, row):
    mul_row = []
    for i in row:
        mul_row.append(const*i)
    return mul_row

def min_index(row):
    min_i = 0
    for i in range(0, len(row)):
        if row[min_i] > row[i]:
            min_i = i

    return min_i


objective = ('max', '3x_1 + 3x_2 - 1x_3 + 1x_4')
constraints = ['1x_1 + 2x_2 + 1x_3 = 8', '1x_1 + 3x_3 + 1x_4 = 6']
Lp_system = Simplex(num_vars=4, constraints=constraints, objective_function=objective)

Lp_system.print_simplex_table()
Lp_system.print_basic_solution()
Lp_system.print_optimal_solution()
Lp_system.plot_solution() 


