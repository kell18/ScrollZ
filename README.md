Постапокалиптический скроллер
-----------------------------




###Тематика игры
Решили сделать нечто похожее на [Earn to Die](https://play.google.com/store/apps/details?id=com.notdoppler.earntodie) или [Zombie Road Trip](https://play.google.com/store/apps/details?id=com.noodlecake.zombieroadtrip)





###Работа с Unity
+ [Unity2D](https://unity3d.com/ru/learn/tutorials/modules/beginner/2d/2d-overview)
+ [Unity3D и 3D Max](http://www.youtube.com/user/4GameFree)





###3D Модели
+ [Как можно просто наделать моделек зданий](http://www.youtube.com/watch?v=A8e1zHEgdI8)




###Работа с репой
#####Клонирование репозитория на комп
1. В командной строке в том месте где хотим разместить проект                              
`git clone https://github.com/bk0606/PostapocalypticScroller.git`
2. Обязательно проверьте чтобы в локальном проекте был файл .gitignore (если его там нет - создать ручками и содержание вставить из файла на github.com)

#####Коммит локальных изменений на github
1. Добавляем (индексируем) файлы которые мы изменяли: `git add .`
2. Коммитим изменения локально: `git commit -m "Сообщение коммита"`
*Сообщение коммита заменить на сообщение о том, что закомитили (кратко и понятно)*
3. Стягиваем изменения с github: `git fetch origin` затем `git pull`
4. Проверяем что всё работает *(как минимум должна запускаться игра)*
5. Пушим файлы на github: `git push`


#####Получение последней версии с github
1. Получаем изменения в удалённой репе: `git fetch origin` 
2. Применяем эти изменения к своей репе: `git pull`

