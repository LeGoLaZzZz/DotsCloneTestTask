using Model;
using UnityEngine;
using View;
using ViewModel.CellConnection;
using ViewModel.ChipCollecting;

namespace ViewModel
{
    public class ModelView : MonoBehaviour
    {
        [SerializeField] private CellGridViewGenerator cellGridViewGenerator;
        [SerializeField] private ChipViewFiller chipViewFiller;
        [SerializeField] private CellViewsConnector cellViewsConnector;
        [SerializeField] private ConnectionCollector connectionCollector;
        [SerializeField] private ChipViewsDropper chipViewsDropper;
        [SerializeField] private DropChipsGrid dropChipsGrid;

        public Vector2Int cellGridSize = new Vector2Int(6, 6);

        private CellGrid _cellGrid;
        private CellConnector _cellConnector;
        private ChipsDropper _chipsDropper;

        private CellGridView _cellGridView;

        [ContextMenu("Start view model")]
        public void StartViewModel()
        {
            InitializeModel();
            SetUpView(_cellGrid);
        }
        [ContextMenu("Log model grid")]
        public void LogGrid()
        {
            Debug.Log(_cellGrid.ToString());
        }
        
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

            cellViewsConnector.SetUp(_cellConnector, _cellGrid);
            connectionCollector.SetUp(_cellConnector, _cellGridView);
            dropChipsGrid.SetUp(_cellGridView);
            chipViewsDropper.SetUp(cellGrid, _cellGridView, dropChipsGrid, chipViewFiller);
        }


        private void Start()
        {
            StartViewModel();
        }
    }
}