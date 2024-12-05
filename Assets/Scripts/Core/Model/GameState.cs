using TicTacToe.Enum;

namespace TicTacToe.Grid.Undo
{
    public class GameState
    {
        public EPlayerId[,] Grid { get; }
        public EPlayerId CurrentPlayerId { get; }

        public GameState(EPlayerId[,] grid, EPlayerId currentPlayerId)
        {
            Grid = (EPlayerId[,])grid.Clone();
            CurrentPlayerId = currentPlayerId;
        }
    }
}