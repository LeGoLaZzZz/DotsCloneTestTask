using UnityEngine;
using Utils;

namespace View
{
    public class CellGridViewGenerator : MonoBehaviour
    {
        [Header("View settings")]
        [SerializeField] private Vector2 paddingLeftTop;
        [SerializeField] private Vector2 spacing = new Vector2(0.5f, 0.5f);

        [Header("Links")]
        [SerializeField] private CellView cellViewPrefab;
        [SerializeField] private CellGridView cellGridView;

        [Header("Monitoring")]
        [SerializeField] private Transform gridParent;
        [SerializeField] private Vector2Int gridSize = new Vector2Int(6, 6);

        public Transform GridParent => gridParent;
        public Vector2Int GridSize => gridSize;
        public Vector3 StartPoint => transform.position;

        [ContextMenu("Generate grid")]
        public void GenerateGrid()
        {
            GenerateGrid(GridSize);
        }

        public CellGridView GenerateGrid(Vector2Int gridSize)
        {
            if (!gridParent) gridParent = new GameObject("Cell grid").transform;
            else gridParent.DestroyImmediateChildren();

            if (!cellGridView) cellGridView = gridParent.gameObject.AddComponent<CellGridView>();
            this.gridSize = gridSize;
            cellGridView.SetUp(gridSize);

            var stepVertical = new Vector3(0, cellViewPrefab.Size.y + spacing.y, 0);
            var stepHorizontal = new Vector3(cellViewPrefab.Size.x + spacing.x, 0, 0);

            var xStartSpawn = StartPoint.x + cellViewPrefab.Size.x / 2 + paddingLeftTop.x;
            var yStartSpawn = StartPoint.y - cellViewPrefab.Size.y / 2 - paddingLeftTop.y;


            var curPosition = new Vector3(xStartSpawn, yStartSpawn, 0);
            CellView newCell;

            for (var y = GridSize.y - 1; y >= 0; y--)
            {
                curPosition.x = xStartSpawn;

                for (var x = 0; x < GridSize.x; x++)
                {
                    newCell = CreateCell(x, y);
                    newCell.transform.position = curPosition;
                    cellGridView.SetCell(newCell);

                    curPosition += stepHorizontal;
                }

                curPosition -= stepVertical;
            }

            return cellGridView;
        }

        private CellView CreateCell(int x, int y)
        {
            var cellView = Instantiate(cellViewPrefab, gridParent, false);
            cellView.SetUp(x, y);
            cellView.name += $" ({x},{y})";
            return cellView;
        }
    }
}