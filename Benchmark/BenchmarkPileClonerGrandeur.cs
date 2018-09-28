using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using CalculatriceLib;
using SDD.Interface;

namespace SDD
{

    [SimpleJob(RunStrategy.Throughput)]
    [IterationTime(20)]         
    [ExecutionValidator(true)]
    public class BenchmarkPileClonerGrandeur
    {
        private IPileCalc pile;

        [Params(
            PileCalcType.Liste, 
            PileCalcType.ListeChainée )]        
        public PileCalcType TypeDePile;

        [Params(1, 10, 100, 1000, 10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            pile = FabriquePile.New(TypeDePile, Enumerable.Repeat<int>(1, N));
        }

        [Benchmark]
        public object Cloner()
        {
            return pile.Cloner();
        }

    }

}