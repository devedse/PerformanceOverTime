using BenchmarkDotNet.Attributes;
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
    public class BenchmarkJob
    {
        private const int SIZE = 4096;
        private const int SEED = 1337;

        [Benchmark]
        public InnerMap AlgorithmBacktrack2()
        {
            var alg = new AlgorithmBacktrack2();
            var innerMapFactory = new InnerMapFactory<BitArreintjeFastInnerMap>(SIZE, SIZE);
            var randomFactory = new RandomFactory<XorShiftRandom>(SEED);

            var maze = alg.GoGenerate2(innerMapFactory, randomFactory, new NoAction());
            return maze;
        }

        [Benchmark]
        public InnerMap AlgorithmBacktrack2Deluxe2()
        {
            var alg = new AlgorithmBacktrack2Deluxe2();
            var innerMapFactory = new InnerMapFactory<BitArreintjeFastInnerMap>(SIZE, SIZE);
            var randomFactory = new RandomFactory<XorShiftRandom>(SEED);

            var maze = alg.GoGenerate2(innerMapFactory, randomFactory, new NoAction());
            return maze;
        }
    }
}
