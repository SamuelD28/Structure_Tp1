``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515626 Hz, Resolution=284.4444 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0 DEBUG  [AttachedDebugger]
  Job-YXHPOP : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0

IterationTime=20.0000 ms  RunStrategy=Throughput  

```
|                  Method |   TypeDePile |         Mean |       Error |      StdDev |
|------------------------ |------------- |-------------:|------------:|------------:|
|           **PushPop1_Vide** |        **Liste** |     **40.93 us** |   **0.7896 us** |   **0.7386 us** |
|           PushPop1_Vide | ListeChainée |     53.86 us |   1.2936 us |   1.5400 us |
|           PushPop1_Vide | ListeInverse |    444.32 us |   9.4716 us |   7.9092 us |
|           PushPop1_Vide |         Pile |     22.69 us |   0.4449 us |   0.5940 us |
|                         |              |              |             |             |
|         **PushPop2_Pleine** |        **Liste** |     **40.01 us** |   **0.6887 us** |   **0.6105 us** |
|         PushPop2_Pleine | ListeChainée |     53.12 us |   1.0164 us |   1.1297 us |
|         PushPop2_Pleine | ListeInverse | 42,816.24 us | 811.7384 us | 902.2450 us |
|         PushPop2_Pleine |         Pile |     22.41 us |   0.2157 us |   0.1801 us |
|                         |              |              |             |             |
| **PushPop3_VidePleineVide** |        **Liste** |     **43.43 us** |   **0.7486 us** |   **0.6251 us** |
| PushPop3_VidePleineVide | ListeChainée |     55.73 us |   0.9810 us |   0.9177 us |
| PushPop3_VidePleineVide | ListeInverse | 22,613.18 us | 443.9633 us | 607.7019 us |
| PushPop3_VidePleineVide |         Pile |     23.50 us |   0.3875 us |   0.3435 us |
