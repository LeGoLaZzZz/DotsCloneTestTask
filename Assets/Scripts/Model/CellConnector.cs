using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;

namespace Model
{
    public class CellConnector
    {
        private int _connectMinCount;

        private ChipsDropper _chipsDropper;
        private CellGrid _cellGrid;

        public event Action<int> ChipsRemovedEvent;

        public CellConnector(CellGrid cellGrid,ChipsDropper chipsDropper, int connectMinCount = 2)
        {
            _connectMinCount = connectMinCount;
            _cellGrid = cellGrid;
            _chipsDropper = chipsDropper;
        }


        public bool CanConnectScore(IEnumerable<Cell> cells, out bool isCycleConnection)
        {
            var canConnect = cells.Count() >= _connectMinCount && CanConnect(cells);
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

                var cellConnectionCondition = CellConnectionCondition(prevCell, cell);
                if (cellConnectionCondition != ConnectStatus.Success)
                {
                    // Debug.Log($"CantConnect [{prevCell.X} {prevCell.Y}][{cell.X} {cell.Y} - {cellConnectionCondition.ToString()}]");
                    return false;
                }

                if (connections[cellsId[cell] + cellsId[prevCell] * count]) //same connection twice is forbidden
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

        public GridChanges TryConnect(IEnumerable<Cell> cells)
        {
            if (!CanConnectScore(cells, out var isCycleConnection)) return null;

            var changes = new GridChanges[_cellGrid.GridSize.x * _cellGrid.GridSize.y];
            List<Cell> removed;

            if (!isCycleConnection)
            {
                removed = new List<Cell>();
                foreach (var cell in cells)
                {
                    cell.RemoveChip();
                    removed.Add(cell);
                }
            }
            else
            {
                removed = RemoveAllColor(cells.First().CurrentChip.ChipType);
            }

            var dropChanges = _chipsDropper.DropChips();
            var spawnNewChanges = _chipsDropper.SpawnNewChips();
            ChipsRemovedEvent?.Invoke(removed.Count);
            return new GridChanges(dropChanges, spawnNewChanges, removed);
        }

        private ConnectStatus CellConnectionCondition(Cell a, Cell b)
        {
            if (a.IsEmpty || b.IsEmpty) return ConnectStatus.EmptyCell;
            if (a == b) return ConnectStatus.SameCell;

            if (a.CurrentChip.ChipType != b.CurrentChip.ChipType) return ConnectStatus.DifferentColors;
            if (!CellUtils.IsNeighbour(a, b)) return ConnectStatus.NotNeighbours;

            return ConnectStatus.Success;
        }

        private bool CheckUnique(IEnumerable<Cell> cells)
        {
            var diffChecker = new HashSet<Cell>();
            bool allDifferent = cells.All(diffChecker.Add);

            return allDifferent;
        }

        private List<Cell> RemoveAllColor(ChipType chipType)
        {
            Cell cell;
            var removed = new List<Cell>();

            for (var x = 0; x < _cellGrid.GridSize.x; x++)
            {
                for (var y = 0; y < _cellGrid.GridSize.y; y++)
                {
                    cell = _cellGrid.GetCell(x, y);
                    if (cell.CurrentChip.ChipType == chipType)
                    {
                        cell.RemoveChip();
                        removed.Add(cell);
                    }
                }
            }

            return removed;
        }


        private enum ConnectStatus
        {
            Other,
            Success,
            SameCell,
            NotNeighbours,
            DifferentColors,
            EmptyCell,
        }
    }
}

[Serializable]
public class GridChanges
{
    public List<ChipsDropper.DropChange> dropChanges;
    public List<ChipsDropper.SpawnNewChange> spawnNewChanges;
    public List<Cell> removedChips;

    public GridChanges(List<ChipsDropper.DropChange> dropChanges, List<ChipsDropper.SpawnNewChange> spawnNewChanges,
        List<Cell> removedChips)
    {
        this.dropChanges = dropChanges;
        this.spawnNewChanges = spawnNewChanges;
        this.removedChips = removedChips;
    }
}