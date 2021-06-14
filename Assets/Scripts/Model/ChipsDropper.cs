using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Model
{
    public class ChipsDropper
    {
        private CellGrid _cellGrid;
        private ChipGenerator _chipGenerator;

        public ChipsDropper(CellGrid cellGrid)
        {
            _cellGrid = cellGrid;
            _chipGenerator = new ChipGenerator();
        }

        public List<DropChange> DropChips()
        {
            Cell from;
            Cell to;

            var changes = new List<DropChange>();
            var dropCount = 0;
            for (int x = 0; x < _cellGrid.GridSize.x; x++)
            {
                dropCount = 0;
                for (int y = 0; y < _cellGrid.GridSize.y; y++)
                {
                    if (_cellGrid.GetCell(x, y).IsEmpty)
                    {
                        dropCount++;
                    }
                    else
                    {
                        if (dropCount == 0) continue;
                        from = _cellGrid.GetCell(x, y);
                        to = _cellGrid.GetCell(x, y - dropCount);
                        _cellGrid.MoveChip(from, to);
                        changes.Add(new DropChange(from, to, to.CurrentChip));
                    }
                }
            }

            return changes;
        }


        public List<SpawnNewChange> SpawnNewChips()
        {
            Cell cell;
            var changes = new List<SpawnNewChange>();

            for (int x = 0; x < _cellGrid.GridSize.x; x++)
            {
                for (int y = 0; y < _cellGrid.GridSize.y; y++)
                {
                    cell = _cellGrid.GetCell(x, y);
                    if (cell.IsEmpty)
                    {
                        cell.SetChip(_chipGenerator.GetRandomChip());
                        changes.Add(new SpawnNewChange(cell, cell.CurrentChip));
                    }
                }
            }

            return changes;
        }


        [Serializable]
        public class DropChange
        {
            public Cell from;
            public Cell to;
            public Chip chip;

            public DropChange(Cell from, Cell to, Chip chip)
            {
                this.from = from;
                this.to = to;
                this.chip = chip;
            }
        }

        [Serializable]
        public class SpawnNewChange
        {
            public Cell to;
            public Chip chip;

            public SpawnNewChange(Cell to, Chip chip)
            {
                this.to = to;
                this.chip = chip;
            }
        }
    }
}