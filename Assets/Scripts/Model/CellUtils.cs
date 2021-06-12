namespace Model
{
    public static class CellUtils
    {
        public static bool IsLeftNeighbour(Cell cell, Cell other)
        {
            return (cell.X - other.X == 1) &&
                   (cell.Y - other.Y == 0);
        }

        public static bool IsRightNeighbour(Cell cell, Cell other)
        {
            return (cell.X - other.X == -1) &&
                   (cell.Y - other.Y == 0);
        }

        public static bool IsUpNeighbour(Cell cell, Cell other)
        {
            return (cell.X - other.X == 0) &&
                   (cell.Y - other.Y == -1);
        }

        public static bool IsDownNeighbour(Cell cell, Cell other)
        {
            return (cell.X - other.X == 0) &&
                   (cell.Y - other.Y == 1);
        }

        public static bool IsNeighbour(Cell a, Cell b)
        {
            return IsDownNeighbour(a, b) ||
                   IsUpNeighbour(a, b) ||
                   IsLeftNeighbour(a, b) ||
                   IsRightNeighbour(a, b);
        }
    }
}