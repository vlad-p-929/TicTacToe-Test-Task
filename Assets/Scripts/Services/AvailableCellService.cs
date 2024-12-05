using System.Collections.Generic;
using TicTacToe.Enum;

namespace TicTacToe.Services
{
    public class AvailableCellService : IAvailableCellService
    {
        public (int x, int y)? FindRandomAvailableCell(EPlayerId[,] grid)
        {
            int gridSize = grid.GetLength(0);
            
            List<(int x, int y)> availableCells = new List<(int x, int y)>();

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    if (grid[x, y] == (int)EPlayerId.None)
                    {
                        availableCells.Add((x, y));
                    }
                }
            }

            if (availableCells.Count == 0)
            {
                return null;
            }

            System.Random random = new System.Random();
            int randomIndex = random.Next(availableCells.Count);
            return availableCells[randomIndex];
        }

        public (int x, int y)? FindBestAvailableCell(EPlayerId[,] grid, EPlayerId player)
        {
            int gridSize = grid.GetLength(0);
            
            for (int i = 0; i < gridSize; i++)
            {
                var rowWin = CheckTwoInLine(grid, (i, 0), (i, 1), (i, 2), player);
                if (rowWin.HasValue) return rowWin.Value;

                var colWin = CheckTwoInLine(grid, (0, i), (1, i), (2, i), player);
                if (colWin.HasValue) return colWin.Value;
            }

            var mainDiagonalWin = CheckTwoInLine(grid, (0, 0), (1, 1), (2, 2), player);
            if (mainDiagonalWin.HasValue) return mainDiagonalWin.Value;

            var antiDiagonalWin = CheckTwoInLine(grid, (0, 2), (1, 1), (2, 0), player);
            if (antiDiagonalWin.HasValue) return antiDiagonalWin.Value;

            List<(int x, int y)> bestCells = new List<(int x, int y)>();
            List<(int x, int y)> availableCells = new List<(int x, int y)>();

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    if (grid[x, y] == (int)EPlayerId.None)
                    {
                        availableCells.Add((x, y));
                        
                        if (HasBestCellNearby(grid, x, y, player))
                        {
                            bestCells.Add((x, y));
                        }
                    }
                }
            }

            if (bestCells.Count > 0)
            {
                return bestCells[0];
            }

            if (availableCells.Count > 0)
            {
                System.Random random = new System.Random();
                int randomIndex = random.Next(availableCells.Count);
                return availableCells[randomIndex];
            }

            return null;
        }
        
        private (int x, int y)? CheckTwoInLine(EPlayerId[,] grid, (int x, int y) cell1, (int x, int y) cell2, (int x, int y) cell3,
            EPlayerId playerId)
        {
            EPlayerId c1 = grid[cell1.x, cell1.y];
            EPlayerId c2 = grid[cell2.x, cell2.y];
            EPlayerId c3 = grid[cell3.x, cell3.y];

            if (c1 == playerId && c2 == playerId && c3 == EPlayerId.None) return cell3;
            if (c1 == playerId && c3 == playerId && c2 == EPlayerId.None) return cell2;
            if (c2 == playerId && c3 == playerId && c1 == EPlayerId.None) return cell1;

            return null;
        }
        
        private bool HasBestCellNearby(EPlayerId[,] grid, int x, int y, EPlayerId playerId)
        {
            int gridSize = grid.GetLength(0);
            
            var directions = new (int dx, int dy)[]
            {
                (-1, 0), // Left
                (1, 0), // Right
                (0, -1), // Up
                (0, 1) // Down
            };

            foreach (var (dx, dy) in directions)
            {
                int newX = x + dx;
                int newY = y + dy;

                if (newX >= 0 && newX < gridSize && newY >= 0 && newY < gridSize)
                {
                    if (grid[newX, newY] == playerId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}