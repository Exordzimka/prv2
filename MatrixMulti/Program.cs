using System;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixMulti
{
    class Program
    {
        private static int[,] _matrix1;
        private static int[,] _matrix2;
        private static int _size;
        private static Thread[] threads;
        static void Main()
        {
            var random = new Random();
            Console.WriteLine("Введите длину массива");
            _size = int.Parse(Console.ReadLine()!);
            _matrix1 = new int[_size, _size];
            _matrix2 = new int[_size, _size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _matrix1[i, j] = random.Next(1, 100);
                    _matrix2[i, j] = random.Next(1, 100);
                }
            }

            Console.WriteLine("1 or 2");
            var cmd = Console.ReadLine();
            if (cmd != null && cmd.Equals("1"))
            {
                SingleStart();
            }
            else if (cmd != null && cmd.Equals("2"))
            {
                MultiStart();
            }
        }

        private static void SingleThreadMultiplication()
        {
            int[,] matrixOfMulti = new int[_matrix1.GetLength(0), _matrix2.GetLength(1)];
            for (int i = 0; i < _matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix2.GetLength(1); j++)
                {
                    int summ = 0;
                    for (int k = 0; k < _matrix2.GetLength(0); k++)
                    {
                        summ += _matrix1[i, j] * _matrix2[k, j];
                    }
                    matrixOfMulti[i, j] = summ;
                }

            }
        }

        private static void MultiThreadMultiplication()
        {
            int[,] matrixOfMulti = new int[_matrix1.GetLength(0), _matrix2.GetLength(1)];
            for (int i = 0; i < _matrix1.GetLength(0); i++)
            {
                Thread thread = new Thread(SumOfElements);
                thread.Start(new MyArg(i, matrixOfMulti));
            }
        }

        private static void ParallelMultiplication()
        {
            int[,] matrixOfMulti = new int[_matrix1.GetLength(0), _matrix2.GetLength(1)];
            Parallel.For(0, _matrix1.GetLength(0), (i) =>
            {
                for (int j = 0; j < _matrix2.GetLength(1); j++)
                {
                    int summ = 0;
                    for (int k = 0; k < _matrix2.GetLength(0); k++)
                    {
                        summ += _matrix1[i, j] * _matrix2[k, j];
                    }
                    matrixOfMulti[i, j] = summ;
                }
            }
            );
        }

        private static void SumOfElements(object data)
        {
            MyArg myArg = (MyArg)data;
            for (int j = 0; j < _matrix2.GetLength(1); j++)
            {
                int summ = 0;
                for (int k = 0; k < _matrix2.GetLength(0); k++)
                {
                    summ += _matrix1[myArg.GetI, j] * _matrix2[k, j];
                }
                myArg.GetMatrix[myArg.GetI, j] = summ;
            }
        }

        private static void SingleStart()
        {
            Console.WriteLine("Start single thread!");
            DateTime start = DateTime.Now;
            SingleThreadMultiplication();
            DateTime end = DateTime.Now;
            Console.WriteLine("time of work = " + end.Subtract(start));

        }

        private static void MultiStart()
        {
            Console.WriteLine("Start Multithread!");
            DateTime start = DateTime.Now;
            MultiThreadMultiplication();
            DateTime end = DateTime.Now;
            Console.WriteLine("time work of threads = " + end.Subtract(start));

            start = DateTime.Now;
            ParallelMultiplication();
            end = DateTime.Now;
            Console.WriteLine("time work of parallel = " + end.Subtract(start));
        }

        class MyArg
        {
            private int i;
            private int[,] matrix;
            public MyArg(int i, int[,] matrix)
            {
                this.i = i;
                this.matrix = matrix;
            }

            public int GetI
            {
                get => i;
            }

            public int[,] GetMatrix
            {
                get => matrix;
            }
        }
           
    }
}