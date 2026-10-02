using System;
namespace Module2Task5
{
    // Класс TemperatureSensor — датчик температуры
    class TemperatureSensor
    {
        // Приватное поле — текущая температура
        private double temperature;
        // Событие, на которое могут подписаться другие классы.
        // Action<double> означает: обработчик принимает одно число типа double.
        public event Action<double> TemperatureChanged;
        // Метод для установки новой температуры
        public void SetTemperature(double newTemp)
        {
            // Сохраняем новое значение
            temperature = newTemp;
            Console.WriteLine("Датчик: температура изменилась на " + temperature);
            // Если на событие кто-то подписан — вызываем его
            if (TemperatureChanged != null)
            {
                TemperatureChanged(temperature);
            }
        }
    }

    // Класс Thermostat — термостат, который реагирует на температуру
    class Thermostat
    {
        // Метод-обработчик — вызывается, когда срабатывает событие
        public void OnTemperatureChanged(double temp)
        {
            // Если температура ниже 20 — включаем отопление
            if (temp < 20)
            {
                Console.WriteLine("Термостат: отопление ВКЛЮЧЕНО");
            }
            else
            {
                Console.WriteLine("Термостат: отопление ВЫКЛЮЧЕНО");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Создаём датчик и термостат
            TemperatureSensor sensor = new TemperatureSensor();
            Thermostat thermostat = new Thermostat();
            // Подписываем термостат на событие датчика
            sensor.TemperatureChanged += thermostat.OnTemperatureChanged;
            // Меняем температуру — термостат реагирует каждый раз
            sensor.SetTemperature(18);
            sensor.SetTemperature(22);
            sensor.SetTemperature(15);
        }
    }
}