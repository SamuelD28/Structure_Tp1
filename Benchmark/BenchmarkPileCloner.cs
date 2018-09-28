using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using CalculatriceLib;
using SDD.Interface;

namespace SDD
{

    [SimpleJob(RunStrategy.Throughput)]
    [IterationTime(20)]
    [ExecutionValidator(true)]
    [Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByMethod)]
    public class BenchmarkPileCloner
    {
        const int N = 1000;

        private IPileCalc pileVide;
        private IPileCalc pilePleine;

        [Params(
            PileCalcType.Pile, 
            PileCalcType.Liste, 
            PileCalcType.ListeInverse, 
            PileCalcType.ListeChainée )]        
        public PileCalcType TypeDePile;

        [GlobalSetup]
        public void Setup()
        {
            pileVide = FabriquePile.New(TypeDePile);
            pilePleine = FabriquePile.New(TypeDePile, Enumerable.Repeat<int>(1, N));
        }

        [Benchmark]
        public object ClonerPleine()
        {
            return pilePleine.Cloner();
        }

        [Benchmark]
        public object ClonerVide()
        {
            return pileVide.Cloner();
        }

    }

}