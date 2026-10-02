using System;

namespace Module2Task1
{
    // Класс, описывающий человека
    class Person
    {
        // Приватные поля — доступны только внутри класса
        private string name;
        private int age;
        private string address;

        // Метод для установки имени
        public void SetName(string n)
        {
            name = n;
        }

        // Метод для получения имени
        public string GetName()
        {
            return name;
        }

        // Метод для установки возраста
        public void SetAge(int a)
        {
            age = a;
        }

        // Метод для получения возраста
        public int GetAge()
        {
            return age;
        }

        // Метод для установки адреса
        public void SetAddress(string addr)
        {
            address = addr;
        }

        // Метод для получения адреса
        public string GetAddress()
        {
            return address;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаём первый объект класса Person
            Person p1 = new Person();
            // Заполняем его данными через сеттеры
            p1.SetName("Даниил");
            p1.SetAge(28);
            p1.SetAddress("Минск, пр. Независимости, д. 15");
            // Выводим информацию о первом человеке
            Console.WriteLine("Человек 1:");
            Console.WriteLine("Имя: " + p1.GetName());
            Console.WriteLine("Возраст: " + p1.GetAge());
            Console.WriteLine("Адрес: " + p1.GetAddress());

            // Создаём второй объект класса Person
            Person p2 = new Person();
            // Заполняем его данными
            p2.SetName("Мария");
            p2.SetAge(34);
            p2.SetAddress("Минск, ул. Немига, д. 8");
            // Выводим информацию о втором человеке
            Console.WriteLine();
            Console.WriteLine("Человек 2:");
            Console.WriteLine("Имя: " + p2.GetName());
            Console.WriteLine("Возраст: " + p2.GetAge());
            Console.WriteLine("Адрес: " + p2.GetAddress());
        }
    }
}