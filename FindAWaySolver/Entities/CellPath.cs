using System.Collections.Generic;
using System.Linq;

namespace FindAWaySolver.Entities
{
    internal class CellPath : List<Cell>
    {
        internal void AddCell(Cell cell)
        {
            cell.Visit();
            Add(cell);
        }

        internal void AddPath(CellPath path)
        {
            AddRange(path);
        }

        internal void Undo()
        {
            foreach (var cell in this)
            {
                cell.Unvisit();
            }
            Clear();
        }

        internal void Undo(int steps)
        {
            while(steps-- > 0)
            {
                var last = this.Last();
                last.Unvisit();
                RemoveAt(Count-1);
            }
        }

        public override string ToString()
        {
            return this.Select(c => c.ToString()).Aggregate((left, right) => $"{left}->{right}");
        }
    }
}