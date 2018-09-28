using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Validators;
using CalculatriceLib;
using SDD.Class;
using SDD.Interface;
using static SDD.Interface.CalcCommande;

namespace SDD
{

    [SimpleJob(RunStrategy.ColdStart)]
    [IterationTime(20)]
    [ExecutionValidator(true)]
    [IterationCount(5)]
    [WarmupCount(1)]
    public class BenchmarkSomme2
    {
        private ICalculatrice calc;

        [Params(
            PileCalcType.Pile,
            PileCalcType.Liste,
            PileCalcType.ListeInverse,
            PileCalcType.ListeChainée)]
        public PileCalcType TypeDePile;

        [Params(1, 10, 100, 1000, 10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            calc = new Calculatrice(
                FabriquePile.New(TypeDePile, Enumerable.Repeat<int>(2, N)),
                new Accumuleur());
        }

        [Benchmark]
        public bool CalcSomme()
        {
            calc.Exécuter(__0, EntrerObligatoire);
            for (int i = 0; i < N; i++) calc.Exécuter(__1, EntrerObligatoire);
            for (int i = 0; i < N; i++) calc.Exécuter(Addition);
            return (int)calc.Résultat == N ? true : throw new Exception();
        }

    }

}