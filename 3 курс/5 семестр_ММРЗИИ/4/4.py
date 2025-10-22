"""
    Без понимания теории эту лабу не сделать.
    Более того, сейчас таким образом её пишу я.
    В коде много букав, чат-кпт-8 чем только не срёт, толку мало будет,
    а самому гороздить не фонтан. Делайте как душе угодно.
"""

import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns

from sklearn.model_selection import train_test_split, cross_val_score
from sklearn.preprocessing import StandardScaler, LabelEncoder
from sklearn.linear_model import LinearRegression
from sklearn.metrics import r2_score,  accuracy_score, f1_score, confusion_matrix, mean_squared_error
from sklearn.cluster import KMeans
from sklearn.neighbors import KNeighborsClassifier
from sklearn.decomposition import PCA

# Настройка отображения в консоли
pd.set_option('display.max_columns', None)      # Показывать все столбцы
pd.set_option('display.max_rows', None)         # Показывать все строки
pd.set_option('display.width', 1000)            # Увеличить ширину вывода
pd.set_option('display.max_colwidth', None)     # Максимальная ширина столбцов
pd.set_option('display.expand_frame_repr', False) # Не переносить строки
pd.set_option('colheader_justify', 'center')    # Выравнивание по центру

excel_data = pd.read_excel('data.xlsx', sheet_name='данные')

print("\nАнализ данных:")
print(excel_data.info())
print("\nПервые 5 записей:")
print(excel_data.head())
print("\nДемографический анализ")
print("Распределение по полу:")
print(excel_data["Пол"].value_counts(normalize=True).mul(100).round(1))
print("\nРаспределение по должности:")
print(excel_data["Должность"].value_counts(normalize=True).mul(100).round(1))

# создание нового признака - возрастная группа
excel_data['Возрастная группа'] = pd.cut(excel_data['Возраст'], bins=[-np.inf, 25, 30, 35, 45, 55, np.inf],
                                labels=['-25','26 - 30','31 - 35','36 - 45','46 - 55','56+'])
print("\nРаспределение по возрасту (в %):")
print(excel_data['Возрастная группа'].value_counts(normalize=True).mul(100).round(1))
excel_data_encoded = excel_data.copy()

scaler = StandardScaler()

# замена категориальных переменных на их численное представление
labelencoder = LabelEncoder()
categorical_cols = ['Пол', 'Должность', 'Значимые ценности', 'Образование',	"Готовность остаться", 'Возрастная группа']
for i in categorical_cols:
    excel_data_encoded[i] = labelencoder.fit_transform(excel_data[i])

# подготовка данных для регрессии
SALARY = scaler.fit_transform(pd.DataFrame(excel_data_encoded['ЗП']))
POSITION = scaler.fit_transform(pd.DataFrame(excel_data_encoded['Должность']))
EDUCATION = scaler.fit_transform(pd.DataFrame(excel_data_encoded['Образование']))
AGE_GROUP = scaler.fit_transform(pd.DataFrame(excel_data_encoded['Возрастная группа']))
STAY = scaler.fit_transform(pd.DataFrame(excel_data_encoded["Готовность остаться"]))
DURATION_WORK = scaler.fit_transform(pd.DataFrame(excel_data_encoded['Длительность работы']))


# Анализируем корреляции между числовыми признаками
correlation_matrix = excel_data_encoded.corr()
plt.figure(figsize=(10, 8))
sns.heatmap(correlation_matrix, annot=True, vmin=-1, vmax=1, cmap='magma', fmt='.2f')
plt.title('Корреляционная матрица признаков')
plt.tight_layout()
plt.show()


# РЕГРЕССИОННЫЙ АНАЛИЗ
plt.figure(figsize=(13, 8))
plt.suptitle(f"Регрессионный анализ", fontsize=20)
MODEL = LinearRegression()

# зарплата/должность
MODEL.fit(POSITION, SALARY)
SAL_POS_REGR_PREDICTION = MODEL.predict(POSITION)
print(MODEL.coef_)
plt.subplot(2, 3, 1)
plt.title(f'ЗП/Должность\n'
          f'Коэф. детерминации R² = {r2_score(SALARY, SAL_POS_REGR_PREDICTION):.4f}\n'
          f'Среднеквадр. ошибка RMSE = {np.sqrt(mean_squared_error(SALARY, SAL_POS_REGR_PREDICTION)):.4f}\n'
          f'Перекрёст. пров. R² (средняя) = {np.mean(cross_val_score(MODEL, POSITION, SALARY, cv=5, scoring="r2")):.4f}')
plt.ylabel('Фактическая зарплата')
plt.xlabel('Должность')
plt.scatter(POSITION, SALARY, alpha=0.4, label='Прогноз')
plt.plot(POSITION, SAL_POS_REGR_PREDICTION)
plt.legend()

# зарплата/образование
MODEL.fit(EDUCATION, SALARY)
SAL_EDUCATION_REGR_PREDICTION = MODEL.predict(EDUCATION)
plt.subplot(2, 3, 2)
plt.title(f'ЗП/Образование\nR² = {r2_score(SALARY, SAL_EDUCATION_REGR_PREDICTION):.4f}\n'
          f'RMSE = {np.sqrt(mean_squared_error(SALARY, SAL_EDUCATION_REGR_PREDICTION)):.4f}\n'
          f'Перекрёст. пров. R² (средняя) = {np.mean(cross_val_score(MODEL, EDUCATION, SALARY, cv=5, scoring="r2")):.4f}')
plt.ylabel('Фактическая зарплата')
plt.xlabel('Образование')
plt.scatter(EDUCATION, SALARY, alpha=0.4, label='Прогноз')
plt.plot(EDUCATION, SAL_EDUCATION_REGR_PREDICTION)
plt.legend()

# зарплата/возрастная группа
MODEL.fit(AGE_GROUP, SALARY)
SAL_AGE_GROUP_REGR_PREDICTION = MODEL.predict(AGE_GROUP)
plt.subplot(2, 3, 3)
plt.title(f'ЗП/Возрастная группа\nR² = {r2_score(SALARY, SAL_AGE_GROUP_REGR_PREDICTION):.4f}\n'
          f'RMSE = {np.sqrt(mean_squared_error(SALARY, SAL_AGE_GROUP_REGR_PREDICTION)):.4f}\n'
          f'Перекрёст. пров. R² (средняя) = {np.mean(cross_val_score(MODEL, AGE_GROUP, SALARY, cv=5, scoring="r2")):.4f}')
plt.ylabel('Фактическая зарплата')
plt.xlabel('Возрастная группа')
plt.scatter(AGE_GROUP, SALARY, alpha=0.4, label='Прогноз')
plt.plot(AGE_GROUP, SAL_AGE_GROUP_REGR_PREDICTION)
plt.legend()

# готовность остаться/возрастная группа
MODEL.fit(STAY, AGE_GROUP)
STAY_AGE_GROUP_REGR_PREDICTION = MODEL.predict(STAY)
plt.subplot(2, 3, 4)
plt.title(f'Готовность остаться/Возрастная группа\n'
          f'R² = {r2_score(AGE_GROUP, STAY_AGE_GROUP_REGR_PREDICTION):.4f}\n'
          f'RMSE = {np.sqrt(mean_squared_error(AGE_GROUP, STAY_AGE_GROUP_REGR_PREDICTION)):.4f}\n'
          f'Перекрёст. пров. R² (средняя) = {np.mean(cross_val_score(MODEL, STAY, AGE_GROUP, cv=5, scoring="r2")):.4f}')
plt.xlabel("Готовность остаться")
plt.ylabel('Возрастная группа')
plt.scatter(STAY, AGE_GROUP, alpha=0.1, label='Прогноз')
plt.plot(STAY, STAY_AGE_GROUP_REGR_PREDICTION)
plt.legend()

# готовность остаться/Остальные признаки
MODEL.fit(STAY, excel_data_encoded.drop(["Готовность остаться"], axis=1))
STAY_OTHER_REGR_PREDICTION = MODEL.predict(STAY)
plt.subplot(2, 3, 5)
plt.title(f'Готовность остаться/Остальные признаки\n'
          f'R² = {r2_score(excel_data_encoded.drop(["Готовность остаться"], axis=1), STAY_OTHER_REGR_PREDICTION):.4f}'
          f'\nRMSE = {np.sqrt(mean_squared_error(excel_data_encoded.drop(["Готовность остаться"], axis=1), STAY_OTHER_REGR_PREDICTION)):.4f}\n'
          f'Перекрёст. пров. R² (средняя) = {np.mean(cross_val_score(MODEL, STAY, excel_data_encoded.drop(["Готовность остаться"], axis=1), cv=5, scoring="r2")):.4f}')
plt.xlabel("Готовность остаться")
plt.ylabel('Остальные признаки')
plt.scatter(STAY, SALARY, alpha=0.1, label='Прогноз')
plt.plot(STAY, STAY_OTHER_REGR_PREDICTION)
plt.legend()

# готовность остаться/продолжительность работы
MODEL.fit(STAY, DURATION_WORK)
STAY_DURATION_WORK_REGR_PREDICTION = MODEL.predict(STAY)
plt.subplot(2, 3, 6)
plt.title(f'Готовность остаться/\nПродолжительность работы\n'
          f'R² = {r2_score(DURATION_WORK, STAY_DURATION_WORK_REGR_PREDICTION):.4f}'
          f'\nRMSE = {np.sqrt(mean_squared_error(DURATION_WORK, STAY_DURATION_WORK_REGR_PREDICTION)):.4f}\n'
          f'Перекрёст. пров. R² (средняя) = {np.mean(cross_val_score(MODEL, STAY, DURATION_WORK, cv=5, scoring="r2")):.4f}')
plt.xlabel("Готовность остаться")
plt.ylabel('Продолжительность работы')
plt.scatter(STAY, SALARY, alpha=0.1, label='Прогноз')
plt.plot(STAY, STAY_DURATION_WORK_REGR_PREDICTION)
plt.legend()

plt.tight_layout()
plt.show()


# КЛАСТЕРИЗАЦИЯ KNN
"""
    Кластеризация — это набор методов без учителя для группировки данных по определённым критериям
    в так называемые кластеры, что позволяет выявлять сходства и различия между объектами,
    а также упрощать их анализ и визуализацию.
    Из-за частичного сходства в постановке задач с классификацией
    кластеризацию ещё называют unsupervised classification.
"""
# выкидываем столбцы, содержащие малое (или вообще не содержащее)
# количество повторяющихся значений (они мешают поиску закономерностей)
# unique должен быть как можно меньше
print("\nОписательная статистика:")
print(excel_data.describe(include = 'O'))
CLUST = excel_data_encoded.drop(columns=['ЗП'])

# Определение оптимального числа кластеров
error = []
silhouette_scores = {}
for i in range(2, 10):
    kmeans = KMeans(n_clusters = i, random_state = 42, init = 'k-means++')
    kmeans.fit(CLUST)
    error.append(kmeans.inertia_)

result_kmeans = KMeans(n_clusters=3, random_state=42, init='k-means++')
excel_data_encoded['Кластер'] = result_kmeans.fit_predict(CLUST)

plt.figure(figsize=(12, 6))
plt.suptitle(f"Кластеризация (исключая 'ЗП')", fontsize=20)
# График локтя
plt.subplot(1, 2, 1)
plt.title('График локтя')
plt.plot(range(2, 10), error, marker='o')
plt.xlabel('Число кластеров')
plt.ylabel('Ошибка')
plt.legend()

# график кластеризации
plt.subplot(1, 2, 2)
plt.title('Кластеризация сотрудников')
plt.scatter(excel_data_encoded.iloc[:, 0], excel_data_encoded.iloc[:, 1], c = excel_data_encoded['Кластер'], cmap='viridis')
plt.scatter(result_kmeans.cluster_centers_[:, 0], result_kmeans.cluster_centers_[:, 1], marker="x", color="black", s=100)
plt.xlabel("Возраст")
plt.ylabel("Длительность работы")
plt.legend()
plt.tight_layout()
plt.show()


# КЛАССИФИКАЦИЯ K-БЛИЖАЙШИХ СОСЕДЕЙ (KNN)
"""
    Этот метод использует концепцию близости, которая также важна в методах кластеризации.
    Эта концепция заключается в том, что объекты с одним и тем же классом (категорией)
    находятся близко друг к другу (в пространстве представлений).
    Это сходство измеряется с помощью функций расстояния, таких как ЕВКЛИДОВО расстояние.

    Если рассматриваемый объект находится рядом с другими объектами определенного класса,
    то рассматриваемому объекту присваивается этот класс.
    Этот метод классификации является основой для метода кластеризации К-средних.

    Классификация — это разделение данных на классы на основе размеченных данных (С УЧИТЕЛЕМ).
    Сначала учитель как бы говорит, к какому классу относятся различные объекты,
    а потом ученик должен сказать, к какому классу относятся новые объекты, которые учитель не показывал.
    Нужно сказать ученику, на основе скольких близко похожих объектов
    нужно классифицировать новый объект (на основе k близко похожих).
"""

# import warnings
# warnings.filterwarnings("ignore")

# Классификация с использованием K-ближайших соседей
X_class = excel_data_encoded.drop(["Готовность остаться"], axis=1).astype('float64')
y_class = excel_data_encoded["Готовность остаться"]

X_scaled = scaler.fit_transform(X_class)

X_train, X_test, y_train, y_test = train_test_split(X_scaled, y_class, test_size=0.3, random_state=42)
knn = KNeighborsClassifier(n_neighbors = 5, weights='distance')

# тренировка модели
knn.fit(X_train, y_train)
y_pred_cls = knn.predict(X_test)

print("Матрица ошибок:\n", confusion_matrix(y_test, y_pred_cls))
print("Точность классификации:", accuracy_score(y_test, y_pred_cls))
print(f"F1-Score (kNN): {f1_score(y_test, y_pred_cls, average='macro'):.2f}")

# Визуализация результатов классификации
colors = ['#FF9999', '#66B2FF']
labels = ['Не готов остаться', 'Возможно останется','Готов остаться']

# Матрица ошибок с улучшенной визуализацией
plt.figure(figsize=(9, 7))
plt.title('Матрица ошибок классификации')
sns.heatmap(confusion_matrix(y_test, y_pred_cls), annot=True, fmt='d', cmap='Blues', xticklabels=labels, yticklabels=labels)
plt.xlabel('Предсказанный статус')
plt.ylabel('Фактический статус')
plt.tight_layout()
plt.show()


# Оцениваем классификацию с использованием перекрестной проверки
cv_scores_cls = cross_val_score(knn, X_scaled, y_class, cv=5, scoring='accuracy')
print("Перекрестная проверка точности (средняя):", np.mean(cv_scores_cls))


print("1. Кластерный анализ выявил", len(np.unique(result_kmeans.labels_)), "основных групп сотрудников")
print("2. Точность прогноза готовности к переезду:", round(np.mean(cv_scores_cls) * 100, 1), "%")