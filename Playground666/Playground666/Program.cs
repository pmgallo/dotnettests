// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Playground666;


List<Person> i = new List<Person>() { new Person{ Name = "adsfasdf"}, new Person(){ Name= "Aleberto"} };
Console.In.ReadLine();
//RunBenchmark();

void RunBenchmark(){
    var summary = BenchmarkRunner.Run(typeof(AsyncTests).Assembly);
}