using UnityEngine;

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

        public void DropChips()
        {
            var dropCount = 0;
            for (int x = 0; x < _cellGrid.GridSize.x; x++)
            {
                dropCount = 0;
                for (int y = 0; y < _cellGrid.GridSize.y; y++)
                {
                    if (_cellGrid.GetCell(x, y).IsEmpty) dropCount++;
                    else _cellGrid.MoveChip(new Vector2Int(x,y),new Vector2Int(x,y - dropCount));
                }
            }
        }


        public void SpawnNewChips()
        {
            for (int x = 0; x < _cellGrid.GridSize.x; x++)
            {
                for (int y = 0; y < _cellGrid.GridSize.y; y++)
                {
                    if (_cellGrid.GetCell(x, y).IsEmpty)
                        _cellGrid.GetCell(x, y).SetChip(_chipGenerator.GetRandomChip());
                }
            }
        }
    }
}