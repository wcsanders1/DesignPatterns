namespace Memento
{
    public class Mine
    {
        private int Width { get; }
        private int Height { get; }
        private MineSpace[][] MineBoard {get;}

        public Mine(int width, int height)
        {
            Width = width;
            Height = height;
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
