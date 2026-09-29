using System;

namespace FactorialApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запрашиваем у пользователя число
            Console.Write("Введите число: ");

            // Считываем строку и преобразуем её в целое число
            int n = int.Parse(Console.ReadLine());

            // Проверяем: факториал отрицательных чисел не определён
            if (n < 0)
            {
                Console.WriteLine("Факториал отрицательного числа не существует.");
                return; // Выходим из программы
            }

            // Переменная для хранения результата.
            // Начинаем с 1, так как 0! = 1 и 1! = 1.
            long result = 1;

            // Цикл от 2 до n: последовательно умножаем result на каждое число
            for (int i = 2; i <= n; i++)
            {
                result *= i; // result = result * i
            }

            // Выводим результат на экран
            Console.WriteLine($"{n}! = {result}");
        }
    }
}