
using System.Diagnostics;

var input = File.ReadAllLines("input.txt");
var sw = Stopwatch.StartNew();

Day7.Run(input);

sw.Stop();

Console.WriteLine($"Elapsed: {sw.Elapsed.TotalSeconds} seconds");