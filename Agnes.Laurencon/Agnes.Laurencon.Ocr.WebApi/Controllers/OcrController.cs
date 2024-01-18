using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Agnes.Laurencon.Ocr.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class OcrController : ControllerBase
{
    private readonly Ocr _ocr;
    public OcrController(Ocr ocr)
    {
        _ocr = ocr;
    }
    [HttpPost]
    public IList<OcrResult>
        OnPostUploadAsync([FromForm(Name = "files")] IList<IFormFile> files)
    {
        var images = new List<byte[]>();
        foreach (var formFile in files)
        {
            using var sourceStream = formFile.OpenReadStream();
            using var memoryStream = new MemoryStream();
            sourceStream.CopyTo(memoryStream);
            images.Add(memoryStream.ToArray());
        }

        return _ocr.Read(images);
    }
}