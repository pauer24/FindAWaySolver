namespace FindAWaySolver.Entities
{
    internal class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public bool Equals(int x, int y)
        {
            return x == X && y == Y;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

    }
}