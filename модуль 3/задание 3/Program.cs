using System; 
using System.Collections.Generic; 
namespace Task3 
{
    public delegate void TaskHandler(string task); // Тип-ссылка на метод выполнения задачи
    class Program // Основной класс
    {
        static void Notify(string t) => Console.WriteLine($"[Уведомление] Задача: {t}"); // Исполнитель: уведомление
        static void Log(string t) => Console.WriteLine($"[Журнал] Записано: {t}");    // Исполнитель: журнал
        static void Main() // Точка входа
        {
            var tasks = new List<(string Desc, TaskHandler Handler)> // Список задач с исполнителями
            {
                ("Проверить почту", Notify),   // Задача с исполнителем Notify
                ("Обновить базу", Log),        // Задача с исполнителем Log
                ("Позвонить клиенту", Notify)  // Задача с исполнителем Notify
            };
            foreach (var task in tasks) // Перебираем задачи
            {
                Console.WriteLine($"Выполняется задача: {task.Desc}"); // Печатаем задачу
                task.Handler(task.Desc); // Выполняем через делегат
            }
        }
    }
}