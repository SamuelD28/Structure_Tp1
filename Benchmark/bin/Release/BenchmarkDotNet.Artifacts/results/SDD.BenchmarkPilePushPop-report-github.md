``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515626 Hz, Resolution=284.4444 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0
  Job-YXHPOP : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0

IterationTime=20.0000 ms  RunStrategy=Throughput  

```
|                  Method |   TypeDePile |          Mean |       Error |      StdDev |
|------------------------ |------------- |--------------:|------------:|------------:|
|           **PushPop1_Vide** |        **Liste** |     **10.111 us** |   **0.0916 us** |   **0.0812 us** |
|           PushPop1_Vide | ListeChainée |     13.274 us |   0.2491 us |   0.2330 us |
|           PushPop1_Vide | ListeInverse |    388.670 us |   6.9891 us |   6.1957 us |
|           PushPop1_Vide |         Pile |      8.757 us |   0.1166 us |   0.0974 us |
|                         |              |               |             |             |
|         **PushPop2_Pleine** |        **Liste** |     **10.414 us** |   **0.2066 us** |   **0.2537 us** |
|         PushPop2_Pleine | ListeChainée |     11.493 us |   0.2077 us |   0.1943 us |
|         PushPop2_Pleine | ListeInverse | 41,673.451 us | 516.5972 us | 483.2253 us |
|         PushPop2_Pleine |         Pile |      8.692 us |   0.0714 us |   0.0668 us |
|                         |              |               |             |             |
| **PushPop3_VidePleineVide** |        **Liste** |     **10.435 us** |   **0.2051 us** |   **0.2014 us** |
| PushPop3_VidePleineVide | ListeChainée |     12.022 us |   0.1164 us |   0.0972 us |
| PushPop3_VidePleineVide | ListeInverse | 22,199.318 us | 229.1565 us | 203.1413 us |
| PushPop3_VidePleineVide |         Pile |     11.542 us |   0.1419 us |   0.1328 us |
