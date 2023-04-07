using FileGenerator;
using System.Diagnostics;

var sw = Stopwatch.StartNew();

var file = new Generator().Generate(500000);
new Sorter().Sort(file, 50000);

sw.Stop();
Console.WriteLine($"Время выполнения: {sw.Elapsed}");