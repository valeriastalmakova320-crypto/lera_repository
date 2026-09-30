using System;

namespace TaskMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер матрицы N: ");
            int n = int.Parse(Console.ReadLine());
            // Создаём квадратную матрицу N x N
            int[,] matrix = new int[n, n];
            // Создаём генератор случайных чисел
            Random rnd = new Random();
            // Заполняем матрицу случайными числами от -50 до 50
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Генерируем число и записываем в ячейку
                    matrix[i, j] = rnd.Next(-50, 51);
                }
            }
            // Выводим исходную матрицу на экран
            Console.WriteLine("Исходная матрица:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
            // Массив для сумм каждой строки
            int[] sums = new int[n];
            // Считаем сумму элементов каждой строки
            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += matrix[i, j];
                }
                sums[i] = sum;
            }
            // Сортируем строки по возрастанию их сумм 
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    // Если сумма текущей строки больше следующей — меняем их местами
                    if (sums[j] > sums[j + 1])
                    {
                        // Меняем местами суммы
                        int tempSum = sums[j];
                        sums[j] = sums[j + 1];
                        sums[j + 1] = tempSum;
                        // Меняем местами сами строки матрицы поэлементно
                        for (int k = 0; k < n; k++)
                        {
                            int temp = matrix[j, k];
                            matrix[j, k] = matrix[j + 1, k];
                            matrix[j + 1, k] = temp;
                        }
                    }
                }
            }
            // Выводим отсортированную матрицу
            Console.WriteLine("Матрица после сортировки строк по сумме:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                // В конце строки выводим её сумму для наглядности
                Console.WriteLine("| сумма = " + sums[i]);
            }
        }
    }
}