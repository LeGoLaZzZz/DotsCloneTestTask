using UnityEngine;

namespace View
{
    public class CellGridView : MonoBehaviour
    {
        [Header("Monitoring")]
        [SerializeField] private CellView[] cells;
        [SerializeField] private Vector2Int gridSize;

        public CellView[] Cells => cells;

        public CellView GetCellView(int x, int y)
        {
            return cells[x + y * gridSize.y];
        }

        public void SetCellView(int x, int y, CellView cellView)
        {
            cells[x + y * gridSize.y] = cellView;
        }

        public void SetUp(Vector2Int size)
        {
            gridSize = size;
            cells = new CellView[gridSize.x * gridSize.y];
        }

        public void SetCell(CellView cellView)
        {
            SetCellView(cellView.Coords.x, cellView.Coords.y, cellView);
        }
    }
}