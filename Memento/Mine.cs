using System;

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
        }

        public void PrintMineBoard()
        {
            if (MineBoard == null)
            {
                CreateMindBoard();
            }
        }

        private void CreateMindBoard()
        {
            var rnd = new Random();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var mineSpace = new MineSpace { ExplosionValue = rnd.Next(MinVal, MaxVal) };
                    MineBoard[y, x] = mineSpace;
                }
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
