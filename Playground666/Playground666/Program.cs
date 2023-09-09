// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Playground666;


List<(string, int)> a = new() { ("hola", 3) , ("cucu", 22)};

List<Person> i = new List<Person>() { new Person{ Name = "adsfasdf"}, new Person(){ Name= "Aleberto"} };
RunSpanBenchmarl();
//RunBenchmark();

void RunBenchmark(){
    var summary = BenchmarkRunner.Run(typeof(AsyncTests).Assembly);
}

void RunSpanBenchmarl()
{
    BenchmarkRunner.Run(typeof(SpanTests));
}