using System;
namespace Module2Task3
{
    // Класс Автор — хранит имя и год рождения
    class Author
    {
        // Поля автора
        public string Name;
        public int BirthYear;
        // Конструктор — принимает имя и год рождения
        public Author(string name, int birthYear)
        {
            Name = name;
            BirthYear = birthYear;
        }
    }
    // Класс Книга — хранит название, год выпуска и объект автора
    class Book
    {
        // Поля книги
        public string Title;
        public int Year;
        // Композиция: внутри книги лежит объект класса Author
        public Author Author;
        // Конструктор — принимает название, год и объект автора
        public Book(string title, int year, Author author)
        {
            Title = title;
            Year = year;
            Author = author;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Создаём двух авторов
            Author a1 = new Author("Лев Толстой", 1828);
            Author a2 = new Author("Фёдор Достоевский", 1821);
            // Создаём две книги, передавая им авторов
            Book b1 = new Book("Война и мир", 1869, a1);
            Book b2 = new Book("Преступление и наказание", 1866, a2);
            // Выводим информацию о первой книге
            Console.WriteLine("Книга: " + b1.Title);
            Console.WriteLine("Год выпуска: " + b1.Year);
            Console.WriteLine("Автор: " + b1.Author.Name);
            Console.WriteLine("Год рождения автора: " + b1.Author.BirthYear);
            Console.WriteLine();
            // Выводим информацию о второй книге
            Console.WriteLine("Книга: " + b2.Title);
            Console.WriteLine("Год выпуска: " + b2.Year);
            Console.WriteLine("Автор: " + b2.Author.Name);
            Console.WriteLine("Год рождения автора: " + b2.Author.BirthYear);
        }
    }
}