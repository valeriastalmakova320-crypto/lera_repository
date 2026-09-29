using System;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запрашиваем у пользователя число
            Console.Write("Введите число: ");
            // Считываем строку и преобразуем её в целое число
            int n = int.Parse(Console.ReadLine());
            // Предполагаем, что число простое
            bool isPrime = true;
            // Числа меньше 2 не являются простыми
            if (n < 2)
            {
                isPrime = false;
            }
            // Проверяем делители от 2 до корня из n
            for (int i = 2; i * i <= n; i++)
            {
                // Если число делится на i без остатка — оно не простое
                if (n % i == 0)
                {
                    isPrime = false;
                    // Прерываем цикл, дальше искать смысла нет
                    break;
                }
            }
            // Выводим результат в зависимости от флага
            if (isPrime)
            {
                Console.WriteLine($"{n} — простое число");
            }
            else
            {
                Console.WriteLine($"{n} — не простое число");
            }
        }
    }
}