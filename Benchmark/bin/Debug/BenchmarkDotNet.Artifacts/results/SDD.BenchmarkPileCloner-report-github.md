``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515626 Hz, Resolution=284.4444 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0 DEBUG  [AttachedDebugger]
  Job-YXHPOP : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0

IterationTime=20.0000 ms  RunStrategy=Throughput  

```
|       Method |   TypeDePile |             Mean |             Error |            StdDev |
|------------- |------------- |-----------------:|------------------:|------------------:|
| **ClonerPleine** |        **Liste** |        **782.09 ns** |        **15.6030 ns** |        **33.5871 ns** |
| ClonerPleine | ListeChainée | 76,400,239.01 ns | 1,262,227.5791 ns | 1,180,688.3457 ns |
| ClonerPleine | ListeInverse |        769.26 ns |        14.9490 ns |        21.9120 ns |
| ClonerPleine |         Pile |     18,452.50 ns |        86.3508 ns |        72.1068 ns |
|              |              |                  |                   |                   |
|   **ClonerVide** |        **Liste** |         **40.32 ns** |         **0.9448 ns** |         **1.3244 ns** |
|   ClonerVide | ListeChainée |        143.52 ns |         2.8922 ns |         2.8406 ns |
|   ClonerVide | ListeInverse |         40.41 ns |         0.8334 ns |         1.0837 ns |
|   ClonerVide |         Pile |         99.54 ns |         1.1226 ns |         0.8765 ns |
