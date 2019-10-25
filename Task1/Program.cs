//Задание 1
//Панченко Иван
using System;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Task1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.Title = "Умножение матриц";

            int m = 999;
            int n = 777; 
            int s = 1111;
            if (args.Count() == 3)
            {
                m = int.Parse(args[0]);
                n = int.Parse(args[1]);
                s = int.Parse(args[2]);
            }
            decimal numberOfMultiplications = (decimal)m * n * s;
            decimal numberOfAdditions = (decimal)m * s * (n - 1);
            decimal numberOfOperations = numberOfAdditions+numberOfMultiplications;

            PrintTableWithSizes(m, n, s);

            double[,] array1Normal, array2Normal, arrayResNormal;
            double[][] array1Jagged, array2Jagged, arrayResJagged;
            array1Normal = new double[m, n];
            array2Normal = new double[n, s];
            array1Jagged = CreateJaggedArray(m, n);
            array2Jagged = CreateJaggedArray(n, s);
            Fill(array1Normal,array1Jagged);
            Fill(array2Normal, array2Jagged);

            Console.WriteLine("{0,20}:{1,5:0} миллионов", "Требуется умножений", numberOfMultiplications/1_000_000);
            Console.WriteLine("{0,20}:{1,5:0} миллионов", "Требуется сложений",numberOfAdditions / 1_000_000);
            Console.WriteLine("{0,20}:{1,5:0} миллионов", "Всего операций", numberOfOperations / 1_000_000);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Начало вычисления. Пожалуйста, подождите...");
            Console.ResetColor();

            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            arrayResNormal = MultiplyNormal(array1Normal, array2Normal);
            sw.Stop();
            long timeNormal = sw.ElapsedMilliseconds;
            
            sw.Reset();
            sw.Start();
            arrayResJagged = MultiplyJagged(array1Jagged, array2Jagged);
            sw.Stop();
            long timeJagged = sw.ElapsedMilliseconds;

            PrintTableWithResults(timeNormal,timeJagged,numberOfOperations);
            
            Console.WriteLine("Программа завершена!");
            Console.ReadLine();            
        }

        private static void PrintTableWithResults(long timeNormal, long timeJagged, decimal numberOfOperations)
        {
            int w = 59;
            Console.WriteLine("ПРОИЗВОДИТЕЛЬНОСТЬ:");
            Console.WriteLine(new string('-', w));
            Console.WriteLine(String.Format("|{0,20}| {1,10}| {2,24}", "", "Время, с", "Производительность |"));
            Console.WriteLine(new string('-', w));
            Console.WriteLine(String.Format("|{0,20}| {1,-10:0.0}| {2,-23:0.2 МФлопс/с}|", "Прямоугольный массив", timeNormal / 1000, numberOfOperations / timeNormal / 1000));
            Console.WriteLine(new string('-', w));
            Console.WriteLine(String.Format("|{0,20}| {1,-10:0.0}| {2,-23:0.2 МФлопс/с}|", "Рваный массив", timeJagged / 1000, numberOfOperations / timeJagged / 1000));
            Console.WriteLine(new string('-', w));
            Console.WriteLine();
        }

        private static void PrintTableWithSizes(int m, int n, int s)
        {
            int w = 29;

            Console.WriteLine(String.Format("{0,24}   ", "Размеры матриц    "));
            Console.WriteLine(new string('-', w));
            Console.WriteLine(String.Format("|{0,10}|{1,6} на{2,6} |", "Матрица А", m, n));
            Console.WriteLine(String.Format("|{0,10}|{1,6} на{2,6} |", "Матрица B", n, s));
            Console.WriteLine(new string('-', w));
            Console.WriteLine(String.Format("|{0,10}|{1,6} на{2,6} |", "Матрица АВ", m, s));
            Console.WriteLine(new string('-', w));
            Console.WriteLine();
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

        public static double[,]MultiplyNormal(double[,] x,double[,]y)
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

        public static double[][] MultiplyJagged(double[][] x, double[][] y)
        {
            if (x[0].Length != y.Length)
                return null;
            double[][] result = CreateJaggedArray(x.Length, y[0].Length);
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y[0].Length; j++)
                {
                    for (int k = 0; k < x[0].Length; k++)
                    {
                        result[i][j] += x[i][k] * y[k][j];
                    }
                }
            }
            return result;
        }

        public static void Fill(double[,] x, double[][] y)
        {
            Random r = new Random();
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    x[i, j] = y[i][j] = r.NextDouble();
                }
            }
        }

        public static double[][] CreateJaggedArray(int m, int n)
        {
            double[][] res = new double[m][];
            for (int i = 0; i < m; i++)
            {
                res[i] = new double[n];
            }
            return res;
        }
    }
}
