``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515626 Hz, Resolution=284.4444 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0 DEBUG  [AttachedDebugger]
  Job-YXHPOP : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0

IterationTime=20.0000 ms  RunStrategy=Throughput  

```
| Method |   TypeDePile |     N |               Mean |             Error |            StdDev |
|------- |------------- |------ |-------------------:|------------------:|------------------:|
| **Cloner** |        **Liste** |     **1** |           **133.6 ns** |          **2.638 ns** |          **3.038 ns** |
| **Cloner** |        **Liste** |    **10** |           **136.2 ns** |          **2.735 ns** |          **3.833 ns** |
| **Cloner** |        **Liste** |   **100** |           **185.8 ns** |          **3.919 ns** |          **5.494 ns** |
| **Cloner** |        **Liste** |  **1000** |           **836.3 ns** |         **16.649 ns** |         **22.790 ns** |
| **Cloner** |        **Liste** | **10000** |         **7,269.7 ns** |         **73.694 ns** |         **61.538 ns** |
| **Cloner** | **ListeChainée** |     **1** |           **338.5 ns** |          **6.634 ns** |          **6.813 ns** |
| **Cloner** | **ListeChainée** |    **10** |         **6,942.6 ns** |        **128.784 ns** |        **114.164 ns** |
| **Cloner** | **ListeChainée** |   **100** |       **326,799.7 ns** |      **4,027.358 ns** |      **3,144.296 ns** |
| **Cloner** | **ListeChainée** |  **1000** |    **26,159,684.4 ns** |    **242,509.499 ns** |    **189,335.449 ns** |
| **Cloner** | **ListeChainée** | **10000** | **2,885,219,766.0 ns** | **55,833,504.052 ns** | **54,835,961.330 ns** |
