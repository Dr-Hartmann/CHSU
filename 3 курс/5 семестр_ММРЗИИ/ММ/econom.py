# Данные по продуктам
products = [
    {'name': 'A', 'Q': 25, 'P': 5, 'AVC': 3},
    {'name': 'B', 'Q': 50, 'P': 10, 'AVC': 5},
    {'name': 'C', 'Q': 100, 'P': 20, 'AVC': 15},
    {'name': 'D', 'Q': 75, 'P': 30, 'AVC': 20},
]

# Постоянные затраты
FC = 780

# Расчет общей выручки, общей маржинальной прибыли и маржинального коэффициента
total_revenue = 0
total_CM = 0
for product in products:
    revenue = product['Q'] * product['P']
    CM = (product['P'] - product['AVC']) * product['Q']
    total_revenue += revenue
    total_CM += CM

CM_ratio = total_CM / total_revenue

# Точка безубыточности в денежном выражении
break_even_sales = FC / CM_ratio

print(f"Точка безубыточности: {break_even_sales:.2f} д.е.")

# Расчет точки безубыточности для каждого продукта
for product in products:
    CM_per_unit = product['P'] - product['AVC']
    if CM_per_unit > 0:
        break_even_units = FC / CM_per_unit
        print(f"Точка безубыточности для продукта {product['name']}: {break_even_units:.2f} единиц")
    else:
        print(f"Продукт {product['name']} не приносит маржинальную прибыль.")