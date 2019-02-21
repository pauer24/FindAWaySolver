using System;

namespace FindAWaySolver.Entities
{
    class Cell : Coordinate
    {
        internal Cell(int x, int y, bool blocked) : base(x, y)
        {
            Blocked = blocked;
        }

        internal Cell(Coordinate coordinate, bool blocked) : this (coordinate.X, coordinate.Y, blocked)
        {
        }

        internal bool Blocked { get; }

        internal bool Visited { get; private set; }

        internal void Visit()
        {
            if (Blocked) throw new InvalidOperationException("Cannot visit a blocked cell");
            Visited = true;
        }

        internal void Unvisit()
        {
            if (Blocked) throw new InvalidOperationException("Cannot visit a blocked cell");
            Visited = false;
        }

        internal bool IsAvailable()
        {
            return !Blocked && !Visited;
        }
    }
}
