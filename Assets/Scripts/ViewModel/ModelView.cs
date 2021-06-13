using System;
using Model;
using UnityEngine;

namespace View
{
    public class ModelView : MonoBehaviour
    {
        [SerializeField] private CellGridViewGenerator cellGridViewGenerator;
        [SerializeField] private ChipViewFiller chipViewFiller;

        public Vector2Int cellGridSize = new Vector2Int(6, 6);

        private CellGrid _cellGrid;
        private CellConnector _cellConnector;
        private ChipsDropper _chipsDropper;

        private CellGridView _cellGridView;

        public void InitializeModel()
        {
            _cellGrid = new CellGrid(cellGridSize);
            _cellConnector = new CellConnector(_cellGrid);
            _chipsDropper = new ChipsDropper(_cellGrid);

            var filler = new ChipsFiller();
            filler.Fill(_cellGrid);
        }

        public void SetUpView(CellGrid cellGrid)
        {
            _cellGridView = cellGridViewGenerator.GenerateGrid(cellGridSize);
            chipViewFiller.FillCellGridView(_cellGridView, cellGrid);
        }

        [ContextMenu("Start view model")]
        public void StartViewModel()
        {
            InitializeModel();
            SetUpView(_cellGrid);
        }


        private void Start()
        {
            StartViewModel();
        }
    }
}