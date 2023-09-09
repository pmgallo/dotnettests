## Get First - Reusing connection

| Method         |        Mean |     Error |      StdDev |      Median | Allocated |
|----------------|------------:|----------:|------------:|------------:|----------:|
| EF_Find        |    372.9 ns |   7.25 ns |     8.06 ns |    372.9 ns |     264 B |
| EF_Single      | 38,052.9 ns | 435.49 ns |   363.65 ns | 37,983.5 ns |    6968 B |
| EF_First       | 38,741.8 ns | 773.62 ns | 1,868.39 ns | 38,010.2 ns |    6968 B |
| Dapper_GetById | 10,903.5 ns | 185.12 ns |   154.58 ns | 10,886.7 ns |    2296 B |

## Add/Remove

| Method            |     Mean |     Error |    StdDev | Allocated |
|-------------------|---------:|----------:|----------:|----------:|
| EF_Add_Delete     | 1.669 ms | 0.0326 ms | 0.0335 ms |  19.89 KB |
| Dapper_Add_Delete | 1.460 ms | 0.0273 ms | 0.0471 ms |   3.91 KB |

## Updates

| Method        |     Mean |    Error |   StdDev | Allocated |
|---------------|---------:|---------:|---------:|----------:|
| EF_Update     | 856.1 us | 17.01 us | 25.47 us |  10.11 KB |
| Dapper_Update | 717.6 us | 14.02 us | 21.40 us |   2.45 KB |

## Delete

| Method        |     Mean |    Error |   StdDev | Allocated |
|---------------|---------:|---------:|---------:|----------:|
| EF_Filter     | 33.74 us | 0.666 us | 0.976 us |   5.69 KB |
| Dapper_Filter | 14.02 us | 0.217 us | 0.203 us |   2.25 KB |
