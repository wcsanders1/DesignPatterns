using State.States;

namespace State
{
    public class MathGame
    {
        public GameState GameState { get; set; }

        public MathGame()
        {
            GameState = new EasyGameState(0, 0, this);
        }
    }
}
