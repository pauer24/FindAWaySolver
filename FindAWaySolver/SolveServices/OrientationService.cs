using FindAWaySolver.Entities;
using System;

namespace FindAWaySolver.SolveServices
{
    internal class OrientationService
    {
        public Orientation GetOrientation(Coordinate from, Coordinate to)
        {
            if (from.X == to.X) return from.Y < to.Y ? Orientation.South : Orientation.North;
            if (from.Y == to.Y) return from.X < to.X ? Orientation.East : Orientation.West;

            if (from.X < to.X && from.Y < to.Y) return Orientation.NorthEast;
            if (from.X > to.X && from.Y > to.Y) return Orientation.SouthWest;
            if (from.X < to.X && from.Y > to.Y) return Orientation.NorthWest;
            if (from.X > to.X && from.Y < to.Y) return Orientation.SouthEast;

            throw new Exception($"Cannot get orientation of {from.ToString()} => {to.ToString()}");
        }
    }
}