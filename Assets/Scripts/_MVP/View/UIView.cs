using System;
using Cysharp.Threading.Tasks;
using TicTacToe.UI.Interface;
using UnityEngine;
using Zenject;

namespace TicTacToe.View
{
    public class UIView
    {
         private readonly IUIButton restartButton;
         private readonly IUIButton undoButton;
         private readonly IUIText endGameText;
         private readonly IUIText playerTurnText;
         private readonly IUIImage endGamePanel;
         private readonly IUIImage hintMarker;
         private readonly IUIButton mainMenuButton;

        public UIView(
            [Inject(Id = "Restart")]        IUIButton restartButton,
            [Inject(Id = "Undo")]           IUIButton undoButton,
            [Inject(Id = "MainMenu")]       IUIButton mainMenuButton,
            [Inject(Id = "EndGameText")]    IUIText endGameText,
            [Inject(Id = "PlayerTurnText")] IUIText playerTurnText,
            [Inject(Id = "EndGamePanel")]   IUIImage endGamePanel,
            [Inject(Id = "HintMarker")]     IUIImage hintMarker)
        {
            this.restartButton = restartButton;
            this.undoButton = undoButton;
            this.mainMenuButton = mainMenuButton;
            this.endGameText = endGameText;
            this.playerTurnText = playerTurnText;
            this.endGamePanel = endGamePanel;
            this.hintMarker = hintMarker;
        }

        public void ShowEndGameUI(string message)
        {
            endGamePanel.SetActive(true);
            endGamePanel.PunchScale(Vector3.one * 0.2f, 0.1f);

            endGameText.SetText(message);
            endGameText.SetActive(true);

            restartButton.SetActive(true);
            restartButton.ShakePosition(2, 2f);
            restartButton.ShakeScale(1, 0.2f);

            hintMarker.Fade(0, 0f);
            hintMarker.SetActive(false);

            undoButton.SetActive(false);
            playerTurnText.SetActive(false);
        }

        public void UpdatePlayerTurnText(string message)
        {
            playerTurnText.SetText(message);
        }

        public void ResetUI()
        {
            endGameText.SetActive(false);
            restartButton.SetActive(false);
            endGamePanel.SetActive(false);
            undoButton.SetActive(false);
            playerTurnText.SetActive(true);
        }
        
        public async void HideHint()  
        {
            hintMarker.Fade(0, 0.2f);

            await UniTask.Delay(200);
            
            hintMarker.SetActive(false);
        }

        public void UpdateUndoButtonState(bool isActive)
        {
            undoButton.SetActive(isActive);
        }

        public void AddUndoButtonListener(System.Action callback)
        {
            undoButton.AddListener(callback);
        }

        public void AddExitButtonListener(System.Action callback)
        {
            mainMenuButton.AddListener(callback);
        }

        public void AddRestartButtonListener(Action action)
        {
            restartButton.AddListener(action);
        }
    }
}