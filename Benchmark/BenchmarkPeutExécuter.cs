using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Validators;
using SDD.Class;
using SDD.Interface;

namespace SDD
{

    [SimpleJob(RunStrategy.ColdStart)]
    [IterationTime(20)]
    [IterationCount(3)]
    [WarmupCount(1)]
    [ExecutionValidator(true)]
    public class BenchmarkPeutExécuter
    {
        [Benchmark]
        public bool PeutExécuter()
        {
            var calc = new Calculatrice();
            bool b = true;
            foreach(CalcCommande c in Enum.GetValues(typeof(CalcCommande)))
            {
                b = b & calc.PeutExécuter(c);
            }
            return b;
        }

    }

}