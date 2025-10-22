# 	a or b / (c * d) – 5 + e - sin(d) 
import math

a = float(input("Введите a: "))
b = float(input("Введите b: "))
c = float(input("Введите c: "))
d = float(input("Введите d: "))

result1 = int(a) | int(b / (c*d) - 5 + math.e - math.sin(d))
print("Результат: ", result1)

result2 = int(a) | int(((b / (c*d)) - 5 + math.e - math.sin(d)))
print(f"Приоритет операторов скобками: {result2:.3f}")

# { }
# f(args…)
# x[index:index]
# x[index]
# x.attribute
# **
# ~x
# +x, -x
# *, /, %
# +, —
# <<, >>
# &
# ^
# |
# in, not in, is, is not, <, <=, >, >=, <>, !=, ==
# not x
# and
# or
# lambda