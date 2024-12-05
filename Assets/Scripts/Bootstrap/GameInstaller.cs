using TicTacToe.AssetManager;
using TicTacToe.AssetManager.Interface;
using TicTacToe.Enum;
using TicTacToe.Model;
using TicTacToe.Player;
using TicTacToe.Player.Strategy;
using TicTacToe.Presenter;
using TicTacToe.Services;
using TicTacToe.View;
using TicTacToe.UI;
using TicTacToe.UI.Adapter;
using TicTacToe.View.Cell;
using Zenject;
using UnityEngine;

namespace TicTacToe.Bootstrap
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GridCell[] gridCells;
        [SerializeField] private StopwatchWidget stopwatchWidget;

        public override void InstallBindings()
        {
            Container.Bind<IWinConditionComparer>().To<WinConditionComparer>().AsSingle();
            Container.Bind<IAvailableCellService>().To<AvailableCellService>().AsSingle();
            
            Container.Bind<GridModel>().AsSingle().NonLazy();
            Container.Bind<GridView>().AsSingle().WithArguments(gridCells);

            var gameSettings = ServiceLocator.GetOrCreateService<GameSettings>();
            InitializePlayers(gameSettings.GameType, gameSettings.Difficulty);

            Container.Bind<UIView>().AsSingle();
            Container.Bind<GameModel>().AsSingle();

            Container.Bind<IResourceManager>().To<ResourceManager>().AsSingle();
            Container.Bind<StopwatchAdapter>().AsSingle().WithArguments(stopwatchWidget);
            
            Container.BindInterfacesAndSelfTo<GameLoopPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UndoPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HintPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GridPresenter>().AsSingle().NonLazy();
        }

        private void InitializePlayers(EGameType selectedEGameType, EDifficulty difficulty)
        {
            GridModel gridModel = Container.Resolve<GridModel>();
            
            switch (selectedEGameType)
            {
                case EGameType.PlayerVsAI:

                    Container.Bind<IPlayer>().WithId(CONSTS.Player1).To<Player.Player>().AsTransient().WithArguments(EPlayerId.X, new HumanMoveStrategy(gridModel));
                    Container.Bind<IPlayer>().WithId(CONSTS.Player2).To<Player.Player>().AsTransient().WithArguments(EPlayerId.O, new AIMoveStrategy(difficulty, gridModel));
                    break;

                case EGameType.AIVsAI:

                    Container.Bind<IPlayer>().WithId(CONSTS.Player1).To<Player.Player>().AsTransient().WithArguments(EPlayerId.X, new AIMoveStrategy(difficulty, gridModel));
                    Container.Bind<IPlayer>().WithId(CONSTS.Player2).To<Player.Player>().AsTransient().WithArguments(EPlayerId.O, new AIMoveStrategy(difficulty, gridModel));
                    break;

                default: // PlayerVsPlayer
                    Container.Bind<IPlayer>().WithId(CONSTS.Player1).To<Player.Player>().AsTransient().WithArguments(EPlayerId.X, new HumanMoveStrategy(gridModel));
                    Container.Bind<IPlayer>().WithId(CONSTS.Player2).To<Player.Player>().AsTransient().WithArguments(EPlayerId.O, new HumanMoveStrategy(gridModel));
                    break;
            }
        }
    }
}