using System;
namespace TaskPrimes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запрашиваем у пользователя количество простых чисел
            Console.Write("Введите количество простых чисел K: ");
            int k = int.Parse(Console.ReadLine());
            // Счётчик уже найденных простых чисел
            int count = 0;
            // Текущее проверяемое число, начинаем с 2 — первого простого
            int num = 2;
            // Счётчик для переноса строки каждые 10 чисел
            int inLine = 0;
            // Ищем простые числа, пока не наберём K штук
            while (count < k)
            {
                // Предполагаем, что число простое
                bool isPrime = true;
                // Проверяем делители от 2 до корня из числа
                for (int i = 2; i * i <= num; i++)
                {
                    // Если число делится на i без остатка — оно не простое
                    if (num % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                // Если число простое — выводим его и увеличиваем счётчик
                if (isPrime)
                {
                    Console.Write(num + "\t");
                    count++;
                    inLine++;
                    // Каждые 10 чисел переходим на новую строку
                    if (inLine == 10)
                    {
                        Console.WriteLine();
                        inLine = 0;
                    }
                }
                // Переходим к следующему числу
                num++;
            }
        }
    }
}