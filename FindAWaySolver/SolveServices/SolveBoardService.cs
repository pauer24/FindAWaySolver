using System;
using FindAWaySolver.Entities;
using System.Collections.Generic;
using System.Linq;
using FindAWaySolver.ExtensionMethods;
using FindAWaySolver.IOServices;

namespace FindAWaySolver.SolveServices
{
    internal class SolveBoardService
    {
        private readonly BoardPrintService _printingService;

        public SolveBoardService(BoardPrintService printingService)
        {
            _printingService = printingService;
        }

        internal CellPath Solve(Board board)
        {
            var path = new CellPath();
            path.AddCell(board.Start);

            FindPath(board, path, board.Start);

            if (board.IsSolved())
            {
                return path;
            }

            throw new Exception("Impossible problem");
        }

        private bool FindPath(Board board, CellPath path, Cell origin)
        {
            var availableCells = board.GetAvailableCells(origin);
            foreach (var cell in availableCells)
            {
                path.AddCell(cell);

                //Console.WriteLine(_printingService.ToString(board, path));

                if (board.IsSolved())
                {
                    return true;
                }

                if (!BoardHasStillSolution(board, cell))
                {
                    //Console.WriteLine("Impossible solution, rolling back.");
                } else
                {
                    if (FindPath(board, path, cell))
                    {
                        return true;
                    }
                }

                path.Undo(1);
            }

            return false;
        }

        /// <summary>
        /// To ensure board is solvable at some point, we have to ensure all points are connected
        /// </summary>
        /// <param name="board"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        private bool BoardHasStillSolution(Board board, Cell origin)
        {
            var availableCells = board.GetAvailableCells(origin).ToList();

            if (!availableCells.Any())
            {
                return false;
            }

            var totalCells = new List<Cell>();
            var cellsToUse = new List<Cell>();
            totalCells.Add(availableCells.First());
            cellsToUse.Add(availableCells.First());

            while (cellsToUse.Any())
            {
                availableCells = cellsToUse.SelectMany(c => board.GetAvailableCells(c)).Distinct().ToList();
                cellsToUse.Clear();  
                var unusedCells = availableCells.Where(ac => totalCells.All(uc => !uc.Equals(ac))).ToArray();
                totalCells.AddRange(unusedCells);  
                cellsToUse.AddRange(unusedCells);
            }

            return totalCells.Count == board.GetAvailableCells().Length;
        }
    }
}