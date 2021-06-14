using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace ViewModel.ChipCollecting
{
    [CreateAssetMenu(fileName = "ChipCollectingChannel", menuName = "Channels/ViewModel/ChipCollectingChannel", order = 0)]
    public class ChipCollectingChannel : ScriptableObject
    {
        public SuccessConnectionScoredEvent successConnectionScoredEvent = new SuccessConnectionScoredEvent();
        public FailConnectionEvent failConnectionEvent = new FailConnectionEvent();

        public void SuccessConnectionScoredInvoke(List<CellView> cellViews, List<Cell> cells, bool isCycleConnection, GridChanges gridChanges)
        {
            successConnectionScoredEvent.Invoke(new SuccessConnectionEventArgs(cellViews, cells, isCycleConnection, gridChanges));
        }

        public void FailConnectionInvoke(List<CellView> cellViews, List<Cell> cells)
        {
            failConnectionEvent.Invoke(new FailConnectionEventArgs(cellViews, cells));
        }
    }

    #region Chips collecting events

    [Serializable]
    public class SuccessConnectionScoredEvent : UnityEvent<SuccessConnectionEventArgs>
    {
    }

    [Serializable]
    public class SuccessConnectionEventArgs
    {
        public List<CellView> cellViews;
        public List<Cell> cells;
        public bool isCycleConnection;
        public GridChanges gridChanges;

        public SuccessConnectionEventArgs(List<CellView> cellViews, List<Cell> cells, bool isCycleConnection, GridChanges gridChanges)
        {
            this.cellViews = cellViews;
            this.cells = cells;
            this.isCycleConnection = isCycleConnection;
            this.gridChanges = gridChanges;
        }
    }

    [Serializable]
    public class FailConnectionEvent : UnityEvent<FailConnectionEventArgs>
    {
    }

    [Serializable]
    public class FailConnectionEventArgs
    {
        public List<CellView> cellViews;
        public List<Cell> cells;

        public FailConnectionEventArgs(List<CellView> cellViews, List<Cell> cells)
        {
            this.cellViews = cellViews;
            this.cells = cells;
        }
    }

    #endregion
}