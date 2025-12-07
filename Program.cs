
using System.Diagnostics;

var input = File.ReadAllLines("input.txt");
var sw = Stopwatch.StartNew();

Day2.Run(input);

sw.Stop();

Console.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds} ms");