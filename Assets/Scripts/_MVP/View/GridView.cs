using System;
using System.IO;
using TicTacToe.AssetManager.Interface;
using TicTacToe.Enum;
using TicTacToe.View.Cell;
using UnityEngine;

namespace TicTacToe.View
{
    public class GridView
    {
        private readonly GridCell[] cells;

        public GridView(GridCell[] cells)
        {
            this.cells = cells;
        }

        public void Initialize(int rows, int cols, IResourceManager resourceManager)
        {
            if (!resourceManager.TryLoad(CONSTS.xSymbol, out Sprite firstPlayerSprite))
            {
                throw new FileNotFoundException();
            }

            if (!resourceManager.TryLoad(CONSTS.oSymbol, out Sprite secondPlayerSprite))
            {
                throw new FileNotFoundException();
            }

            int index = 0;

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    var gridCell = cells[index];
                    gridCell.Initialize(x, y, firstPlayerSprite, secondPlayerSprite);

                    index++;
                }
            }
        }

        public void SubscribeToCells(Action<int,int> onClick)
        {
            foreach (GridCell cell in cells)
            {
                cell.OnCellClicked += onClick;
            }
        }

        public void ApplyVisualMove(int x, int y, EPlayerId playerId)
        {
            GetGridCell(x, y)?.OccupyBy(playerId);
        }

        public void ResetVisualGrid()
        {
            foreach (var cell in cells)
            {
                cell.ResetCell();
            }
        }

        public void UndoVisualMove(int x, int y)
        {
            foreach (GridCell cell in cells)
            {
                if ((cell.x, cell.y) == (x, y))
                {
                    cell.ResetCell();
                }
            }
        }

        public GridCell GetGridCell(int x, int y)
        {
            foreach (var cell in cells)
            {
                if ((cell.x, cell.y) == (x, y))
                {
                    return cell;
                }
            }

            return null;
        }
    }
}