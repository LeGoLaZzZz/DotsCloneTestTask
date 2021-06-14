using Model;
using UnityEngine;
using Utils;
using View;

namespace ViewModel
{
    public class ChipViewFiller : MonoBehaviour
    {
        [SerializeField] private ChipView chipViewPrefab;
        [SerializeField] private Transform chipsParent;


        public void FillCellGridView(CellGridView cellGridView, CellGrid cellGrid)
        {
            if (!chipsParent) chipsParent = new GameObject("Chips").transform;
            else chipsParent.DestroyImmediateChildren();

            foreach (var cellView in cellGridView.Cells)
            {
                var cellModel = cellGrid.GetCell(cellView.Coords.x, cellView.Coords.y);
                var chipView = GetChipView(cellModel.CurrentChip.ChipType, chipsParent);
                cellView.AttachChipView(chipView, true);
            }
        }

        public ChipView GetChipView(ChipType chipType)
        {
            return GetChipView(chipType, chipsParent);
        }
        
        public ChipView GetChipView(ChipType chipType, Transform parent)
        {
            var chipView = Instantiate(chipViewPrefab, parent, false);
            chipView.SetUp(chipType);

            return chipView;
        }
    }
}