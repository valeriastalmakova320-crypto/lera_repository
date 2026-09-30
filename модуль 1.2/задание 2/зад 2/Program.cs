using System;

namespace TaskReplaceMax
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаём и инициализируем массив из 10 элементов
            int[] arr = { 15, -3, 42, 7, 0, 8, -19, 25, 11, 6 };
            // Выводим исходный массив на экран
            Console.Write("Исходный массив: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            // Запрашиваем у пользователя целое число
            Console.Write("Введите число для замены: ");
            int num = int.Parse(Console.ReadLine());
            // Изначально считаем, что максимум — первый элемент
            int maxIndex = 0;
            // Ищем индекс максимального элемента
            for (int i = 1; i < arr.Length; i++)
            {
                // Если текущий элемент больше найденного максимума — обновляем индекс
                if (arr[i] > arr[maxIndex]) maxIndex = i;
            }
            // Заменяем максимальный элемент введённым числом
            arr[maxIndex] = num;
            // Выводим изменённый массив
            Console.Write("Изменённый массив: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
    }
}