namespace Memento
{
    public class MineSpace
    {
        public bool IsExploded { get; set; }
        public bool HasTreasure { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int[,] BoardPosition { get; set; }
    }
}
