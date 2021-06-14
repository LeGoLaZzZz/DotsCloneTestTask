using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace ViewModel.CellConnection
{
    [CreateAssetMenu(fileName = "CellViewConnectionsChannel",
        menuName = "Channels/ViewModel/CellViewConnectionsChannel", order = 0)]
    public class CellViewConnectionsChannel : ScriptableObject
    {
        [SerializeField] private bool isConnecting;

        public CellAddedEvent cellAdded = new CellAddedEvent();
        public CellRemovedEvent cellRemoved = new CellRemovedEvent();
        public ConnectionStartedEvent connectionStartedEvent = new ConnectionStartedEvent();
        public ConnectionEndedEvent connectionEndedEvent = new ConnectionEndedEvent();


        public bool IsConnecting => isConnecting;

        public void SetConnecting(bool isConnecting)
        {
            this.isConnecting = isConnecting;
        }

        public void CellAddedInvoke(CellView cellView)
        {
            cellAdded.Invoke(new CellAddedEventArgs(cellView));
        }

        public void CellRemovedInvoke()
        {
            cellRemoved.Invoke(new CellRemovedEventArgs());
        }

        public void ConnectionStartedInvoke()
        {
            connectionStartedEvent.Invoke();
        }

        public void ConnectionEndedInvoke(List<CellView> interactingCellViews, List<Cell> interactingCells)
        {
            connectionEndedEvent.Invoke(new ConnectionEndedEventArgs(interactingCellViews, interactingCells));
        }
    }

    #region Connection events

    [Serializable]
    public class CellAddedEvent : UnityEvent<CellAddedEventArgs>
    {
    }

    [Serializable]
    public class CellAddedEventArgs
    {
        public CellView cellView;

        public CellAddedEventArgs(CellView cellView)
        {
            this.cellView = cellView;
        }
    }

    [Serializable]
    public class CellRemovedEvent : UnityEvent<CellRemovedEventArgs>
    {
    }

    [Serializable]
    public class CellRemovedEventArgs
    {
    }

    [Serializable]
    public class ConnectionStartedEvent : UnityEvent
    {
    }

    [Serializable]
    public class ConnectionEndedEvent : UnityEvent<ConnectionEndedEventArgs>
    {
    }

    [Serializable]
    public class ConnectionEndedEventArgs
    {
        public List<CellView> cellViews;
        public List<Cell> cells;

        public ConnectionEndedEventArgs(List<CellView> cellViews, List<Cell> cells)
        {
            this.cellViews = cellViews;
            this.cells = cells;
        }
    }

    #endregion
}