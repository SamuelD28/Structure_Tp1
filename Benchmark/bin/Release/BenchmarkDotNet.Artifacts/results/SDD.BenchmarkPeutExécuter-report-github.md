``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-4790 CPU 3.60GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3515626 Hz, Resolution=284.4444 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0
  Job-QDNTOF : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3163.0

IterationCount=3  IterationTime=20.0000 ms  RunStrategy=ColdStart  
WarmupCount=1  

```
|       Method |     Mean |    Error |   StdDev |
|------------- |---------:|---------:|---------:|
| PeutExécuter | 5.167 ms | 133.1 ms | 7.520 ms |
