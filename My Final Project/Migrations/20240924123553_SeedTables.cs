using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Final_Project.Migrations
{
    /// <inheritdoc />
    public partial class SeedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        INSERT INTO Countries (Country_Name, Flag_Image)
        VALUES
            (N'США', N'/Flags/948778682.jpg'),
            (N'Великобритания', N'/Flags/53e87dc1874e487d38eeba168d8ff54e.png'),
            (N'Германия', N'/Flags/47004-0.png'),
            (N'Франция', N'/Flags/wallpaper.png'),
            (N'Испания', N'/Flags/klmklklk.jpg'),
            (N'Италия', N'/Flags/флаг_италия.png'),
            (N'Канада', N'/Flags/Flag_of_Canada_(1964).png'),
            (N'Россия', N'/Flags/53e87dc1874e487d38eeba168d8ff54e.png'),
            (N'Австралия', N'/Flags/Флаг_России_(1).jpg'),
            (N'Сингапур', N'/Flags/сингапур.png'),
            (N'Ирландия', N'/Flags/Fin_Flash_of_Ireland.png');



        INSERT INTO Cities (Country_ID, City_Name)
        VALUES
            (1, N'Нью-Йорк'),
            (1, N'Лос-Анджелес'),
            (2, N'Лондон'),
            (4, N'Париж'),
            (4, N'Марсель'),
            (5, N'Мадрид'),
            (5, N'Барселона'),
            (6, N'Рим'),
            (6, N'Флоренция'),
	        (8, N'Москва'),
	        (8, N'Санкт-Питербург'),
	        (7, N'Кэмпбелл-Ривер'),
	        (7, N'Оттава'),
	        (2, N'Уэльс'),
	        (2, N'Лондон'),
	        (9, N'Перт'),
	        (9, N'Канберра'),
	        (11, N'Дублин'),
	        (11, N'Корк');

        INSERT INTO Directors (Full_Name)
        VALUES
            (N'Стивен Спилберг'),
            (N'Кристофер Нолан'),
	        (N'Крис Коламбус'),
            (N'Гильермо дель Торо'),
	        (N'Кольдо Серра'),
	        (N'Хесус Кольменар'),
	        (N'Алехандро Бассано'),
	        (N'Мигель Анхель Вивас'),
	        (N'Алекс Родриго'),
	        (N'Альберт Пинто'),
	        (N'Хавьер Кинтас');

        INSERT INTO Genres (Genre_Name)
        VALUES
            (N'Экшн'),
            (N'Драма'),
            (N'Фэнтези'),
            (N'Научная фантастика'),
            (N'Триллер'),
	        (N'Военный'),
	        (N'Криминал'),
	        (N'Детектив'),
	        (N'Семейный'),
	        (N'Приключения');


        INSERT INTO Actors (Full_Name, Country_ID)
        VALUES
        (N'Том Хэнкс', 1),
        (N'Мэтт Дэймон', 1),
        (N'Вин Дизель', 1),
        (N'Том Сайзмор', 1),
        (N'Эдвард Бёрнс', 1),
        (N'Барри Пеппер', 7),
        (N'Джованни Рибизи', 1),
        (N'Адам Голдберг', 1),
        (N'Джереми Дэвис', 7),
        (N'Райан Херст', 1),
        (N'Натан Филлион', 7),
        (N'Кристиан Бэйл', 2),
        (N'Хит Леджер', 9),
        (N'Аарон Экхарт', 1),
        (N'Мэгги Джилленхол', 1),
        (N'Гэри Олдман', 2),
        (N'Майкл Кейн', 2),
        (N'Морган Фрименч', 1),
        (N'Чинь Хань', 10),
        (N'Нестор Карбонелл', 1),
        (N'Эрик Робертс', 1),
        (N'Дэниэл Рэдклифф', 1),
        (N'Руперт Гринт', 2),
        (N'Эмма Уотсон', 2),
        (N'Ричард Харрис', 11),
        (N'Алан Рикман', 2),
        (N'Ивана Бакеро', 5),
        (N'Серхи Лопес', 5),
        (N'Урсула Корберо', 5),
        (N'Альба Флорес', 5),
        (N'Альваро Морте', 5),
        (N'Педро Алонсо', 5),
        (N'Мигель Эрран', 5),
        (N'Фернандо Сото', 5);


        INSERT INTO Content_Type (Content_Type_Name)
        VALUES
            (N'Фильм'),
            (N'Сериал');


        INSERT INTO Movies (Country_ID, Content_Type_ID, Title, Start_Date, End_Date, Date_Added, Currency, Price, Cover_Image_URL, Short_Description, Released_Episodes, Total_Episodes, IsFinished)
        VALUES
            (1, 1, N'Спасти рядового Райана', N'1998-01-01', N'1998-12-31', N'2020-01-01', N'$', 70000000, N'/Posters/spasti_ryadovogo_rayana.jpg', N'Описание фильма 1', 0, 1, 0), 
            (1, 1, N'Темный рыцарь', N'2008-01-01', N'2008-12-31', N'2021-01-01', N'$', 185000000, N'/Posters/batman-2020-dark-knight-uz-2560x1600.jpg', N'Описание фильма 2', 0, 1, 0),
            (1, 1, N'Гарри Поттер и Философский камень', N'2001-01-01', N'2001-12-31', N'2021-01-01', N'$', 50000000, N'/Posters/1680152230_papik-pro-p-poster-garri-potter-i-filosofskii-kamen-23.jpg', N'Описание фильма 3', 0, 1, 0),
            (5, 1, N'Пан Лабиринт', N'2006-01-01', N'2006-12-31', N'2021-02-01', N'$', 19000000, N'/Posters/p1_2633217_49c33702.jpg', N'Описание фильма 4', 0, 1, 0),
            (5, 2, N'Бумажный дом', N'2017-01-01', N'2023-12-31', N'2023-01-01', N'$', 5000000, N'/Posters/1630688152_bumazhnyj-dom-0.jpeg', N'Группа преступников, носивших маски с изображениями Сальвадора Дали, захватывает Испанский Королевский Монетный двор, где они планируют ограбить 2,4 млрд евро и взять заложников.', 0, 13, 0),
            (5, 2, N'Бумажный дом (Второй сезон)', N'2017-01-01', N'2023-12-31', N'2023-01-01', N'$', 5000000, N'/Posters/1630688152_bumazhnyj-dom-0.jpeg', N'Продолжение истории группы преступников, пытающихся разделить деньги после успешного ограбления.', 0, 9, 0);



        INSERT INTO Episodes (Movie_ID, Episode_Number, Duration, Title, Short_Description, Release_Date, File_URL)
        VALUES
        (1, 1, N'2 ч 49 мин', N'Фильм', N'Четверо братьев Райанов участвуют на различных фронтах Второй мировой войны. Двое из них гибнут ...', N'1998-07-24', N'/Episodes/Spasti_ryadovogo_Rajana-2056_(anwap.org).mp4'),
        (2, 1, N'2 ч 32 мин', N'Фильм', N'История о Брюсе Уэйне, который борется с преступностью в Готэме в образе Бэтмена. Прокурор Харви Дент обещает избавить город от бандитов, но появляется новый враг - Джокер, сеющий хаос и панику.', N'2008-07-18', N'/Episodes/Byetmen_Temnyj_rycarq-2015_(anwap.org).mp4'),
        (3, 1, N'2 ч 32 мин', N'Фильм ""Гарри Поттер и Философский камень""', N'Юный Гарри Поттер обнаруживает свою магическую силу и попадает в школу волшебства Хогвартс. Он начинает увлекательное путешествие по миру магии и свершает невероятные приключения.', N'2001-01-01', N'/Episodes/harry_potter_philosophers_stone.mp4'),
        (4, 1, N'1 ч 52 мин', N'Фильм ""Пан Лабиринт""', N'Во время Гражданской войны в Испании в 1944 году девочка Офелия попадает в таинственный лабиринт, где ей предстоит пройти опасные испытания.', N'2006-02-01', N'/Episodes/pan_labyrinth.mp4'),
        (5, 1, N'42–76 минут', N'Первая серия', N'Группа преступников начинает реализацию своего плана по захвату Испанского Королевского Монетного двора.', N'2017-05-02', N'/Episodes/[anwap.space]_Бумажный дом_[LostFilm]_S1_E1_[720p].mp4'),
        (5, 2, N'42–76 минут', N'Вторая серия', N'Преступники продолжают свой план, а полиция начинает давить на них.', N'2017-05-09', N'/Episodes/[anwap.space]_Бумажный дом_[LostFilm]_S1_E2_[720p].mp4'),
        (5, 3, N'42–76 минут', N'Третья серия', N'Преступники создают хаос, подрывая внутренние системы монетного двора.', N'2017-05-16', N'/Episodes/[anwap.space]_Бумажный дом_[LostFilm]_S1_E3_[720p].mp4'),

            -- Серии второго сезона
        (6, 1, N'42–76 минут', N'Первая серия', N'Группа преступников продолжает свой план, находясь внутри монетного двора.', N'2019-07-19', N'/Episodes/[anwap.space]_Бумажный дом_[LostFilm]_S2_E1_[720p].mp4'),
        (6, 2, N'42–76 минут', N'Вторая серия', N'Преступники сталкиваются с новыми преградами, а полиция продолжает борьбу с ними.', N'2019-07-26', N'/Episodes/[anwap.space]_Бумажный дом_[LostFilm]_S2_E2_[720p].mp4'),
        (6, 3, N'42–76 минут', N'Третья серия', N'Напряжение внутри монетного двора и среди преступников нарастает.', N'2019-08-02', N'/Episodes/[anwap.space]_Бумажный дом_[LostFilm]_S2_E3_[720p].mp4');



        INSERT INTO Announcements (Movie_ID, Title, Short_Description, Announcement_Date, Trailer_URL)
        VALUES
            (1, N'Новость о фильме', N'Один фильм, одно призвание, одна миссия!', Null, N'\Trailers\Saving Private Ryan.mp4'),
            (2, N'Новость о сериале', N'Погрузитесь в мир темных героев и нерешенных загадок.', Null, N'\Trailers\The Dark Knight.mp4');


	
        INSERT INTO Movie_Director (Movie_ID, Director_ID)
        VALUES
            (1, 1),
            (2, 2),
	        (3, 3),
            (4, 4),
	        (5, 5),
	        (5, 6),
	        (5, 7),
	        (5, 8),
	        (5, 9),
	        (5, 10),
	        (5, 11),
	        (6, 6),
	        (6, 7),
	        (6, 8),
	        (6, 9),
	        (6, 10),
	        (6, 11);

        INSERT INTO Genre_Movie (Movie_ID, Genre_ID)
        VALUES
            (1, 1),
            (1, 2),
            (1, 6),
            (2, 1),
            (2, 5),
	        (2, 2),
	        (2, 7),
	        (2, 3),
	        (4, 3),
	        (4, 2),
	        (4, 6),
	        (3, 3),
	        (3, 9),
	        (3, 10),
	        (5, 1),
	        (5, 2),
	        (5, 7),
	        (5, 8),
	        (6, 5),
	        (6, 1),
	        (6, 2),
	        (6, 7),
	        (6, 8);

        INSERT INTO Movie_Actor (Movie_ID, Actor_ID)
        VALUES
        (1, 1),
        (1, 2),
        (1, 3),
        (1, 4),
        (1, 5),
        (1, 6),
        (1, 7),
        (1, 8),
        (1, 9),
        (1, 10),
        (1, 11),
        (2, 12),
        (2, 13),
        (2, 14),
        (2, 15),
        (2, 16),
        (2, 17),
        (2, 18),
        (2, 19),
        (2, 20),
        (2, 21),
        (3, 22),
        (3, 23),
        (3, 24),
        (3, 25),
        (3, 26),
        (4, 27),
        (4, 28),
        (5, 29),
        (5, 30),
        (5, 31),
        (5, 32),
        (5, 33),
        (5, 34),
        (6, 29),
        (6, 30),
        (6, 31),
        (6, 32),
        (6, 33),
        (6, 34);

        INSERT INTO Downloads (Movie_ID, User_ID, Download_Date)
        VALUES
            (1, N'80b27df5-1519-4177-b7d3-aae75f58e655', GETDATE()), 
            (1, N'f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', GETDATE());


        INSERT INTO Ratings (Movie_ID, User_ID, Rating_Count, Rating_Date)
        VALUES
            (1, N'80b27df5-1519-4177-b7d3-aae75f58e655', 4, GETDATE()),
            (3, N'f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', 4.9, GETDATE());

        INSERT INTO Reviews (Movie_ID, User_ID, Likes, Review_Text, Publication_Date)
        VALUES
            (4, N'80b27df5-1519-4177-b7d3-aae75f58e655', 4, N'Отличный фильм!', GETDATE()),
            (5, N'f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', 5, N'Лучший фильм всех времен!', GETDATE());

        INSERT INTO Comments (Review_ID, Parent_Comment_ID, Movie_ID, Likes, User_ID, Comment_Text, Publication_Date)
        VALUES
            (1, NULL, 1, 1, N'80b27df5-1519-4177-b7d3-aae75f58e655', N'Согласен с отзывом!', GETDATE()),
            (1, 1, 1, 2, N'f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', N'Спасибо за рекомендацию!', GETDATE());


        INSERT INTO Favorites (User_ID, Movie_ID)
        VALUES
            (N'80b27df5-1519-4177-b7d3-aae75f58e655', 2),
	        (N'80b27df5-1519-4177-b7d3-aae75f58e655', 5),
            (N'f1f118bd-76c4-4ccc-bd63-5986dc9c65ac', 1);

        INSERT INTO Tags (Tag_Name) 
        VALUES
            (N'#экшн'),
            (N'#драма'),
            (N'#фэнтези'),
            (N'#научнаяФантастика'),
            (N'#триллер'),
	        (N'#военный'),
	        (N'#криминал'),
	        (N'#бетмен'),
	        (N'#джокер'),
	        (N'#магия'),
	        (N'#гарриПоттер'),
	        (N'#лабиринт'),
	        (N'#лабиринтФавна'),
	        (N'#детектив'),
	        (N'#бумажный'),
	        (N'#дом');



        INSERT INTO Movie_Tag (Movie_ID, Tag_ID)
        VALUES
        (1, 2),
        (1, 6),
        (2, 1),
        (2, 2),
        (2, 5),
        (2, 7),
        (2, 8),
        (2, 9),
        (3, 3),
        (3, 10),
        (3, 11),
        (4, 5),
        (4, 12),
        (4, 13),
        (5, 14),
        (5, 15),
        (5, 16);

        INSERT INTO Movie_Fragments (Movie_ID, Image_URL)
        VALUES
            (1, N'/Fragments/67747.jpg'),
            (1, N'/Fragments/MV5BMzAyZmE4NTYtYmQ5ZC00NjFmLWJhMzQtYzNmZjk4NDA5OTFhXkEyXkFqcGdeQXVyMjMzMDI4MjQ@._V1_.jpg'),
            (1, N'/Fragments/scale_1200.jpg'),
  
            (2, N'/Fragments/MV5BNDE4OTgzYzMtMTkyNS00MzBhLTliMTctOGI4NGMxYWQxMzM2XkEyXkFqcGdeQXVyNDIyNjA2MTk@._V1_.jpg'),
            (2, N'/Fragments/w1500_87689.jpg'),
            (2, N'/Fragments/w1500_48871465.jpg'),
  
            (3, N'/Fragments/2023-08-06_135912-1.jpg'),
            (3, N'/Fragments/Goblin_WB_F1_GoblinFacesHarry_Still_100615_Land.jpg'),
            (3, N'/Fragments/Harry-Potter-and-the-Philosopher-s-Stone-rupert-grint-27550920-1280-544.jpg'),
  
            (4, N'/Fragments/z_39d1f6fb.jpg'),
            (4, N'/Fragments/labyrinth179.jpg'),
            (4, N'/Fragments/panslabyrinth2.jpg'),
  
            (5, N'/Fragments/1679685703_mykaleidoscope-ru-p-bumazhnii-dom-elison-parker-oboi-35.jpg'),
            (5, N'/Fragments/1679969153_mykaleidoscope-ru-p-oslo-bumazhnii-dom-oboi-54.jpg'),
            (5, N'/Fragments/48464115056c9b3ad88f72b51f2d3653.jpg');

        -- ""Спасти рядового Райана"" (Movie_ID = 1)
        INSERT INTO Roles_Actors (Role_Name, Actor_ID, Movie_ID, Actor_Photo_URL) 
        VALUES
        --https://dzen.ru/a/ZMhrpwJgbSSTbakx
         (N'капитан Джон Миллер', 1, 1, N'/Actor Photo/6f5986b869fffdc1c2362e961283c0a8.png'),
         (N'рядовой Джеймс Райан', 2, 1, N'/Actor Photo/matt-damon-wallpapers-69037-1569100-8554533.png'),
         (N'рядовой Адриан Капарзо', 3, 1, N'/Actor Photo/scale_2400.jpg'),
         (N'сержант Майк Хорват', 4, 1, N'/Actor Photo/tom-sizemore.jpg'),
         (N'рядовой Ричард Рейбен', 5, 1, N'/Actor Photo/scale_1200.jpg'),
         (N'рядовой Дэниел Джексон', 6, 1, N'/Actor Photo/scale_1200 (1).jpg'),
         (N'Ирвин Уэйд', 7, 1, N'/Actor Photo/scale_1200 (2).jpg'),
         (N'рядовой Стэнли Меллиш', 8, 1, N'/Actor Photo/scale_1200 (3).jpg'),
         (N'капрал Тимоти Апхэм', 9, 1, N'/Actor Photo/scale_1200 (4).jpg'),
         (N'Парашютист Майклсон', 10, 1, N'/Actor Photo/scale_2400 (1).jpg'),
         (N'Миннесота Райан', 11, 1, N'/Actor Photo/scale_1200 (5).jpg');


        -- ""Темный рыцарь"" (Movie_ID = 2)
        INSERT INTO Roles_Actors (Role_Name, Actor_ID, Movie_ID, Actor_Photo_URL) 
        VALUES
         (N'Брюс Уэйн / Бэтмен', 12, 2, N'/Actor Photo/0000288.jpg'),
         (N'Джокер', 13, 2, N'/Actor Photo/1456078-2250995.jpeg'),
         (N'Харви Дент', 14, 2, N'/Actor Photo/1172.jpg'),
         (N'Рэйчел', 15, 2, N'/Actor Photo/264585.jpg'),
         (N'Джим Гордон', 16, 2, N'/Actor Photo/198.jpg'),
         (N'Альфред Пенниворт', 17, 2, N'/Actor Photo/198.jpg'),
         (N'Люциус Фокс', 18, 2, N'/Actor Photo/151.jpg'),
         (N'Лау', 19, 2, N'/Actor Photo/1300093.jpg'),
         (N'мэр', 20, 2, N'/Actor Photo/65987.jpg'),
         (N'Марони', 21, 2, N'/Actor Photo/5895.jpg');

        -- ""Гарри Поттер и Философский камень"" (Movie_ID = 3)
        INSERT INTO Roles_Actors (Role_Name, Actor_ID, Movie_ID, Actor_Photo_URL)
        VALUES
            (N'Гарри Поттер', 22, 3, N'/Actor Photo/529502.jpg'),
            (N'Рон Уинли', 23, 3, N'/Actor Photo/258546.jpg'),
            (N'Гермиона Грейнджер', 24, 3, N'/Actor Photo/685215.png'),
            (N'Северус Снегг', 25, 3, N'/Actor Photo/614.jpg'),
            (N'Альбус Дамблдор', 26, 3, N'/Actor Photo/w600_1320.jpg');

        -- ""Пан Лабиринт"" (Movie_ID = 4)
        INSERT INTO Roles_Actors (Role_Name, Actor_ID, Movie_ID, Actor_Photo_URL)
        VALUES
            (N'Офелия', 27, 4, N'/Actor Photo/1456259-1206572.jpg'),
            (N'Раул', 28, 4, N'/Actor Photo/0530365.jpg');

        -- ""Бумажный дом"" (Movie_ID = 5)
        INSERT INTO Roles_Actors (Role_Name, Actor_ID, Movie_ID, Actor_Photo_URL) 
        VALUES
            (N'Токио', 29, 5, N'/Actor Photo/1425200.jpg'),
            (N'Найроби', 30, 5, N'/Actor Photo/1138901.jpg'),
            (N'Профессор', 31, 5, N'/Actor Photo/1022853.jpg'),
            (N'Берлин', 32, 5, N'/Actor Photo/18298.jpg'),
            (N'Рио', 33, 5, N'/Actor Photo/3092724.png'),
            (N'Ангел', 34, 5, N'/Actor Photo/611344.jpg');
	
        INSERT INTO Awards (Movie_ID, Award_Name, Award_Year, Award_Photo_URL)
        VALUES
            (1, N'Премия «Оскар» (1998): лучший режиссёр (Стивен Спилберг), лучший оператор (Януш Камински), лучший звук (Гэри Ридстром, Гэри Саммерс, Энди Нельсон, Рон Джадкинс), лучший монтаж (Майкл Кан), лучшие звуковые эффекты (Гэри Ридстром, Ричард Химнс)', 1999, NULL),
	        (1, N'Номинация на «Оскар» (1998): лучший фильм, лучший актёр (Том Хэнкс), лучший сценарий (Роберт Родат)', 1998, NULL),
            (1, N'Премия «Золотой глобус» (1998): лучший фильм, драма, лучший режиссёр (Стивен Спилберг).', 1998, NULL),
	        (1, N'Премия «BAFTA» (1998): лучший звук (Гэри Ридстрем), лучшие спецэффекты (Стефен Фангмейер).', 1998, NULL),
	        (1, N'Номинация на премию «Спутник» (1998): лучший режиссёр (Стивен Спилберг).', 1998, NULL),
	        (2, N'Премия Оскар: Лучший звуковой монтаж', 2009, NULL),
	        (2, N'Золотой глобус: Лучший актёр второго плана (Хит Леджер)', 2009, NULL),
	        (2, N'Сатурн: Лучший фильм фэнтези', 2009, NULL),
	        (2, N'MTV Movie Awards: Лучший фильм', 2009, NULL),
	        (2, N'Премия «Оскар»: Лучший грим и причёски', 2009, NULL),
            (3, N'Премия BAFTA (British Academy Film Awards)  за лучшую музыку', 2002, NULL),
	        (3, N'Номинация на премию Оскар в категории ""Лучшие спецэффекты""', 2002, NULL),
	        (3, N'Награда Empire Award за лучший британский актер (Дэниел Рэдклифф)',  2002, NULL),
	        (3, N'Saturn Awards: Лучший дизайн костюмов', 2001, NULL),
            (4, N'Золотой глобус: Лучший фильм на иностранном языке', 2007, NULL),
	        (4, N'Премия Goya: Лучший фильм', 2007, NULL),
	        (4, N'Премия BAFTA: Лучший неанглоязычный фильм', 2007, NULL),
            (5, N'Премия Империо: Лучший испаноязычный драматический сериал.', 2018, NULL),
	        (5, N'Номинация на премию Эмми: Категория ""Лучший драматический сериал"".', 2018, NULL),
	        (5, N'Союз испанских актеров.', 2018, NULL);


            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
