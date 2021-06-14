using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UIElements;
using View;
using ViewModel.CellConnection;

namespace ViewModel.ChipCollecting
{
    public class ConnectionCollector : MonoBehaviour
    {
        [SerializeField] private CellViewConnectionsChannel cellViewConnectionsChannel;
        [SerializeField] private ChipCollectingChannel chipCollectingChannel;

        private CellConnector _cellConnector;
        private CellGridView _cellGridView;

        public void SetUp(CellConnector cellConnector, CellGridView cellGridView)
        {
            _cellConnector = cellConnector;
            _cellGridView = cellGridView;
        }

        private void OnEnable()
        {
            cellViewConnectionsChannel.connectionEndedEvent.AddListener(OnConnectionEnded);
        }


        private void OnDisable()
        {
            cellViewConnectionsChannel.connectionEndedEvent.RemoveListener(OnConnectionEnded);
        }


        private void OnConnectionEnded(ConnectionEndedEventArgs arg0)
        {
            if (_cellConnector.CanConnectScore(arg0.cells, out var isCycleConnection))
            {
                var gridChanges = _cellConnector.TryConnect(arg0.cells);
                ScoreOutChips(gridChanges.removedChips);
                chipCollectingChannel.SuccessConnectionScoredInvoke(arg0.cellViews, arg0.cells, isCycleConnection,
                    gridChanges);
            }
        }


        private void ScoreOutChips(List<Cell> cells)
        {
            foreach (var cell in cells)
            {
                _cellGridView.GetCellView(cell.X, cell.Y).ScoreOutChip();
            }
        }
    }
}