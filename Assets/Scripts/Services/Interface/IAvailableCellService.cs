using TicTacToe.Enum;

namespace TicTacToe.Services
{
    public interface IAvailableCellService
    {
        (int x, int y)? FindRandomAvailableCell(EPlayerId[,] grid);
        (int x, int y)? FindBestAvailableCell(EPlayerId[,] grid, EPlayerId player);
    }
}