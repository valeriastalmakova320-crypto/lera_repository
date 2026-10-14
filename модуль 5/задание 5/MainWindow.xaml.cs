using System; // Нужно для базовых функций программы
using System.Windows; // Нужно для работы с окнами
using System.Windows.Controls; // Нужно для работы с кнопками и другими элементами

namespace задание_1 // Имя проекта
{
    public partial class MainWindow : Window // Класс окна программы
    {
        // Переменная хранит первое число до операции, изначально ноль
        private double firstNumber = 0;
        // Переменная хранит выбранную операцию (плюс, минус, умножить, разделить), изначально пусто
        private string currentOperator = "";
        // Флаг показывает, начинаем ли мы вводить новое число, изначально true
        private bool isNewNumber = true;

        public MainWindow() // Выполняется при запуске программы
        {
            InitializeComponent(); // Команда собирает окно из того, что нарисовано в XAML
        }

        private void BtnDigit_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии любой кнопки с цифрой
        {
            Button button = (Button)sender; // Получаем кнопку, на которую нажали
            string digit = button.Content.ToString(); // Берем текст с кнопки, то есть саму цифру

            if (isNewNumber) // Проверяем, начинаем ли вводить новое число
            {
                txtDisplay.Text = digit; // Заменяем экран на эту цифру
                isNewNumber = false; // Переключаем флаг, теперь мы вводим то же число
            }
            else // Если продолжаем вводить текущее число
            {
                if (txtDisplay.Text == "0") // Проверяем, что на экране только ноль
                {
                    txtDisplay.Text = digit; // Заменяем ноль на цифру
                }
                else // Если на экране уже что-то есть
                {
                    txtDisplay.Text = txtDisplay.Text + digit; // Дописываем цифру справа
                }
            }
        }

        private void BtnOperator_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки операции плюс минус умножить разделить
        {
            Button button = (Button)sender; // Получаем нажатую кнопку
            firstNumber = double.Parse(txtDisplay.Text); // Запоминаем текущее число как первое
            currentOperator = button.Content.ToString(); // Запоминаем выбранную операцию
            isNewNumber = true; // Переключаем флаг, следующая цифра начнет новое число
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки равно
        {
            double secondNumber = double.Parse(txtDisplay.Text); // Запоминаем текущее число как второе
            double result = 0; // Переменная для результата, изначально ноль

            if (currentOperator == "+") // Проверяем, была ли выбрана операция сложения
            {
                result = firstNumber + secondNumber; // Складываем два числа
            }
            else if (currentOperator == "-") // Проверяем, была ли выбрана операция вычитания
            {
                result = firstNumber - secondNumber; // Вычитаем второе число из первого
            }
            else if (currentOperator == "*") // Проверяем, была ли выбрана операция умножения
            {
                result = firstNumber * secondNumber; // Умножаем два числа
            }
            else if (currentOperator == "/") // Проверяем, была ли выбрана операция деления
            {
                if (secondNumber == 0) // Проверяем, не пытается ли пользователь делить на ноль
                {
                    MessageBox.Show("На ноль делить нельзя"); // Показываем сообщение об ошибке
                    return; // Выходим из метода, чтобы не делить
                }
                result = firstNumber / secondNumber; // Делим первое число на второе
            }

            txtDisplay.Text = result.ToString(); // Показываем результат на экране
            isNewNumber = true; // Переключаем флаг для следующего ввода
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки C
        {
            txtDisplay.Text = "0"; // Сбрасываем экран на ноль
            firstNumber = 0; // Сбрасываем первое число
            currentOperator = ""; // Сбрасываем операцию
            isNewNumber = true; // Сбрасываем флаг
        }

        private void BtnDot_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки точка
        {
            if (!txtDisplay.Text.Contains(",")) // Проверяем, что в числе еще нет запятой
            {
                txtDisplay.Text = txtDisplay.Text + ","; // Добавляем запятую к числу
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки назад
        {
            if (txtDisplay.Text.Length > 1) // Проверяем, что в числе больше одного символа
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1); // Убираем последний символ
            }
            else // Если в числе только один символ
            {
                txtDisplay.Text = "0"; // Сбрасываем на ноль
            }
        }
    }
}