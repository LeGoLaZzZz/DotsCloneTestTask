using System;
using System.Linq;
using UnityEngine;

namespace View
{
    public class DropChipsGrid : MonoBehaviour
    {
        private CellGridView _cellGridView;
        private Vector3 Difference => transform.position - _cellGridView.transform.position;


        public void SetUp(CellGridView cellGridView)
        {
            _cellGridView = cellGridView;
        }

        public Vector3 GetDropStartPoint(int x, int y)
        {
            return GetDropStartPoint(_cellGridView.GetCellView(x, y));
        }

        public Vector3 GetDropStartPoint(CellView cellView)
        {
            return cellView.transform.position + Difference;
        }

        private void OnDrawGizmosSelected()
        {
            if (_cellGridView != null)
            {
                Gizmos.color = Color.cyan;
                foreach (var cell in _cellGridView.Cells)
                {
                    Gizmos.DrawSphere(cell.transform.position + Difference, 0.1f);
                }
            }
        }
    }
}