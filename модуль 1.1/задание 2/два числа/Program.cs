using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запрашиваем у пользователя первое число
            Console.Write("Введите первое число: ");
            // Считываем строку и преобразуем её в целое число
            int a = int.Parse(Console.ReadLine());
            // Запрашиваем у пользователя второе число
            Console.Write("Введите второе число: ");
            // Считываем строку и преобразуем её в целое число
            int b = int.Parse(Console.ReadLine());
            // Вычисляем сумму двух чисел
            int sum = a + b;
            // Выводим результат на экран
            Console.WriteLine($"Сумма = {sum}");
        }
    }
}