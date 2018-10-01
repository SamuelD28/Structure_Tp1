``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515626 Hz, Resolution=284.4444 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0
  Job-YXHPOP : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0

IterationTime=20.0000 ms  RunStrategy=Throughput  

```
| Method |   TypeDePile |     N |        Mean |       Error |      StdDev |      Median |
|------- |------------- |------ |------------:|------------:|------------:|------------:|
| **Cloner** |        **Liste** |     **1** |   **114.47 ns** |   **0.9428 ns** |   **0.8358 ns** |   **114.35 ns** |
| **Cloner** |        **Liste** |    **10** |   **122.48 ns** |   **2.2955 ns** |   **2.0349 ns** |   **122.57 ns** |
| **Cloner** |        **Liste** |   **100** |   **168.49 ns** |   **2.1151 ns** |   **1.7662 ns** |   **167.73 ns** |
| **Cloner** |        **Liste** |  **1000** |   **772.91 ns** |  **15.4083 ns** |  **39.4974 ns** |   **757.32 ns** |
| **Cloner** |        **Liste** | **10000** | **7,117.06 ns** | **156.8880 ns** | **139.0771 ns** | **7,089.10 ns** |
| **Cloner** | **ListeChainée** |     **1** |    **12.51 ns** |   **0.2300 ns** |   **0.2151 ns** |    **12.53 ns** |
| **Cloner** | **ListeChainée** |    **10** |    **12.15 ns** |   **0.1624 ns** |   **0.1440 ns** |    **12.18 ns** |
| **Cloner** | **ListeChainée** |   **100** |    **12.28 ns** |   **0.2793 ns** |   **0.2613 ns** |    **12.24 ns** |
| **Cloner** | **ListeChainée** |  **1000** |    **12.48 ns** |   **0.2946 ns** |   **0.2894 ns** |    **12.35 ns** |
| **Cloner** | **ListeChainée** | **10000** |    **12.14 ns** |   **0.1057 ns** |   **0.0989 ns** |    **12.14 ns** |
