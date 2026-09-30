using System;

namespace TaskGCM
{
    class Program
    {
        // Функция для вычисления наибольшего общего делителя  двух чисел
        static int GCM(int a, int b)
        {
            // Пока оба числа не равны нулю — заменяем большее на остаток от деления
            while (b != 0)
            {
                // Сохраняем b во временную переменную
                int temp = b;
                // b становится остатком от деления a на b
                b = a % b;
                // a становится прежним b
                a = temp;
            }
            return a;
        }
        static void Main(string[] args)
        {
            // Запрашиваем у пользователя числитель
            Console.Write("Введите числитель: ");
            int num = int.Parse(Console.ReadLine());
            // Запрашиваем у пользователя знаменатель
            Console.Write("Введите знаменатель: ");
            int den = int.Parse(Console.ReadLine());
            // Находим НОД числителя и знаменателя с помощью функции
            int gcd = GCM(num, den);
            // Сокращаем дробь: делим числитель и знаменатель на НОД
            int newNum = num / gcd;
            int newDen = den / gcd;
            // Выводим сокращённую дробь на экран
            Console.WriteLine($"Сокращённая дробь: {newNum}/{newDen}");
        }
    }
}