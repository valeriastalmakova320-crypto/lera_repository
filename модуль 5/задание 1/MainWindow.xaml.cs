using System; // Нужно для базовых функций программы
using System.Windows; // Нужно для работы с окнами
using System.Windows.Controls; // Нужно для работы с элементами на экране
using System.Windows.Input; // Нужно для обработки нажатий мыши
using System.Windows.Media; // Нужно для работы с цветами
using System.Windows.Shapes; // Нужно для создания фигур

namespace задание_1 // Имя проекта
{
    public partial class MainWindow : Window // Класс окна программы
    {
        private string selectedShape = "Line"; // Переменная хранит название выбранной фигуры, изначально линия

        public MainWindow() // Выполняется при запуске программы
        {
            InitializeComponent(); // Команда собирает окно из того, что нарисовано в XAML
        }

        private void BtnLine_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Линия
        {
            selectedShape = "Line"; // Запоминаем, что выбрана линия
            txtStatus.Text = "Выбрана Линия"; // Меняем текст на экране
        }

        private void BtnCircle_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Круг
        {
            selectedShape = "Circle"; // Запоминаем, что выбран круг
            txtStatus.Text = "Выбран Круг"; // Меняем текст на экране
        }

        private void BtnSquare_Click(object sender, RoutedEventArgs e) // Выполняется при нажатии кнопки Квадрат
        {
            selectedShape = "Square"; // Запоминаем, что выбран квадрат
            txtStatus.Text = "Выбран Квадрат"; // Меняем текст на экране
        }

        private void PaintCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) // Выполняется при клике мышкой по холсту
        {
            Point clickPoint = e.GetPosition(paintCanvas); // Узнаем координаты места клика
            Shape newShape = null; // Создаем пустую переменную для будущей фигуры

            switch (selectedShape) // Смотрим, какая фигура выбрана, и создаем ее
            {
                case "Line": // Если выбрана линия
                    newShape = new Line() // Создаем линию
                    {
                        X1 = clickPoint.X, // Начало линии по X там, где кликнули
                        Y1 = clickPoint.Y, // Начало линии по Y там, где кликнули
                        X2 = clickPoint.X + 100, // Конец линии по X на 100 пикселей правее
                        Y2 = clickPoint.Y + 100, // Конец линии по Y на 100 пикселей ниже
                        Stroke = Brushes.Black, // Цвет линии черный
                        StrokeThickness = 2 // Толщина линии 2 пикселя
                    };
                    break; // Выходим из проверки

                case "Circle": // Если выбран круг
                    newShape = new Ellipse() // Создаем круг
                    {
                        Width = 80, // Ширина круга 80 пикселей
                        Height = 80, // Высота круга 80 пикселей
                        Stroke = Brushes.Red, // Обводка красная
                        StrokeThickness = 2, // Толщина обводки 2 пикселя
                        Fill = Brushes.Transparent // Внутри круг пустой
                    };
                    Canvas.SetLeft(newShape, clickPoint.X - 40); // Сдвигаем круг влево на 40 пикселей для центрирования
                    Canvas.SetTop(newShape, clickPoint.Y - 40); // Сдвигаем круг вверх на 40 пикселей для центрирования
                    break; // Выходим из проверки

                case "Square": // Если выбран квадрат
                    newShape = new Rectangle() // Создаем квадрат
                    {
                        Width = 80, // Ширина 80 пикселей
                        Height = 80, // Высота 80 пикселей
                        Stroke = Brushes.Blue, // Обводка синяя
                        StrokeThickness = 2, // Толщина обводки 2 пикселя
                        Fill = Brushes.Transparent // Внутри квадрат пустой
                    };
                    Canvas.SetLeft(newShape, clickPoint.X - 40); // Сдвигаем квадрат влево на 40 пикселей для центрирования
                    Canvas.SetTop(newShape, clickPoint.Y - 40); // Сдвигаем квадрат вверх на 40 пикселей для центрирования
                    break; // Выходим из проверки
            }

            if (newShape != null) // Проверяем, что фигура создалась
            {
                paintCanvas.Children.Add(newShape); // Добавляем фигуру на холст
            }
        }
    }
}