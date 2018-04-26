using System;
using System.Text;

namespace Memento
{
    public class Mine
    {
        private int Width { get; }
        private int Height { get; }
        private int MinVal { get; }
        private int MaxVal { get; }
        private MineSpace[,] MineBoard { get; }

        public Mine(int width, int height, int minVal, int maxVal)
        {
            Width = width;
            Height = height;
            MinVal = minVal;
            MaxVal = maxVal;
            MineBoard = new MineSpace[Height, Width];
            CreateMindBoard();
        }

        public void PrintMineBoard()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var mineSpace = MineBoard[y, x];
                    var xPosition = mineSpace.XPosition;
                    var yPosition = mineSpace.YPosition;
                    

                    Console.CursorLeft = xPosition;
                    Console.CursorTop = yPosition - 1;
                    Console.WriteLine("___");
                    Console.CursorLeft = xPosition - 1;
                    Console.WriteLine("|   |");
                    Console.CursorLeft = xPosition - 1;
                    Console.Write("|");

                    if (mineSpace.IsOccupied)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }

                    Console.Write(" X ");
                    Console.ResetColor();
                    Console.WriteLine("|");
                    Console.CursorLeft = xPosition - 1;
                    Console.WriteLine("|___|");
                }
            }
        }

        private void CreateMindBoard()
        {
            var rnd = new Random();
            var topLine = new string('_', Width * 4);
            var xStartPosition = (Console.WindowWidth / 2) - (topLine.Length / 2);
            var yPosition = Console.CursorTop;

            for (int y = 0; y < Height; y++)
            {
                var currentXPosition = xStartPosition;
                for (int x = 0; x < Width; x++)
                {
                    var mineSpace = new MineSpace
                    {
                        ExplosionValue = rnd.Next(MinVal, MaxVal),
                        XPosition = currentXPosition,
                        YPosition = yPosition,
                        IsOccupied = (y == 0 && x == 0) ? true : false
                    };
                    MineBoard[y, x] = mineSpace;
                    currentXPosition += 4;
                }
                yPosition += 3;
            }
        }

        public IMineMemento CreateMemento()
        {
            var copy = MineBoard.Clone();

            return new MineMemento
            {
                State = copy
            };
        }

        public class MineMemento : IMineMemento
        {
            public object State { get; set; }
        }
    }
}
