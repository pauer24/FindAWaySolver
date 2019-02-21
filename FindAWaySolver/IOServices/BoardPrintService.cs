using FindAWaySolver.Entities;
using FindAWaySolver.ExtensionMethods;
using FindAWaySolver.SolveServices;
using System;
using System.Linq;
using System.Text;

namespace FindAWaySolver.IOServices
{
    internal class BoardPrintService
    {
        private readonly OrientationService _orientationService;

        public BoardPrintService(OrientationService orientationService)
        {
            _orientationService = orientationService;
        }

        public string ToString(Board board)
        {
            return DrawBoard(board, (b, x, y) =>
                {
                    return ToString(board.GetCell(x, y));
                }
            );                
        }

        public string ToString(Board board, CellPath path)
        {
            return DrawBoard(board, (b, x, y) =>
                {
                    var pathCell = path.FirstIndexOf(c => c.Equals(x, y));
                    if (pathCell.Index != -1) return PrintPathWay(board, path, pathCell.Index);
                    else return ToString(board.GetCell(x, y));
                }
            );
        }

        private string DrawBoard(Board board, Func<Board,int,int,string> printCoordinate)
        {
            var boardRepresentation = new StringBuilder();
            boardRepresentation.AppendLine(PrintLine(board, true));

            for (var y = 0; y < board.Height; y++)
            {
                boardRepresentation.Append("|");
                for (var x = 0; x < board.Width; x++)
                {
                    if (x == board.Start.X && y == board.Start.Y) boardRepresentation.Append("§");
                    else boardRepresentation.Append(printCoordinate(board, x, y));
                }
                boardRepresentation.AppendLine("|");
            }
            boardRepresentation.AppendLine(PrintLine(board, false));

            return boardRepresentation.ToString();
        }

        private string PrintPathWay(Board board, CellPath path, int index)
        {
            if (IsSolutionLastStep(board, path, index)) return "╬";
            else if (path.Count-1 == index) return ToString(path.Last());

            var orientation = _orientationService.GetOrientation(path[index], path[index + 1]);

            return ToString(orientation);
        }

        private bool IsSolutionLastStep(Board board, CellPath path, int index)
        {
            return board.IsSolved() && path.Count-1 == index;
        }

        private string ToString(Cell cell)
        {
            if (cell.Blocked) return "■";
            if (cell.Visited) return "●";
            return "o";
        }

        private string PrintLine(Board board, bool start)
        {
            var midLine = new string('─', board.Width);
            return start ? $"┌{midLine}┐" : $"└{midLine}┘";
        }

        private string ToString(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return "↑";//⤶ ⤷ ⤴ ⤵
                case Orientation.NorthEast:
                    return "↗";
                case Orientation.East:
                    return "→";
                case Orientation.SouthEast:
                    return "↘";
                case Orientation.South:
                    return "↓";
                case Orientation.SouthWest:
                    return "↙";
                case Orientation.West:
                    return "←";
                case Orientation.NorthWest:
                    return "↖";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}