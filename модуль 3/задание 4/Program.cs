using System;
using System.Collections.Generic; 
namespace Task4 
{
    public delegate bool DataFilter(string item); // Тип-ссылка на правило фильтрации
    class Program // Основной класс
    {
        static bool ByKeyword(string s) => s.Contains("важно", StringComparison.OrdinalIgnoreCase); // Правило: оставить строки со словом «важно»
        static bool ByDate(string s) => s.Contains("2024");                                       // Правило: оставить строки с «2024»
        static List<string> Apply(List<string> data, DataFilter f) // Прогоняет список через правило
        {
            var result = new List<string>(); // Сюда складываем подходящие
            foreach (var item in data) if (f(item)) result.Add(item); // Проверяем через делегат
            return result; // Отдаём результат
        }
        static void Main() // Точка входа
        {
            var data = new List<string> // Исходные строки
            {
                "Важно: совещание в 10:00", // Строка 1
                "Обычная заметка",          // Строка 2
                "Важно: отчёт за 2024 год", // Строка 3
                "Заметка за 2023 год",      // Строка 4
                "Важно: продлить подписку"  // Строка 5
            };
            Console.WriteLine("Фильтр по слову 'важно':"); // Заголовок
            Apply(data, ByKeyword).ForEach(Console.WriteLine); // Применяем фильтр по слову
            Console.WriteLine("\nФильтр по дате '2024':"); // Заголовок
            Apply(data, ByDate).ForEach(Console.WriteLine);    // Применяем фильтр по дате
        }
    }
}