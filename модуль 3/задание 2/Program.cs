using System; 
namespace Task2 
{
    public class NotifyArgs : EventArgs // Данные события
    {
        public string Message; // Храним текст уведомления
        public NotifyArgs(string msg) { Message = msg; } // Запоминаем текст
    }
    public class Notification // Рассылает уведомления
    {
        public event EventHandler<NotifyArgs> OnSms, OnCall, OnEmail; // Три события для подписки
        public void SendSms(string m) { Console.WriteLine($"СМС: {m}"); OnSms?.Invoke(this, new NotifyArgs(m)); }   // Шлём СМС и оповещаем подписчиков
        public void SendCall(string m) { Console.WriteLine($"Звонок: {m}"); OnCall?.Invoke(this, new NotifyArgs(m)); }  // Шлём звонок и оповещаем подписчиков
        public void SendEmail(string m) { Console.WriteLine($"Письмо: {m}"); OnEmail?.Invoke(this, new NotifyArgs(m)); } // Шлём письмо и оповещаем подписчиков
    }
    class Program // Точка входа
    {
        static void Main() // Запуск программы
        {
            var n = new Notification(); // Создаём уведомитель
            n.OnSms += (s, e) => Console.WriteLine($"[СМС] Получено: {e.Message}");    // Подписка: что делать при СМС
            n.OnCall += (s, e) => Console.WriteLine($"[Звонок] Входящий: {e.Message}"); // Подписка: что делать при звонке
            n.OnEmail += (s, e) => Console.WriteLine($"[Письмо] Новое: {e.Message}");    // Подписка: что делать при письме
            n.SendSms("Привет!");           // Запускаем СМС
            n.SendCall("Мама звонит");      // Запускаем звонок
            n.SendEmail("Письмо с работы"); // Запускаем письмо
        }
    }
}