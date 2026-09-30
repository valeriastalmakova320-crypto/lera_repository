using System;

namespace TaskMinMax
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запрашиваем количество элементов массива
            Console.Write("Введите количество элементов K: ");
            int k = int.Parse(Console.ReadLine());
            // Запрашиваем нижнюю границу диапазона A
            Console.Write("Введите A: ");
            int a = int.Parse(Console.ReadLine());
            // Запрашиваем верхнюю границу диапазона B
            Console.Write("Введите B: ");
            int b = int.Parse(Console.ReadLine());
            // Создаём массив из K элементов
            int[] arr = new int[k];
            // Создаём генератор случайных чисел
            Random rnd = new Random();
            // Заполняем массив случайными числами из [A, B) и выводим его
            for (int i = 0; i < k; i++)
            {
                arr[i] = rnd.Next(a, b);
                Console.Write(arr[i] + " ");
            }
            // Переходим на новую строку
            Console.WriteLine();
            // Изначально минимум и максимум — на позиции 0
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