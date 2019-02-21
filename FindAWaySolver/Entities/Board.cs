using System;
using System.Collections.Generic;
using System.Linq;

namespace FindAWaySolver.Entities
{
    internal class Board
    {
        private readonly Cell[][] _cells;
        
        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            _cells = new Cell[Height][];
            for (var i = 0; i < _cells.Length; i++)
            {
                _cells[i] = new Cell[Width];
            }
        }

        public Board(int width, int height, Coordinate start, params Coordinate[] blockedCells) : this(width, height)
        {
            Initialize(start, blockedCells);
        }

        public int Width { get; }

        public int Height{ get; }

        public Cell Start { get; private set; }

        internal Cell GetCell(int x, int y)
        {
            return _cells[y][x];
        }

        private void SetCell(int x, int y, Cell cell)
        {
            _cells[y][x] = cell;
        }

        internal void Initialize(Coordinate start, Coordinate[] blockedCells)
        {
            foreach (var blockCell in blockedCells)
            {
                SetCell(blockCell.X, blockCell.Y, new Cell(blockCell.X, blockCell.Y, true));
            }

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (GetCell(i, j) == null)
                    {
                        SetCell(i, j, new Cell(i, j, false));
                    }
                }
            }

            Start = GetCell(start.X, start.Y);
        }

        internal bool IsSolved()
        {
            return GetAvailableCells().Length == 0;
        }

        internal Cell[] GetAvailableCells()
        {
            return _cells.SelectMany(column => column.Where(cell => cell.IsAvailable())).ToArray();
        }

        internal Cell[] GetAvailableCells(Coordinate coord)
        {
            var cells = new List<Cell>();
            if (coord.X > 0)
            {
                cells.Add(GetCell(coord.X - 1, coord.Y));
            }
            if (coord.X < Width - 1)
            {
                cells.Add(GetCell(coord.X + 1, coord.Y));
            }
            if (coord.Y > 0)
            {
                cells.Add(GetCell(coord.X, coord.Y - 1));
            }
            if (coord.Y < Height - 1)
            {
                cells.Add(GetCell(coord.X, coord.Y + 1));
            }

            return cells.Where(c => c.IsAvailable()).ToArray();
        }
    }
}