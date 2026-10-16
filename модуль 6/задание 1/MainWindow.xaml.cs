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
namespace задание_1
{
    // Класс окна. Здесь вся логика - обработка кнопок и работа с базой.
    public partial class MainWindow : Window
    {
        // Строка с адресом базы. Без неё программа не знает, к какой базе подключаться.
        // @ перед строкой. Чтобы символ \ не ломал путь.
        // |DataDirectory|. Обозначает папку с программой, чтобы путь работал у любого.
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                            AttachDbFilename=|DataDirectory|\Database1.mdf;
                                            Integrated Security=True";
        // Id выбранного сотрудника. Нужен для редактирования и удаления именно его.
        // -1 значит "никто не выбран" - стартовое значение.
        private int selectedId = -1;
        // Конструктор окна. Запускается сам при старте, готовит программу к работе.
        public MainWindow()
        {
            // Команда создать элементы из XAML. Без неё окно будет пустым.
            InitializeComponent();
            // Загрузка сотрудников. Чтобы пользователь сразу видел список из базы.
            LoadEmployees();
        }
        // Метод чтения всех сотрудников. Показывать актуальный список в таблице.
        private void LoadEmployees()
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
                    // SELECT - запрос "прочитать". Получить список сотрудников из таблицы.
                    string query = "SELECT Id, Name, Position, Salary FROM [Table]";
                    // Адаптер. Он сам перевозит данные из базы в память.
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    // DataTable. Временная таблица в памяти, куда сложить прочитанное.
                    DataTable table = new DataTable();
                    // Залить данные. Чтобы таблица в памяти наполнилась строками из базы.
                    adapter.Fill(table);
                    // Привязать таблицу к DataGrid. Чтобы пользователь увидел данные на экране.
                    dgEmployees.ItemsSource = table.DefaultView;
                }
                // catch - сработает при ошибке. Показать пользователю, что пошло не так.
                catch (Exception ex)
                {
                    // MessageBox. Сообщить пользователю текст ошибки.
                    MessageBox.Show("Ошибка загрузки: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Добавить". Сохранить нового сотрудника в базу.
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Проверка пустых полей. Не сохранять сотрудника без имени/должности/зарплаты.
            if (txtName.Text == "" || txtPosition.Text == "" || txtSalary.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                // return. Прекратить работу метода, раз данные неполные.
                return;
            }
            // Переменная для зарплаты. Чтобы превратить текст в число и хранить как число.
            decimal salary;
            // TryParse. Проверить, что пользователь ввёл число, а не буквы.
            if (!decimal.TryParse(txtSalary.Text, out salary))
            {
                MessageBox.Show("Зарплата должна быть числом!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // INSERT - запрос "добавить строку". Сохранить нового сотрудника.
                    // @Name и т.д. Безопасно подставить данные, без ошибок с кавычками.
                    string query = "INSERT INTO [Table] (Name, Position, Salary) VALUES (@Name, @Position, @Salary)";
                    // SqlCommand. Упаковать запрос и отправить его в базу.
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Подстановка имени. Чтобы база получила текст из поля txtName.
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    // Подстановка должности. Чтобы база получила текст из txtPosition.
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                    // Подстановка зарплаты. Чтобы база получила число salary.
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    // ExecuteNonQuery. Выполнить запрос, ответ не нужен (для INSERT/UPDATE/DELETE).
                    cmd.ExecuteNonQuery();
                    // Очистка поля имени. Чтобы пользователь мог сразу ввести следующего.
                    txtName.Text = "";
                    // Очистка поля должности.
                    txtPosition.Text = "";
                    // Очистка поля зарплаты.
                    txtSalary.Text = "";
                    // Перезагрузка списка. Чтобы новый сотрудник появился в таблице.
                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Редактировать". Сохранить изменения выбранного сотрудника.
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя редактировать того, кого не выбрали.
            if (dgEmployees.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника в таблице!");
                return;
            }
            // Проверка полей. Не сохранять пустые данные.
            if (txtName.Text == "" || txtPosition.Text == "" || txtSalary.Text == "")
            {
                MessageBox.Show("Заполните поля новыми данными!");
                return;
            }
            // Преобразование зарплаты в число. Чтобы в базу ушло число, а не текст.
            decimal salary;
            if (!decimal.TryParse(txtSalary.Text, out salary))
            {
                MessageBox.Show("Зарплата должна быть числом!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // UPDATE - запрос "обновить". Поменять данные выбранного сотрудника.
                    // WHERE Id=@Id. Чтобы обновилась только одна строка, а не все.
                    string query = "UPDATE [Table] SET Name=@Name, Position=@Position, Salary=@Salary WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    // Id выбранного. Указать базе, какую именно строку обновлять.
                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.ExecuteNonQuery();
                    // Очистка полей. Чтобы можно было сразу выбрать другого сотрудника.
                    txtName.Text = "";
                    txtPosition.Text = "";
                    txtSalary.Text = "";
                    // Сброс выбранного Id. Иначе случайно снова отредактируем старого.
                    selectedId = -1;
                    // Перезагрузка. Чтобы изменения сразу отразились в таблице.
                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка редактирования: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Удалить". Убрать выбранного сотрудника из базы.
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя удалить того, кого не выбрали.
            if (dgEmployees.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника в таблице!");
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
                    // WHERE Id=@Id. Удалить только выбранного, а не всех.
                    string query = "DELETE FROM [Table] WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.ExecuteNonQuery();
                    selectedId = -1;
                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Обновить". Дать пользователю кнопку "перечитать список".
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // Вызов загрузки. Перечитать базу и заново показать список.
            LoadEmployees();
        }
        // Обработчик клика по строке таблицы. Перенести данные из строки в поля для правки.
        private void dgEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка, что выбрана строка. Убедиться, что есть что переносить.
            if (dgEmployees.SelectedItem is DataRowView row)
            {
                // Запомнить Id. Пригодится при редактировании и удалении.
                selectedId = Convert.ToInt32(row["Id"]);
                // Имя в поле. Чтобы пользователь видел и мог править.
                txtName.Text = row["Name"].ToString();
                // Должность в поле. Чтобы можно было править.
                txtPosition.Text = row["Position"].ToString();
                // Зарплата в поле. Чтобы можно было править.
                txtSalary.Text = row["Salary"].ToString();
            }
        }
    }
}