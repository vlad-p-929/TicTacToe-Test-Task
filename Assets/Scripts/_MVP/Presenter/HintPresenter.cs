using TicTacToe.Enum;
using TicTacToe.Model;
using TicTacToe.View;
using TicTacToe.View.Cell;
using UnityEngine;
using Zenject;

namespace TicTacToe.Presenter
{
    public class HintPresenter : ITickable, IInitializable
    {
        private readonly GameModel model;
        private readonly GridModel gridModel;
        private readonly GridView gridView;
        private readonly UIView uiView;

        private float timeSinceLastAction;
        
        [Inject]
        public HintPresenter(GameModel model, GridModel gridModel, GridView gridView, UIView uiView)
        {
            this.model = model;
            this.gridModel = gridModel;
            this.gridView = gridView;
            this.uiView = uiView;
        }

        public void Initialize()
        {
            model.OnPlayerChanged += HideHint;
        }
        
        public void Tick()
        {
            if (model.IsGameOver || !model.IsPlayerVsAI)
            {
                return;
            }

            timeSinceLastAction += Time.deltaTime;

            if (!model.CurrentPlayer.IsAI && timeSinceLastAction >= 5f)
            {
                ShowHint();
                timeSinceLastAction = 0f;
            }
        }

        private void HideHint()
        {
            uiView.HideHint();
        }
        
        private void ShowHint()
        {
            var bestMove = gridModel.FindBestAvailableCell(EPlayerId.X);
            if (bestMove == null)
            {
                Debug.LogError("No best move found for hint.");
                return;
            }

            (int x, int y) = bestMove.Value;
            GridCell cell = gridView.GetGridCell(x, y);

            if (cell == null)
            {
                Debug.LogError($"No UI element found for cell at position ({x}, {y}).");
                return;
            }

            cell.ShowHint();
        }
    }
}