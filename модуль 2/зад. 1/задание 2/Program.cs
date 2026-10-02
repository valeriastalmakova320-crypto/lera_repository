using System;
namespace Module2Task2
{
    // Базовый класс — геометрическая фигура
    class Shape
    {
        // Виртуальные методы — заготовки, которые переопределят наследники
        public virtual double Area() { return 0; }
        public virtual double Perimeter() { return 0; }
    }
    // Класс Круг — наследуется от Shape
    class Circle : Shape
    {
        // Поле для радиуса
        public double Radius;
        // Конструктор — принимает радиус при создании
        public Circle(double r) { Radius = r; }
        // Площадь круга: π * R²
        public override double Area() { return Math.PI * Radius * Radius; }
        // Периметр круга (длина окружности): 2 * π * R
        public override double Perimeter() { return 2 * Math.PI * Radius; }
    }
    // Класс Прямоугольник — наследуется от Shape
    class Rectangle : Shape
    {
        // Поля для сторон
        public double Width;
        public double Height;
        // Конструктор — принимает ширину и высоту
        public Rectangle(double w, double h) { Width = w; Height = h; }
        // Площадь прямоугольника: ширина * высота
        public override double Area() { return Width * Height; }
        // Периметр прямоугольника: 2 * (ширина + высота)
        public override double Perimeter() { return 2 * (Width + Height); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Создаём круг радиусом 5
            Circle circle = new Circle(5);
            Console.WriteLine("Круг:");
            Console.WriteLine("Площадь: " + circle.Area().ToString("F2"));
            Console.WriteLine("Периметр: " + circle.Perimeter().ToString("F2"));
            Console.WriteLine();
            // Создаём прямоугольник 4 на 6
            Rectangle rect = new Rectangle(4, 6);
            Console.WriteLine("Прямоугольник:");
            Console.WriteLine("Площадь: " + rect.Area().ToString("F2"));
            Console.WriteLine("Периметр: " + rect.Perimeter().ToString("F2"));
        }
    }
}