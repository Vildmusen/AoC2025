
using System.Diagnostics;

var input = File.ReadAllLines("input.txt");
var sw = Stopwatch.StartNew();

Day1.Run(input);

sw.Stop();

Console.WriteLine($"Elapsed: {sw.Elapsed.Milliseconds} ms");