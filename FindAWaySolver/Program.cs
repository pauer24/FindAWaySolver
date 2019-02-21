using FindAWaySolver.Entities;
using FindAWaySolver.IOServices;
using FindAWaySolver.SolveServices;
using System;
using System.Diagnostics;

namespace FindAWaySolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var board = Problems[4];
            var printingService = new BoardPrintService(new OrientationService());
            var solveService = new SolveBoardService(printingService);
            Console.Write(printingService.ToString(board));

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = solveService.Solve(board);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            Console.WriteLine($"Solution found: {result.ToString()}");
            Console.WriteLine(printingService.ToString(board, result));
            Console.WriteLine(string.Format("It tooked: {0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds));
            Console.ReadKey();
        }

        static Board[] Problems = new Board[]
        {
            new Board(3, 3, new Coordinate(1, 2), new Coordinate(1,1)),
            new Board(4, 7, new Coordinate(0,6), 
                new Coordinate(2,2),
                new Coordinate(2,3),
                new Coordinate(2,5),
                new Coordinate(3,5),
                new Coordinate(3,6)
                ),
            new Board(5, 7, new Coordinate(0,6), 
                new Coordinate(0,0),
                new Coordinate(2,2),
                new Coordinate(0,3),
                new Coordinate(4,3),
                new Coordinate(4,4),
                new Coordinate(1,6)
                ),
            new Board(6, 8, new Coordinate(3,1),
                new Coordinate(2,1),
                new Coordinate(4,1),
                new Coordinate(0,2),
                new Coordinate(1,3),
                new Coordinate(3,4),
                new Coordinate(0,5),
                new Coordinate(1,5),
                new Coordinate(5,5)
                ),
            new Board(6, 8, new Coordinate(0,7),
                new Coordinate(1,1),
                new Coordinate(4,1),
                new Coordinate(1,2),
                new Coordinate(5,2),
                new Coordinate(2,4),
                new Coordinate(5,6),
                new Coordinate(1,7),
                new Coordinate(5,7)
                ),
        };
    }
}
