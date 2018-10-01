``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515626 Hz, Resolution=284.4444 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0
  Job-YXHPOP : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0

IterationTime=20.0000 ms  RunStrategy=Throughput  

```
|  Method |   TypeDePile |      N |             Mean |          Error |         StdDev |
|-------- |------------- |------- |-----------------:|---------------:|---------------:|
| **PushPop** |        **Liste** |      **1** |        **10.573 us** |      **0.4994 us** |      **0.5344 us** |
| **PushPop** |        **Liste** |     **10** |        **10.459 us** |      **0.2061 us** |      **0.2752 us** |
| **PushPop** |        **Liste** |    **100** |        **10.595 us** |      **0.2083 us** |      **0.3242 us** |
| **PushPop** |        **Liste** |   **1000** |        **10.555 us** |      **0.1749 us** |      **0.1636 us** |
| **PushPop** |        **Liste** |  **10000** |        **10.247 us** |      **0.1527 us** |      **0.1428 us** |
| **PushPop** |        **Liste** | **100000** |        **10.269 us** |      **0.2220 us** |      **0.2077 us** |
| **PushPop** | **ListeChainée** |      **1** |        **11.123 us** |      **0.1194 us** |      **0.1058 us** |
| **PushPop** | **ListeChainée** |     **10** |        **11.479 us** |      **0.1830 us** |      **0.1711 us** |
| **PushPop** | **ListeChainée** |    **100** |        **11.137 us** |      **0.1519 us** |      **0.1268 us** |
| **PushPop** | **ListeChainée** |   **1000** |        **11.249 us** |      **0.1556 us** |      **0.1455 us** |
| **PushPop** | **ListeChainée** |  **10000** |        **11.242 us** |      **0.2174 us** |      **0.1927 us** |
| **PushPop** | **ListeChainée** | **100000** |        **12.591 us** |      **0.2231 us** |      **0.2087 us** |
| **PushPop** | **ListeInverse** |      **1** |       **630.793 us** |     **10.4226 us** |      **9.7493 us** |
| **PushPop** | **ListeInverse** |     **10** |     **1,413.064 us** |     **27.3969 us** |     **29.3144 us** |
| **PushPop** | **ListeInverse** |    **100** |     **5,630.628 us** |     **94.3497 us** |     **88.2548 us** |
| **PushPop** | **ListeInverse** |   **1000** |    **42,754.202 us** |    **605.8745 us** |    **537.0919 us** |
| **PushPop** | **ListeInverse** |  **10000** |   **418,519.679 us** |    **932.8211 us** |    **728.2853 us** |
| **PushPop** | **ListeInverse** | **100000** | **5,541,233.405 us** | **79,350.7386 us** | **66,261.4542 us** |
| **PushPop** |         **Pile** |      **1** |         **8.810 us** |      **0.1740 us** |      **0.1709 us** |
| **PushPop** |         **Pile** |     **10** |         **8.925 us** |      **0.1186 us** |      **0.1052 us** |
| **PushPop** |         **Pile** |    **100** |         **8.923 us** |      **0.0904 us** |      **0.0801 us** |
| **PushPop** |         **Pile** |   **1000** |         **8.911 us** |      **0.1134 us** |      **0.1005 us** |
| **PushPop** |         **Pile** |  **10000** |         **8.960 us** |      **0.1762 us** |      **0.1730 us** |
| **PushPop** |         **Pile** | **100000** |         **8.827 us** |      **0.0940 us** |      **0.0879 us** |
