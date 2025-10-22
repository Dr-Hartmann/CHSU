import struct
import numpy as np
import matplotlib.pyplot as plt
import sys
import tkinter as tk
from tkinter import filedialog


def parse_header(data):
    # Decode the header as UTF-8 and split by '|'
    header_text = data.decode('utf-8').split('|')


    # Map the values from header to expected fields
    h = {
        "xlen": int(header_text[0]),
        "ylen": int(header_text[1]),
        "stepx": float(header_text[2].replace(',', '.')),
        "stepy": float(header_text[3].replace(',', '.')),
        "startx": int(header_text[4]),
        "starty": int(header_text[5]),
        "lastx": int(header_text[6]),
        "lasty": int(header_text[7]),
        "width": int(header_text[8]),
        "height": int(header_text[9]),
        "level": float(header_text[10].replace(',', '.')),
    }

    return h


def read_binary_data(file_path, h):

    num_floats = h["width"] * h["height"]
    float_size = struct.calcsize('f') # 4

    # Read the binary data after the header
    with open(file_path, 'rb') as f:

        f.seek(h["size"])
        binary_data = f.read(num_floats * float_size)

        # Unpack binary data to float
        normal_data = struct.unpack(f'{num_floats}f', binary_data)

    # Reshape list into matrix (2d array)
    matrix = np.array(normal_data).reshape((h["height"], h["width"]))

    return matrix


def plot_surface(matrix, h):

    # NORMALIZATION
    data_leveled = matrix + h["level"]

    minimum = 20
    maximum = 25
    data_min = data_leveled.min()
    data_max = data_leveled.max()
    data_normalized = minimum + ((data_leveled - data_min) * (maximum - minimum) / (data_max - data_min))



    plt.figure(figsize=(20, 8))
    # Transposition matrix so length is horizontal and width is vertical
    heatmap = plt.imshow(data_normalized.T, cmap='hot', origin='lower',
    extent= (h["starty"], h["height"] * h["stepy"], h["startx"], h["width"] * h["stepx"]))

    cb = plt.colorbar(heatmap, label='Толщина, мм', format='%.1f')
    cb.set_ticks(np.linspace(minimum, maximum, num=10)) # Divide the colorbar

    plt.title('Поверхность листа', fontweight="bold")
    plt.xlabel('Длина, мм')
    plt.ylabel('Ширина, мм')

    contours = np.arange(minimum, maximum, 0.2) # divide range from min to maximum
    contour_plot = plt.contour(data_normalized.T, levels=contours, colors='black', linewidths=0.5, extent=(h["starty"], h["height"] * h["stepy"], h["startx"], h["width"] * h["stepx"]))
    plt.clabel(contour_plot, fontsize=8, fmt="%.3f mm")


    plt.show()


def main():
    # Проверяем, передан ли файл в аргументах
    if len(sys.argv) > 1:
        file_path = sys.argv[1]
    else:
        # Иначе открываем диалог выбора файла
        root = tk.Tk()
        root.withdraw()
        file_path = filedialog.askopenfilename(title="Выберите бинарный файл данных",
                                               filetypes=(("Binary files", "*.dat"), ("All files", "*.*")))
        if not file_path:
            print("Файл не выбран!")
            return

    # Чтение заголовка
    with open(file_path, 'rb') as f:
        header_data = f.readline()
        header = parse_header(header_data)
        header["size"] = int(f.readline().decode('utf-8'))

    # Чтение и отображение данных
    matrix = read_binary_data(file_path, header)
    plot_surface(matrix, header)


if __name__ == "__main__":
    main()