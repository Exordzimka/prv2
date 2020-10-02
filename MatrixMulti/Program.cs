using System;

namespace MatrixMulti
{
    class Program
    {
        private static int[,] _matrix1;
        private static int[,] _matrix2;
        private static int _size;
        static void Main()
        {
            var random = new Random();
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

            var cmd = Console.ReadLine();
            if (cmd != null && cmd.Equals("1"))
            {
                   
            }
            else if (cmd != null && cmd.Equals("2"))
            {
                
            }
        }

        private static void Multiplication()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    for (int k = 0; k < _size; k++)
                    {
                        
                    }
                }
            }
        }
    }
}