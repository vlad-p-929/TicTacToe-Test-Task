using NUnit.Framework;
using TicTacToe.Enum;
using TicTacToe.Services;

namespace TicTacToe.Tests
{
    public class WinCheckerTests
    {
        private IWinConditionComparer winComparer;

        [SetUp]
        public void SetUp()
        {
            winComparer = new WinConditionComparer();
        }

        [Test]
        public void Test_Win_DetectsRowWinCondition()
        {
            var grid = new[,]
            {
                { EPlayerId.X, EPlayerId.X, EPlayerId.X },
                { EPlayerId.None, EPlayerId.None, EPlayerId.None },
                { EPlayerId.None, EPlayerId.None, EPlayerId.None }
            };

            Assert.IsTrue(winComparer.CheckWinCondition(grid));
        }
        
        [Test]
        public void Test_Win_DetectsColumnWinCondition()
        {
            var grid = new[,]
            {
                { EPlayerId.X, EPlayerId.None, EPlayerId.None },
                { EPlayerId.X, EPlayerId.None, EPlayerId.None },
                { EPlayerId.X, EPlayerId.None, EPlayerId.None }
            };

            Assert.IsTrue(winComparer.CheckWinCondition(grid));
        }

        [Test]
        public void Test_No_Win()
        {
            var grid = new[,]
            {
                { EPlayerId.X, EPlayerId.O, EPlayerId.X },
                { EPlayerId.None, EPlayerId.None, EPlayerId.None },
                { EPlayerId.None, EPlayerId.None, EPlayerId.None }
            };

            Assert.IsFalse(winComparer.CheckWinCondition(grid));
        }

        [Test]
        public void Test_Win_DetectsDiagonalWinCondition()
        {
            var grid = new[,]
            {
                { EPlayerId.X, EPlayerId.None, EPlayerId.None },
                { EPlayerId.None, EPlayerId.X, EPlayerId.None },
                { EPlayerId.None, EPlayerId.None, EPlayerId.X }
            };

            Assert.IsTrue(winComparer.CheckWinCondition(grid));
        }
    }
}