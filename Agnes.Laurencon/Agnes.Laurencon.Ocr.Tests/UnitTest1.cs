using System.Collections.Generic; 
using System.IO; 
using System.Reflection; 
using System.Threading.Tasks; 
using Xunit; 

namespace Agnes.Laurencon.Ocr.Tests ;
public class OcrUnitTest 
{ 
    [Fact] 
    public async Task ImagesShouldBeReadCorrectly() 
    { 
        var executingPath = GetExecutingPath(); 
        var images = new List<byte[]>(); 
        foreach (var imagePath in 
                 Directory.EnumerateFiles(Path.Combine(executingPath, "images"))) 
        { 
            var imageBytes = await File.ReadAllBytesAsync(imagePath); 
            images.Add(imageBytes); 
        } 
 
        var ocrResults = await new Ocr().Read(images); 
 
        Assert.Equal(ocrResults[0].Text, "développeur C# au sens large. Car si vous savez coder en C#, potentiellemer vous savez coder avec toutes les"); 
        Assert.Equal(ocrResults[0].Confidence, 0.939999998); 
        Assert.Equal(ocrResults[1].Text, "Malheureusement les cabinets de recrutement et les annonces, ainsi que les études sur les salaires, gardent cette terminologie obsolète."); 
        Assert.Equal(ocrResults[1].Confidence, 0.949999988); 
        Assert.Equal(ocrResults[2].Text, "Quel salaire peut-on espérer ? Comme toujours, les écarts moyens sont importants. La base serait :"); 
        Assert.Equal(ocrResults[2].Confidence, 0.949999988); 
    } 
    private static string GetExecutingPath() 
    { 
        var executingAssemblyPath = 
            Assembly.GetExecutingAssembly().Location; 
        var executingPath = 
            Path.GetDirectoryName(executingAssemblyPath); 
        return executingPath; 
    } 
}