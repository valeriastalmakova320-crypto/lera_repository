using System; // Нужно для базовых функций программы
using System.IO; // Нужно для работы с файлами
using System.Windows; // Нужно для работы с окнами
using Microsoft.Win32; // Нужно для диалогов открытия и сохранения файлов

namespace задание_1 // Имя проекта
{
    public partial class MainWindow : Window // Класс окна программы
    {
        public MainWindow() // Выполняется при запуске программы
        {
            InitializeComponent(); // Команда собирает окно из того, что нарисовано в XAML
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Открыть
        {
            OpenFileDialog openDialog = new OpenFileDialog(); // Создаем диалог выбора файла для открытия
            openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"; // Настраиваем фильтр файлов, показываем только txt
            if (openDialog.ShowDialog() == true) // Открываем диалог и проверяем, выбрал ли пользователь файл
            {
                txtEditor.Text = File.ReadAllText(openDialog.FileName); // Читаем весь текст из файла и вставляем в текстовое поле
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Сохранить
        {
            SaveFileDialog saveDialog = new SaveFileDialog(); // Создаем диалог выбора места для сохранения файла
            saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"; // Настраиваем фильтр файлов
            if (saveDialog.ShowDialog() == true) // Открываем диалог и проверяем, выбрал ли пользователь место
            {
                File.WriteAllText(saveDialog.FileName, txtEditor.Text); // Записываем весь текст из поля в выбранный файл
            }
        }
    }
}