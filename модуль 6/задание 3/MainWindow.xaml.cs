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
namespace Задание3
{
    // Класс окна. Здесь вся логика - обработка кнопок и работа с базой.
    public partial class MainWindow : Window
    {
        // Строка с адресом базы. Без неё программа не знает, к какой базе подключаться.
        // @ перед строкой. Чтобы символ \ не ломал путь.
        // |DataDirectory|. Обозначает папку с программой, чтобы путь работал у любого.
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                            AttachDbFilename=|DataDirectory|\LibraryDB.mdf;
                                            Integrated Security=True";
        // Id выбранной книги. Нужен для обновления, аренды, возврата и удаления.
        // -1 значит "никто не выбран" - стартовое значение.
        private int selectedId = -1;
        // Конструктор окна. Запускается сам при старте, готовит программу к работе.
        public MainWindow()
        {
            // Команда создать элементы из XAML. Без неё окно будет пустым.
            InitializeComponent();
            // Загрузка книг. Чтобы пользователь сразу видел список из базы.
            LoadBooks("SELECT Id, Title, Author, Year, Status FROM [Table]");
        }
        // Метод чтения книг по SQL-запросу. Показывать результат в таблице.
        private void LoadBooks(string query)
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
                    // Адаптер. Он сам перевозит данные из базы в память.
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    // DataTable. Временная таблица в памяти, куда сложить прочитанное.
                    DataTable table = new DataTable();
                    // Залить данные. Чтобы таблица в памяти наполнилась строками из базы.
                    adapter.Fill(table);
                    // Привязать таблицу к DataGrid. Чтобы пользователь увидел данные на экране.
                    dgBooks.ItemsSource = table.DefaultView;
                }
                // catch - сработает при ошибке. Показать пользователю, что пошло не так.
                catch (Exception ex)
                {
                    // MessageBox. Сообщить пользователю текст ошибки.
                    MessageBox.Show("Ошибка загрузки: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Найти". Искать книги по названию или автору.
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Проверка пустого поля поиска. Нечего искать - ругаемся.
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Введите что искать!");
                return;
            }
            // LIKE с процентами. Означает "содержит такую подстроку".
            // @Search - параметр, куда подставим текст поиска.
            string query = "SELECT Id, Title, Author, Year, Status FROM [Table] WHERE Title LIKE @Search OR Author LIKE @Search";
            // using - открыть и закрыть соединение.
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // SqlCommand. Упаковать запрос и отправить его в базу.
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Подстановка шаблона поиска. % вокруг - значит "содержит".
                    cmd.Parameters.AddWithValue("@Search", "%" + txtSearch.Text + "%");
                    // Адаптер и DataTable - для чтения результата.
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    // Показать результат в таблице.
                    dgBooks.ItemsSource = table.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка поиска: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Показать всё". Сбросить поиск и показать все книги.
        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            // Очистить поле поиска.
            txtSearch.Text = "";
            // Перечитать все книги.
            LoadBooks("SELECT Id, Title, Author, Year, Status FROM [Table]");
        }
        // Обработчик кнопки "Добавить книгу". Сохранить новую книгу в базу.
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Проверка пустых полей. Нельзя добавить книгу без названия и автора.
            if (txtTitle.Text == "" || txtAuthor.Text == "")
            {
                MessageBox.Show("Заполните название и автора!");
                return;
            }
            // Переменная для года. Чтобы превратить текст в число и хранить как число.
            int year;
            // TryParse. Проверить, что пользователь ввёл число, а не буквы.
            if (!int.TryParse(txtYear.Text, out year))
            {
                MessageBox.Show("Год должен быть числом!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // INSERT - запрос "добавить строку". Сохранить новую книгу.
                    // Вместо @Status ставим сразу "В наличии" - новая книга доступна.
                    string query = "INSERT INTO [Table] (Title, Author, Year, Status) VALUES (@Title, @Author, @Year, @Status)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Подстановка названия.
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    // Подстановка автора.
                    cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                    // Подстановка года.
                    cmd.Parameters.AddWithValue("@Year", year);
                    // Статус новой книги всегда "В наличии".
                    cmd.Parameters.AddWithValue("@Status", "В наличии");
                    // ExecuteNonQuery. Выполнить запрос, ответ не нужен.
                    cmd.ExecuteNonQuery();
                    // Очистка полей. Чтобы пользователь мог сразу ввести следующую.
                    txtTitle.Text = "";
                    txtAuthor.Text = "";
                    txtYear.Text = "";
                    // Перезагрузка списка. Чтобы новая книга появилась в таблице.
                    LoadBooks("SELECT Id, Title, Author, Year, Status FROM [Table]");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Обновить запись". Сохранить изменения выбранной книги.
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя обновить то, что не выбрано.
            if (dgBooks.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу в таблице!");
                return;
            }
            // Проверка полей. Не сохранять пустые данные.
            if (txtTitle.Text == "" || txtAuthor.Text == "")
            {
                MessageBox.Show("Заполните название и автора!");
                return;
            }
            // Преобразование года в число.
            int year;
            if (!int.TryParse(txtYear.Text, out year))
            {
                MessageBox.Show("Год должен быть числом!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // UPDATE - запрос "обновить". Поменять название, автора, год.
                    // WHERE Id=@Id. Обновить только выбранную книгу.
                    string query = "UPDATE [Table] SET Title=@Title, Author=@Author, Year=@Year WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                    cmd.Parameters.AddWithValue("@Year", year);
                    // Id выбранной книги.
                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.ExecuteNonQuery();
                    // Очистка полей.
                    txtTitle.Text = "";
                    txtAuthor.Text = "";
                    txtYear.Text = "";
                    // Сброс выбранного Id.
                    selectedId = -1;
                    // Перезагрузка. Чтобы изменения появились в таблице.
                    LoadBooks("SELECT Id, Title, Author, Year, Status FROM [Table]");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обновления: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Арендовать". Поставить статус "В аренде" выбранной книге.
        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя арендовать то, что не выбрано.
            if (dgBooks.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу в таблице!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // UPDATE - обновить только статус. WHERE Id=@Id - только выбранную.
                    string query = "UPDATE [Table] SET Status=@Status WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Новый статус - "В аренде".
                    cmd.Parameters.AddWithValue("@Status", "В аренде");
                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.ExecuteNonQuery();
                    // Обновить таблицу на экране.
                    LoadBooks("SELECT Id, Title, Author, Year, Status FROM [Table]");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка аренды: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Вернуть". Поставить статус "В наличии" выбранной книге.
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя вернуть то, что не выбрано.
            if (dgBooks.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу в таблице!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // UPDATE - обновить только статус. WHERE Id=@Id - только выбранную.
                    string query = "UPDATE [Table] SET Status=@Status WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Новый статус - "В наличии".
                    cmd.Parameters.AddWithValue("@Status", "В наличии");
                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.ExecuteNonQuery();
                    LoadBooks("SELECT Id, Title, Author, Year, Status FROM [Table]");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка возврата: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Удалить". Убрать выбранную книгу из базы.
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя удалить то, что не выбрано.
            if (dgBooks.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу в таблице!");
                return;
            }
            // Подтверждение. Чтобы случайно не удалить книгу одним кликом.
            MessageBoxResult result = MessageBox.Show("Точно удалить?", "Подтверждение", MessageBoxButton.YesNo);
            // Если не "Да" - выходим.
            if (result != MessageBoxResult.Yes) return;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // DELETE - удалить строку. WHERE Id=@Id - только выбранную.
                    string query = "DELETE FROM [Table] WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.ExecuteNonQuery();
                    selectedId = -1;
                    LoadBooks("SELECT Id, Title, Author, Year, Status FROM [Table]");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }
        // Обработчик клика по строке таблицы. Перенести данные книги в поля.
        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка, что выбрана строка.
            if (dgBooks.SelectedItem is DataRowView row)
            {
                // Запомнить Id. Пригодится при обновлении, аренде, возврате и удалении.
                selectedId = Convert.ToInt32(row["Id"]);
                // Название в поле.
                txtTitle.Text = row["Title"].ToString();
                // Автор в поле.
                txtAuthor.Text = row["Author"].ToString();
                // Год в поле.
                txtYear.Text = row["Year"].ToString();
            }
        }
    }
}