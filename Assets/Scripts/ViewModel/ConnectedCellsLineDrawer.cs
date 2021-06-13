using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using PlayerInput;
using UnityEngine;

namespace View
{
    public class ConnectedCellsLineDrawer : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private CellViewsConnector cellViewsConnector;
        [SerializeField] private ChipTypesConfig chipTypesConfig;
        [SerializeField] private InputChannel inputChannel;
        [SerializeField] private Camera gameCamera;
        [SerializeField] private ConnectionLine connectionLine;


        private void OnEnable()
        {
            cellViewsConnector.cellAdded.AddListener(OnCellAdded);
            cellViewsConnector.cellRemoved.AddListener(OnCellRemoved);
            cellViewsConnector.connectionStartedEvent.AddListener(OnConnectionStarted);
            cellViewsConnector.connectionEndedEvent.AddListener(OnConnectionEnded);
        }

        private void Update()
        {
            if (cellViewsConnector.IsConnecting && connectionLine.SegmentsCount > 0)
            {
                connectionLine.GetLastSegment().SetEndPoint(GetInteractWorldPoint());
            }
        }

        private void OnDisable()
        {
            cellViewsConnector.cellAdded.RemoveListener(OnCellAdded);
            cellViewsConnector.cellRemoved.RemoveListener(OnCellRemoved);
            cellViewsConnector.connectionStartedEvent.RemoveListener(OnConnectionStarted);
            cellViewsConnector.connectionEndedEvent.RemoveListener(OnConnectionEnded);
        }

        private void OnConnectionStarted()
        {
            // connectionLine.gameObject.SetActive(true);
        }

        private void OnConnectionEnded()
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