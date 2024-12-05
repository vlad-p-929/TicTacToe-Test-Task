using TicTacToe.View;
using TicTacToe.Model;
using Zenject;

namespace TicTacToe.UI
{
    public class UndoPresenter : ITickable, IInitializable
    {
        private readonly GameModel model;
        private readonly GridModel gridModel;
        private readonly GridView gridView;
        private readonly UIView view;

        [Inject]
        public UndoPresenter(GameModel model, GridModel gridModel, GridView gridView, UIView view)
        {
            this.model = model;
            this.gridModel = gridModel;
            this.gridView = gridView;
            this.view = view;
        }

        public void Initialize()
        {
            view.AddUndoButtonListener(UndoLastMove);

            gridModel.OnCellReset += gridView.UndoVisualMove;
        }

        public void Tick()
        {
            if (model.IsPlayerVsAI)
            {
                UpdateUndoButtonState();
            }
        }

        private void UndoLastMove()
        {
            if (gridModel.UndoMove())
            {
                UpdateUndoButtonState();
            }
        }

        private void UpdateUndoButtonState()
        {
            view.UpdateUndoButtonState(CanUndoMove() && !model.IsGameOver);
        }

        private bool CanUndoMove()
        {
            return gridModel.HasMoves();
        }
    }
}