import abc

# абстрактный класс
class Machine(abc.ABC):
    @abc.abstractmethod 
    def type(self): pass
    def print_type(): pass
    def print(self): pass
    def __add__(self, other): pass
    def __str__(self): pass
    def __del__(self): pass

#описание класса
class Car(Machine):
    #конструктор
    def __init__(self, name, amount):
        self.amount = amount
        self.name = name
        print('!', "Создание объекта", self.name, self.amount, "!")
    
    #функция
    def reverse_number(self, number):
        tmp = 0
        while number > 0:
            tmp *= 10
            tmp += number % 10
            number //= 10
        return tmp
    
    #свойство-геттер
    @property
    def amount(self):
        return self.__amount
    #свойство-сеттер
    @amount.setter
    def amount(self, amount):
        if amount > 0:
            self.__amount = amount
        else:
            print("Некорректный ввод:", amount) 

    #свойство-геттер
    @property
    def name(self):
        return self.__name
    #свойство-сеттер
    @name.setter
    def name(self, name):
        self.__name = name
    
    #деструктор
    def __del__(self):
        print(f"! Удаление: {self.name} !")

    #атрибут класса
    __type = "Car"

    #статический метод
    @staticmethod
    def print_type():
        print('Type = ', Car.__type)

    def type(self):
        return self.__type
    
    #перегрузка операторов
    def __add__(self, other):
        return Car(self.name, self.amount + other.amount)
    
    def __getitem__(self, prop):
        if prop == "name": return self.name
        elif prop == "amount": return self.amount
        return None
    
    def __str__(self):
        return f"Name: {self.name}  Amount: {self.amount}  Type: {self.type()}"
    
    def print(self):
        print(self.__str__(), end=" ")




class Ferrari(Car):
    default_name = "Undefined"
    
    __type = "Supercar"

    def type(self):
        return self.__type

    def __init__(self, name, amount, power):
        super().__init__(name, amount)
        self.__power = power
        self.mas = []

    @property
    def power(self):
        return self.__power
    @power.setter
    def power(self, power):
        if power > 0:
            self.__power = power
        else:
            print("Некорректный ввод:", power)

    def __str__(self):
        return f"Name: {self.name}  Power: {self.power}bhp  Type: {self.type()}"
    
    #чтобы убрать пустоту базового класса
    def print(self):
        print(super().print())

#множественное наследование?



class FileException(Exception):
    def __init__(self, file_name):
        self.__name = file_name
 
    def __str__(self):
        return f"Недопустимое значение: {self.__name}. " \
               f"Выберите другой файл или скорректируйте имя"



#создание объектов
vlg = Car("волга", 3)
ldcln = Car("лада калина", 8841)

# #динамическое создание атрибута у конкретного объекта
# vlg.age = 200
# print(vlg.age)

# #доступ к приватному полю через полное имя
# vlg._Car__name = "газ-24"

# print('reverse', ldcln.reverse_number(ldcln.amount))

ferrari448gtb2016edition = Ferrari("ferrari 448 gtb", 100, 660)
# ferrari448gtb2016edition.print()
# print('__str__:', ferrari448gtb2016edition)

ldcln.print_type()

print(ldcln)
ldcln += vlg
print(ldcln)
print("NameAmountName", f"!drghd!")
#{ldcln["name"]}-{ldcln["amount"]}-{ldcln["name"]}

try:
    # number = int(input("Введите число: "))
    # print("Значение параболы в этой точке:", 1/number)
    
    name = input(print("Введите имя файла: "))
    file = open(f"{name}.txt", "w+")

    if name == "123" or name.__len__() > 12:
        raise FileException(name)
    
    print("Файл создан")
    
    file.write(ldcln.__str__() + "\n")
    print("1")
    file.write(vlg.__str__() + "\n")
    print("2")
    
    assert ferrari448gtb2016edition.amount < 99, "Слишком много машин"
    file.write(ferrari448gtb2016edition.__str__() + "\n")
    print("3")
    
except ZeroDivisionError as e:
    print("Деление числа на ноль, ДУРАК!\n", e)
except FileException as e:
    print("Ошибка файла", e)
except BaseException as e:
    print("Общее исключение, ИЩИ САМ!", "\n", e)

finally:
    if file.closed == False:
        file.close()
    print("Блок try завершил выполнение")
    
print("Завершение программы")