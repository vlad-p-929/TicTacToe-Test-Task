using Cysharp.Threading.Tasks;
using TicTacToe.Model;
using TicTacToe.Services;
using TicTacToe.UI.Adapter;
using Zenject;

namespace TicTacToe.View
{
    public class GameLoopPresenter : IInitializable
    {
        private readonly GameModel gameModel;
        private readonly GridModel gridModel;
        private readonly UIView uiView;
        private readonly StopwatchAdapter stopwatch;
        
        private readonly IWinConditionComparer winComparerService;
        
        [Inject]
        public GameLoopPresenter(IWinConditionComparer winComparerService, GameModel gameModel, GridModel gridModel,
            GridView gridView, UIView uiView, StopwatchAdapter stopwatch)
        {
            this.winComparerService = winComparerService;
            
            this.gameModel = gameModel;
            this.gridModel = gridModel;
            this.uiView = uiView;
            this.stopwatch = stopwatch;
        }

        public void Initialize()
        {
            uiView.AddRestartButtonListener(StartGame);
            uiView.AddExitButtonListener(FinishGame);
            
            gameModel.OnPlayerChanged += ChangeTurns;
            
            StartGame();
        }

        private void StartGame()
        {
            stopwatch.ResetTimer();
            gridModel.ResetLogicalGrid();
            gameModel.ResetGame();
            uiView.ResetUI();
            uiView.UpdatePlayerTurnText($"Turn of {gameModel.GetCurrentPlayerName()}");
            StartGameLoop().Forget();
        }
        
        private void FinishGame()
        {
            gameModel.ResetGame();
            gridModel.ResetLogicalGrid();
            uiView.ResetUI();
        }

        private async UniTaskVoid StartGameLoop()
        {
            stopwatch.StartTimer();
            
            while (!gameModel.IsGameOver)
            {
                var won = await MakeMoveAsync();

                if (gameModel.IsGameOver)
                {
                    stopwatch.StopTimer();
                    var resultMessage = won ? $"{gameModel.GetCurrentPlayerName()} wins!" : "Draw! No cells left!";
                    uiView.ShowEndGameUI(resultMessage);
                    break;
                }
            }
        }

        private async UniTask<bool> MakeMoveAsync()
        {
            await gameModel.CurrentPlayer.MakeMoveAsync();
            
            if (winComparerService.CheckWinCondition(gridModel.Grid))
            {
                gameModel.GameOver();
                return true;
            }

            if (!gridModel.HasAvailableCells())
            {
                gameModel.GameOver();
                return false;
            }

            gameModel.SwitchPlayer();
            return false;
        }

        private void ChangeTurns()
        {
            uiView.UpdatePlayerTurnText($"Turn of {gameModel.GetCurrentPlayerName()}");
        }
    }
}