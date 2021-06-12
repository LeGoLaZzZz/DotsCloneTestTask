using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{
    public class CellConnector
    {
        public int connectMinCount;
        private CellGrid _cellGrid;
        private ChipsDropper _chipsDropper;

        public CellConnector(CellGrid cellGrid, int connectMinCount = 2)
        {
            _cellGrid = cellGrid;
            this.connectMinCount = connectMinCount;
            _chipsDropper = new ChipsDropper(_cellGrid);
        }

        public bool CanConnect(IEnumerable<Cell> cells)
        {
            if (cells.Count() < connectMinCount) return false;

            Cell prevCell = null;
            foreach (var cell in cells)
            {
                if (prevCell == null)
                {
                    prevCell = cell;
                    continue;
                }

                if (!CellConnectionCondition(prevCell, cell))
                {
                    //Debug.Log($"CantConnect [{prevCell.X} {prevCell.Y}][{cell.X} {cell.Y}]");
                    return false;
                }

                prevCell = cell;
            }

            return true;
        }

        public bool TryConnect(IEnumerable<Cell> cells)
        {
            if (!CanConnect(cells)) return false;

            foreach (var cell in cells)
            {
                cell.RemoveChip();
            }

            _chipsDropper.DropChips();
            _chipsDropper.SpawnNewChips();

            return true;
        }

        private bool CellConnectionCondition(Cell a, Cell b)
        {
            if (a.IsEmpty || b.IsEmpty) return false;
            if (a.CurrentChip.ChipType != b.CurrentChip.ChipType) return false;
            if (!CellUtils.IsNeighbour(a, b)) return false;

            return true;
        }
    }
}