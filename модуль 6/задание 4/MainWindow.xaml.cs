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
namespace Задание4
{
    // Класс окна. Здесь вся логика - обработка кнопок и работа с базой.
    public partial class MainWindow : Window
    {
        // Строка с адресом базы. Без неё программа не знает, к какой базе подключаться.
        // @ перед строкой. Чтобы символ \ не ломал путь.
        // |DataDirectory|. Обозначает папку с программой, чтобы путь работал у любого.
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                            AttachDbFilename=|DataDirectory|\FinanceDB.mdf;
                                            Integrated Security=True";
        // Id выбранной записи. Нужен для редактирования и удаления именно её.
        // -1 значит "никто не выбран" - стартовое значение.
        private int selectedId = -1;
        // Конструктор окна. Запускается сам при старте, готовит программу к работе.
        public MainWindow()
        {
            // Команда создать элементы из XAML. Без неё окно будет пустым.
            InitializeComponent();
            // Загрузка записей. Чтобы пользователь сразу видел список из базы.
            LoadRecords();
        }
        // Метод чтения всех записей и пересчёта баланса.
        private void LoadRecords()
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
                    // SELECT - запрос "прочитать". Получить список записей из таблицы.
                    // [Table] в квадратных скобках. Потому что Table - служебное слово в SQL.
                    string query = "SELECT Id, Date, Type, Category, Amount, Comment FROM [Table]";
                    // Адаптер. Он сам перевозит данные из базы в память.
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    // DataTable. Временная таблица в памяти, куда сложить прочитанное.
                    DataTable table = new DataTable();
                    // Залить данные. Чтобы таблица в памяти наполнилась строками из базы.
                    adapter.Fill(table);
                    // Привязать таблицу к DataGrid. Чтобы пользователь увидел данные на экране.
                    dgRecords.ItemsSource = table.DefaultView;
                    // После показа данных - пересчитать и показать баланс.
                    UpdateBalance(conn);
                }
                // catch - сработает при ошибке. Показать пользователю, что пошло не так.
                catch (Exception ex)
                {
                    // MessageBox. Сообщить пользователю текст ошибки.
                    MessageBox.Show("Ошибка загрузки: " + ex.Message);
                }
            }
        }
        // Метод подсчёта баланса. Баланс = сумма доходов минус сумма расходов.
        private void UpdateBalance(SqlConnection conn)
        {
            // try/catch. Чтобы при ошибке не рухнула программа, а показала сообщение.
            try
            {
                // SQL-запрос: сложить все суммы, где Type = "Доход".
                string queryIncome = "SELECT ISNULL(SUM(Amount), 0) FROM [Table] WHERE Type = @Type";
                // SqlCommand. Упаковать запрос и отправить его в базу.
                SqlCommand cmdIncome = new SqlCommand(queryIncome, conn);
                // Подстановка "Доход". Чтобы база выбрала только строки с этим типом.
                cmdIncome.Parameters.AddWithValue("@Type", "Доход");
                // ExecuteScalar. Выполнить и вернуть одно значение (число-сумму). Convert.ToDecimal - привести к числу.
                decimal income = Convert.ToDecimal(cmdIncome.ExecuteScalar());
                // SQL-запрос: сложить все суммы, где Type = "Расход".
                string queryExpense = "SELECT ISNULL(SUM(Amount), 0) FROM [Table] WHERE Type = @Type";
                // SqlCommand для расходов.
                SqlCommand cmdExpense = new SqlCommand(queryExpense, conn);
                // Подстановка "Расход".
                cmdExpense.Parameters.AddWithValue("@Type", "Расход");
                // Сумма расходов.
                decimal expense = Convert.ToDecimal(cmdExpense.ExecuteScalar());
                // Считаем баланс: доходы минус расходы.
                decimal balance = income - expense;
                // Показать баланс в надписи. ToString("0.00") - два знака после запятой.
                txtBalance.Text = balance.ToString("0.00");
            }
            // catch - сработает при ошибке.
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подсчёта баланса: " + ex.Message);
            }
        }
        // Обработчик кнопки "Добавить". Сохранить новую запись в базу.
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Проверка пустых полей. Обязательные: дата, тип, категория, сумма.
            if (txtDate.Text == "" || txtType.Text == "" || txtCategory.Text == "" || txtAmount.Text == "")
            {
                MessageBox.Show("Заполните дату, тип, категорию и сумму!");
                return;
            }
            // Переменная для суммы. Чтобы превратить текст в число и хранить как число.
            decimal amount;
            // TryParse. Проверить, что пользователь ввёл число, а не буквы.
            if (!decimal.TryParse(txtAmount.Text, out amount))
            {
                MessageBox.Show("Сумма должна быть числом!");
                return;
            }
            // Проверка типа. Разрешаем только "Доход" или "Расход" - иначе баланс сломается.
            if (txtType.Text != "Доход" && txtType.Text != "Расход")
            {
                MessageBox.Show("Тип должен быть 'Доход' или 'Расход'!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // INSERT - запрос "добавить строку". Сохранить новую запись.
                    // @Date и т.д. Безопасно подставить данные, без ошибок с кавычками.
                    string query = "INSERT INTO [Table] (Date, Type, Category, Amount, Comment) VALUES (@Date, @Type, @Category, @Amount, @Comment)";
                    // SqlCommand. Упаковать запрос и отправить его в базу.
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Подстановка даты. Чтобы база получила текст из поля txtDate.
                    cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                    // Подстановка типа.
                    cmd.Parameters.AddWithValue("@Type", txtType.Text);
                    // Подстановка категории.
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                    // Подстановка суммы.
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    // Подстановка комментария.
                    cmd.Parameters.AddWithValue("@Comment", txtComment.Text);
                    // ExecuteNonQuery. Выполнить запрос, ответ не нужен (для INSERT/UPDATE/DELETE).
                    cmd.ExecuteNonQuery();
                    // Очистка полей. Чтобы пользователь мог сразу ввести следующую запись.
                    txtDate.Text = "";
                    txtType.Text = "";
                    txtCategory.Text = "";
                    txtAmount.Text = "";
                    txtComment.Text = "";
                    // Перезагрузка списка и баланса.
                    LoadRecords();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Редактировать". Сохранить изменения выбранной записи.
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя редактировать то, что не выбрано.
            if (dgRecords.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись в таблице!");
                return;
            }
            // Проверка полей.
            if (txtDate.Text == "" || txtType.Text == "" || txtCategory.Text == "" || txtAmount.Text == "")
            {
                MessageBox.Show("Заполните дату, тип, категорию и сумму!");
                return;
            }
            // Преобразование суммы в число.
            decimal amount;
            if (!decimal.TryParse(txtAmount.Text, out amount))
            {
                MessageBox.Show("Сумма должна быть числом!");
                return;
            }
            // Проверка типа.
            if (txtType.Text != "Доход" && txtType.Text != "Расход")
            {
                MessageBox.Show("Тип должен быть 'Доход' или 'Расход'!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // UPDATE - запрос "обновить". Поменять данные выбранной записи.
                    // WHERE Id=@Id. Чтобы обновилась только одна строка, а не все.
                    string query = "UPDATE [Table] SET Date=@Date, Type=@Type, Category=@Category, Amount=@Amount, Comment=@Comment WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                    cmd.Parameters.AddWithValue("@Type", txtType.Text);
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@Comment", txtComment.Text);
                    // Id выбранной записи.
                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.ExecuteNonQuery();
                    // Очистка полей.
                    txtDate.Text = "";
                    txtType.Text = "";
                    txtCategory.Text = "";
                    txtAmount.Text = "";
                    txtComment.Text = "";
                    // Сброс выбранного Id.
                    selectedId = -1;
                    // Перезагрузка списка и баланса.
                    LoadRecords();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка редактирования: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Удалить". Убрать выбранную запись из базы.
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Проверка выбора. Нельзя удалить то, что не выбрано.
            if (dgRecords.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись в таблице!");
                return;
            }
            // Подтверждение. Чтобы случайно не удалить данные одним кликом.
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
                    LoadRecords();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }
        // Обработчик кнопки "Перечитать". Обновить список и баланс.
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // Вызов загрузки. Перечитать базу и пересчитать баланс.
            LoadRecords();
        }
        // Обработчик клика по строке таблицы. Перенести данные записи в поля.
        private void dgRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка, что выбрана строка.
            if (dgRecords.SelectedItem is DataRowView row)
            {
                // Запомнить Id. Пригодится при редактировании и удалении.
                selectedId = Convert.ToInt32(row["Id"]);
                // Дата в поле.
                txtDate.Text = row["Date"].ToString();
                // Тип в поле.
                txtType.Text = row["Type"].ToString();
                // Категория в поле.
                txtCategory.Text = row["Category"].ToString();
                // Сумма в поле.
                txtAmount.Text = row["Amount"].ToString();
                // Комментарий в поле.
                txtComment.Text = row["Comment"].ToString();
            }
        }
    }
}