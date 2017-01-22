# -*- coding: utf-8 -*-
# Туториал: http://scikit-learn.org/stable/tutorial/text_analytics/working_with_text_data.html

import codecs
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.feature_extraction.text import TfidfTransformer
from sklearn.naive_bayes import MultinomialNB

types = ['science', 'style', 'culture', 'life', 'economics', 'business', 'travel', 'forces', 'media', 'sport']

t_types, data = [], []

# Загрузка пар "тема - слова"
for line in codecs.open("news/news_train.txt", encoding="utf-8"):
    t_type, t_data = line.split('\t', 1)
    t_types.append(types.index(t_type))
    data.append(t_data)

# Построение модели
count_vect = CountVectorizer()
tfidf_transformer = TfidfTransformer()

X_train_counts = count_vect.fit_transform(data)
X_train_tfidf = tfidf_transformer.fit_transform(X_train_counts)

# Обучение
clf = MultinomialNB().fit(X_train_tfidf, t_types)

docs_new = []
for line in codecs.open("news/news_test.txt", encoding="utf-8"):
    docs_new.append(line)
X_new_counts = count_vect.transform(docs_new)
X_new_tfidf = tfidf_transformer.transform(X_new_counts)

# Предсказание
predicted = clf.predict(X_new_tfidf)

file = codecs.open('news/news_output.txt', 'w', encoding="utf-8")
for i in predicted:
    file.write(types[i] + '\n')
file.close()