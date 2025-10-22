# Python
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from sklearn.metrics import classification_report

# Загрузка данных
df = pd.read_csv('data.csv')

# Предобработка данных
X = df.drop('success', axis=1)
y = df['success']

# Разделение на обучающую и тестовую выборки
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2)

# Обучение модели случайного леса
model = RandomForestClassifier()
model.fit(X_train, y_train)

# Предсказание
y_pred = model.predict(X_test)

# Оценка модели
print(classification_report(y_test, y_pred))