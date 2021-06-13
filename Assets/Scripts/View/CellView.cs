using System;
using UnityEngine;

namespace View
{
    public class CellView : MonoBehaviour
    {
        [Header("Monitoring")]
        [SerializeField] private Vector2Int coords;
        [SerializeField] private ChipView chip;


        public Vector2Int Coords => coords;
        public Vector2 Size => transform.localScale;

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
    }
}