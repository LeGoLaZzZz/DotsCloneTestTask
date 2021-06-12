using System;
using System.Text;
using UnityEngine;

namespace Model
{
    public class CellGrid
    {
        private Cell[,] _cellGrid;
        private Vector2Int _gridSize;

        public Vector2Int GridSize => _gridSize;

        public CellGrid(Vector2Int gridSize)
        {
            _gridSize = gridSize;
            InitializeGrid(_gridSize);
        }

        public Cell GetCell(int x, int y)
        {
            if (x >= _gridSize.x)
                throw new ArgumentOutOfRangeException(nameof(x), "invalid cell X");

            if (y >= _gridSize.y)
                throw new ArgumentOutOfRangeException(nameof(y), "invalid cell Y");

            return _cellGrid[x, y];
        }

        
        public void MoveChip(Vector2Int from,Vector2Int to)
        {
            var cellFrom = GetCell(from.x,from.y);
            var cellTo = GetCell(to.x,to.y);

            var chip = cellFrom.CurrentChip;
            cellFrom.RemoveChip();
            cellTo.SetChip(chip);
            
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Grid size: x:{GridSize.x} y:{GridSize.y}");
            sb.Append("\n");

            for (var y = GridSize.y - 1; y >= 0; y--)
            {
                for (var x = 0; x < GridSize.x; x++)
                {
                    sb.Append($"[{x,2},{y,2} ");
                    if (_cellGrid[x, y].IsEmpty) sb.Append($"- ----Empty");
                    else sb.Append($"- {_cellGrid[x, y].CurrentChip.ChipType,10}");
                    sb.Append("] ");
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }

        private void InitializeGrid(Vector2Int gridSize)
        {
            _cellGrid = new Cell[gridSize.x, gridSize.x];

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    _cellGrid[x, y] = new Cell(x, y);
                }
            }
        }
    }
}