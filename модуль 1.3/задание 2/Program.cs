using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Спрашиваем у пользователя предельную сумму
            Console.Write("Введите предельное число: ");
            int limit = int.Parse(Console.ReadLine());
            Random rnd = new Random();
            // Создаём массив с запасом 
            int[] arr = new int[100];
            // Счётчик элементов и текущая сумма
            int count = 0;
            int sum = 0;
            // Пока сумма не превышает предел — добавляем новые элементы
            while (sum <= limit)
            {
                // Генерируем случайное число от 1 до 9
                int num = rnd.Next(1, 10);
                // Проверяем, не выйдет ли сумма за предел
                if (sum + num > limit) break;
                // Записываем число в массив
                arr[count] = num;
                // Увеличиваем сумму и счётчик
                sum += num;
                count++;
            }
            // Выводим получившийся массив
            Console.Write("Массив: ");
            for (int i = 0; i < count; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            // Выводим количество элементов и итоговую сумму
            Console.WriteLine($"Количество элементов: {count}");
            Console.WriteLine($"Сумма: {sum}");
        }
    }
}
