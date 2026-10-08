using System; 
namespace Task1 // Группируем классы
{
    public delegate double AreaCalculator(); // Тип-ссылка на метод площади
    public abstract class Figure // Общий шаблон фигуры
    {
        public abstract string Name { get; } // Требуем имя у каждой фигуры
        public abstract double GetArea(); // Требуем метод площади
        public AreaCalculator GetAreaDelegate() => GetArea; // Отдаём метод площади как делегат
    }
    public class Circle : Figure // Круг
    {
        public override string Name => "Круг"; // Имя фигуры
        public double Radius; // Храним радиус
        public Circle(double r) { Radius = r; } // Запоминаем радиус
        public override double GetArea() => Math.PI * Radius * Radius; // Считаем площадь πr²
    }
    public class Rectangle : Figure // Прямоугольник
    {
        public override string Name => "Прямоугольник"; // Имя фигуры
        public double Width, Height; // Храним стороны
        public Rectangle(double w, double h) { Width = w; Height = h; } // Запоминаем стороны
        public override double GetArea() => Width * Height; // Считаем площадь a*b
    }
    public class Triangle : Figure // Треугольник
    {
        public override string Name => "Треугольник"; // Имя фигуры
        public double Base, Height; // Храним основание и высоту
        public Triangle(double b, double h) { Base = b; Height = h; } // Запоминаем основание и высоту
        public override double GetArea() => 0.5 * Base * Height; // Считаем площадь ½ah
    }
    class Program // Точка входа
    {
        static void Main() // Запуск программы
        {
            Figure[] figures = { new Circle(5), new Rectangle(4, 6), new Triangle(10, 3) }; // Массив из трёх фигур
            foreach (Figure fig in figures) // Перебираем фигуры
            {
                AreaCalculator calc = fig.GetAreaDelegate(); // Берём делегат фигуры
                Console.WriteLine($"{fig.Name}: {calc():F2}"); // Вызываем через делегат и печатаем
            }
        }
    }
}