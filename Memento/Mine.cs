using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Memento.Managers;

namespace Memento
{
    public class Mine
    {
        private const int ExplosionValue = 15;

        private int Width { get; }
        private int Height { get; }
        private MineSpace[,] MineBoard { get; set; }
        private int[,] CurrentPosition { get; }
        private ExplosionsRemainingManager ExplosionsRemainingManager { get; }
        private WinLoseMessageManager WinLoseMessageManager { get; }
        private UndosRemainingManager UndosRemainingManager { get; }
        private GameState GameState { get; set; }
        private Stack<IMineMemento> States { get; }

        public Mine(int width, int height, int numExplosions, int numUndos)
        {
            Width = width;
            Height = height;
            MineBoard = new MineSpace[Height, Width];
            CurrentPosition = new [,] {{0,0}};  // Start at the top left of the board, just because that seems natural I guess.
            States = new Stack<IMineMemento>();
            CreateMindBoard();
            GameState = GameState.InProgress;

            var topLeftPosition = new[,] { { MineBoard[0, 0].YPosition, MineBoard[0, 0].XPosition } };
            ExplosionsRemainingManager = new ExplosionsRemainingManager(numExplosions, topLeftPosition);

            var boardMiddleXPosition = MineBoard[0, Width / 2].XPosition;
            WinLoseMessageManager = new WinLoseMessageManager(topLeftPosition[0,0], boardMiddleXPosition);

            var topRightPosision = new[,] { { MineBoard[0, 0].YPosition, MineBoard[0, Width - 1].XPosition } };
            UndosRemainingManager = new UndosRemainingManager(numUndos, topRightPosision);
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

                    if (CurrentPosition[0, 0] == y && CurrentPosition[0, 1] == x)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }

                    if (mineSpace.IsExploded && !mineSpace.HasTreasure)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.Write(" \u263A ");
                        Console.OutputEncoding = Encoding.ASCII;
                    }
                    else if (mineSpace.IsExploded && mineSpace.HasTreasure)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" $ ");
                        GameState = GameState.Won;
                    }
                    else
                    {
                        Console.Write(" X ");
                    }

                    Console.ResetColor();
                    Console.WriteLine("|");
                    Console.CursorLeft = xPosition - 1;
                    Console.WriteLine("|___|");
                }
            }

            if (GameState == GameState.Won)
            {
                WinLoseMessageManager.PrintMessage(GameState);
            }
        }

        public GameState GetGameState()
        {
            return GameState;
        }

        public void MoveRight()
        {
            if (CurrentPosition[0, 1] == Width - 1)
            {
                CurrentPosition[0, 1] = 0;
            }
            else
            {
                CurrentPosition[0, 1]++;
            }
            PrintMineBoard();
        }

        public void MoveLeft()
        {
            if (CurrentPosition[0, 1] == 0)
            {
                CurrentPosition[0, 1] = Width - 1;
            }
            else
            {
                CurrentPosition[0, 1]--;
            }
            PrintMineBoard();
        }

        public void MoveDown()
        {
            if (CurrentPosition[0, 0] == Height - 1)
            {
                CurrentPosition[0, 0] = 0;
            }
            else
            {
                CurrentPosition[0, 0]++;
            }
            PrintMineBoard();
        }

        public void MoveUp()
        {
            if (CurrentPosition[0, 0] == 0)
            {
                CurrentPosition[0, 0] = Height - 1;
            }
            else
            {
                CurrentPosition[0, 0]--;
            }
            PrintMineBoard();
        }

        public void Blast()
        {
            var occupiedMineSpace = GetOccupiedMineSpace();
            if (occupiedMineSpace.IsExploded)
            {
                return;
            }

            var explosionValue = ExplosionValue;
            occupiedMineSpace.IsExploded = true;
            explosionValue--;
            ExplosionsRemainingManager.ReduceExplosionsRemaining();

            var possibleExplosionDirections = new List<Direction>();

            Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList().ForEach(direction =>
            {
                if (GetAdjacentSpace(occupiedMineSpace, direction) != null)
                {
                    possibleExplosionDirections.Add(direction);
                }
            });

            var apportionedExplosionValue = explosionValue / possibleExplosionDirections.Count;
            possibleExplosionDirections.ForEach(direction =>
            {
                var spaceToExplode = GetAdjacentSpace(occupiedMineSpace, direction);
                ExplodeSpace(spaceToExplode, apportionedExplosionValue);
            });

            PrintMineBoard();

            if (!ExplosionsRemainingManager.ExplosionsRemain() && GameState != GameState.Won)
            {
                GameState = GameState.Lost;
                WinLoseMessageManager.PrintMessage(GameState);
            }

            StoreState();
        }

        public void UndoBlast()
        {
            if (States.Count < 2 || !UndosRemainingManager.UndosRemain())
            {
                return;
            }

            States.Pop();
            var lastState = States.Peek();
            SetMemento(lastState);
            UndosRemainingManager.RedudeUndosRemaining();
        }

        private int[,] GetWinningPosition()
        {
            var rnd = new Random();
            var yPosition = rnd.Next(0, Height);
            var xPosition = rnd.Next(0, Width);

            return new[,] { { yPosition, xPosition } };
        }

        private void ExplodeSpace(MineSpace mineSpace, int explosionValue)
        {
            mineSpace.IsExploded = true;
            explosionValue--;

            if (explosionValue > 0)
            {
                var nextDirectionToExplode = GetRandomDirection();
                var nextSpaceToExplode = GetAdjacentSpace(mineSpace, nextDirectionToExplode);

                if (nextSpaceToExplode != null)
                {
                    ExplodeSpace(nextSpaceToExplode, explosionValue);
                }
            }
        }

        private Direction GetRandomDirection()
        {
            var rnd = new Random();
            var direction = rnd.Next(0, Enum.GetNames(typeof(Direction)).Length);

            return (Direction)direction;
        }

        private MineSpace GetAdjacentSpace(MineSpace mineSpace, Direction direction)
        {
            switch(direction)
            {
                case Direction.Above:
                    return GetSpaceAbove(mineSpace);
                case Direction.Below:
                    return GetSpaceBelow(mineSpace);
                case Direction.Left:
                    return GetSpaceLeft(mineSpace);
                case Direction.Right:
                    return GetSpaceRight(mineSpace);
                default:
                    return null;
            }
        }

        private MineSpace GetSpaceAbove(MineSpace mineSpace)
        {
            var yPosition = mineSpace.BoardPosition[0, 0];
            if (yPosition < 1)
            {
                return null;
            }

            var xPosition = mineSpace.BoardPosition[0, 1];

            return MineBoard[--yPosition, xPosition];
        }

        private MineSpace GetSpaceBelow(MineSpace mineSpace)
        {
            var yPosition = mineSpace.BoardPosition[0, 0];
            if (yPosition >= Height - 1)
            {
                return null;
            }

            var xPosition = mineSpace.BoardPosition[0, 1];

            return MineBoard[++yPosition, xPosition];
        }

        private MineSpace GetSpaceLeft(MineSpace mineSpace)
        {
            var xPosition = mineSpace.BoardPosition[0, 1];
            if (xPosition < 1)
            {
                return null;
            }

            var yPosition = mineSpace.BoardPosition[0, 0];

            return MineBoard[yPosition, --xPosition];
        }

        private MineSpace GetSpaceRight(MineSpace mineSpace)
        {
            var xPosition = mineSpace.BoardPosition[0, 1];
            if (xPosition >= Width - 1)
            {
                return null;
            }

            var yPosition = mineSpace.BoardPosition[0, 0];

            return MineBoard[yPosition, ++xPosition];
        }

        private MineSpace GetOccupiedMineSpace()
        {
            return MineBoard[CurrentPosition[0, 0], CurrentPosition[0, 1]];
        }

        private void CreateMindBoard()
        {
            var topLine = new string('_', Width * 4);
            var xStartPosition = (Console.WindowWidth / 2) - (topLine.Length / 2);
            var yPosition = Console.CursorTop + 1;
            var winningPosition = GetWinningPosition();

            for (int y = 0; y < Height; y++)
            {
                var currentXPosition = xStartPosition;
                for (int x = 0; x < Width; x++)
                {
                    var mineSpace = new MineSpace
                    {
                        XPosition = currentXPosition,
                        YPosition = yPosition,
                        BoardPosition = new int[,] {{y,x}},
                        HasTreasure = y == winningPosition[0,0] && x == winningPosition[0,1]
                    };
                    MineBoard[y, x] = mineSpace;
                    currentXPosition += 4;
                }
                yPosition += 3;
            }

            StoreState();
        }

        public IMineMemento CreateMemento()
        {
            var copy = MineBoard.Copy();

            return new MineMemento
            {
                State = copy
            };
        }

        public void SetMemento(IMineMemento mineMemento)
        {
            MineBoard = ((MineSpace[,])mineMemento.State).Copy();
            PrintMineBoard();
        }

        public void StoreState()
        {
            var memento = CreateMemento();
            States.Push(memento);
        }

        public class MineMemento : IMineMemento
        {
            public object State { get; set; }
        }
    }
}
