using System;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запрашиваем у пользователя строку
            Console.Write("Введите строку: ");
            // Считываем введённую строку
            string input = Console.ReadLine();
            // Переменная для хранения перевёрнутой строки
            string reversed = "";
            // Проходим по строке с конца в начало
            for (int i = input.Length - 1; i >= 0; i--)
            {
                // Добавляем текущий символ к перевёрнутой строке
                reversed += input[i];
            }
            // Выводим перевёрнутую строку на экран
            Console.WriteLine($"В обратном порядке: {reversed}");
        }
    }
}