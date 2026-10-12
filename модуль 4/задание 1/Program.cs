using System;
namespace InterfaceTasks
{
    // Интерфейс IShape — общий список правил для всех фигур, чтобы с ними работать одинаково
    public interface IShape
    {
        // Метод GetArea — нужен, чтобы каждая фигура считала свою площадь
        double GetArea();
        // Метод GetPerimeter — нужен, чтобы каждая фигура считала свой периметр
        double GetPerimeter();
    }
    // Класс Circle — описывает круг и выполняет правила интерфейса IShape
    public class Circle : IShape
    {
        // Свойство Radius — хранит радиус круга для расчетов
        public double Radius { get; set; }
        // Конструктор — задает радиус при создании круга
        public Circle(double radius)
        {
            // Записываем радиус, чтобы использовать его в формулах
            Radius = radius;
        }
        // Метод GetArea — считает площадь круга
        public double GetArea()
        {
            // Формула площади круга: Пи умножить на радиус два раза
            return Math.PI * Radius * Radius;
        }
        // Метод GetPerimeter — считает периметр (длину окружности)
        public double GetPerimeter()
        {
            // Формула: 2 умножить на Пи и на радиус
            return 2 * Math.PI * Radius;
        }
    }
    // Класс Rectangle — описывает прямоугольник и выполняет правила интерфейса IShape
    public class Rectangle : IShape
    {
        // Свойство Width — хранит ширину прямоугольника
        public double Width { get; set; }
        // Свойство Height — хранит высоту прямоугольника
        public double Height { get; set; }
        // Конструктор — задает ширину и высоту при создании
        public Rectangle(double width, double height)
        {
            // Записываем ширину
            Width = width;

            // Записываем высоту
            Height = height;
        }
        // Метод GetArea — считает площадь прямоугольника
        public double GetArea()
        {
            // Формула: ширина умножить на высоту
            return Width * Height;
        }
        // Метод GetPerimeter — считает периметр прямоугольника
        public double GetPerimeter()
        {
            // Формула: 2 умножить на (ширина плюс высота)
            return 2 * (Width + Height);
        }
    }
    // Класс Triangle — описывает треугольник и выполняет правила интерфейса IShape
    public class Triangle : IShape
    {
        // Свойства A, B, C — хранят три стороны треугольника
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        // Конструктор — задает три стороны при создании
        public Triangle(double a, double b, double c)
        {
            // Записываем первую сторону
            A = a;
            // Записываем вторую сторону
            B = b;
            // Записываем третью сторону
            C = c;
        }
        // Метод GetArea — считает площадь треугольника
        public double GetArea()
        {
            // Сначала считаем полупериметр, он нужен для формулы
            double p = (A + B + C) / 2;

            // Формула Герона: так находят площадь по трем сторонам
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }
        // Метод GetPerimeter — считает периметр треугольника
        public double GetPerimeter()
        {
            // Просто складываем все три стороны
            return A + B + C;
        }
    }
    // Главный класс программы
    class Program
    {
        // Точка входа — отсюда начинается работа программы
        static void Main(string[] args)
        {
            // Создаем круг радиусом 5 для проверки
            IShape circle = new Circle(5);
            // Выводим площадь и периметр круга
            Console.WriteLine($"Круг: Площадь = {circle.GetArea():F2}, Периметр = {circle.GetPerimeter():F2}");
            // Создаем прямоугольник 4 на 6 для проверки
            IShape rect = new Rectangle(4, 6);
            // Выводим площадь и периметр прямоугольника
            Console.WriteLine($"Прямоугольник: Площадь = {rect.GetArea()}, Периметр = {rect.GetPerimeter()}");
            // Создаем треугольник со сторонами 3, 4, 5
            IShape triangle = new Triangle(3, 4, 5);
            // Выводим площадь и периметр треугольника
            Console.WriteLine($"Треугольник: Площадь = {triangle.GetArea()}, Периметр = {triangle.GetPerimeter()}");
            // Пауза, чтобы консоль не закрылась сразу
            Console.ReadLine();
        }
    }
}