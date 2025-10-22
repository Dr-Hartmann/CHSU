import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns

from sklearn.model_selection import train_test_split, cross_val_score
from sklearn.preprocessing import StandardScaler
from sklearn.linear_model import LogisticRegression
from sklearn.metrics import r2_score,  accuracy_score, f1_score, confusion_matrix, mean_squared_error
from sklearn.metrics import silhouette_score
from sklearn.cluster import KMeans
from sklearn.neighbors import KNeighborsClassifier

from sklearn.preprocessing import OrdinalEncoder

scaler = StandardScaler()

# НАСТРОЙКА ОТОБРАЖЕНИЯ В КОНСОЛИ
pd.set_option('display.max_columns', None)      # Показывать все поля
pd.set_option('display.expand_frame_repr', False) # Не переносить строки
pd.set_option('display.max_colwidth', None)     # Максимальная ширина столбцов


# ОПРЕДЕЛЕНИЕ ИСПОЛЬЗУЕМЫХ ПРИЗНАКОВ
categorical_cols = ['Description', 'InvoiceDate', 'Country', 'PaymentMethod', 'Category', 'SalesChannel', 'ReturnStatus', 'ShipmentProvider', 'OrderPriority']
numerical_cols = ['Quantity', 'UnitPrice', 'Discount', 'ShippingCost']


# ЗАГРУЗКА ДАННЫХ
excel_data = pd.read_excel('kurs_data.xlsx', index_col=False)
print(excel_data.shape)

# УДАЛЕНИЕ ПУСТЫХ КАТЕГОРИАЛЬНЫХ, ЗАПОЛНЕНИЕ ЧИСЛОВЫХ
excel_data.dropna(subset=categorical_cols, inplace=True)
excel_data = excel_data[(excel_data[numerical_cols] > 0).all(axis=1)]
excel_data = excel_data[(excel_data['Discount'] <= 1.0)]
excel_data['InvoiceDate'] = pd.to_datetime(excel_data['InvoiceDate'], format='%Y-%m-%d %H:%M')
excel_data = excel_data.fillna({'ShippingCost':excel_data['ShippingCost'].mode()})
useful_data = excel_data[categorical_cols + numerical_cols]


# ПРОВЕРКА ЗАВИСИМОСТИ ОДНОГО ПРИЗНАКА ОТ ДРУГОГО
pd.crosstab(useful_data['Country'], useful_data['ReturnStatus']).plot(kind='bar', title='Проверка зависимостей')
pd.crosstab(useful_data['Country'], useful_data['Description']).plot(kind='bar', title='Проверка зависимостей')
pd.crosstab(useful_data['Country'], useful_data['PaymentMethod']).plot(kind='bar', title='Проверка зависимостей')
pd.crosstab(useful_data['Country'], useful_data['ShipmentProvider']).plot(kind='bar', title='Проверка зависимостей')
pd.crosstab(useful_data['ShipmentProvider'], useful_data['ReturnStatus']).plot(kind='bar', title='Проверка зависимостей')


# КОРРЕЛЯЦИЯ
plt.figure(figsize=(12, 7))
plt.title('Карта корреляций')
sns.heatmap(useful_data.select_dtypes(include=[np.number]).corr(), annot=True, fmt='.2f', cmap='coolwarm')
plt.show()


# СОЗДАНИЕ НОВЫХ ПРИЗНАКОВ
labels_Discount = ['0-10%','10-20%','20-40%','40-100%']
labels_ShippingCost = ['0-10','10-20','20-30']
labels_UnitPrice = ['0-10','10-30','30-50','50-100']
# скидка
useful_data['DiscountGroup'] = pd.cut(excel_data['Discount'], bins=[0.0, 0.1, 0.2, 0.4, 1], labels=labels_Discount, include_lowest=True)
# стоимость доставки
useful_data['ShippingCostGroup'] = pd.cut(excel_data['ShippingCost'], bins=[0.0, 10.0, 20.0, 30.0], labels=labels_ShippingCost, include_lowest=True)
# цена за одну единицу
useful_data['UnitPriceGroup'] = pd.cut(excel_data['UnitPrice'], bins=[0.0, 10.0, 30.0, 50.0, 100.0], labels=labels_UnitPrice)
# сумма покупки
useful_data['Sum'] = useful_data['Quantity'] * useful_data['UnitPrice'] * (1 - useful_data['Discount']) + useful_data['ShippingCost']


# ПРОВЕРКА БАЛАНСА ДАННЫХ
plt.figure(figsize=(14, 8))
plt.title('Проверка сбалансированности данных')
plt.subplot(3, 3, 1)
sns.countplot(data = useful_data, x='ReturnStatus')
plt.subplot(3, 3, 2)
sns.countplot(data = useful_data, x='OrderPriority')
plt.subplot(3, 3, 3)
sns.countplot(data = useful_data, x='SalesChannel')
plt.subplot(3, 3, 4)
sns.countplot(data = useful_data, x='PaymentMethod')
plt.subplot(3, 3, 5)
sns.countplot(data = useful_data, x='Category')
plt.xticks(rotation='vertical')
plt.subplot(3, 3, 6)
sns.countplot(data = useful_data, x='Country')
plt.xticks(rotation='vertical')
plt.subplot(3, 3, 7)
sns.countplot(data = useful_data, x='Description')
plt.xticks(rotation='vertical')
plt.subplot(3, 3, 8)
sns.countplot(data = useful_data, x='ShipmentProvider')
plt.tight_layout()
plt.show()


# ОПРЕДЕЛЕНИЕ УНИКАЛЬНЫХ ЗНАЧЕНИЙ
# print(useful_data['ShipmentProvider'].unique())

# ПРЕОБРАЗОВАНИЕ КАТЕГОРИАЛЬНЫХ ПЕРЕМЕННЫХ
pd.options.mode.chained_assignment = None
useful_data['OrderPriority'] = OrdinalEncoder(categories=[['Low', 'Medium', 'High']]).fit_transform(useful_data[['OrderPriority']]).astype('int64')
useful_data['ReturnStatus'] = OrdinalEncoder(categories=[['Returned', 'Not Returned']]).fit_transform(useful_data[['ReturnStatus']]).astype('int64')
useful_data['SalesChannel'] = OrdinalEncoder(categories=[['In-store', 'Online']]).fit_transform(useful_data[['SalesChannel']]).astype('int64')
useful_data['UnitPriceGroup'] = OrdinalEncoder(categories=[labels_UnitPrice]).fit_transform(useful_data[['UnitPriceGroup']]).astype('int64')
useful_data['ShippingCostGroup'] = OrdinalEncoder(categories=[labels_ShippingCost]).fit_transform(useful_data[['ShippingCostGroup']]).astype('int64')
useful_data['DiscountGroup'] = OrdinalEncoder(categories=[labels_Discount]).fit_transform(useful_data[['DiscountGroup']]).astype('int64')
useful_data['Country'] = OrdinalEncoder(categories=[['Australia', 'Spain', 'Germany', 'Netherlands', 'Sweden', 'Belgium', 'Norway',
                                                     'Italy', 'United Kingdom', 'Portugal', 'France', 
                                                     'United States']]).fit_transform(useful_data[['Country']]).astype('int64')
useful_data['Category'] = OrdinalEncoder(categories=[['Apparel', 'Electronics', 'Accessories', 'Stationery',
                                                      'Furniture']]).fit_transform(useful_data[['Category']]).astype('int64')
useful_data['Description'] = OrdinalEncoder(categories=[['White Mug', 'Headphones', 'Desk Lamp', 'Office Chair', 'USB Cable', 
                                                        'Notebook', 'Wireless Mouse', 'Blue Pen', 'Wall Clock', 'T-shirt',
                                                        'Backpack']]).fit_transform(useful_data[['Description']]).astype('int64')
useful_data['ShipmentProvider'] = OrdinalEncoder(categories=[['UPS', 'Royal Mail','DHL', 'FedEx']]).fit_transform(useful_data[['ShipmentProvider']]).astype('int64')
# useful_data['Country'] = useful_data['Country'].map(useful_data['Country'].value_counts(normalize=True))

# Параметры сглаживания
smoothing = 5  # Чем больше значение, тем больше веса общего среднего
category_stats = useful_data.groupby('PaymentMethod')['Sum'].agg(['mean', 'count'])
category_stats['smoothed_mean'] = ((category_stats['mean'] * category_stats['count'] + useful_data['Sum'].mean() * smoothing) / (category_stats['count'] + smoothing))
useful_data['PaymentMethod'] = useful_data['PaymentMethod'].map(category_stats['smoothed_mean'])


# print(useful_data)
print(useful_data.info())


# # ЛОГИСТИЧЕСКАЯ РЕГРЕССИЯ
# X = useful_data[['Country']]
# y = useful_data['ReturnStatus'].astype(np.int32)
# X_scaled = scaler.fit_transform(X)
# X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, random_state=42)

# reg_logistic_model = LogisticRegression()
# reg_logistic_model.fit(X_train, y_train)
# print(reg_logistic_model.coef_)
# y_predict = reg_logistic_model.predict(X_test)

# reg_mean_squared_error = np.sqrt(mean_squared_error(y_test, y_predict))
# reg_cross_val_score = cross_val_score(reg_logistic_model, X_scaled, y, cv=5)

# plt.figure(figsize=(13, 8))
# plt.suptitle(f"Регрессионный анализ", fontsize=20)
# plt.title(f'ЗП/Должность\n'
#           f'Коэф. детерминации R² = {r2_score(y_test, y_predict):.4f}\n'
#           f'Среднеквадр. ошибка RMSE = {reg_mean_squared_error:.4f}\n'
#           f'Перекрёстная проверка = {reg_cross_val_score}')
# plt.scatter(X_test, y_test, alpha=0.2, label='Прогноз')
# plt.plot(X_test, y_predict, 'r--', lw=3)
# plt.legend()
# plt.tight_layout()
# plt.show()


# # КЛАСТЕРИЗАЦИЯ
# X_scaled = scaler.fit_transform(useful_data[['Sum', 'ShippingCost']])

# # Определение оптимального числа кластеров
# error = []
# silhouette_scores = {}
# for i in range(2, 10):
#     kmeans = KMeans(n_clusters = i, random_state = 42, init = 'k-means++')
#     kmeans.fit(X_scaled)
#     error.append(kmeans.inertia_)

# # ГРАФИК ЛОКТЯ
# plt.title('График локтя (Sum, ShippingCost)')
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
# plt.xlabel('Sum')
# plt.ylabel('ShippingCost')
# plt.scatter(X_scaled[:, 0], X_scaled[:, 1], c=kmeans.labels_, cmap='viridis', marker='o')
# plt.scatter(kmeans.cluster_centers_[:, 0], kmeans.cluster_centers_[:, 1], s=200, c='red', marker='X', label='Centroids')
# plt.legend()
# plt.show()


# # КЛАСТЕРИЗАЦИЯ
# X_scaled = scaler.fit_transform(useful_data[['Description', 'Country']])

# # Определение оптимального числа кластеров
# error = []
# silhouette_scores = {}
# for i in range(2, 10):
#     kmeans = KMeans(n_clusters = i, random_state = 42, init = 'k-means++')
#     kmeans.fit(X_scaled)
#     error.append(kmeans.inertia_)

# # ГРАФИК ЛОКТЯ
# plt.title('График локтя (Description, Country)')
# plt.plot(range(2, 10), error, marker='o')
# plt.xlabel('Число кластеров')
# plt.ylabel('Ошибка')
# plt.tight_layout()
# plt.show()

# kmeans = KMeans(n_clusters = 4, random_state=42)
# kmeans.fit(X_scaled)
# silhouette = silhouette_score(X_scaled, kmeans.labels_)
# print(f'Оценка силуэта = {silhouette}')

# # Визуализация кластеров
# plt.figure(figsize=(10, 6))
# plt.title('Кластеризация')
# plt.xlabel('Description')
# plt.ylabel('Country')
# plt.scatter(X_scaled[:, 0], X_scaled[:, 1], c=kmeans.labels_, cmap='viridis', marker='o')
# plt.scatter(kmeans.cluster_centers_[:, 0], kmeans.cluster_centers_[:, 1], s=200, c='red', marker='X', label='Centroids')
# plt.legend()
# plt.show()


numerical_cols = ['Quantity', 'SalesChannel', 'ReturnStatus', 'OrderPriority',
                  'ShipmentProvider', 'PaymentMethod', 'UnitPriceGroup', 'ShippingCostGroup', 'DiscountGroup']

# КЛАССИФИКАЦИЯ С ИСПОЛЬЗОВАНИЕМ K-БЛИЖАЙШИХ СОСЕДЕЙ
X = useful_data[numerical_cols].drop(["ReturnStatus"], axis=1)
y = useful_data["ReturnStatus"]
X_scaled = scaler.fit_transform(X)

X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, random_state=42)
knn = KNeighborsClassifier(n_neighbors = 6, weights='distance')

# тренировка модели
knn.fit(X_train, y_train)
y_pred_cls = knn.predict(X_test)

print("Точность классификации:", accuracy_score(y_test, y_pred_cls))
print(f"F1-Score (kNN): {f1_score(y_test, y_pred_cls, average='macro'):.2f}")

# Визуализация результатов классификации
labels = ['Return', 'NoReturn']
plt.figure(figsize=(9, 7))
plt.title('Матрица ошибок классификации')
sns.heatmap(confusion_matrix(y_test, y_pred_cls), annot=True, fmt='d', cmap='Blues', xticklabels=labels, yticklabels=labels)
plt.xlabel('Предсказанный статус')
plt.ylabel('Фактический статус')
plt.tight_layout()
plt.show()

# Оцениваем точность классификации с использованием перекрестной проверки
cv_scores_cls = cross_val_score(knn, X_scaled, y, cv=5, scoring='accuracy')
print("Перекрестная проверка точности =", np.mean(cv_scores_cls))


# КЛАССИФИКАЦИЯ С ИСПОЛЬЗОВАНИЕМ K-БЛИЖАЙШИХ СОСЕДЕЙ
X = useful_data[numerical_cols].drop(["DiscountGroup"], axis=1)
y = useful_data["DiscountGroup"]
X_scaled = scaler.fit_transform(X)

X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size=0.2, random_state=42)
knn = KNeighborsClassifier(n_neighbors = 6, weights='distance')

# тренировка модели
knn.fit(X_train, y_train)
y_pred_cls = knn.predict(X_test)

print("Точность классификации:", accuracy_score(y_test, y_pred_cls))
print(f"F1-Score (kNN): {f1_score(y_test, y_pred_cls, average='macro'):.2f}")

# Визуализация результатов классификации
labels = labels_Discount
plt.figure(figsize=(9, 7))
plt.title('Матрица ошибок классификации')
sns.heatmap(confusion_matrix(y_test, y_pred_cls), annot=True, fmt='d', cmap='Blues', xticklabels=labels, yticklabels=labels)
plt.xlabel('Предсказанный статус')
plt.ylabel('Фактический статус')
plt.tight_layout()
plt.show()

# Оцениваем классификацию с использованием перекрестной проверки
cv_scores_cls = cross_val_score(knn, X_scaled, y, cv=5, scoring='accuracy')
print("Перекрестная проверка точности =", np.mean(cv_scores_cls))


# # ДИАГРАММЫ ОЦЕНКИ ПОКАЗАТЕЛЕЙ ПРОДАЖ ПО ОТДЕЛЬНЫМ КАТЕГОРИЯМ
# grouped = useful_data.groupby('Category')['Quantity'].sum().reset_index()
# plt.figure(figsize=(10, 6))
# bars = plt.barh(grouped['Category'], grouped['Quantity'], color=["#E0FBE2","#9EDF9C","#508D4E","#1A5319",'#88D66C'])
# for bar in bars:
#     width = bar.get_width()
#     plt.text(width + 10, bar.get_y() + bar.get_height() / 2, f'{width}', va='center', ha='left', color='white', fontweight='bold')
# plt.title('Total Quantity by Category')
# plt.xlabel('Quantity')
# plt.ylabel('Category')
# plt.tight_layout()
# plt.show()

# useful_data['Netprofit'] = useful_data['Sum'] - useful_data['ShippingCost']
# data_profit=useful_data['Netprofit'].sum()
# grouped_profit = useful_data.groupby('Category')['Netprofit'].sum().reset_index()
# plt.figure(figsize=(10, 6))
# bars = plt.barh(grouped_profit['Category'], grouped_profit['Netprofit'], color=["#E0FBE2","#9EDF9C","#347928","#1A5319",'#73EC8B'])
# for bar in bars:
#     width = bar.get_width()
#     plt.text(width + 10, bar.get_y() + bar.get_height() / 2, f'{width}', va='center', ha='left', color='white', fontweight='bold')
# plt.title('Total Profit by Category')
# plt.xlabel('Profit')
# plt.ylabel('Category')
# plt.tight_layout()
# plt.show()