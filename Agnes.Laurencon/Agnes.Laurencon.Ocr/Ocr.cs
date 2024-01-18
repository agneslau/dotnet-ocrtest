using System; 
using System.Collections.Generic; 
using System.IO; 
using System.Reflection; 
using System.Threading.Tasks; 
using Tesseract;
namespace Agnes.Laurencon.Ocr; 
public class Ocr 
{ 
    private static string GetExecutingPath() 
    { 
        var executingAssemblyPath = 
            Assembly.GetExecutingAssembly().Location; 
        var executingPath = Path.GetDirectoryName(executingAssemblyPath); 
        return executingPath; 
    }

    public OcrResult Read(byte[] image)
    {
        var executingPath = GetExecutingPath();
        using var engine = new TesseractEngine(Path.Combine(executingPath, 
            @"tessdata"), "fra", EngineMode.Default);
        using var pix = Pix.LoadFromMemory(image); 
        var test = engine.Process(pix);
        var Text = test.GetText();
        var Confidence = test.GetMeanConfidence(); 
        return new OcrResult(Text, Confidence);
    }
    public IList<OcrResult> Read(IList<byte[]> images)
    {
        
        var tasks = new List<Task<OcrResult>>();

        foreach (var image in images)
        {
            var task = Task.Run(() => Read(image));
            tasks.Add(task);
        }

        Task.WaitAll(tasks.ToArray());

        IList<OcrResult> results = new List<OcrResult>();
        foreach (var task in tasks)
        {
            results.Add(task.Result);
        }

        return results;
    } 
    
    public IList<OcrResult> ReadFromConsole(string[] args)
    {
        var executingPath = GetExecutingPath();
        var images = new List<byte[]>();
        foreach (var path in args)
        {
            var imagePath = Path.Combine(executingPath, "images", path);
            var imageBytes = File.ReadAllBytes(imagePath);
            images.Add(imageBytes);
        }
        
        return Read(images);
    }
}