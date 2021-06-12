namespace Model
{
    public class ChipsFiller
    {
        public void Fill(CellGrid cellGrid)
        {
            var chipGenerator = new ChipGenerator();

            for (int x = 0; x < cellGrid.GridSize.x; x++)
            {
                for (int y = 0; y < cellGrid.GridSize.y; y++)
                {
                    cellGrid.GetCell(x, y).SetChip(chipGenerator.GetRandomChip());
                }
            }
        }
    }
}