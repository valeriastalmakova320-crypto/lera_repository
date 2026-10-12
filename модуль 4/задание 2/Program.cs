using System;
namespace InterfaceTasks
{
    // Интерфейс IProduct — общий список правил для всех товаров
    public interface IProduct
    {
        // Свойство Name — нужно, чтобы у любого товара можно было узнать название
        string Name { get; }
        // Метод GetPrice — нужен, чтобы у любого товара можно было узнать стоимость
        decimal GetPrice();
        // Метод GetStockQuantity — нужен, чтобы у любого товара можно было узнать остаток
        int GetStockQuantity();
    }
    // Класс FoodProduct — описывает продукт питания и выполняет правила интерфейса IProduct
    public class FoodProduct : IProduct
    {
        // Свойство Name — хранит название продукта
        public string Name { get; set; }
        // Свойство PricePerKg — хранит цену за килограмм
        public decimal PricePerKg { get; set; }
        // Свойство WeightKg — хранит вес продукта в килограммах
        public int WeightKg { get; set; }
        // Конструктор — задает название, цену и вес при создании
        public FoodProduct(string name, decimal price, int weight)
        {
            // Записываем название
            Name = name;
            // Записываем цену за килограмм
            PricePerKg = price;
            // Записываем вес
            WeightKg = weight;
        }
        // Метод GetPrice — считает общую стоимость продукта
        public decimal GetPrice()
        {
            // Умножаем цену за кг на вес, потому что еда продается на вес
            return PricePerKg * WeightKg;
        }
        // Метод GetStockQuantity — показывает остаток на складе
        public int GetStockQuantity()
        {
            // Возвращаем вес, потому что для еды остаток считается в килограммах
            return WeightKg;
        }
    }
    // Класс ElectronicsProduct — описывает электронику и выполняет правила интерфейса IProduct
    public class ElectronicsProduct : IProduct
    {
        // Свойство Name — хранит название товара
        public string Name { get; set; }
        // Свойство Price — хранит цену за одну штуку
        public decimal Price { get; set; }
        // Свойство Quantity — хранит количество штук на складе
        public int Quantity { get; set; }
        // Конструктор — задает название, цену и количество при создании
        public ElectronicsProduct(string name, decimal price, int quantity)
        {
            // Записываем название
            Name = name;
            // Записываем цену
            Price = price;
            // Записываем количество
            Quantity = quantity;
        }
        // Метод GetPrice — показывает стоимость одной штуки
        public decimal GetPrice()
        {
            // Возвращаем цену, потому что техника продается поштучно
            return Price;
        }
        // Метод GetStockQuantity — показывает остаток на складе
        public int GetStockQuantity()
        {
            // Возвращаем количество штук
            return Quantity;
        }
    }
    // Главный класс программы
    class Program
    {
        // Точка входа — отсюда начинается работа программы
        static void Main(string[] args)
        {
            // Создаем яблоки по 50 рублей за кг, 10 кг
            IProduct apple = new FoodProduct("Яблоки", 50, 10);
            // Выводим информацию о яблоках
            Console.WriteLine($"Товар: {apple.Name}, Стоимость: {apple.GetPrice()} руб., Остаток: {apple.GetStockQuantity()} кг");
            // Создаем iPhone по 80000 рублей, 5 штук
            IProduct phone = new ElectronicsProduct("iPhone", 80000, 5);
            // Выводим информацию об iPhone
            Console.WriteLine($"Товар: {phone.Name}, Стоимость: {phone.GetPrice()} руб., Остаток: {phone.GetStockQuantity()} шт.");
            // Пауза, чтобы консоль не закрылась сразу
            Console.ReadLine();
        }
    }
}