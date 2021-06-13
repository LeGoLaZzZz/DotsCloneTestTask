using UnityEngine;

namespace PlayerInput
{
    public class MouseInput : InputReader
    {
        public int interactMouseButton = 0;

        private bool _isInteracting = false;

        private void Update()
        {
            InteractButtonTick();
        }

        private void InteractButtonTick()
        {
            SetInteractPoint(Input.mousePosition);

            SetInteracting(Input.GetMouseButton(interactMouseButton));
            if (Input.GetMouseButtonDown(interactMouseButton)) Pressed();
            if (Input.GetMouseButtonUp(interactMouseButton)) Released();
        }

        private void Pressed()
        {
            if (_isInteracting) return;
            _isInteracting = true;
            InteractPressedInvoke(Input.mousePosition);
        }

        private void Released()
        {
            if (!_isInteracting) return;
            _isInteracting = false;
            InteractReleased(Input.mousePosition);
        }
    }
}