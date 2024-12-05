using System.Collections.Generic;
using TicTacToe.Enum;
using TicTacToe.Grid.Undo;
using TicTacToe.Services;
using Zenject;

namespace TicTacToe.Model
{
    public class GridModel
    {
        private IAvailableCellService availableCellService;
        public event System.Action<int, int, EPlayerId> OnCellOccupied;
        public event System.Action OnGridReset;
        public event System.Action<int, int> OnCellReset;

        public EPlayerId[,] Grid { get; private set; }
        private int gridSize;
        private Stack<GameState> undoStack;
        
        [Inject]
        public GridModel(IAvailableCellService availableCellService)
        {
            this.availableCellService = availableCellService;
        }

        public void Initialize(int rows, int cols)
        {
            gridSize = rows;
            Grid = new EPlayerId[rows, cols];
            undoStack = new Stack<GameState>();
            
            SaveState((int)EPlayerId.None); //The very first state
        }

        public bool MakeMove(int x, int y, EPlayerId playerId)
        {
            if (Grid[x, y] != EPlayerId.None)
            {
                return false;
            }

            Grid[x, y] = playerId;
            SaveState(playerId);

            OnCellOccupied?.Invoke(x, y, playerId);

            return true;
        }

        public void ResetLogicalGrid()
        {
            Grid = new EPlayerId[gridSize, gridSize];

            undoStack.Clear();
            SaveState(EPlayerId.None); //The very first state

            OnGridReset?.Invoke();
        }

        public bool UndoMove()
        {
            if (undoStack.Count <= 2)
            {
                return false;
            }

            undoStack.Pop(); //EPlayerId.O
            undoStack.Pop(); //EPlayerId.X - We want this state

            var previousState = undoStack.Peek();
            RestoreState(previousState);

            return true;
        }

        public bool HasAvailableCells()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    if (Grid[x, y] == (int)EPlayerId.None) return true;
                }
            }

            return false;
        }

        public (int x, int y)? FindRandomAvailableCell()
        {
            return availableCellService.FindRandomAvailableCell(Grid);
        }

        public (int x, int y)? FindBestAvailableCell(EPlayerId player)
        {
            return availableCellService.FindBestAvailableCell(Grid, player);
        }

        public EPlayerId GetCellValue(int x, int y)
        {
            return Grid[x, y];
        }

        public bool HasMoves()
        {
            return undoStack.Count > 1;
        }

        private void SaveState(EPlayerId currentPlayerId)
        {
            undoStack.Push(new GameState(Grid, currentPlayerId));
        }

        private void RestoreState(GameState state)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    EPlayerId previousValue = state.Grid[x, y];
                    EPlayerId currentValue = Grid[x, y];

                    if (previousValue != currentValue)
                    {
                        Grid[x, y] = previousValue;

                        if (previousValue != (int)EPlayerId.None)
                        {
                            OnCellOccupied?.Invoke(x, y, previousValue);
                        }
                        else
                        {
                            OnCellReset?.Invoke(x, y);
                        }
                    }
                }
            }
        }
    }
}