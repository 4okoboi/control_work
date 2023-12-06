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

