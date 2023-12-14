using MatrixOOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixOOP
{
    internal class Matrix
    {
        private int rows;
        private int columns;
        private int[,] matrix;

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }

        public int this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            matrix = new int[rows, columns];
        }

        public Matrix(int size) : this(size, size) { }

        public Matrix(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            string[] dimensions = lines[0].Split(' ');
            int n = int.Parse(dimensions[0]);
            int m = int.Parse(dimensions[1]);
            this.rows = n;
            this.columns = m;
            matrix = new int[n, m];

            for (int i = 1; i < n + 1; i++)
            {
                string[] elements = lines[i].Split(' ');
                for (int j = 0; j < m; j++)
                {
                    matrix[i - 1, j] = int.Parse(elements[j]);
                }
            }
        }

        public void FillRandom()
        {
            var r = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    matrix[i, j] = r.Next(10);
            }
        }



        public Matrix Transpose(Matrix matrix)
        {
            var res = new Matrix(matrix.Columns, matrix.Rows);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    res[j, i] = matrix[i, j];
                }
            }
            return res;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    sb.Append($"{matrix[i, j],4}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.columns != b.columns)
                throw new ArgumentException("Размерности матриц должны быть одинаковыми");
            var res = new Matrix(a.rows, a.columns);
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                    res[i, j] += a[i, j] + b[i, j];
            }
            return res;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.rows != b.rows || a.columns != b.columns)
                throw new ArgumentException("Размерности матриц должны быть одинаковыми");
            var res = new Matrix(a.rows, a.columns);
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                    res[i, j] += a[i, j] - b[i, j];
            }
            return res;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.columns != b.rows)
                throw new ArgumentException("Умножаются только матрицы размерностей m x n и n x k");
            var res = new Matrix(a.rows, b.columns);
            for (int i = 0; i < a.rows; i++)
                for (int j = 0; j < b.columns; j++)
                    for (int k = 0; k < b.rows; k++)
                        res[i, j] += a[i, k] * b[k, j];

            return res;
        }

        public void ReplacePrimesInMatrix()
        {
            if (rows != columns)
                throw new InvalidOperationException("матрица должна быть квадратной для проверки главной диагонали");
            bool isAscendingDiagonal = true;
            for (int i = 0; i < rows - 1; i++)
            {
                if (matrix[i, i] >= matrix[i + 1, i + 1])
                {
                    isAscendingDiagonal = false;
                    break;
                }
            }
            if (isAscendingDiagonal)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (IsPrime(matrix[i, j]))
                        {
                            matrix[i, j] = 1;
                        }
                    }
                }
            }
        }


        private bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}

public class Program
{
    static void Main()
    {
        var m1 = new Matrix("/Users/kkamilka/Projects/Matrix/Matrix/test.txt");
        Console.WriteLine(m1);
        m1.ReplacePrimesInMatrix();
        Console.WriteLine(m1);
    }
}
