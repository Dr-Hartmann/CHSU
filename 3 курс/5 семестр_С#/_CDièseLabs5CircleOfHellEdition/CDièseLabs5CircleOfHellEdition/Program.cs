namespace CDièseLabs5CircleOfHellEdition
{
    internal partial class Program
    {
        public static void Main(string[] args)
        {
            FinalProgram();
        }

        static void FinalProgram()
        {
            var productsData = new List<IProduct> { };
            var showcasesData = new List<Showcase<IProduct>> { };

            int state = 0;
            int buffer = 0;
            int numShowcase = -1;
            int numProduct = -1;

            bool process = true;
            while (process)
            {
                switch (state)
                {
                    /// начало - готово
                    case 0:
                        Console.WriteLine(" - Введите номер операции:");
                        Console.WriteLine("1. Создать витрину");
                        Console.WriteLine("2. Создать товар");
                        Console.WriteLine("3. Выбрать витрину для работы");
                        Console.WriteLine("4. Выбрать товар для работы");
                        Console.WriteLine("5. Выход");

                        buffer = EnterNumber();

                        if (buffer == 1) state = 1;
                        else if (buffer == 2) state = 2;
                        else if (buffer == 3) state = 3;
                        else if (buffer == 4) state = 4;
                        else if (buffer == 5) state = 5;
                        break;
                    /// создание витрины - готово
                    case 1:
                        Showcase<IProduct> showcase = null;

                        Console.Write(" - Введите размер витрины:\t");
                        int size = EnterNumber();

                        Console.WriteLine(" - Указать ID витрины? (y/n)");

                        if (Console.ReadKey().KeyChar == 'y')
                        {
                            Console.WriteLine("\n");
                            Console.Write(" - Введите ID:\t");
                            showcase = (size, EnterNumber());
                        }
                        else
                        {
                            showcase = size;
                        }

                        if (showcase is not null)
                        {
                            showcase.Notify += DisplayMessage;
                            showcasesData.Add(showcase);
                            numShowcase = showcasesData.IndexOf(showcase);
                            state = 3;
                        }
                        else
                        {
                            Console.WriteLine(" - Ошибка при создании - ");
                            state = 0;
                        }

                        break;
                    /// создание товара - готово
                    case 2:
                        IProduct product = null;

                        Console.WriteLine(" - Введите тип товара:");
                        Console.WriteLine("1. Винный шкаф");
                        Console.WriteLine("2. Винный холодильник");

                        buffer = EnterNumber();

                        if (buffer > 2 || buffer < 1) break;

                        Console.WriteLine(" - Введите:");
                        Console.Write("Имя:\t");
                        string name = EnterStr();

                        Console.Write("Идентификатор:\t");
                        int id = EnterNumber();

                        Console.Write("Серия:\t");
                        string series = EnterStr();

                        if (buffer == 1)
                        {
                            Console.Write("Число на складе:\t");
                            int amount = EnterNumber();

                            Console.Write("Цена:\t");
                            int price = EnterNumber();

                            product = new Wineсase(id, name, series, amount, price);
                        }
                        else if (buffer == 2)
                        {
                            Console.Write("Число на складе:\t");
                            int amount = EnterNumber();

                            Console.Write("Цена:\t");
                            int price = EnterNumber();

                            Console.Write("Вместимость бутылок:\t");
                            int capacityOfBottle = EnterNumber();

                            Console.Write("Объём в литрах:\t");
                            int volume = EnterNumber();

                            product = new Winefridge(id, name, series, amount, price, capacityOfBottle, volume);
                        }

                        if (product is null) break;

                        productsData.Add(product);

                        Console.WriteLine(" - Добавить товар на витрину? (y/n)");

                        if (Console.ReadKey().KeyChar == 'y')
                        {
                            Console.WriteLine("\n");

                            if (!showcasesData.Any())
                            {
                                Console.WriteLine(" - Нет доступной витрины - ");
                                state = 0;
                                break;
                            }

                            Console.WriteLine(" - Выберите витрину для добавления:");

                            int i = 0;
                            foreach (var item in showcasesData)
                            {
                                Console.WriteLine($"{++i}. Витрина {item.ID}");
                            }

                            int j = EnterNumber() - 1;
                            if (j <= showcasesData.Count && j > 0)
                            {
                                showcasesData[j].Push(product);
                                numProduct = showcasesData[j].GetProductIndexById(product.ID);

                            }
                            else
                            {
                                Console.WriteLine(" - Неверный ввод - ");
                                state = 0;
                                break;
                            }
                        }

                        state = 4;

                        break;
                    /// изменение витрины
                    case 3:
                        if (numShowcase == -1)
                        {
                            if (!showcasesData.Any())
                            {
                                Console.WriteLine(" - Нет доступных витрин - ");
                                state = 0;
                                break;
                            }

                            Console.WriteLine(" - Выберите витрину для операции:");

                            int i = 0;
                            foreach (var item in showcasesData)
                            {
                                Console.WriteLine($"{++i}. Витрина {item.ID}");
                            }

                            buffer = EnterNumber();

                            if (buffer <= i && buffer >= 1)
                            {
                                numShowcase = buffer - 1;
                            }
                            else break;

                            Console.WriteLine($"\n - Текущая витрина: {showcasesData[numShowcase].ID}\n");
                        }

                        Console.WriteLine(" - Что Вы хотите сделать с витриной?");
                        Console.WriteLine("1. Изменить ID");
                        Console.WriteLine("2. Добавить существующий товар");
                        Console.WriteLine("3. Удалить последний товар");
                        Console.WriteLine("4. Удалить товар по позиции на витрине");
                        Console.WriteLine("5. Найти позицию товара по ID");
                        Console.WriteLine("6. Найти позицию товара по Имени");
                        Console.WriteLine("7. Отсортировать по ID");
                        Console.WriteLine("8. Отсортировать по Имени");
                        Console.WriteLine("9. Очистить витрину");
                        Console.WriteLine("10. Удалить витрину");
                        Console.WriteLine("11. Выбрать другую витрину");
                        Console.WriteLine("12. Вывести содержимое витрины");
                        Console.WriteLine("13. Выйти в главное меню");

                        buffer = EnterNumber();
                        if (buffer > 13 || buffer < 1) break;

                        if (buffer == 1)
                        {
                            Console.Write(" - Введите новое ID для витрины:\t");
                            showcasesData[numShowcase].ID = EnterNumber();
                        }
                        else if (buffer == 2)
                        {
                            if (!productsData.Any())
                            {
                                Console.WriteLine(" - Нет доступных товаров - ");
                                break;
                            }

                            Console.WriteLine(" - Выберите товар из списка:");

                            int i = 0;
                            foreach (var item in productsData)
                            {
                                Console.WriteLine($"{++i}. Товар {item.Name}");
                            }

                            buffer = EnterNumber() - 1;

                            i = 0;
                            foreach (var item in productsData)
                            {
                                if (i < buffer) ++i;
                                else if (i == buffer)
                                {
                                    showcasesData[numShowcase].Push(item);
                                }
                                else break;
                            }
                        }
                        else if (buffer == 3)
                        {
                            var prodBuf1 = showcasesData[numShowcase].DeleteLast();
                            if (prodBuf1 is null)
                            {
                                Console.WriteLine($" - Витрина пуста - ");
                                break;
                            }
                            Console.WriteLine($" - Удалён товар из списка: {prodBuf1.Name}");
                        }
                        else if (buffer == 4)
                        {
                            var outStr = showcasesData[numShowcase].OutputListOfProducts();

                            if (outStr != "")
                            {
                                Console.WriteLine(" - Выберите товар из списка:");
                                Console.WriteLine(outStr);
                                Console.WriteLine($" - Удалён товар из списка: {showcasesData[numShowcase].DeleteByIndex(EnterNumber() - 1).Name}");
                            }
                            else
                                Console.WriteLine($" - Витрина пуста - ");
                        }
                        else if (buffer == 5)
                        {
                            Console.Write(" - Введите ID товара:\t");
                            Console.WriteLine($" - Позиция товара на витрине: {showcasesData[numShowcase].GetProductIndexById(EnterNumber() - 1)}");
                        }
                        else if (buffer == 6)
                        {
                            Console.WriteLine(" - Введите Имя товара:");
                            Console.WriteLine($" - Позиция товара на витрине: {showcasesData[numShowcase].GetProductIndexByName(EnterStr())}");
                        }
                        else if (buffer == 7)
                        {
                            showcasesData[numShowcase].OrderById();
                            Console.WriteLine(" - Витрина отсортирована по ID");
                        }
                        else if (buffer == 8)
                        {
                            showcasesData[numShowcase].OrderByName();
                            Console.WriteLine(" - Витрина отсортирована по Имени");
                        }
                        else if (buffer == 9)
                        {
                            Console.WriteLine(" - Вы точно хотите очистить витрину? (y/n)");
                            if (Console.ReadKey().KeyChar == 'y')
                            {
                                showcasesData[numShowcase].DeleteAll();
                            }
                        }
                        else if (buffer == 10)
                        {
                            Console.WriteLine(" - Вы точно хотите удалить витрину? (y/n)");

                            if (Console.ReadKey().KeyChar == 'y')
                            {
                                Console.WriteLine(" - Удаление витрину приведёт к потере всех данных, продолжить? (y/n)");
                                if (Console.ReadKey().KeyChar == 'y')
                                {
                                    showcasesData[numShowcase].Dispose();
                                    showcasesData.Remove(showcasesData[numShowcase]);
                                    showcasesData[numShowcase].Notify -= DisplayMessage;
                                    showcasesData[numShowcase].Dispose();
                                    state = 0;
                                    numShowcase = -1;
                                    numProduct = -1;
                                }
                            }
                        }
                        else if (buffer == 11)
                        {
                            numShowcase = -1;
                        }
                        else if (buffer == 12)
                        {
                            Console.WriteLine(" -- Вывод витрины -- ");
                            Console.WriteLine(showcasesData[numShowcase].OutputShowcase());
                            Console.WriteLine(" -- Вывод витрины завершён -- ");
                        }
                        else
                        {
                            state = 0;
                            numShowcase = -1;
                            numProduct = -1;
                        }

                        break;
                    /// изменение товара
                    case 4:
                        if (numProduct == -1)
                        {
                            if (!productsData.Any())
                            {
                                Console.WriteLine(" - Нет доступных товаров");
                                state = 0;
                                break;
                            }

                            Console.WriteLine(" - Выберите товар для операции:");

                            int i = 0;
                            foreach (var item in productsData)
                            {
                                Console.WriteLine($"{++i}. Товар {item.Name} - {item.ID}");
                            }

                            buffer = EnterNumber() - 1;

                            if (buffer < productsData.Count && buffer >= 0)
                            {
                                numProduct = buffer;
                            }
                            else
                            {
                                Console.WriteLine(" - Неверный ввод - ");
                                break;
                            }
                        }

                        Console.WriteLine($"\n - Текущий товар: {productsData[numProduct].Name} - {productsData[numProduct].ID}\n");

                        Console.WriteLine(" - Что Вы хотите сделать с товаром?");
                        Console.WriteLine("1. Изменить ID");
                        Console.WriteLine("2. Изменить Имя");
                        Console.WriteLine("3. Удалить товар");
                        Console.WriteLine("4. Выбрать другой товар");
                        Console.WriteLine("5. Выйти в главное меню");

                        int valueBoundary = 5;

                        if (productsData[numProduct] is Wineсase)
                        {
                            Console.WriteLine("6. Изменить Серию");
                            Console.WriteLine("7. Изменить Цену");
                            Console.WriteLine("8. Изменить Число на складе");
                            valueBoundary = 8;
                        }
                        else if (productsData[numProduct] is Winefridge)
                        {
                            Console.WriteLine("6. Изменить Серию");
                            Console.WriteLine("7. Изменить Цену");
                            Console.WriteLine("8. Изменить Число на складе");
                            Console.WriteLine("9. Изменить Вместимость бутылок");
                            Console.WriteLine("10. Изменить Объём в литрах");
                            valueBoundary = 10;
                        }

                        buffer = EnterNumber();
                        if (buffer > valueBoundary || buffer < 1) break;

                        switch (buffer)
                        {
                            case 1:
                                Console.WriteLine(" - Введите новое ID:");
                                productsData[numProduct].ID = EnterNumber();
                                break;
                            case 2:
                                Console.WriteLine(" - Введите новое Имя:");
                                productsData[numProduct].Name = EnterStr();
                                break;
                            case 3:
                                Console.WriteLine(" - Вы точно хотите удалить товар? (y/n)");

                                if (Console.ReadKey().ToString() == "y")
                                {
                                    productsData[numProduct].Dispose();
                                    productsData.Remove(productsData[numProduct]);
                                    Console.WriteLine(" - Товар удалён - ");
                                }
                                break;
                            case 4:
                                numProduct = -1;
                                break;
                            case 5:
                                state = 0;
                                numShowcase = -1;
                                numProduct = -1;
                                break;
                            case 6:
                                Console.WriteLine(" - Введите новую Серию:");
                                if (productsData[numProduct] is Wineсase item1)
                                {
                                    item1.Series = EnterStr();
                                    productsData[numProduct] = item1;
                                }
                                else Console.WriteLine(" - Ошибка приведения типов");
                                break;
                            case 7:
                                Console.WriteLine(" - Введите новую Цену для:");
                                if (productsData[numProduct] is Wineсase item2)
                                {
                                    item2.Price = EnterNumber();
                                    productsData[numProduct] = item2;
                                }
                                else Console.WriteLine(" - Ошибка приведения типов");
                                break;
                            case 8:
                                Console.WriteLine(" - Введите новое Число на складе:");
                                if (productsData[numProduct] is Wineсase item3)
                                {
                                    item3.Amount = EnterNumber();
                                    productsData[numProduct] = item3;
                                }
                                else Console.WriteLine(" - Ошибка приведения типов");
                                break;
                            case 9:
                                Console.WriteLine(" - Введите новую Вместимость бутылок:");
                                if (productsData[numProduct] is Winefridge item4)
                                {
                                    item4.CapacityOfBottle = EnterNumber();
                                    productsData[numProduct] = item4;
                                }
                                else Console.WriteLine(" - Ошибка приведения типов");
                                break;
                            case 10:
                                Console.WriteLine(" - Введите новый Объём в литрах:");
                                if (productsData[numProduct] is Winefridge item5)
                                {
                                    item5.Volume = EnterNumber();
                                    productsData[numProduct] = item5;
                                }
                                else Console.WriteLine(" - Ошибка приведения типов");
                                break;
                            default:
                                break;
                        }
                        break;
                    /// остановка работы - готово
                    case 5:
                        productsData.DisposeAll();
                        showcasesData.DisposeAll();
                        process = false;
                        break;

                    default:
                        Console.WriteLine(" - Ошибочное состояние - ");
                        state = 0;
                        numProduct = -1;
                        numShowcase = -1;
                        break;
                }
                Console.WriteLine("\n");
                buffer = -1;
            }
            Console.WriteLine(" --- Работа закончена --- ");
        }

        static int EnterNumber()
        {
            string inputStr = EnterStr();
            int number = 0;

            while (!Int32.TryParse(inputStr, out number))
            {
                Console.WriteLine(" - Некорректный ввод числа, попробуйте ещё раз - ");
                inputStr = EnterStr();
            }
            return number;
        }

        static string EnterStr()
        {
            string inputStr = Console.ReadLine();

            while (inputStr.Length == 0)
            {
                Console.WriteLine(" - Некорректный ввод строки, попробуйте ещё раз - ");
                inputStr = Console.ReadLine();
            }
            return inputStr;
        }

        static void DisplayMessage<T>(Showcase<T> sender, ShowcaseEventArgs e)
            where T : class, IProduct
        {
            if (sender is Showcase<T> item)
            {
                Console.WriteLine($"Витрина Товароподобных: {item.ID}");
                Console.WriteLine($"Тип сообщения: {e.Type}");
                Console.WriteLine($"{e.Message}\n");
            }
        }
    }
}