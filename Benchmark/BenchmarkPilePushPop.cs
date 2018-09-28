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
    [Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Alphabetical)]
    [ExecutionValidator(true)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByMethod)]
    public class BenchmarkPilePushPop
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
        public bool PushPop1_Vide()
        {
            bool b = true;
            for(int i = 0; i < N; i++)
            {
                pileVide.Push(1);
                b &= pileVide.Pop() == 1;
            }
            return b && pileVide.EstVide ? true : throw new Exception();

        }

        [Benchmark]
        public bool PushPop2_Pleine()
        {
            bool b = true;
            for (int i = 0; i < N; i++)
            {
                pilePleine.Push(2);
                b &= pilePleine.Pop() == 2;
            }
            return b && pilePleine.Dessus == 1 ? true : throw new Exception();
        }

        [Benchmark]
        public bool PushPop3_VidePleineVide()
        {
            bool b = true;
            for (int i = 1; i <= N; i++) 
                pileVide.Push(i);
            for (int i = N; i > 0; i--) 
                b &= pileVide.Pop() == i;
            return b && pileVide.EstVide ? true : throw new Exception();
        }

    }

}