using PlayerInput;
using UnityEngine;
using View;

namespace ViewModel.CellConnection
{
    public class ConnectedCellsLineDrawer : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private CellViewConnectionsChannel cellViewConnectionsChannel;
        [SerializeField] private ChipTypesConfig chipTypesConfig;
        [SerializeField] private InputChannel inputChannel;
        [SerializeField] private Camera gameCamera;
        [SerializeField] private ConnectionLine connectionLine;


        private void OnEnable()
        {
            cellViewConnectionsChannel.cellAdded.AddListener(OnCellAdded);
            cellViewConnectionsChannel.cellRemoved.AddListener(OnCellRemoved);
            cellViewConnectionsChannel.connectionEndedEvent.AddListener(OnConnectionEnded);
        }

        private void Update()
        {
            if (cellViewConnectionsChannel.IsConnecting && connectionLine.SegmentsCount > 0)
            {
                connectionLine.GetLastSegment().SetEndPoint(GetInteractWorldPoint());
            }
        }

        private void OnDisable()
        {
            cellViewConnectionsChannel.cellAdded.RemoveListener(OnCellAdded);
            cellViewConnectionsChannel.cellRemoved.RemoveListener(OnCellRemoved);
            cellViewConnectionsChannel.connectionEndedEvent.RemoveListener(OnConnectionEnded);
        }


        private void OnConnectionEnded(ConnectionEndedEventArgs arg0)
        {
            connectionLine.RemoveAllSegments();
            connectionLine.gameObject.SetActive(false);
        }


        private void OnCellRemoved(CellRemovedEventArgs arg0)
        {
            if (connectionLine.SegmentsCount > 1)
            {
                connectionLine.RemoveSegment(connectionLine.SegmentsCount - 2);
            }
            else
            {
                connectionLine.RemoveSegment(connectionLine.SegmentsCount - 1);
            }
        }

        private void OnCellAdded(CellAddedEventArgs arg0)
        {
            if (!connectionLine.gameObject.activeSelf) connectionLine.gameObject.SetActive(true);

            var cellView = arg0.cellView;
            var newCellPoint = arg0.cellView.transform.position;

            connectionLine.AddSegment(newCellPoint, GetInteractWorldPoint(), GetCellColor(cellView));
        }

        private Vector3 GetInteractWorldPoint()
        {
            var screenToWorldPoint = gameCamera.ScreenToWorldPoint(new Vector3(
                inputChannel.InteractPosition.x,
                inputChannel.InteractPosition.y,
                connectionLine.transform.position.z - gameCamera.transform.position.z));

            screenToWorldPoint.z = connectionLine.transform.position.z;
            return screenToWorldPoint;
        }

        private Color GetCellColor(CellView cellView)
        {
            return chipTypesConfig[cellView.Chip.ChipType].color;
        }
    }
}