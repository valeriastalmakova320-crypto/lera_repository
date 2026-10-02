using System;
namespace Module2Task6
{
    // Класс Студент со свойствами
    class Student
    {
        // Свойства: имя, фамилия, возраст, средний балл
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public double AverageMark { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Первый студент
            Student s1 = new Student();
            s1.Name = "Даниил";
            s1.Surname = "Ковалёв";
            s1.Age = 19;
            s1.AverageMark = 7.4;
            // Второй студент
            Student s2 = new Student();
            s2.Name = "Мария";
            s2.Surname = "Петрова";
            s2.Age = 20;
            s2.AverageMark = 6.8;
            // Выводим информацию о первом студенте
            Console.WriteLine("Студент 1:");
            Console.WriteLine("Имя: " + s1.Name);
            Console.WriteLine("Фамилия: " + s1.Surname);
            Console.WriteLine("Возраст: " + s1.Age);
            Console.WriteLine("Средний балл: " + s1.AverageMark);
            Console.WriteLine();
            // Выводим информацию о втором студенте
            Console.WriteLine("Студент 2:");
            Console.WriteLine("Имя: " + s2.Name);
            Console.WriteLine("Фамилия: " + s2.Surname);
            Console.WriteLine("Возраст: " + s2.Age);
            Console.WriteLine("Средний балл: " + s2.AverageMark);
        }
    }
}