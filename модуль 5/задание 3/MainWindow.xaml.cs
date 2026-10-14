using System; // Нужно для базовых функций программы
using System.Windows; // Нужно для работы с окнами
using System.Windows.Controls; // Нужно для работы с элементами управления

namespace задание_1 // Имя проекта
{
    public partial class MainWindow : Window // Класс окна программы
    {
        public MainWindow() // Выполняется при запуске программы
        {
            InitializeComponent(); // Команда собирает окно из того, что нарисовано в XAML
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Добавить
        {
            if (txtTask.Text != "") // Проверяем, что поле ввода не пустое
            {
                listTasks.Items.Add(txtTask.Text); // Добавляем текст из поля в список задач
                txtTask.Clear(); // Очищаем поле ввода после добавления
            }
            else // Если поле пустое
            {
                MessageBox.Show("Введите текст задачи"); // Показываем сообщение пользователю
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Удалить
        {
            if (listTasks.SelectedItem != null) // Проверяем, что пользователь выбрал задачу
            {
                listTasks.Items.Remove(listTasks.SelectedItem); // Удаляем выбранную задачу из списка
            }
            else // Если задача не выбрана
            {
                MessageBox.Show("Выберите задачу для удаления"); // Показываем сообщение пользователю
            }
        }

        private void BtnDone_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Отметить выполненной
        {
            if (listTasks.SelectedItem != null) // Проверяем, что пользователь выбрал задачу
            {
                string task = listTasks.SelectedItem.ToString(); // Получаем текст выбранной задачи
                if (!task.StartsWith("[Выполнено]")) // Проверяем, не отмечена ли задача уже
                {
                    int index = listTasks.SelectedIndex; // Запоминаем номер выбранной задачи
                    listTasks.Items[index] = "[Выполнено] " + task; // Добавляем пометку к тексту задачи
                }
            }
            else // Если задача не выбрана
            {
                MessageBox.Show("Выберите задачу"); // Показываем сообщение пользователю
            }
        }
    }
}