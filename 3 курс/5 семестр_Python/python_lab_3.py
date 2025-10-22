# heapq и list

# Продемонстрировать основные операции, которые можно применить 
# к каждой из структур.

# Определить, какая из структур быстрее в операциях: 
# доступа, добавления, удаления, объединения, пересечения, разницы, 
# сравнения, поиска, сортировки и т.п., при условии, что обе структуры 
# поддерживают данные операции.

# Обеспечить возможность сохранения данных в файл и загрузке из файла.

import heapq
import timeit

list1=[1,2,3,4,5]
list2=[1,2,3,4,5]
heapq.heapify(list2)

def list_add():
    list1.append(10)
    # heapq.heappush(list, 99)
    # heapq.heappop(list)
    # heapq.heappushpop(list, 11)

def heapq_add():
    list2.append(10)


add_time1 = timeit.timeit('list_add()', number=1000, globals=globals())
add_time2 = timeit.timeit('heapq_add()', number=1000, globals=globals())

print('\nВремя добавления элемента в лист: {} usec'.format((add_time1)*1e6))
print('Время добавления элемента в кучу: {} usec'.format((add_time2)*1e6))

add_time1 = timeit.timeit('list_add()', number=1000, globals=globals())
add_time2 = timeit.timeit('heapq_add()', number=1000, globals=globals())

print('\nВремя добавления элемента в лист: {} usec'.format((add_time1)*1e6))
print('Время добавления элемента в кучу: {} usec'.format((add_time2)*1e6))


import timeit 
from collections import defaultdict 
import pickle 
 
# set funcs 
def set_add(s): 
    s.add(1000) 
 
def set_remove(s): 
    s.add(1000) 
    s.remove(1000) 
 
def set_access(s): 
    return 1000 in s 
 
def set_union(s1, s2): 
    return s1.union(s2) 
 
def set_intersection(s1, s2): 
    return s1.intersection(s2) 
 
def set_difference(s1, s2): 
    return s1.difference(s2) 
 
# dict funcs 
def dd_add(d): 
    d[1000] += 1 
 
def dd_remove(d): 
    d[1000] += 1 
    del d[1000] 
 
def dd_access(d): 
    return d[1000] 
 
def dd_union(d1, d2): 
    res = defaultdict(int) 
    for k in d1: 
        res[k] += d1[k] 
    for k in d2: 
        res[k] += d2[k] 
    return res 
 
def dd_intersection(d1, d2): 
    return {k: d1[k] for k in d1 if k in d2} 
 
def dd_difference(d1, d2): 
    return {k: d1[k] for k in d1 if k not in d2} 
 
s1 = set(range(1000)) 
s2 = set(range(500, 1500)) 
dd1 = defaultdict(int, {i: 1 for i in range(1000)}) 
dd2 = defaultdict(int, {i: 1 for i in range(500, 1500)}) 
 
def measure_time(): 
    operations = ["add", "remove", "access", "union", "intersection", "difference"] 
    results = [] 
 
    results.append([ 
        timeit.timeit(lambda: set_add(s1.copy()), number=10000) * 1000, 
        timeit.timeit(lambda: dd_add(dd1.copy()), number=10000) * 1000, 
    ]) 
    results.append([ 
        timeit.timeit(lambda: set_remove(s1.copy()), number=10000) * 1000, 
        timeit.timeit(lambda: dd_remove(dd1.copy()), number=10000) * 1000, 
    ]) 
    results.append([ 
        timeit.timeit(lambda: set_access(s1), number=10000) * 1000, 
        timeit.timeit(lambda: dd_access(dd1), number=10000) * 1000, 
    ]) 
    results.append([ 
        timeit.timeit(lambda: set_union(s1, s2), number=1000) * 1000, 
        timeit.timeit(lambda: dd_union(dd1, dd2), number=1000) * 1000, 
    ]) 
    results.append([ 
        timeit.timeit(lambda: set_intersection(s1, s2), number=1000) * 1000, 
        timeit.timeit(lambda: dd_intersection(dd1, dd2), number=1000) * 1000, 
    ]) 
    results.append([ 
        timeit.timeit(lambda: set_difference(s1, s2), number=1000) * 1000, 
        timeit.timeit(lambda: dd_difference(dd1, dd2), number=1000) * 1000, 
    ]) 
 
    return operations, results 
 
def save_data(structure, filename): 
    with open(filename, 'wb') as f: 
        pickle.dump(structure, f) 
 
def load_data(filename): 
    with open(filename, 'rb') as f: 
        return pickle.load(f) 
 
def print_results(operations, results): 
    print(f"{'Operation':<15}{'Set Time (ms)':<15}{'Defaultdict Time (ms)':<15}") 
    for i, op in enumerate(operations): 
        print(f"{op:<15}{results[i][0]:<15.3f}{results[i][1]:<15.3f}") 
 
def main(): 
    operations, results = measure_time() 
     
    print_results(operations, results) 
 
    save_data(s1, 'set_data.pkl') 
    save_data(dd1, 'defaultdict_data.pkl') 
 
    loaded_set = load_data('set_data.pkl') 
    loaded_dd = load_data('defaultdict_data.pkl') 
     
    print("\nLoaded data structures:") 
    print("Set:", loaded_set) 
    print("Defaultdict:", loaded_dd) 
 
main()