using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerInput
{
    [CreateAssetMenu(fileName = "InputChannel", menuName = "Input/InputChannel", order = 0)]
    public class InputChannel : ScriptableObject
    {
        public InteractButtonPressedEvent interactButtonPressedEvent = new InteractButtonPressedEvent();
        public InteractButtonReleasedEvent interactButtonReleasedEvent = new InteractButtonReleasedEvent();
        [SerializeField] private bool isInteracting;

        public Vector2 InteractPosition { get; set; }
        public bool IsInteracting
        {
            get => isInteracting;
            set => isInteracting = value;
        }

        public void InteractButtonPressedInvoke(Vector2 position)
        {
            interactButtonPressedEvent.Invoke(new InteractButtonPressedEventArgs(position));
        }

        public void InteractButtonReleasedInvoke(Vector2 position)
        {
            interactButtonReleasedEvent.Invoke(new InteractButtonReleasedEventArgs(position));
        }
    }

    #region Input events

    [Serializable]
    public class InteractButtonPressedEvent : UnityEvent<InteractButtonPressedEventArgs>
    {
    }

    [Serializable]
    public class InteractButtonPressedEventArgs
    {
        public Vector2 screenPoint;

        public InteractButtonPressedEventArgs(Vector2 screenPoint)
        {
            this.screenPoint = screenPoint;
        }
    }

    [Serializable]
    public class InteractButtonReleasedEvent : UnityEvent<InteractButtonReleasedEventArgs>
    {
    }

    [Serializable]
    public class InteractButtonReleasedEventArgs
    {
        public Vector2 screenPoint;

        public InteractButtonReleasedEventArgs(Vector2 screenPoint)
        {
            this.screenPoint = screenPoint;
        }
    }

    #endregion
}