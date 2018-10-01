``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515626 Hz, Resolution=284.4444 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0
  Job-YXHPOP : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0

IterationTime=20.0000 ms  RunStrategy=Throughput  

```
|       Method |   TypeDePile |         Mean |       Error |      StdDev |
|------------- |------------- |-------------:|------------:|------------:|
| **ClonerPleine** |        **Liste** |    **770.57 ns** |   **6.9279 ns** |   **5.7851 ns** |
| ClonerPleine | ListeChainée |     12.12 ns |   0.2688 ns |   0.2514 ns |
| ClonerPleine | ListeInverse |    741.62 ns |   7.1286 ns |   6.3193 ns |
| ClonerPleine |         Pile | 18,855.49 ns | 260.9905 ns | 244.1307 ns |
|              |              |              |             |             |
|   **ClonerVide** |        **Liste** |     **26.51 ns** |   **0.3070 ns** |   **0.2721 ns** |
|   ClonerVide | ListeChainée |     12.29 ns |   0.2943 ns |   0.3022 ns |
|   ClonerVide | ListeInverse |     26.47 ns |   0.4396 ns |   0.3897 ns |
|   ClonerVide |         Pile |     84.25 ns |   0.7851 ns |   0.6960 ns |
