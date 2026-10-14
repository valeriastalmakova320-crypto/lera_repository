using System; // Нужно для базовых функций программы
using System.Windows; // Нужно для работы с окнами
using System.Windows.Media.Imaging; // Нужно для работы с изображениями
using Microsoft.Win32; // Нужно для диалога выбора файла

namespace задание_1 // Имя проекта
{
    public partial class MainWindow : Window // Класс окна программы
    {
        private double currentScale = 1.0; // Переменная хранит текущий масштаб, изначально 1 (100 процентов)

        public MainWindow() // Выполняется при запуске программы
        {
            InitializeComponent(); // Команда собирает окно из того, что нарисовано в XAML
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Открыть
        {
            OpenFileDialog openDialog = new OpenFileDialog(); // Создаем диалог выбора файла
            openDialog.Filter = "Изображения (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|Все файлы (*.*)|*.*"; // Настраиваем фильтр только для картинок
            if (openDialog.ShowDialog() == true) // Открываем диалог и проверяем, выбрал ли пользователь файл
            {
                BitmapImage bitmap = new BitmapImage(); // Создаем объект для хранения картинки
                bitmap.BeginInit(); // Начинаем настройку картинки
                bitmap.UriSource = new Uri(openDialog.FileName); // Указываем путь к выбранному файлу
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // Загружаем картинку сразу, чтобы файл не был заблокирован
                bitmap.EndInit(); // Заканчиваем настройку
                imgView.Source = bitmap; // Показываем картинку в элементе Image
                currentScale = 1.0; // Сбрасываем масштаб на 100 процентов
                imgView.Width = bitmap.PixelWidth; // Ставим ширину картинки равной её настоящей ширине
                imgView.Height = bitmap.PixelHeight; // Ставим высоту картинки равной её настоящей высоте
            }
        }

        private void BtnZoomIn_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Увеличить
        {
            if (imgView.Source != null) // Проверяем, что картинка уже загружена
            {
                currentScale = currentScale * 1.2; // Увеличиваем масштаб в 1.2 раза
                imgView.Width = imgView.Width * 1.2; // Увеличиваем ширину картинки
                imgView.Height = imgView.Height * 1.2; // Увеличиваем высоту картинки
            }
        }

        private void BtnZoomOut_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Уменьшить
        {
            if (imgView.Source != null) // Проверяем, что картинка уже загружена
            {
                currentScale = currentScale / 1.2; // Уменьшаем масштаб в 1.2 раза
                imgView.Width = imgView.Width / 1.2; // Уменьшаем ширину картинки
                imgView.Height = imgView.Height / 1.2; // Уменьшаем высоту картинки
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Сбросить
        {
            if (imgView.Source != null) // Проверяем, что картинка уже загружена
            {
                BitmapImage bitmap = (BitmapImage)imgView.Source; // Получаем текущую картинку
                imgView.Width = bitmap.PixelWidth; // Возвращаем настоящую ширину картинки
                imgView.Height = bitmap.PixelHeight; // Возвращаем настоящую высоту картинки
                currentScale = 1.0; // Сбрасываем масштаб на 100 процентов
            }
        }
    }
}