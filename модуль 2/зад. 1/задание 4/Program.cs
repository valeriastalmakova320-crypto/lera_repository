using System;
namespace Module2Task4
{
    // Интерфейс — договор: у любого, кто его реализует, должен быть метод Draw()
    interface IDrawable
    {
        void Draw();
    }
    // Класс Круг — реализует интерфейс IDrawable
    class Circle : IDrawable
    {
        // Поле радиуса
        public double Radius;
        // Конструктор — принимает радиус
        public Circle(double r) { Radius = r; }
        // Реализация метода Draw — выводим информацию о круге
        public void Draw()
        {
            Console.WriteLine("Круг с радиусом " + Radius);
        }
    }
    // Класс Прямоугольник — реализует интерфейс IDrawable
    class Rectangle : IDrawable
    {
        // Поля сторон
        public double Width;
        public double Height;
        // Конструктор — принимает ширину и высоту
        public Rectangle(double w, double h) { Width = w; Height = h; }
        // Реализация метода Draw — выводим информацию о прямоугольнике
        public void Draw()
        {
            Console.WriteLine("Прямоугольник " + Width + " x " + Height);
        }
    }

    // Класс Треугольник — реализует интерфейс IDrawable
    class Triangle : IDrawable
    {
        // Поля сторон
        public double A;
        public double B;
        public double C;
        // Конструктор — принимает три стороны
        public Triangle(double a, double b, double c) { A = a; B = b; C = c; }
        // Реализация метода Draw — выводим информацию о треугольнике
        public void Draw()
        {
            Console.WriteLine("Треугольник со сторонами " + A + ", " + B + ", " + C);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Создаём массив типа IDrawable и кладём туда объекты трёх классов
            IDrawable[] figures = new IDrawable[]
            {
                new Circle(5),
                new Rectangle(4, 6),
                new Triangle(3, 4, 5)
            };
            // Проходим по массиву и вызываем Draw() у каждого элемента
            foreach (IDrawable figure in figures)
            {
                figure.Draw();
            }
        }
    }
}