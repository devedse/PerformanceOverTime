using BenchmarkDotNet.Running;
using DeveMazeGeneratorCore.Factories;
using DeveMazeGeneratorCore.Generators;
using DeveMazeGeneratorCore.Generators.Helpers;
using DeveMazeGeneratorCore.Generators.SpeedOptimization;
using DeveMazeGeneratorCore.Imageification;
using DeveMazeGeneratorCore.InnerMaps;
using DeveMazeGeneratorCore.PathFinders;
using System.Diagnostics;

namespace MazeGeneratorPerformanceTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchmarkJob>();
           
        }

        public void ManualTest()
        {
            Console.WriteLine("Generating maze...");

            //int size = 16384;
            int size = 4096;

            var alg = new AlgorithmBacktrack2Deluxe2();

            Console.WriteLine($"Generating mazes using {alg.GetType().Name}...");

            int seed = 1337;


            var innerMapFactory = new InnerMapFactory<BitArreintjeFastInnerMap>(size, size);
            var randomFactory = new RandomFactory<XorShiftRandom>(seed);

            var wMaze = Stopwatch.StartNew();
            var maze = alg.GoGenerate2(innerMapFactory, randomFactory, new NoAction());
            wMaze.Stop();

            Console.WriteLine($"Maze generation time: {wMaze.Elapsed}");

            var wPath = Stopwatch.StartNew();
            var path = PathFinderDepthFirstSmartWithPos.GoFind(maze, null);
            wPath.Stop();

            Console.WriteLine($"Maze pathfinding time: {wPath.Elapsed}");

            var wSave = Stopwatch.StartNew();
            using (var fs = new FileStream($"icon{seed}.png", FileMode.Create))
            {
                WithPath.SaveMazeAsImageDeluxePng(maze, path, fs);
            }
            wSave.Stop();

            Console.WriteLine($"Maze save to image time: {wSave.Elapsed}");
        }
    }
}