import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.model_selection import train_test_split, cross_val_score
from sklearn.preprocessing import StandardScaler
from sklearn.linear_model import LinearRegression
from sklearn.metrics import r2_score,  accuracy_score, f1_score, confusion_matrix, mean_squared_error, silhouette_score
from sklearn.cluster import KMeans
from sklearn.neighbors import KNeighborsClassifier

scaler = StandardScaler()
pd.options.mode.chained_assignment = None


# НАСТРОЙКА ОТОБРАЖЕНИЯ В КОНСОЛИ
pd.set_option('display.max_columns', None)      # Показывать все поля
pd.set_option('display.expand_frame_repr', False) # Не переносить строки
pd.set_option('display.max_colwidth', None)     # Максимальная ширина столбцов
pd.set_option('display.float_format', lambda x: '%.3f' % x)


# ЗАГРУЗКА ДАННЫХ
excel_data = pd.read_csv('all_v2.csv', delimiter=',')
# excel_data = excel_data.iloc[::40] # сокращение числа записей
excel_data = excel_data.iloc[::1] # сокращение числа записей


# УДАЛЕНИЕ ПУСТЫХ КАТЕГОРИАЛЬНЫХ, ЗАПОЛНЕНИЕ ЧИСЛОВЫХ
excel_data = excel_data[(excel_data["rooms"] <= 5)]
excel_data['rooms'] = excel_data["rooms"].apply(lambda x: 1 if x <= 0 else x)
excel_data["price"] = excel_data["price"].abs()
min_price = 600000
max_price = 6000000
excel_data = excel_data[(excel_data["price"] <= max_price) & (excel_data["price"] >= min_price)]#
excel_data["object_type"] = excel_data["object_type"].apply(lambda x: 2 if x == 11 else x)
excel_data['date'] = pd.to_datetime(excel_data['date'], format='%Y-%m-%d').dt.year
excel_data = excel_data.drop(['time'], axis = 1)
min_area = 10
max_area = 150
min_kitchen_area = 6
max_kitchen_area = 60
excel_data = excel_data[(excel_data["area"] <= max_area) & (excel_data["area"] >= min_area)]
excel_data = excel_data[(excel_data["kitchen_area"] <= max_kitchen_area) & (excel_data["kitchen_area"] >= min_kitchen_area)]
region_name = {
    '2661': 'Санкт-Петербург',
    '3446': 'Ленинградская область', 
    '3': 'Москва',
    '81': 'Московская область',
    '2843': 'Краснодарский край',
    '2871': 'Нижегородская область',
    '3230': 'Ростовская область',
    '3106': 'Самарская область',
    '2922': 'Республика Татарстан',
    '2900': 'Ставропольский край',
    '2722': 'Республика Башкортостан',
    '6171': 'Свердловская область', 
    '4417': 'Республика Коми', 
    '5282': 'Челябинская область', 
    '5368': 'Иркутская область', 
    '5520': 'Пермский край', 
    '6817': 'Алтайский край',
    '9579': 'Республика Бурятия',
    '2604': 'Ярославская область',
    '1010': 'Удмуртская Республика',
    '7793': 'Псковская область',
    '13919': 'Республика Северная Осетия — Алания',
    '2860': 'Кемеровская область',
    '3019': 'Чувашская Республика',
    '4982': 'Республика Марий Эл',
    '9648': 'Кабардино-Балкарская Республика',
    '5241': 'Республика Мордовия',
    '3870': 'Красноярский край',
    '3991': 'Тюменская область',
    '2359': 'Республика Хакасия',
    '9654': 'Новосибирская область',
    '2072': 'Воронежская область',
    '8090': 'Республика Карелия',
    '4007': 'Республика Дагестан',
    '11171': 'Республика Саха (Якутия)',
    '10160': 'Забайкальский край',
    '7873, 6937': 'Республика Крым',
    '2594': 'Кировская область',
    '8509': 'Республика Калмыкия',
    '11416': 'Республика Адыгея',
    '11991': 'Карачаево-Черкесская Республика',
    '5178': 'Республика Тыва',
    '13913': 'Республика Ингушетия',
    '6309': 'Республика Алтай',
    '5952': 'Белгородская область',
    '6543': 'Архангельская область',
    '2880': 'Тверская область',
    '5993': 'Пензенская область',
    '2484': 'Ханты-Мансийский автономный округ',
    '4240': 'Липецкая область',
    '5789': 'Владимирская область',
    '14880': 'Ямало-Ненецкий автономный округ',
    '1491': 'Рязанская область',
    '2885': 'Чеченская Республика',
    '5794': 'Смоленская область',
    '2528': 'Саратовская область',
    '4374': 'Вологодская область',
    '4695': 'Волгоградская область',
    '2328': 'Калужская область',
    '5143': 'Тульская область',
    '2806': 'Тамбовская область',
    '14368': 'Мурманская область',
    '5736': 'Новгородская область',
    '7121': 'Курская область',
    '4086': 'Хабаровский край',
    '821': 'Брянская область',
    '10582': 'Астраханская область',
    '7896': 'Калининградская область',
    '8640': 'Омская область',
    '5703': 'Курганская область',
    '10201': 'Томская область',
    '4249': 'Ульяновская область',
    '3153': 'Оренбургская область',
    '4189': 'Костромская область',
    '2814': 'Орловская область',
    '13098': 'Камчатский край',
    '8894': 'Ивановская область',
    '7929': 'Амурская область',
    '16705': 'Магаданская область',
    '69': 'Еврейская автономная область',
    '4963': 'Приморский край',
    '1901': 'Сахалинская область',
    '61888': 'Ненецкий автономный округ'
}
excel_data['region_name'] = excel_data["region"].astype(str).map(region_name) 
excel_data.dropna(inplace=True)
useful_data = excel_data.drop_duplicates()


# # ОПРЕДЕЛЕНИЕ УНИКАЛЬНЫХ ЗНАЧЕНИЙ, проверка данных
# print(useful_data.describe())
# print(useful_data.head())
# print(useful_data.info())
# print(useful_data['rooms'].unique())
# print(useful_data.isna().sum())


# # КОРРЕЛЯЦИЯ
# plt.figure(figsize=(12, 7))
# plt.title('Карта корреляций')
# sns.heatmap(useful_data.select_dtypes(include=[np.number]).corr(), annot=True, fmt='.2f', cmap='coolwarm')
# plt.show()

# # РЕГИОНЫ С НАИБОЛЬШИМ КОЛИЧЕСТВОМ ОБЪЯВЛЕНИЙ О ПРОДАЖЕ КВАРТИР
# useful_data['region_name'].value_counts().head(20).plot(kind='pie',stacked=True,figsize=(20,10))
# plt.title("Регионы с наибольшим количеством объявлений о продаже квартир",fontsize=15)
# plt.ylabel(' ')
# plt.show()

# # ГРАФИК СРЕДНЕЙ ЦЕНЫ ПО ВСЕМ РЕГИОНАМ
# mean_price = useful_data.groupby('region_name', as_index=False).agg(avg_price = ("price", "mean")).round(2)
# plt.figure(figsize=(15, 6))
# plt.bar(x=mean_price['region_name'],height=mean_price['avg_price'])
# plt.title('График средней цены по всем регионам', fontsize=20)
# plt.xticks(rotation=90)
# plt.show()


# # САМЫЙ ДОРОГОЙ И САМЫЙ ДЕШЕВЫЙ СРЕДИ ВСЕХ РЕГИОНОВ
# most_expensive = useful_data.sort_values(by = 'price', ascending=False)
# print(most_expensive[['region_name', 'price']].head(10).drop_duplicates())
# cheapest = useful_data.sort_values(by = 'price', ascending=True)
# print(cheapest[['region_name', 'price']].head(10))
data_novobl = useful_data[useful_data['region_name'] == 'Новосибирская область'].copy().drop(['region_name', 'region'], axis = 1)


# # ОБЩИЕ ДИАГРАММЫ ЗАВИСИМОСТЕЙ ДЛЯ Новосибирская область
# axes = pd.plotting.scatter_matrix(data_novobl, figsize=(15,15), diagonal='kde', grid=True)
# corr = data_novobl.corr().values
# for i, j in zip(*plt.np.triu_indices_from(axes, k=1)):
#     axes[i, j].annotate("%.3f" %corr[i,j], (0.6, 0.8), xycoords='axes fraction', ha='center', va='center')
# plt.show()


# # ПРОВЕРКА БАЛАНСА ДАННЫХ
# sns.countplot(data = useful_data, x='building_type')
# plt.show()
# sns.countplot(data = useful_data, x='level')
# plt.show()
# sns.countplot(data = useful_data, x='levels')
# plt.xticks(rotation='vertical')
# plt.show()
# sns.countplot(data = useful_data, x='rooms')
# plt.xticks(rotation='vertical')
# plt.show()
# sns.countplot(data = useful_data, x='object_type')
# plt.show()
# sns.countplot(data = useful_data, x='price')
# plt.show()


# # ПРОВЕРКА ЗАВИСИМОСТИ ОДНОГО ПРИЗНАКА ОТ ДРУГОГО
# pd.crosstab(useful_data['region'], useful_data['object_type']).plot(kind='bar', title='Проверка зависимостей')
# plt.xticks(rotation='vertical')
# plt.show()
# pd.crosstab(useful_data['region'], useful_data['building_type']).plot(kind='bar', title='Проверка зависимостей')
# plt.xticks(rotation='vertical')
# plt.show()


# # ЛИНЕЙНАЯ РЕГРЕССИЯ
# X = data_novobl[['price']]
# y = data_novobl['area'].astype(np.int64)
# X_scaled = scaler.fit_transform(X)
# X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, random_state=42)
# reg_logistic_model = LinearRegression()
# reg_logistic_model.fit(X_train, y_train)
# print(reg_logistic_model.coef_)
# print(reg_logistic_model.intercept_)
# y_predict = reg_logistic_model.predict(X_test)
# plt.figure(figsize=(13, 8))
# plt.suptitle(f"Регрессионный анализ", fontsize=20)
# plt.title(f'price/area\n'
#           f'Коэф. детерминации R² = {r2_score(y_test, y_predict):.4f}\n'
#           f'Среднеквадр. ошибка RMSE = {np.sqrt(mean_squared_error(y_test, y_predict)):.4f}\n'
#           f'Перекрёстная проверка = {cross_val_score(reg_logistic_model, X_scaled, y, cv=5)}')
# plt.scatter(X_train, y_train, alpha=0.2, label='Прогноз')
# plt.plot(X_test, y_predict, 'r--', lw=3)
# plt.legend()
# plt.tight_layout()
# plt.show()


# ЛИНЕЙНАЯ РЕГРЕССИЯ
test_data = data_novobl[(data_novobl['levels']==5)]
X = test_data[['area']].astype(np.float64)
y = test_data['price'].astype(np.float64)
X_scaled = scaler.fit_transform(X)
X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, random_state=42)
reg_logistic_model = LinearRegression()
reg_logistic_model.fit(X_train, y_train)
print(reg_logistic_model.coef_)
print(reg_logistic_model.intercept_)
y_predict = reg_logistic_model.predict(X_test)
plt.figure(figsize=(13, 8))
plt.suptitle(f"Регрессионный анализ", fontsize=20)
plt.title(f'area/price\n'
          f'Коэф. детерминации R² = {r2_score(y_test, y_predict):.4f}\n'
          f'Среднеквадр. ошибка RMSE = {np.sqrt(mean_squared_error(y_test, y_predict)):.4f}\n'
          f'Перекрёстная проверка = {cross_val_score(reg_logistic_model, X_scaled, y, cv=5)}')
plt.scatter(X_train, y_train, alpha=0.2, label='Прогноз')
plt.plot(X_test, y_predict, 'r--', lw=3)
plt.legend()
plt.tight_layout()
plt.show()


# # ЛИНЕЙНАЯ РЕГРЕССИЯ
# X = data_novobl[['area']]
# y = data_novobl['rooms'].astype(np.int64)
# X_scaled = scaler.fit_transform(X)
# X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, random_state=42)
# reg_logistic_model = LinearRegression()
# reg_logistic_model.fit(X_train, y_train)
# print(reg_logistic_model.coef_)
# print(reg_logistic_model.intercept_)
# y_predict = reg_logistic_model.predict(X_test)
# plt.figure(figsize=(13, 8))
# plt.suptitle(f"Регрессионный анализ", fontsize=20)
# plt.title(f'area/rooms\n'
#           f'Коэф. детерминации R² = {r2_score(y_test, y_predict):.4f}\n'
#           f'Среднеквадр. ошибка RMSE = {np.sqrt(mean_squared_error(y_test, y_predict)):.4f}\n'
#           f'Перекрёстная проверка = {cross_val_score(reg_logistic_model, X_scaled, y, cv=5)}')
# plt.scatter(X_train, y_train, alpha=0.2, label='Прогноз')
# plt.plot(X_test, y_predict, 'r--', lw=3)
# plt.legend()
# plt.tight_layout()
# plt.show()


# # КЛАСТЕРИЗАЦИЯ
# X_scaled = scaler.fit_transform(useful_data[['geo_lat', 'price']])
# # Определение оптимального числа кластеров
# error = []
# silhouette_scores = {}
# for i in range(2, 10):
#     kmeans = KMeans(n_clusters = i, random_state = 42, init = 'k-means++')
#     kmeans.fit(X_scaled)
#     error.append(kmeans.inertia_)
# # ГРАФИК ЛОКТЯ
# plt.title('График локтя (geo_lat, price)')
# plt.plot(range(2, 10), error, marker='o')
# plt.xlabel('Число кластеров')
# plt.ylabel('Ошибка')
# plt.tight_layout()
# plt.show()
# kmeans = KMeans(n_clusters = 3, random_state=42)
# kmeans.fit(X_scaled)
# silhouette = silhouette_score(X_scaled, kmeans.labels_)
# print(f'Оценка силуэта = {silhouette}')
# # Визуализация кластеров
# plt.figure(figsize=(10, 6))
# plt.title('Кластеризация')
# plt.xlabel('географическая широта')
# plt.ylabel('цена')
# plt.scatter(X_scaled[:, 0], X_scaled[:, 1], c=kmeans.labels_, cmap='viridis', marker='o')
# plt.scatter(kmeans.cluster_centers_[:, 0], kmeans.cluster_centers_[:, 1], s=200, c='red', marker='X', label='Centroids')
# plt.legend()
# plt.show()


# КЛАССИФИКАЦИЯ С ИСПОЛЬЗОВАНИЕМ K-БЛИЖАЙШИХ СОСЕДЕЙ
X = data_novobl.drop(["rooms"], axis=1)
y = data_novobl["rooms"]
X_scaled = scaler.fit_transform(X)
X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, random_state=42)
knn = KNeighborsClassifier(n_neighbors = 3, weights='distance')
# тренировка модели
knn.fit(X_train, y_train)
y_pred_cls = knn.predict(X_test)
print("Точность классификации:", accuracy_score(y_test, y_pred_cls))
print(f"F1-Score (kNN): {f1_score(y_test, y_pred_cls, average='macro'):.2f}")
# Визуализация результатов классификации
labels = ['1', '2', '3', '4', '5']
plt.figure(figsize=(9, 7))
plt.title('Матрица ошибок классификации')
sns.heatmap(confusion_matrix(y_test, y_pred_cls), annot=True, fmt='d', cmap='Blues', xticklabels=labels, yticklabels=labels)
plt.xlabel('Предсказанный статус')
plt.ylabel('Фактический статус количества комнат')
plt.tight_layout()
plt.show()
# Оцениваем точность классификации с использованием перекрестной проверки
cv_scores_cls = cross_val_score(knn, X_scaled, y, cv=5, scoring='accuracy')
print("Перекрестная проверка точности =", np.mean(cv_scores_cls))