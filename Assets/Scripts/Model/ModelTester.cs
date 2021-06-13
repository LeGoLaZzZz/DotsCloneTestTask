using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model
{
    public class ModelTester : MonoBehaviour
    {
        public Vector2Int cellGridSize = new Vector2Int(6, 6);

        public CellGrid CellGrid;
        public CellConnector CellConnector;
        public ChipsDropper ChipsDropper;


        public void InitCellGrid()
        {
            CellGrid = new CellGrid(cellGridSize);
            CellConnector = new CellConnector(CellGrid);
            ChipsDropper = new ChipsDropper(CellGrid);
            LogGrid();
        }

        public void FillGrid()
        {
            var filler = new ChipsFiller();
            filler.Fill(CellGrid);
            LogGrid();
        }


        public void RemoveCells(IEnumerable<Vector2Int> cells)
        {
            foreach (var cell in cells)
            {
                CellGrid.GetCell(cell.x, cell.y).RemoveChip();
            }

            LogGrid();
        }

        public void DropChips()
        {
            ChipsDropper.DropChips();
            LogGrid();
        }

        public void SpawnNewChips()
        {
            ChipsDropper.SpawnNewChips();
            LogGrid();
        }


        public void Connect(IEnumerable<Vector2Int> cells)
        {
            var connectCells = new List<Cell>();
            foreach (var cell in cells)
            {
                connectCells.Add(CellGrid.GetCell(cell.x, cell.y));
            }

            if (CellConnector.CanConnectScore(connectCells, out var isCycleConnection))
            {
                CellConnector.TryConnect(connectCells);
                LogGrid();
            }
        }

        public void LogGrid()
        {
            Debug.Log(CellGrid.ToString());
        }
    }
}