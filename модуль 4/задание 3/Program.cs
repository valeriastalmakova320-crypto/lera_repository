using System;
namespace InterfaceTasks
{
    // Интерфейс IStudent — общий список правил для всех студентов
    public interface IStudent
    {
        // Свойство Name — нужно, чтобы у любого студента можно было узнать имя
        string Name { get; }
        // Метод GetAverageGrade — нужен, чтобы узнать средний балл студента
        double GetAverageGrade();
        // Метод GetCourseInfo — нужен, чтобы узнать, на каком курсе учится студент
        string GetCourseInfo();
    }
    // Класс FirstYearStudent — описывает студента 1 курса и выполняет правила интерфейса IStudent
    public class FirstYearStudent : IStudent
    {
        // Свойство Name — хранит имя студента
        public string Name { get; set; }
        // Поля Grade1, Grade2, Grade3 — хранят три оценки студента
        public double Grade1, Grade2, Grade3;
        // Конструктор — задает имя и три оценки при создании
        public FirstYearStudent(string name, double g1, double g2, double g3)
        {
            // Записываем имя
            Name = name;
            // Записываем первую оценку
            Grade1 = g1;
            // Записываем вторую оценку
            Grade2 = g2;
            // Записываем третью оценку
            Grade3 = g3;
        }
        // Метод GetAverageGrade — считает средний балл
        public double GetAverageGrade()
        {
            // Складываем три оценки и делим на три
            return (Grade1 + Grade2 + Grade3) / 3;
        }
        // Метод GetCourseInfo — показывает, на каком курсе учится студент
        public string GetCourseInfo()
        {
            // Возвращаем текст с информацией о курсе
            return "Студент 1 курса";
        }
    }
    // Класс ThirdYearStudent — описывает студента 3 курса и выполняет правила интерфейса IStudent
    public class ThirdYearStudent : IStudent
    {
        // Свойство Name — хранит имя студента
        public string Name { get; set; }
        // Поля Grade1, Grade2 — хранят две оценки студента
        public double Grade1, Grade2;
        // Конструктор — задает имя и две оценки при создании
        public ThirdYearStudent(string name, double g1, double g2)
        {
            // Записываем имя
            Name = name;
            // Записываем первую оценку
            Grade1 = g1;
            // Записываем вторую оценку
            Grade2 = g2;
        }
        // Метод GetAverageGrade — считает средний балл
        public double GetAverageGrade()
        {
            // Складываем две оценки и делим на два
            return (Grade1 + Grade2) / 2;
        }
        // Метод GetCourseInfo — показывает, на каком курсе учится студент
        public string GetCourseInfo()
        {
            // Возвращаем текст с информацией о курсе
            return "Студент 3 курса";
        }
    }
    // Главный класс программы
    class Program
    {
        // Точка входа — отсюда начинается работа программы
        static void Main(string[] args)
        {
            // Создаем студента 1 курса с именем Алексей и оценками 8, 9, 7
            IStudent student1 = new FirstYearStudent("Алексей", 8, 9, 7);
            // Выводим информацию о первом студенте
            Console.WriteLine($"Студент: {student1.Name}, Инфо: {student1.GetCourseInfo()}, Средний балл: {student1.GetAverageGrade():F2}");
            // Создаем студента 3 курса с именем Ольга и оценками 9, 10
            IStudent student2 = new ThirdYearStudent("Ольга", 9, 10);
            // Выводим информацию о втором студенте
            Console.WriteLine($"Студент: {student2.Name}, Инфо: {student2.GetCourseInfo()}, Средний балл: {student2.GetAverageGrade():F2}");
            // Пауза, чтобы консоль не закрылась сразу
            Console.ReadLine();
        }
    }
}