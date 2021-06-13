using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerInput
{
    public abstract class InputReader : MonoBehaviour
    {
        [SerializeField] protected InputChannel inputChannel;

        protected void SetInteractPoint(Vector2 point)
        {
            inputChannel.InteractPosition = point;
        }
        
        protected void SetInteracting(bool isInteracting)
        {
            inputChannel.IsInteracting = isInteracting;
        }

        protected void InteractPressedInvoke(Vector2 point)
        {
            inputChannel.InteractButtonPressedInvoke(point);
        }

        protected void InteractReleased(Vector2 point)
        {
            inputChannel.InteractButtonReleasedInvoke(point);
        }
    }
}