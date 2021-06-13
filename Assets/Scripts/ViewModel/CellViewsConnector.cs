using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using UnityEngine;
using UnityEngine.Events;

namespace View
{
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

    public class ConnectionEndedEvent : UnityEvent
    {
    }

    public class CellViewsConnector : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private CellViewInteractChannel cellViewInteractChannel;

        [Header("Monitoring")]
        [SerializeField] private List<CellView> interactingCellViews;
        [SerializeField] private List<Cell> interactingCells;
        [SerializeField] private bool isConnecting;

        private CellConnector _cellConnector;
        private CellGrid _cellGrid;


        public bool IsConnecting => isConnecting;
        public ReadOnlyCollection<CellView> InteractingCellViews => interactingCellViews.AsReadOnly();


        public CellAddedEvent cellAdded = new CellAddedEvent();
        public CellRemovedEvent cellRemoved = new CellRemovedEvent();
        public ConnectionStartedEvent connectionStartedEvent = new ConnectionStartedEvent();
        public ConnectionEndedEvent connectionEndedEvent = new ConnectionEndedEvent();


        public void SetUp(CellConnector cellConnector, CellGrid cellGrid)
        {
            _cellConnector = cellConnector;
            _cellGrid = cellGrid;
        }


        private void OnEnable()
        {
            cellViewInteractChannel.cellInteractionStartedEvent.AddListener(OnCellInteracted);
            cellViewInteractChannel.cellsInteractionStoppedEvent.AddListener(OnInteractStopped);
        }


        private void OnDisable()
        {
            cellViewInteractChannel.cellInteractionStartedEvent.RemoveListener(OnCellInteracted);
            cellViewInteractChannel.cellsInteractionStoppedEvent.RemoveListener(OnInteractStopped);
        }

        private void OnInteractStopped()
        {
            if (_cellConnector.CanConnectScore(interactingCells,out var isCycleConnection))
            {
                Debug.Log("Connection good");
                ClearCells();
            }
            else
            {
                Debug.Log("Connection bad");
                ClearCells();
            }
            isConnecting = false;
            connectionEndedEvent.Invoke();
        }


        private void OnCellInteracted(CellInteractionStartedEventArgs arg0)
        {
            if (!isConnecting) FirstInteraction(arg0.cell);
            else ExtraConnection(arg0.cell);
        }

        private void ExtraConnection(CellView newCell)
        {
            if (TryUndoSelection(newCell)) return;

            if (newCell.IsEmpty)
            {
                Debug.LogError("Cell empty");
                return;
            }

            TryAddInteractionCell(newCell);
        }

        private bool TryUndoSelection(CellView newCell)
        {
            if (!interactingCellViews.Contains(newCell)) return false;
            if (interactingCellViews.Count <= 1) return false;

            //if it is previous point - undo the last point
            if (interactingCellViews[interactingCellViews.Count - 2] == newCell)
            {
                RemoveInteractionCell(interactingCellViews[interactingCellViews.Count - 1]);
                cellRemoved.Invoke(new CellRemovedEventArgs());
                return true;
            }

            return false;
        }

        private void FirstInteraction(CellView cell)
        {
            if (cell.IsEmpty)
            {
                Debug.LogError("Cell empty");
                return;
            }

            isConnecting = true;
            connectionStartedEvent.Invoke();
            ClearCells();
            TryAddInteractionCell(cell);
        }


        private bool TryAddInteractionCell(CellView newCell)
        {
            AddInteractionCell(newCell);
            var canConnect = _cellConnector.CanConnect(interactingCells);
            if (!canConnect) RemoveInteractionCell(newCell);
            else cellAdded.Invoke(new CellAddedEventArgs(newCell));
            return canConnect;
        }

        private void AddInteractionCell(CellView cellView)
        {
            interactingCells ??= new List<Cell>();
            interactingCellViews ??= new List<CellView>();

            interactingCellViews.Add(cellView);
            interactingCells.Add(_cellGrid.GetCell(cellView.Coords.x, cellView.Coords.y));
        }

        private void RemoveInteractionCell(CellView cellView)
        {
            if (interactingCells == null) return;
            if (interactingCellViews == null) return;

            for (var i = interactingCellViews.Count - 1; i >= 0; i--)
            {
                if (interactingCellViews[i] == cellView)
                {
                    interactingCellViews.RemoveAt(i);
                    interactingCells.RemoveAt(i);
                    break;
                }
            }
            
        }

        private void ClearCells()
        {
            interactingCells = new List<Cell>();
            interactingCellViews = new List<CellView>();
        }
    }
}