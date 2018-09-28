using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using System;
using static System.Console;
using System.Linq;

namespace SDD
{
    public class Benchmarks
    {
        public static void Main(string[] args)
        {
            while(true)
            {
                WriteLine("\nBenchmarks TP1:");
                WriteLine();
                WriteLine("0. Pile - Push & Pop");
                WriteLine("1. Pile - Cloner");
                WriteLine("2. Pile - Cloner     - Ordre de grandeur");
                WriteLine("3. Pile - Push & Pop - Ordre de grandeur");
                WriteLine("4. Calc - Somme 1    - Ordre de grandeur");
                WriteLine("5. Calc - Somme 2    - Ordre de grandeur");
                WriteLine("9. Test rapide de bon fonctionnement");
                WriteLine("x. Exit");
                Write("\nVotre choix (0-9): ");
                char choix = ReadKey().KeyChar;
                WriteLine();
                switch (choix)
                {
                    case '0':
                        BenchmarkRunner.Run<BenchmarkPilePushPop>(new AllowNonOptimized());
                        break;

                    case '1':
                        BenchmarkRunner.Run<BenchmarkPileCloner>(new AllowNonOptimized());
                        break;

                    case '2':
                        BenchmarkRunner.Run<BenchmarkPileClonerGrandeur>(new AllowNonOptimized());
                        break;

                    case '3':
                        BenchmarkRunner.Run<BenchmarkPilePushPopGrandeur>(new AllowNonOptimized());
                        break;

                    case '4':
                        BenchmarkRunner.Run<BenchmarkSomme1>(new AllowNonOptimized());
                        break;

                    case '5':
                        BenchmarkRunner.Run<BenchmarkSomme2>(new AllowNonOptimized());
                        break;

                    case '9':
                        BenchmarkRunner.Run<BenchmarkPeutExécuter>(new AllowNonOptimized());
                        break;

                    case 'x':
                        return;

                    default:
                        WriteLine($"Choix invalide: {choix}");
                        break;
                }
            }
        }
    }

    public class AllowNonOptimized : ManualConfig
    {
        public AllowNonOptimized()
        {
            Add(JitOptimizationsValidator.DontFailOnError); // ALLOW NON-OPTIMIZED DLLS

            Add(DefaultConfig.Instance.GetLoggers().ToArray()); // manual config has no loggers by default
            Add(DefaultConfig.Instance.GetExporters().ToArray()); // manual config has no exporters by default
            Add(DefaultConfig.Instance.GetColumnProviders().ToArray()); // manual config has no columns by default
        }
    }
}