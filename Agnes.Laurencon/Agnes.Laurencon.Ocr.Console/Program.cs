// See https://aka.ms/new-console-template for more information

using Agnes.Laurencon.Ocr;

Console.WriteLine("Hello, World!");

// Path: Agnes.Laurencon.Ocr.Console/Program.cs



var ocrResults = new Ocr().ReadFromConsole(args);

foreach (var ocrResult in ocrResults) 
{ 
    Console.WriteLine($"Confidence :{ocrResult.Confidence}"); 
    Console.WriteLine($"Text :{ocrResult.Text}"); 
} 


