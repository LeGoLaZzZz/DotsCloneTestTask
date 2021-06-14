using System;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace ViewModel.CellInteraction
{
    [CreateAssetMenu(fileName = "CellViewInteractChannel", menuName = "Channels/ViewModel/CellViewInteractChannel",
        order = 0)]
    public class CellViewInteractChannel : ScriptableObject
    {
        public CellInteractionStartedEvent cellInteractionStartedEvent = new CellInteractionStartedEvent();
        public CellsInteractionStoppedEvent cellsInteractionStoppedEvent = new CellsInteractionStoppedEvent();

        public void InteractionStartInvoke(CellView cell)
        {
            cellInteractionStartedEvent.Invoke(new CellInteractionStartedEventArgs(cell));
        }

        public void InteractionStoppedInvoke()
        {
            cellsInteractionStoppedEvent.Invoke();
        }
    }

    #region Interaction events

    [Serializable]
    public class CellsInteractionStoppedEvent : UnityEvent
    {
    }

    [Serializable]
    public class CellInteractionStartedEvent : UnityEvent<CellInteractionStartedEventArgs>
    {
    }

    [Serializable]
    public class CellInteractionStartedEventArgs
    {
        public CellView cell;

        public CellInteractionStartedEventArgs(CellView cell)
        {
            this.cell = cell;
        }
    }

    #endregion
}