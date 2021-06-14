using System;
using System.Linq;
using UnityEngine;

namespace View
{
    public class DropChipsGrid : MonoBehaviour
    {
        [SerializeField] private CellGridView cellGridView;
        private Vector3 Difference => transform.position - cellGridView.transform.position;


        public void SetUp(CellGridView cellGridView)
        {
            this.cellGridView = cellGridView;
        }

        public Vector3 GetDropStartPoint(int x, int y)
        {
            return GetDropStartPoint(cellGridView.GetCellView(x, y));
        }

        public Vector3 GetDropStartPoint(CellView cellView)
        {
            return cellView.transform.position + Difference;
        }

        private void OnDrawGizmosSelected()
        {
            if (cellGridView != null)
            {
                Gizmos.color = Color.cyan;
                foreach (var cell in cellGridView.Cells)
                {
                    Gizmos.DrawSphere(cell.transform.position + Difference, 0.1f);
                }
            }
        }
    }
}