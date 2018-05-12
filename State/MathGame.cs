using CommonClientLib;
using State.States;

namespace State
{
    public class MathGame
    {
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private GameState GameState { get; set; }

        public MathGame()
        {
            // The game starts out easy
            GameState = new EasyGameState(0, 0, this);
        }

        public void PlayGame()
        {
            var keepPlaying = true;
            while (keepPlaying)
            {
                GameState.AskQuestion();
                keepPlaying = ContinuationDeterminer.GoAgain("Do you want another question?");
            }
        }

        public void ChangeState(GameState newState)
        {
            GameState = newState;
        }
    }
}
