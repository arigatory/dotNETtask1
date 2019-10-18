//Задание 1
//Панченко Иван
using System;
using System.Linq;
using System.Diagnostics;

namespace Task1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.Title = "Умножение матриц";
            double[,] a, b, c;
            uint m = 999;
            uint n = 777; 
            uint s = 1111;
            if (args.Count() == 3)
            {
                m = uint.Parse(args[0]);
                n = uint.Parse(args[1]);
                s = uint.Parse(args[2]);
            }
            decimal numberOfMultiplications = (decimal)m * n * s;
            decimal numberOfAdditions = (decimal)m * s * (n - 1);
            decimal numberOfOperations = numberOfAdditions+numberOfMultiplications;

            a = new double[m, n];
            Fill(a);
            b = new double[n, s];
            Fill(b);

            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Умножение матрицы А[{0}x{1}] на матрицу В[{1}x{2}]\n",m,n,s);
            Console.WriteLine("Требуется умножений:  {0,10:0} миллионов", numberOfMultiplications/1_000_000);
            Console.WriteLine("Требуется сложений:   {0,10:0} миллионов",numberOfAdditions / 1_000_000);
            Console.WriteLine("Всего операций:       {0,10:0} миллионов", numberOfOperations / 1_000_000);
            Console.WriteLine("Начало вычисления. Пожалуйста, подождите...");
            sw.Start();
            c = Multiply(a, b);
            sw.Stop();
            Console.WriteLine("Потребовалось {0:0.0} секунд.",sw.ElapsedMilliseconds/1000);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Производительность: {0} MФлоп/с", numberOfOperations/sw.ElapsedMilliseconds/1000);
            Console.ResetColor();
            Console.WriteLine("Программа завершена!");
            Console.ReadLine();            
        }

        public static void Print(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0,7}",matrix[i,j]);
                }
                Console.WriteLine();
            }
        }

        public static double[,]Multiply(double[,] x,double[,]y)
        {
            if (x.GetLength(1) != y.GetLength(0))
                return null;
            double[,] result = new double[x.GetLength(0), y.GetLength(1)];
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < y.GetLength(1); j++)
                {
                    for (int k = 0; k < x.GetLength(1); k++)
                    {
                        result[i, j] += x[i,k]*y[k, j];
                    }
                }
            }
            return result;
        }

        public static void Fill(double[,] x)
        {
            Random r = new Random();
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    x[i, j] = r.NextDouble();
                }
            }
        }
    }
}
