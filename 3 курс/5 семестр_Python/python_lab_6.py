from tkinter import filedialog
import re
import numpy as np
import struct
import matplotlib.pyplot as plt
from functools import reduce


class Map:
    # конструктор
    def __init__(self, header_data, matrix):
        self.xlen = int(header_data[0])
        self.ylen = int(header_data[1])
        self.stepx = float(header_data[2].replace(',', '.'))
        self.stepy = float(header_data[3].replace(',', '.'))
        self.startx = int(header_data[4])
        self.starty = int(header_data[5])
        self.lastx = int(header_data[6])
        self.lasty = int(header_data[7])
        self.width = int(header_data[8])
        self.height = int(header_data[9])
        self.level = float(header_data[10].replace(',', '.'))

        self.heapmap = [[0] * self.ylen for i in range(self.xlen)]

        for i in range(self.xlen):
            for j in range(self.ylen):
                self.heapmap[i][j] = float(matrix[j][i])/10 + self.level

    def print(self):
        print(f"{self.xlen}, {self.ylen}, {self.stepx}, {self.stepy}, {self.startx}, {self.starty}, {self.lastx}, {self.lasty}, {self.width}, {self.height}, {self.level}")

    # деструктор
    def __del__(self):
        print(f"Удаление: {self}")



# file_name = filedialog.askopenfilename()
file_name = "e:\_Python\data.dat"

if file_name != "":
    try:
        with open(file_name, "r") as map_file:

            header_data = map_file.readline()
            header_data = re.split(r"[\|| |\r|\n]+", header_data)
            header_length = int(map_file.readline())

        with open(file_name, "rb") as map_file_2:

            map_file_2.seek(header_length)
            binary_data = map_file_2.read((int(header_data[0]) * int(header_data[1])) * 4)
            final_data = struct.unpack(f'{(int(header_data[0]) * int(header_data[1]))}f', binary_data)
            read_data = np.array(final_data).reshape((int(header_data[1]), int(header_data[0])))

            map = Map(header_data, read_data)
            map.print()

            plt.figure(figsize=(15, 6))

            

            heapmap = plt.imshow(map.heapmap, cmap='hot', extent = (map.starty, map.height * map.stepy, map.startx, map.width * map.stepx))

            cb = plt.colorbar(heapmap, label='Толщина, мм', format='%.1f')
            cb.set_ticks(np.linspace(np.min(map.heapmap), np.max(map.heapmap), num=15))

            plt.show()

            print("Работа с файлом закончена")

    except Exception as ex:
        print(ex)
    
else:
    print("Отсутствует путь")