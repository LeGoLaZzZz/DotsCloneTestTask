using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Model
{
    public class CellConnector
    {
        public int connectMinCount;
        private ChipsDropper _chipsDropper;
        private CellGrid _cellGrid;

        public CellConnector(CellGrid cellGrid, int connectMinCount = 2)
        {
            this.connectMinCount = connectMinCount;
            _chipsDropper = new ChipsDropper(cellGrid);
        }


        public bool CanConnectScore(IEnumerable<Cell> cells, out bool isCycleConnection)
        {
            var canConnect = cells.Count() >= connectMinCount && CanConnect(cells);
            isCycleConnection = !CheckUnique(cells);
            return canConnect;
        }

        public bool CanConnect(IEnumerable<Cell> cells)
        {

            //For checking repeated connections
            var distinct = cells.Distinct().ToList();
            var count = distinct.Count;
            var connections = new bool[count * count];
            var cellsId = new Dictionary<Cell, int>();

            for (var j = 0; j < distinct.Count; j++)
            {
                cellsId.Add(distinct[j], j);
            }

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

                if (connections[cellsId[cell] + cellsId[prevCell] * count])//same connection twice is forbidden
                {
                    return false; 
                }
                else
                {
                    connections[cellsId[prevCell] + cellsId[cell] * count] = true;
                    connections[cellsId[cell] + cellsId[prevCell] * count] = true;
                }


                prevCell = cell;
            }


            return true;
        }

        public bool TryConnect(IEnumerable<Cell> cells)
        {
            if (!CanConnectScore(cells, out var isCycleConnection)) return false;

            if (!isCycleConnection)
            {
                foreach (var cell in cells)
                {
                    cell.RemoveChip();
                }
            }
            else
            {
                RemoveAllColor(cells.First().CurrentChip.ChipType);
            }

            _chipsDropper.DropChips();
            _chipsDropper.SpawnNewChips();

            return true;
        }

        private bool CellConnectionCondition(Cell a, Cell b)
        {
            if (a.IsEmpty || b.IsEmpty) return false;
            if (a == b)
            {
                Debug.Log("Cant same " + a.X + " " + b.Y);
                return false;
            }

            if (a.CurrentChip.ChipType != b.CurrentChip.ChipType) return false;
            if (!CellUtils.IsNeighbour(a, b))
            {
                Debug.Log("Cant not neighbours " + a.X + " " + b.Y);
                return false;
            }

            return true;
        }

        private bool CheckUnique(IEnumerable<Cell> cells)
        {
            var diffChecker = new HashSet<Cell>();
            bool allDifferent = cells.All(diffChecker.Add);

            return allDifferent;
        }

        private void RemoveAllColor(ChipType chipType)
        {
            Cell cell;

            for (var x = 0; x < _cellGrid.GridSize.x; x++)
            {
                for (var y = 0; y < _cellGrid.GridSize.y; y++)
                {
                    cell = _cellGrid.GetCell(x, y);
                    if (cell.CurrentChip.ChipType == chipType)
                    {
                        cell.RemoveChip();
                    }
                }
            }
        }
    }
}