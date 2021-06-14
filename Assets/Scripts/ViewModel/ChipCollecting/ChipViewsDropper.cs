using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using View;

namespace ViewModel.ChipCollecting
{
    public class ChipViewsDropper : MonoBehaviour
    {
        [SerializeField] private float dropDelay = 0.2f;
        [SerializeField] private ChipCollectingChannel chipCollectingChannel;

        private DropChipsGrid _dropChipsGrid;
        private ChipViewFiller _chipViewFiller;
        private CellGridView _cellGridView;
        private CellGrid _cellGrid;

        public void SetUp(CellGrid cellGrid, CellGridView cellGridView, DropChipsGrid dropChipsGrid,
            ChipViewFiller chipViewFiller)
        {
            _cellGridView = cellGridView;
            _chipViewFiller = chipViewFiller;
            _dropChipsGrid = dropChipsGrid;
            _cellGrid = cellGrid;
        }

        private void OnEnable()
        {
            chipCollectingChannel.successConnectionScoredEvent.AddListener(OnScored);
        }

        private void OnDisable()
        {
            chipCollectingChannel.successConnectionScoredEvent.RemoveListener(OnScored);
        }

        private void OnScored(SuccessConnectionEventArgs arg0)
        {
            StartCoroutine(DelayedAction(dropDelay, () => DropChips(arg0.gridChanges)));
        }

        private void DropChips(GridChanges gridChanges)
        {
            CellView from;
            CellView to;
            ChipView chipView;

            foreach (var dropChange in gridChanges.dropChanges)
            {
                from = _cellGridView.GetCellView(dropChange.from.X, dropChange.from.Y);
                to = _cellGridView.GetCellView(dropChange.to.X, dropChange.to.Y);
                chipView = from.Chip;

                from.DetachChipView();
                to.AttachChipView(chipView, false);

                chipView.Drop(from.transform.position, to.transform.position);
            }

            foreach (var newChange in gridChanges.spawnNewChanges)
            {
                to = _cellGridView.GetCellView(newChange.to.X, newChange.to.Y);

                chipView = _chipViewFiller.GetChipView(newChange.chip.ChipType);


                to.AttachChipView(chipView, false);
                chipView.Drop(_dropChipsGrid.GetDropStartPoint(to), to.transform.position);
            }
        }


        private IEnumerator DelayedAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }
    }
}