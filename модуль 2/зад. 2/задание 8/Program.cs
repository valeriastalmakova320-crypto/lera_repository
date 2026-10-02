using System;
namespace Module2Task8
{
    // Базовый класс — общая геометрическая фигура
    class Shape
    {
        // Виртуальные методы — заготовки, которые переопределят наследники
        public virtual double Area() { return 0; }
        public virtual double Perimeter() { return 0; }
    }
    // Класс Круг — наследуется от Shape
    class Circle : Shape
    {
        // Поле радиуса
        public double Radius;
        // Конструктор — принимает радиус
        public Circle(double r) { Radius = r; }
        // Площадь круга: π * R²
        public override double Area() { return Math.PI * Radius * Radius; }

        // Периметр круга (длина окружности): 2 * π * R
        public override double Perimeter() { return 2 * Math.PI * Radius; }
    }
    // Класс Прямоугольник — наследуется от Shape
    class Rectangle : Shape
    {
        // Поля сторон
        public double Width;
        public double Height;

        // Конструктор — принимает ширину и высоту
        public Rectangle(double w, double h) { Width = w; Height = h; }
        // Площадь прямоугольника: ширина * высота
        public override double Area() { return Width * Height; }
       // Периметр прямоугольника: 2 * (ширина + высота)
        public override double Perimeter() { return 2 * (Width + Height); }
    }

    // Класс Треугольник — наследуется от Shape
    class Triangle : Shape
    {
        // Поля трёх сторон
        public double A;
        public double B;
        public double C;
        // Конструктор — принимает три стороны
        public Triangle(double a, double b, double c) { A = a; B = b; C = c; }
        // Площадь по формуле Герона: √(p * (p-a) * (p-b) * (p-c)), где p — полупериметр
        public override double Area()
        {
            double p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }
        // Периметр треугольника: сумма сторон
        public override double Perimeter() { return A + B + C; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Круг радиусом 5
            Circle circle = new Circle(5);
            Console.WriteLine("Круг: S = " + circle.Area().ToString("F2") + ", P = " + circle.Perimeter().ToString("F2"));
            // Прямоугольник 4 на 6
            Rectangle rect = new Rectangle(4, 6);
            Console.WriteLine("Прямоугольник: S = " + rect.Area().ToString("F2") + ", P = " + rect.Perimeter().ToString("F2"));
            // Треугольник со сторонами 3, 4, 5
            Triangle tri = new Triangle(3, 4, 5);
            Console.WriteLine("Треугольник: S = " + tri.Area().ToString("F2") + ", P = " + tri.Perimeter().ToString("F2"));
        }
    }
}