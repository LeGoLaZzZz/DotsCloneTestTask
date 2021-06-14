using System;
using UnityEngine;
using UnityEngine.Events;

namespace View
{
    [Serializable]
    public class CellViewChipScoredEvent : UnityEvent<CellViewChipScoredEventArgs>
    {
    }

    [Serializable]
    public class CellViewChipScoredEventArgs
    {
    }


    public class CellView : MonoBehaviour
    {
        [Header("Monitoring")]
        [SerializeField] private Vector2Int coords;
        [SerializeField] private ChipView chip;

        public CellViewChipScoredEvent cellViewChipScored = new CellViewChipScoredEvent();

        public bool IsEmpty => chip == null;
        public Vector2Int Coords => coords;
        public Vector2 Size => transform.localScale;
        public ChipView Chip => chip;

        public void SetUp(int x, int y)
        {
            coords = new Vector2Int(x, y);
        }

        public void AttachChipView(ChipView chip, bool needMove = false)
        {
            if (this.chip != null) throw new Exception("Cell view already has chip");

            this.chip = chip;

            if (needMove)
            {
                chip.transform.position = transform.position;
            }
        }

        public void DetachChipView()
        {
            chip = null;
        }

        public void ScoreOutChip()
        {
            chip.ScoreOut();
            DetachChipView();
            cellViewChipScored.Invoke(new CellViewChipScoredEventArgs());
        }
    }
}