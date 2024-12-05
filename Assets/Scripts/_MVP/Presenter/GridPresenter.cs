using TicTacToe.AssetManager.Interface;
using TicTacToe.Enum;
using TicTacToe.View;
using TicTacToe.Model;
using Zenject;

namespace TicTacToe.Presenter
{
    public class GridPresenter : IInitializable
    {
        private readonly GridModel gridModel;
        private readonly GridView gridView;

        [Inject]
        public GridPresenter(GameModel gameModel, GridModel gridModel, GridView gridView, IResourceManager resourceManager)
        {
            this.gridModel = gridModel;
            this.gridView = gridView;

            gridModel.Initialize(3, 3);
            gridView.Initialize(3, 3, resourceManager);
            gridView.SubscribeToCells((x, y) => gameModel.CurrentPlayer.OnUserInput(x, y));
        }

        public void Initialize()
        {
            gridModel.OnCellOccupied += MakeMove;
            gridModel.OnGridReset += ResetGrid;
        }

        private void MakeMove(int x, int y, EPlayerId playerId)
        {
            gridView.ApplyVisualMove(x, y, playerId);
        }

        private void ResetGrid()
        {
            gridView.ResetVisualGrid();
        }
    }
}