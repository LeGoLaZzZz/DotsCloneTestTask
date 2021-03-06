using PlayerInput;
using UnityEngine;
using View;

namespace ViewModel.CellInteraction
{
    public class CellViewsInteractDetection : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int maxRaycastTargetCount = 10;

        [Header("Links")]
        [SerializeField] private Camera playerCamera;
        [SerializeField] private InputChannel inputChannel;
        [SerializeField] private CellViewInteractChannel cellViewInteractChannel;

        [Header("Monitoring")]
        [SerializeField] private bool isSearching;
        [SerializeField] private bool isCellInteracting;

        private RaycastHit[] _raycastHits;

        private void Awake()
        {
            _raycastHits = new RaycastHit[maxRaycastTargetCount];
        }

        private void Update()
        {
            isSearching = inputChannel.IsInteracting;

            //search stopped
            if (!isSearching && isCellInteracting) StopInteract();

            if (isSearching)
            {
                if (TryFindCell(inputChannel.InteractPosition, out var cellView)) StartInteract(cellView);
            }
        }

        private bool TryFindCell(Vector2 screenPoint, out CellView cellView)
        {
            cellView = default;

            var ray = playerCamera.ScreenPointToRay(screenPoint);
            var size = Physics.RaycastNonAlloc(ray, _raycastHits);

            
            if (size <= 0) return false;

            foreach (var hit in _raycastHits)
            {
                if (hit.collider.TryGetComponent<CellView>(out cellView))
                {
                    return true;
                }
            }

            return false;
        }

        private void StartInteract(CellView cellView)
        {
            isCellInteracting = true;
            cellViewInteractChannel.InteractionStartInvoke(cellView);
        }

        private void StopInteract()
        {
            isCellInteracting = false;
            cellViewInteractChannel.InteractionStoppedInvoke();
        }
    }
}