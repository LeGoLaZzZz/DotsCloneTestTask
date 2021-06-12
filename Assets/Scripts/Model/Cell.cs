namespace Model
{
    public class Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Chip CurrentChip { get; private set; }
        public bool IsEmpty { get; private set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            IsEmpty = true;
        }

        public void SetChip(Chip chip)
        {
            CurrentChip = chip;
            IsEmpty = false;
        }

        public void RemoveChip()
        {
            CurrentChip = null;
            IsEmpty = true;
        }

    }
}