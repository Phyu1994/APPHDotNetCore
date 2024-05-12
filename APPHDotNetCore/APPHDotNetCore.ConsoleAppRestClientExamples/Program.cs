// See https://aka.ms/new-console-template for more information
using APPHDotNetCore.ConsoleAppRestClientExamples;

Console.WriteLine("Hello, World!");

RestClientExample restClientExamples = new RestClientExample();
await restClientExamples.RunAsync();

Console.ReadLine();