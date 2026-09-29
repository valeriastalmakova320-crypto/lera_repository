using System;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Спрашиваем у пользователя K, A и B
            Console.Write("Введите количество элементов K: ");
            int k = int.Parse(Console.ReadLine());
            Console.Write("Введите A: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Введите B: ");
            int b = int.Parse(Console.ReadLine());
            // Создаём массив из K элементов
            int[] arr = new int[k];
            Random rnd = new Random();
            // Заполняем массив случайными числами из [A, B)
            for (int i = 0; i < k; i++)
            {
                arr[i] = rnd.Next(a, b);
                // Заодно выводим массив на экран
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            int minIndex = 0;
            int maxIndex = 0;
            // Ищем индексы минимума и максимума в одном проходе
            for (int i = 1; i < k; i++)
            {
                if (arr[i] < arr[minIndex]) minIndex = i;
                if (arr[i] > arr[maxIndex]) maxIndex = i;
            }
            // Определяем, какой индекс левее, какой правее
            int start = Math.Min(minIndex, maxIndex);
            int end = Math.Max(minIndex, maxIndex);
            // Выводим найденные индексы
            Console.WriteLine($"Индекс минимума: {minIndex}, индекс максимума: {maxIndex}");
            // Выводим элементы между ними, включая их самих
            Console.Write("Элементы между ними: ");
            for (int i = start; i <= end; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
    }
}