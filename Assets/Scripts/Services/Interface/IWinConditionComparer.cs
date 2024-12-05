using TicTacToe.Enum;

namespace TicTacToe.Services
{
    public interface IWinConditionComparer
    {
        bool CheckWinCondition(EPlayerId[,] grid);
    }
}