using System;
using System.Collections.Generic;
namespace InterfaceTasks
{
    // Интерфейс IDrawable — общий список правил для всех фигур, которые можно нарисовать
    public interface IDrawable
    {
        // Метод Draw — нужен, чтобы каждая фигура умела себя рисовать
        void Draw();
    }

    // Класс Line — описывает линию и выполняет правила интерфейса IDrawable
    public class Line : IDrawable
    {
        // Метод Draw — рисует линию
        public void Draw() => Console.WriteLine("   Рисуем линию: ----------");
    }
    // Класс CircleShape — описывает круг и выполняет правила интерфейса IDrawable
    public class CircleShape : IDrawable
    {
        // Метод Draw — рисует круг
        public void Draw() => Console.WriteLine("   Рисуем круг:   ( O )");
    }
    // Класс RectShape — описывает прямоугольник и выполняет правила интерфейса IDrawable
    public class RectShape : IDrawable
    {
        // Метод Draw — рисует прямоугольник
        public void Draw() => Console.WriteLine("   Рисуем прямоугольник: [ ]");
    }
    // Главный класс программы
    class Program
    {
        // Точка входа — отсюда начинается работа программы
        static void Main(string[] args)
        {
            // Создаем список, чтобы хранить разные фигуры в одном месте
            List<IDrawable> canvas = new List<IDrawable>();
            // Добавляем линию
            canvas.Add(new Line());
            // Добавляем круг
            canvas.Add(new CircleShape());
            // Добавляем прямоугольник
            canvas.Add(new RectShape());
            // Перебираем все фигуры в списке
            foreach (var item in canvas)
            {
                // Вызываем у каждой фигуры метод рисования
                item.Draw();
            }
            // Пауза, чтобы консоль не закрылась сразу
            Console.ReadLine();
        }
    }
}