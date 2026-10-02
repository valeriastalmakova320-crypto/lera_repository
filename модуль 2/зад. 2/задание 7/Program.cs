using System;
namespace Module2Task7
{
    // Структура Train — хранит информацию о поезде
    struct Train
    {
        // Поля: пункт назначения, номер поезда, время отправления
        public string Destination;
        public int Number;
        public DateTime DepartureTime;
    }
    class Program
    {
        // Метод ввода данных о поездах в массив
        static void InputTrains(Train[] trains)
        {
            // Проходим по всем элементам массива
            for (int i = 0; i < trains.Length; i++)
            {
                // Заголовок для текущего поезда
                Console.WriteLine("Введите данные для поезда " + (i + 1) + ":");
                // Запрашиваем пункт назначения
                Console.Write("Пункт назначения: ");
                trains[i].Destination = Console.ReadLine();
                // Запрашиваем номер поезда
                Console.Write("Номер поезда: ");
                trains[i].Number = int.Parse(Console.ReadLine());
                // Запрашиваем время отправления
                Console.Write("Время отправления (чч:мм): ");
                trains[i].DepartureTime = DateTime.Parse(Console.ReadLine());
                // Пустая строка между поездами для читаемости
                Console.WriteLine();
            }
        }
        // Метод вывода информации о всех поездах
        static void PrintTrains(Train[] trains)
        {
            // Проходим по всем элементам и печатаем данные
            for (int i = 0; i < trains.Length; i++)
            {
                Console.WriteLine("Пункт назначения: " + trains[i].Destination +
                                  ", Поезд №: " + trains[i].Number +
                                  ", Отправление: " + trains[i].DepartureTime.ToString("HH:mm:ss"));
            }
        }
        //Метод сортировки по номеру поезда
        static void SortByNumber(Train[] trains)
        {
            // Метод «пузырька» по номеру поезда
            for (int i = 0; i < trains.Length - 1; i++)
            {
                for (int j = 0; j < trains.Length - 1 - i; j++)
                {
                    // Если номер текущего больше следующего — меняем местами
                    if (trains[j].Number > trains[j + 1].Number)
                    {
                        Train temp = trains[j];
                        trains[j] = trains[j + 1];
                        trains[j + 1] = temp;
                    }
                }
            }
        }
        // Метод сортировки по пункту назначения, при равенстве — по времени
        static void SortByDestination(Train[] trains)
        {
            // Метод «пузырька»
            for (int i = 0; i < trains.Length - 1; i++)
            {
                for (int j = 0; j < trains.Length - 1 - i; j++)
                {
                    // Сначала сравниваем пункты назначения
                    int cmp = string.Compare(trains[j].Destination, trains[j + 1].Destination);
                    // Если пункты одинаковые — сравниваем время
                    if (cmp == 0) cmp = DateTime.Compare(trains[j].DepartureTime, trains[j + 1].DepartureTime);
                    // Если порядок неправильный — меняем местами
                    if (cmp > 0)
                    {
                        Train temp = trains[j];
                        trains[j] = trains[j + 1];
                        trains[j + 1] = temp;
                    }
                }
            }
        }
        // Метод поиска поезда по номеру
        static void FindByNumber(Train[] trains)
        {
            // Запрашиваем номер для поиска
            Console.Write("Введите номер поезда для поиска: ");
            int search = int.Parse(Console.ReadLine());
            // Флаг — найден ли поезд
            bool found = false;
            // Ищем поезд в массиве
            for (int i = 0; i < trains.Length; i++)
            {
                // Если номер совпал — выводим информацию
                if (trains[i].Number == search)
                {
                    Console.WriteLine("Пункт назначения: " + trains[i].Destination +
                                      ", Поезд №: " + trains[i].Number +
                                      ", Отправление: " + trains[i].DepartureTime.ToString("HH:mm:ss"));
                    found = true;
                    break;
                }
            }
            // Если не нашли — сообщаем
            if (!found) Console.WriteLine("Поезд с таким номером не найден.");
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            // Создаём массив из пяти поездов
            Train[] trains = new Train[5];
            // Вводим данные с клавиатуры
            InputTrains(trains);
            // Сортируем по номеру и выводим
            SortByNumber(trains);
            Console.WriteLine("Поезда, отсортированные по номеру:");
            PrintTrains(trains);
            Console.WriteLine();
            // Ищем поезд по введённому номеру
            FindByNumber(trains)
            // Сортируем по пункту назначения и времени и выводим
            SortByDestination(trains);
            Console.WriteLine("Поезда, отсортированные по пункту назначения и времени:");
            PrintTrains(trains);
        }
    }
}