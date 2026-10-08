using System; // Подключаем System
namespace Task5 // Объявляем пространство имён
{
    public delegate void SortDelegate(int[] arr); // Тип-ссылка на метод сортировки
    class Program // Основной класс
    {
        static void Bubble(int[] a) // Сортировка пузырьком
        {
            for (int i = 0; i < a.Length - 1; i++) // Внешний проход
                for (int j = 0; j < a.Length - 1 - i; j++) // Внутренний проход по парам
                    if (a[j] > a[j + 1]) (a[j], a[j + 1]) = (a[j + 1], a[j]); // Меняем, если порядок неверный
        }
        static void Quick(int[] a) => QuickRec(a, 0, a.Length - 1); // Запуск быстрой сортировки
        static void QuickRec(int[] a, int l, int r) // Рекурсивная часть
        {
            if (l >= r) return; // Сортировать нечего
            int p = a[(l + r) / 2], i = l, j = r; // Опорный элемент и указатели
            while (i <= j) // Пока указатели не сошлись
            {
                while (a[i] < p) i++; // Двигаем левый вправо
                while (a[j] > p) j--; // Двигаем правый влево
                if (i <= j) { (a[i], a[j]) = (a[j], a[i]); i++; j--; } // Меняем пару и сдвигаем
            }
            QuickRec(a, l, j); // Сортируем левую часть
            QuickRec(a, i, r); // Сортируем правую часть
        }
        static void Main() // Точка входа
        {
            int[] original = { 5, 3, 8, 1, 9, 2, 7, 4, 6 }; // Исходный массив
            int[] b = (int[])original.Clone(); // Копия под пузырёк
            SortDelegate bubble = Bubble;      // Кладём в делегат метод пузырька
            bubble(b);                         // Сортируем через делегат
            Console.WriteLine("Пузырьком: " + string.Join(", ", b)); // Печатаем результат
            int[] q = (int[])original.Clone(); // Копия под быструю
            SortDelegate quick = Quick;        // Кладём в делегат метод быстрой
            quick(q);                          // Сортируем через делегат
            Console.WriteLine("Быстрая:   " + string.Join(", ", q)); // Печатаем результат
        }
    }
}