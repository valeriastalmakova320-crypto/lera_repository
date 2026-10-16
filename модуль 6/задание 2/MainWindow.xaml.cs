// Подключение базовых инструментов. Без них не будет Convert, Exception.
using System;
// Подключение DataTable. Нужна таблица в памяти для чтения из базы.
using System.Data;
// Подключение SQL-инструментов. Без них не подключиться к базе.
using System.Data.SqlClient;
// Подключение WPF-окон. Нужны Window и MessageBox.
using System.Windows;
// Подключение элементов управления. Нужны DataGrid, TextBox.
using System.Windows.Controls;
// "Папка" класса. Должна совпадать с именем проекта, иначе VS не соберёт.
namespace задание_2
{
    // Класс окна. Здесь вся логика - обработка кнопок и работа с базой.
    public partial class MainWindow : Window
    {
        // Строка с адресом базы. Без неё программа не знает, к какой базе подключаться.
        // @ перед строкой. Чтобы символ \ не ломал путь.
        // |DataDirectory|. Обозначает папку с программой, чтобы путь работал у любого.
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                            AttachDbFilename=|DataDirectory|\TasksDB.mdf;
                                            Integrated Security=True";
        // Id выбранной задачи. Нужен для обновления и удаления именно её.
        // -1 значит "никто не выбран" - стартовое значение.
        private int selectedId = -1;
        // Конструктор окна. Запускается сам при старте, готовит программу к работе.
        public MainWindow()
        {
            // Команда создать элементы из XAML. Без неё окно будет пустым.
            InitializeComponent();
            // Загрузка задач. Чтобы пользователь сразу видел список из базы.
            LoadTasks();
        }
        // Метод чтения всех задач. Показывать актуальный список в таблице.
        private void LoadTasks()
        {
            // using - открыть и потом закрыть соединение. Чтобы не забыть закрыть и не занять базу.
            // SqlConnection - канал связи с базой. Без него никак не отправить запрос.
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // try/catch. Чтобы при ошибке не рухнула программа, а показала сообщение.
                try
                {
                    // Открыть соединение. Без открытой линии база запрос не примет.
                    conn.Open();
                    // SELECT - запрос "прочитать". Получить список задач из таблицы.
                    // [Table] в квадратных скобках. Потому что Table - служебное слово в SQL.
                    string query = "SELECT Id, Title, Description, Status FROM [Table]";
                    // Адаптер. Он сам перевозит данные из базы в память.
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    // DataTable. Временная таблица в памяти, куда сложить прочитанное.
                    DataTable table = new DataTable();
                    // Залить данные. Чтобы таблица в памяти наполнилась строками из базы.
                    adapter.Fill(table);
                    // Привязать таблицу к DataGrid. Чтобы пользователь увидел данные на экране.
                    dgTasks.ItemsSource = table.DefaultView;
                }
                // catch - сработает при ошибке. Показать пользователю, что пошло не так.
                catch (Exception ex)
                {
                    // MessageBox. Сообщить пользователю текст ошибки.
                    MessageBox.Show("Ошибка загрузки: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Добавить". Сохранить новую задачу в базу.
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Проверка пустых полей. Не сохранять задачу без названия и статуса.
            if (txtTitle.Text == "" || txtStatus.Text == "")
            {
                MessageBox.Show("Заполните название и статус!");
                // return. Прекратить работу метода, раз данные неполные.
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // INSERT - запрос "добавить строку". Сохранить новую задачу.
                    // @Title и т.д. Безопасно подставить данные, без ошибок с кавычками.
                    string query = "INSERT INTO [Table] (Title, Description, Status) VALUES (@Title, @Description, @Status)";
                    // SqlCommand. Упаковать запрос и отправить его в базу.
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Подстановка названия. Чтобы база получила текст из поля txtTitle.
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    // Подстановка описания. Чтобы база получила текст из txtDescription.
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    // Подстановка статуса. Чтобы база получила текст из txtStatus.
                    cmd.Parameters.AddWithValue("@Status", txtStatus.Text);
                    // ExecuteNonQuery. Выполнить запрос, ответ не нужен (для INSERT/UPDATE/DELETE).
                    cmd.ExecuteNonQuery();
                    // Очистка поля названия. Чтобы пользователь мог сразу ввести следующую.
                    txtTitle.Text = "";
                    // Очистка поля описания.
                    txtDescription.Text = "";
                    // Очистка поля статуса.
                    txtStatus.Text = "";
                    // Перезагрузка списка. Чтобы новая задача появилась в таблице.
                    LoadTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Обновить". Сохранить изменения выбранной задачи.
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя обновить то, что не выбрано.
            if (dgTasks.SelectedItem == null)
            {
                MessageBox.Show("Выберите задачу в таблице!");
                return;
            }
            // Проверка полей. Не сохранять пустые данные.
            if (txtTitle.Text == "" || txtStatus.Text == "")
            {
                MessageBox.Show("Заполните название и статус!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // UPDATE - запрос "обновить". Поменять данные выбранной задачи.
                    // WHERE Id=@Id. Чтобы обновилась только одна строка, а не все.
                    string query = "UPDATE [Table] SET Title=@Title, Description=@Description, Status=@Status WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Status", txtStatus.Text);
                    // Id выбранного. Указать базе, какую именно строку обновлять.
                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.ExecuteNonQuery();
                    // Очистка полей. Чтобы можно было сразу выбрать другую задачу.
                    txtTitle.Text = "";
                    txtDescription.Text = "";
                    txtStatus.Text = "";
                    // Сброс выбранного Id. Иначе случайно снова обновим старую.
                    selectedId = -1;
                    // Перезагрузка. Чтобы изменения сразу отразились в таблице.
                    LoadTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обновления: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Удалить". Убрать выбранную задачу из базы.
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя удалить то, что не выбрано.
            if (dgTasks.SelectedItem == null)
            {
                MessageBox.Show("Выберите задачу в таблице!");
                return;
            }
            // Подтверждение. Чтобы случайно не удалить данные одним кликом.
            MessageBoxResult result = MessageBox.Show("Точно удалить?", "Подтверждение", MessageBoxButton.YesNo);
            // Проверка ответа. Если пользователь нажал "Нет" - ничего не делаем.
            if (result != MessageBoxResult.Yes) return;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // DELETE - запрос "удалить". Убрать строку из базы.
                    // WHERE Id=@Id. Удалить только выбранную, а не все.
                    string query = "DELETE FROM [Table] WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.ExecuteNonQuery();
                    selectedId = -1;
                    LoadTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Перечитать". Дать пользователю кнопку "перечитать список".
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // Вызов загрузки. Перечитать базу и заново показать список.
            LoadTasks();
        }
        // Обработчик клика по строке таблицы. Перенести данные из строки в поля для правки.
        private void dgTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка, что выбрана строка. Убедиться, что есть что переносить.
            if (dgTasks.SelectedItem is DataRowView row)
            {
                // Запомнить Id. Пригодится при обновлении и удалении.
                selectedId = Convert.ToInt32(row["Id"]);
                // Название в поле. Чтобы пользователь видел и мог править.
                txtTitle.Text = row["Title"].ToString();
                // Описание в поле. Чтобы можно было править.
                txtDescription.Text = row["Description"].ToString();
                // Статус в поле. Чтобы можно было править.
                txtStatus.Text = row["Status"].ToString();
            }
        }
    }
}