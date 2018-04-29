namespace Memento
{
    public static class ArrayExtensions
    {
        public static MineSpace[,] Copy(this MineSpace[,] mineSpaces)
        {
            var arrayHeight = mineSpaces.GetLength(0);
            var arrayWidth = mineSpaces.GetLength(1);
            var newMineSpaces = new MineSpace[arrayHeight, arrayWidth];

            for (int y = 0; y < arrayHeight; y++)
            {
                for (int x = 0; x < arrayWidth; x++)
                {
                    newMineSpaces[y, x] = new MineSpace
                    {
                        XPosition = mineSpaces[y, x].XPosition,
                        YPosition = mineSpaces[y, x].YPosition,
                        IsExploded = mineSpaces[y, x].IsExploded,
                        HasTreasure = mineSpaces[y, x].HasTreasure,
                        BoardPosition = mineSpaces[y, x].BoardPosition
                    };
                }
            }

            return newMineSpaces;
        }
    }
}
