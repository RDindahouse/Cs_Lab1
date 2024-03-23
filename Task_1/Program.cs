using System;

class Program
{
    static void Main()
    {
        int[,] Array = new int[4, 5];
        Random R = new Random();

        // Заполнение двумерного массива произвольными значениями
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Array[i, j] = R.Next(100); //Заполнение массива случайными числами
            }
        }

        // Вывод содержимого начального двумерного массива на консоль
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write(Array[i, j] + "\t");
            }
            Console.WriteLine();
        }

        // Замена строк массива с использованием временной строки
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int temp = Array[i, j];
                Array[i, j] = Array[3 - i, j];
                Array[3 - i, j] = temp;
            }
        }

        Console.WriteLine();

        // Вывод содержимого конечного двумерного массива на консоль
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write(Array[i, j] + "\t");
            }
            Console.WriteLine();
        }

    }
}
