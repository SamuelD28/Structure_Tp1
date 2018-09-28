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
using CalculatriceLib;
using SDD.Interface;
using static SDD.Interface.CalcCommande;

namespace SDD
{

    [SimpleJob(RunStrategy.Throughput)]
    [IterationTime(20)]
    [ExecutionValidator(true)]
    public class BenchmarkPilePushPopGrandeur
    {

        private IPileCalc pile;

        [Params(
            PileCalcType.Pile, 
            PileCalcType.Liste, 
            PileCalcType.ListeInverse, 
            PileCalcType.ListeChainée )]        
        public PileCalcType TypeDePile;

        [Params(1, 10, 100, 1000, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            pile = FabriquePile.New(TypeDePile, Enumerable.Repeat<int>(2, N));
        }

        [Benchmark]
        public bool PushPop()
        {
            bool b = true;
            for(int i = 0; i < 1000; i++)
            {
                pile.Push(1);
                b &= pile.Pop() == 1;
            }
            return b && pile.Dessus == 2 ? true : throw new Exception();
        }

    }

}