using TicTacToe.UI.Adapter;
using TicTacToe.UI.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.Bootstrap
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button undoButton;
        [SerializeField] private TMP_Text endGameText;
        [SerializeField] private TMP_Text playerTurnText;
        [SerializeField] private Image endGamePanel;
        [SerializeField] private Image hintMarker;
        [SerializeField] private Button mainMenuButton;

        public override void InstallBindings()
        {
            Container.Bind<IUIButton>().WithId("Restart").To<UIButtonAdapter>().FromInstance(new UIButtonAdapter(restartButton));
            Container.Bind<IUIButton>().WithId("Undo").To<UIButtonAdapter>().FromInstance(new UIButtonAdapter(undoButton));
            Container.Bind<IUIText>().WithId("EndGameText").To<UITextAdapter>().FromInstance(new UITextAdapter(endGameText));
            Container.Bind<IUIText>().WithId("PlayerTurnText").To<UITextAdapter>().FromInstance(new UITextAdapter(playerTurnText));
            Container.Bind<IUIImage>().WithId("EndGamePanel").To<UIImageAdapter>().FromInstance(new UIImageAdapter(endGamePanel));
            Container.Bind<IUIImage>().WithId("HintMarker").To<UIImageAdapter>().FromInstance(new UIImageAdapter(hintMarker));
            Container.Bind<IUIButton>().WithId("MainMenu").To<UIButtonAdapter>().FromInstance(new UIButtonAdapter(mainMenuButton));
        }
    }
}