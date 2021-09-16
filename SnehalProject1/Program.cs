using System;
using System.Text;
using System.Threading;
namespace Assignment
{
    public class GRID
    {
        public static void Main(string[] args)
        {
            var game = new Grid();
            Console.WriteLine("Enter Number of rows--> ");
            var rows = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Number of columns--> ");
            var coloumns = Convert.ToInt32(Console.ReadLine());
            game.StartGame(rows, coloumns);
            Console.WriteLine("Would you like to continue? 1 for ReProcess , 2 for adding more rows and 0 to exit");
            var x = Console.ReadLine();
            while (x != "0")

            {
                if (x == "1")
                {
                    game.GenerateNewGenerations();
                    Console.WriteLine(game.Print());
                    Console.WriteLine("Would you like to continue? 1 for ReProcess , 2 for adding more rows and 0 to exit");
                    x = Console.ReadLine();
                }
                if (x == "2")
                {

                    int[,] temp = new int[rows, coloumns];
                    for (var i = 0; i < rows; i++)
                    {
                        Console.WriteLine("Enter for rows: ");
                        Console.WriteLine(i + 1);
                        for (var j = 0; j < coloumns; j++)
                        {
                            temp[i, j] = Convert.ToInt32(Console.ReadLine());
                        }
                    }
                    game.Append(temp, rows, coloumns);
                    game.GenerateNewGenerations();
                    Console.WriteLine(game.Print());
                    Console.WriteLine("Would you like to continue? 1 for ReProcess , 2 for adding more rows and 0 to exit");
                    x = Console.ReadLine();

                }

            }


        }

    }
    public class Grid
    {
        private bool[,] _generation;
        private Random _random = new Random();

        public void StartGame(int numberRows, int numberCols)
        {
            InitializeGrid(numberRows, numberCols);
            InsertRandomLives();
            Console.Write(Print());

        }

        public void GenerateNewGenerations()
        {
            var tempGen = new bool[_generation.GetLength(0), _generation.GetLength(1)];

            Iterator((row, col) =>
            {
                if (_generation[row, col])
                {
                    if (CountLiveNeighbours(row, col) < 2)
                    {
                        tempGen[row, col] = !_generation[row, col];
                    }

                    if (CountLiveNeighbours(row, col) == 2 || CountLiveNeighbours(row, col) == 3)
                    {
                        tempGen[row, col] = _generation[row, col];
                    }

                    if (CountLiveNeighbours(row, col) > 3)
                    {
                        tempGen[row, col] = !_generation[row, col];
                    }
                }
                else if (CountLiveNeighbours(row, col) == 3)
                {
                    tempGen[row, col] = !_generation[row, col];
                }
            });
            _generation = (bool[,])tempGen.Clone();
        }

        public string Print()
        {
            var sBuilder = new StringBuilder();

            Iterator((row, col) =>
            {
                sBuilder.Append(_generation[row, col] ? "1" : "0");
            }, () =>
            {
                sBuilder.AppendLine();
            });

            return sBuilder.ToString();
        }

        public void Append(int[,] temp, int rows, int columns)
        {
            bool[,] _convert = new bool[rows, columns];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    _convert[i, j] = temp[i, j] == 1;
                }
            }

            _generation = Concat(_convert, rows, columns);

        }

        private bool[,] Concat(bool[,] array, int rows, int columns)
        {
            bool[,] result = new bool[_generation.GetLength(0) + array.GetLength(0), _generation.GetLength(1)];
            for (var i = 0; i < _generation.GetLength(0); i++)
            {
                for (var j = 0; j < _generation.GetLength(1); j++)
                {
                    result[i, j] = _generation[i, j];
                }
            }

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    result[_generation.GetLength(0) + i, j] = array[i, j];
                }
            }

            return result;
        }

        private int CountLiveNeighbours(int x, int y)
        {
            var count = 0;

            count += x + 1 < _generation.GetLength(0) && _generation[x + 1, y] ? 1 : 0;
            count += y + 1 < _generation.GetLength(1) && _generation[x, y + 1] ? 1 : 0;
            count += x - 1 >= _generation.GetLowerBound(0) && _generation[x - 1, y] ? 1 : 0;
            count += y - 1 >= _generation.GetLowerBound(1) && _generation[x, y - 1] ? 1 : 0;
            count += x + 1 < _generation.GetLength(0) && y - 1 >= _generation.GetLowerBound(1) && _generation[x + 1, y - 1] ? 1 : 0;
            count += x - 1 >= _generation.GetLowerBound(0) && y - 1 >= _generation.GetLowerBound(1) && _generation[x - 1, y - 1] ? 1 : 0;
            count += x + 1 < _generation.GetLength(0) && y + 1 < _generation.GetLength(1) && _generation[x + 1, y + 1] ? 1 : 0;
            count += x - 1 >= _generation.GetLowerBound(0) && y + 1 < _generation.GetLowerBound(1) && _generation[x - 1, y + 1] ? 1 : 0;

            return count;
        }

        private void InsertRandomLives() => Iterator((row, col) => { _generation[row, col] = _random.Next(10) == 1; });

        private void InitializeGrid(int numberRows, int numberOfCols)
        {

            _generation = new bool[numberRows, numberOfCols];
        }

        private void Iterator(Action<int, int> action, Action action2 = null)
        {
            for (int row = 0; row < _generation.GetLength(0); row++)
            {
                for (int col = 0; col < _generation.GetLength(1); col++)
                {
                    action.Invoke(row, col);
                }
                action2?.Invoke();
            }
        }
    }

}
