using TicTacToe.Enum;

namespace TicTacToe.Services
{
    public class WinConditionComparer : IWinConditionComparer
    {
        public bool CheckWinCondition(EPlayerId[,] grid)
        {
            const bool win = true;
            const bool loose = false;
            
            int gridSize = grid.GetLength(0);
            
            if (CheckRows(grid, gridSize))
            {
                return win;
            }

            if (CheckColumns(grid, gridSize))
            {
                return win;
            }

            if (CheckDiagonals(grid))
            {
                return win;
            }

            return loose;
        }

        private static bool CheckDiagonals(EPlayerId[,] grid)
        {
            if (grid[0, 0] != EPlayerId.None &&
                grid[0, 0] == grid[1, 1] &&
                grid[1, 1] == grid[2, 2])
            {
                return true;
            }

            if (grid[0, 2] != EPlayerId.None &&
                grid[0, 2] == grid[1, 1] &&
                grid[1, 1] == grid[2, 0])
            {
                return true;
            }

            return false;
        }

        private static bool CheckColumns(EPlayerId[,] grid, int gridSize)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (grid[0, j] != EPlayerId.None &&
                    grid[0, j] == grid[1, j] &&
                    grid[1, j] == grid[2, j])
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckRows(EPlayerId[,] grid, int gridSize)
        {
            for (int i = 0; i < gridSize; i++)
            {
                if (grid[i, 0] != EPlayerId.None &&
                    grid[i, 0] == grid[i, 1] &&
                    grid[i, 1] == grid[i, 2])
                {
                    return true;
                }
            }

            return false;
        }
    }
}