using System;
namespace InterfaceTasks
{
    // Интерфейс IBook — общий список правил для всех книг
    public interface IBook
    {
        // Свойство Title — нужно, чтобы у любой книги можно было узнать название
        string Title { get; }
        // Свойство IsAvailable — нужно, чтобы проверить, доступна ли книга
        bool IsAvailable { get; }
        // Метод Checkout — нужен, чтобы выдать книгу читателю
        void Checkout();
        // Метод ReturnBook — нужен, чтобы принять книгу обратно
        void ReturnBook();
    }
    // Класс LibraryBook — описывает книгу в библиотеке и выполняет правила интерфейса IBook
    public class LibraryBook : IBook
    {
        // Свойство Title — хранит название книги
        public string Title { get; private set; }
        // Свойство IsAvailable — хранит статус книги (доступна или нет)
        public bool IsAvailable { get; private set; }
        // Конструктор — задает название книги при создании
        public LibraryBook(string title)
        {
            // Записываем название
            Title = title;
            // Книга только поступила, поэтому она доступна
            IsAvailable = true;
        }
        // Метод Checkout — выдает книгу, если она свободна
        public void Checkout()
        {
            // Проверяем, доступна ли книга
            if (IsAvailable)
            {
                // Меняем статус на "выдана", чтобы ее не взяли второй раз
                IsAvailable = false;
                // Сообщаем, что книга выдана
                Console.WriteLine($"   Книга '{Title}' успешно выдана.");
            }
            else
            {
                // Сообщаем, что книга уже у кого-то
                Console.WriteLine($"   Книга '{Title}' уже выдана кому-то другому!");
            }
        }
        // Метод ReturnBook — возвращает книгу в библиотеку
        public void ReturnBook()
        {
            // Меняем статус обратно на "доступна"
            IsAvailable = true;
            // Сообщаем, что книга возвращена
            Console.WriteLine($"   Книга '{Title}' возвращена в библиотеку.");
        }
    }
    // Главный класс программы
    class Program
    {
        // Точка входа — отсюда начинается работа программы
        static void Main(string[] args)
        {
            // Создаем книгу "Преступление и наказание"
            IBook book = new LibraryBook("Преступление и наказание");
            // Показываем начальный статус книги
            Console.WriteLine($"Статус книги '{book.Title}': Доступна = {book.IsAvailable}");
            // Пытаемся выдать книгу
            book.Checkout();
            // Показываем статус после выдачи
            Console.WriteLine($"Статус после выдачи: Доступна = {book.IsAvailable}");
            // Пытаемся выдать книгу повторно (должна быть ошибка)
            book.Checkout();
            // Возвращаем книгу
            book.ReturnBook();
            // Показываем статус после возврата
            Console.WriteLine($"Статус после возврата: Доступна = {book.IsAvailable}");
            // Пауза, чтобы консоль не закрылась сразу
            Console.ReadLine();
        }
    }
}