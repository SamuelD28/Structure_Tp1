``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515626 Hz, Resolution=284.4444 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0
  Job-YXHPOP : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0

IterationTime=20.0000 ms  RunStrategy=Throughput  

```
|    Method |   TypeDePile |    N |         Mean |      Error |     StdDev |
|---------- |------------- |----- |-------------:|-----------:|-----------:|
| **CalcSomme** |        **Liste** |    **1** |     **2.948 ms** |  **0.1817 ms** |  **0.5329 ms** |
| **CalcSomme** |        **Liste** |   **10** |    **20.913 ms** |  **1.8752 ms** |  **5.5292 ms** |
| **CalcSomme** |        **Liste** |  **100** |    **27.448 ms** |  **1.3626 ms** |  **4.0177 ms** |
| **CalcSomme** |        **Liste** | **1000** | **1,896.518 ms** | **11.8749 ms** | **11.1078 ms** |
| **CalcSomme** | **ListeChainée** |    **1** |     **3.183 ms** |  **0.2795 ms** |  **0.8242 ms** |
| **CalcSomme** | **ListeChainée** |   **10** |    **15.414 ms** |  **1.8390 ms** |  **5.4224 ms** |
| **CalcSomme** | **ListeChainée** |  **100** |    **29.313 ms** |  **1.4403 ms** |  **4.2469 ms** |
| **CalcSomme** | **ListeChainée** | **1000** | **2,013.039 ms** | **40.0444 ms** | **37.4576 ms** |
| **CalcSomme** | **ListeInverse** |    **1** |     **3.162 ms** |  **0.2143 ms** |  **0.6319 ms** |
| **CalcSomme** | **ListeInverse** |   **10** |    **21.529 ms** |  **1.9548 ms** |  **5.7639 ms** |
| **CalcSomme** | **ListeInverse** |  **100** |    **28.754 ms** |  **1.3477 ms** |  **3.9736 ms** |
| **CalcSomme** | **ListeInverse** | **1000** | **1,944.727 ms** | **28.6732 ms** | **25.4180 ms** |
| **CalcSomme** |         **Pile** |    **1** |     **2.867 ms** |  **0.1876 ms** |  **0.5530 ms** |
| **CalcSomme** |         **Pile** |   **10** |    **18.722 ms** |  **1.9627 ms** |  **5.7870 ms** |
| **CalcSomme** |         **Pile** |  **100** |    **28.208 ms** |  **1.3806 ms** |  **4.0707 ms** |
| **CalcSomme** |         **Pile** | **1000** | **1,954.599 ms** | **18.3106 ms** | **17.1277 ms** |
