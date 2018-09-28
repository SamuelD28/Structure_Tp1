using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using CalculatriceLib;
using SDD.Class;
using SDD.Interface;
using static SDD.Interface.CalcCommande;

namespace SDD
{

    [SimpleJob(RunStrategy.Throughput)]
    [IterationTime(20)]
    [ExecutionValidator(true)]
    public class BenchmarkSomme1
    {
        private ICalculatrice calc;

        [Params(
            PileCalcType.Pile,
            PileCalcType.Liste,
            PileCalcType.ListeInverse,
            PileCalcType.ListeChainée)]
        public PileCalcType TypeDePile;

        [Params(1, 10, 100, 1000)]
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