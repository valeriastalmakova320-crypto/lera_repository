using System;

namespace TaskLetters
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запрашиваем у пользователя количество элементов
            Console.Write("Введите количество элементов K: ");
            int k = int.Parse(Console.ReadLine());
            // Строка со всеми буквами русского алфавита
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            // Строка только с согласными буквами русского алфавита
            string consonants = "бвгджзйклмнпрстфхцчшщ";
            // Создаём символьный массив из K элементов
            char[] arr = new char[k];
            // Создаём генератор случайных чисел
            Random rnd = new Random();
            // Заполняем массив случайными буквами русского алфавита
            for (int i = 0; i < k; i++)
            {
                // Берём случайный индекс из строки alphabet
                arr[i] = alphabet[rnd.Next(alphabet.Length)];
            }
            // Выводим исходный массив
            Console.Write("Исходный массив: ");
            for (int i = 0; i < k; i++)
            {
                Console.Write(arr[i] + " ");
            }
            // Переходим на новую строку
            Console.WriteLine();
            // Создаём новый массив для согласных (с запасом по размеру K)
            char[] consonantArr = new char[k];
            // Счётчик согласных букв в новом массиве
            int count = 0;
            // Проходим по исходному массиву и отбираем согласные
            for (int i = 0; i < k; i++)
            {
                // Проверяем, содержится ли текущая буква в строке согласных
                if (consonants.IndexOf(arr[i]) != -1)
                {
                    // Если да — записываем в новый массив
                    consonantArr[count] = arr[i];
                    count++;
                }
            }
            // Выводим массив только из согласных букв
            Console.Write("Массив согласных: ");
            for (int i = 0; i < count; i++)
            {
                Console.Write(consonantArr[i] + " ");
            }
        }
    }
}