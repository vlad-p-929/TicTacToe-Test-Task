using System.IO;
using NUnit.Framework;
using TicTacToe.Enum;
using TicTacToe.Model;
using TicTacToe.Services;
using UnityEngine;

namespace TicTacToe.Tests
{
    public class GameManagerTester
    {
        private GridModel gridModel;
        private WinConditionComparer winComparer;

        [SetUp]
        public void SetUp()
        {
            winComparer = new WinConditionComparer();
            gridModel = new GridModel(new AvailableCellService());
            gridModel.Initialize(3, 3);
        }

        [Test]
        public void Hint_ShowsCorrectAvailableCellTopRight()
        {
            gridModel.MakeMove(0, 1, EPlayerId.X);
            gridModel.MakeMove(2, 2, EPlayerId.O);
            gridModel.MakeMove(0, 0, EPlayerId.X);
            gridModel.MakeMove(1, 0, EPlayerId.O);

            var hint = gridModel.FindBestAvailableCell(EPlayerId.X);

            Assert.IsTrue(hint.HasValue);
            Assert.AreEqual((0, 2), hint.Value);
        }

        [Test]
        public void Hint_ShowsCorrectAvailableCellBottomLeft()
        {
            gridModel.MakeMove(2, 1, EPlayerId.X);
            gridModel.MakeMove(1, 2, EPlayerId.O);
            gridModel.MakeMove(2, 2, EPlayerId.X);
            gridModel.MakeMove(1, 0, EPlayerId.O);

            var hint = gridModel.FindBestAvailableCell(EPlayerId.X);

            Assert.IsTrue(hint.HasValue);
            Assert.AreEqual((2, 0), hint.Value);
        }

        [Test]
        public void Hint_ShowsCorrectAvailableCellDiagonal()
        {
            gridModel.MakeMove(0, 0, EPlayerId.X);
            gridModel.MakeMove(1, 2, EPlayerId.O);
            gridModel.MakeMove(1, 1, EPlayerId.X);
            gridModel.MakeMove(1, 0, EPlayerId.O);

            var hint = gridModel.FindBestAvailableCell(EPlayerId.X);

            Assert.IsTrue(hint.HasValue);
            Assert.AreEqual((2, 2), hint.Value);
        }

        [Test]
        public void Test_MakeMove_UpdatesGridCorrectly()
        {
            bool result = gridModel.MakeMove(0, 0, EPlayerId.X);

            Assert.IsTrue(result);
            Assert.AreEqual(EPlayerId.X, gridModel.GetCellValue(0, 0));
        }

        [Test]
        public void Test_MakeMove_FailsForOccupiedCell()
        {
            gridModel.MakeMove(0, 0, EPlayerId.X);
            bool result = gridModel.MakeMove(0, 0, EPlayerId.O);

            Assert.IsFalse(result);
        }

        [Test]
        public void Test_Undo_RevertsLastMove()
        {
            gridModel.MakeMove(0, 0, EPlayerId.X);
            gridModel.MakeMove(1, 1, EPlayerId.O);

            bool undoSuccessful = gridModel.UndoMove();

            Assert.IsTrue(undoSuccessful);
            Assert.AreEqual(EPlayerId.None, gridModel.GetCellValue(1, 1));
        }

        [Test]
        public void Test_Win_DetectsRowWinCondition()
        {
            gridModel.MakeMove(0, 0, EPlayerId.X);
            gridModel.MakeMove(1, 0, EPlayerId.O);
            gridModel.MakeMove(0, 1, EPlayerId.X);
            gridModel.MakeMove(1, 2, EPlayerId.O);
            gridModel.MakeMove(0, 2, EPlayerId.X);

            var winConditionMet = winComparer.CheckWinCondition(gridModel.Grid);

            Assert.IsTrue(winConditionMet);
        }

        [Test]
        public void Test_Win_DetectsColumnWinCondition()
        {
            gridModel.MakeMove(0, 0, EPlayerId.X);
            gridModel.MakeMove(1, 1, EPlayerId.O);
            gridModel.MakeMove(1, 0, EPlayerId.X);
            gridModel.MakeMove(1, 2, EPlayerId.O);
            gridModel.MakeMove(2, 0, EPlayerId.X);

            var winConditionMet = winComparer.CheckWinCondition(gridModel.Grid);

            Assert.IsTrue(winConditionMet);
        }

        [Test]
        public void Test_Lose_DetectsDrawCondition()
        {
            gridModel.MakeMove(0, 0, EPlayerId.X);
            gridModel.MakeMove(0, 1, EPlayerId.O);
            gridModel.MakeMove(0, 2, EPlayerId.X);

            gridModel.MakeMove(1, 0, EPlayerId.O);
            gridModel.MakeMove(1, 1, EPlayerId.X);
            gridModel.MakeMove(1, 2, EPlayerId.O);

            gridModel.MakeMove(2, 0, EPlayerId.X);
            gridModel.MakeMove(2, 2, EPlayerId.O);
            gridModel.MakeMove(2, 1, EPlayerId.X);

            var hasAvailableCells = gridModel.HasAvailableCells();

            Assert.IsFalse(hasAvailableCells);
        }

        [Test]
        public void Test_Exist_AssetBundle()
        {
            string bundlePath = Path.Combine(Application.streamingAssetsPath, CONSTS.AssetBundleName);

            Assert.IsTrue(File.Exists(bundlePath), $"AssetBundle not found at path: {bundlePath}");

            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            Assert.IsNotNull(bundle, "Failed to load AssetBundle!");

            bundle.Unload(true);
        }
    }
}