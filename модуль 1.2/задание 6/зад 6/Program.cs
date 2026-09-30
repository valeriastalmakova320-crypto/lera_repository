using System;

namespace TaskIndexSort
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаём вещественный массив из 10 элементов
            double[] arr = new double[10];
            // Создаём генератор случайных чисел
            Random rnd = new Random();
            // Заполняем массив случайными числами из диапазона [-10, 10)
            for (int i = 0; i < 10; i++)
            {
                // rnd.NextDouble() даёт число от 0 до 1, растягиваем на 20 и сдвигаем на -10
                arr[i] = rnd.NextDouble() * 20 - 10;
            }
            // Выводим исходный массив
            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < 10; i++)
            {
                // F3 — три знака после запятой для наглядности
                Console.WriteLine($"[{i}] = {arr[i].ToString("F3")}");
            }
            // Создаём массив индексов: изначально это 0, 1, 2, ..., 9
            int[] indices = new int[10];
            for (int i = 0; i < 10; i++)
            {
                indices[i] = i;
            }
            // Сортируем массив индексов так, чтобы значения arr[indices[i]] шли по возрастанию
            // Используем метод «пузырька» с обменом местами самих индексов
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9 - i; j++)
                {
                    // Сравниваем значения элементов массива по индексам
                    if (arr[indices[j]] > arr[indices[j + 1]])
                    {
                        // Меняем местами индексы, если значения стоят не по порядку
                        int temp = indices[j];
                        indices[j] = indices[j + 1];
                        indices[j + 1] = temp;
                    }
                }
            }
            // Выводим массив индексов и соответствующие им значения
            Console.WriteLine("Индексы в порядке возрастания значений:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{indices[i]} -> {arr[indices[i]].ToString("F3")}");
            }
        }
    }
}