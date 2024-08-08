using DocTemplate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace DocumentTemplate.Controllers
{
    [ApiController]
    //[Route("[controller]/{filename}")]
    [Route("[controller]")]
    public class DocumentController : Controller
    {
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(ILogger<DocumentController> logger)
        {
            _logger = logger;
        }        
        
        [HttpGet("Download/{fileName}/")]
        public async Task<IActionResult> GetDownload(string fileName)
        {
            if (!System.IO.File.Exists($"/files/{fileName}"))
            {
                return BadRequest("Brak podanego pliku w lokalizacji dokumentów");
            }
            GenerateDocument g = new GenerateDocument();
            DocField docField = new DocField();
            docField.FileName = $"/files/{fileName}";
            docField.Fields = new Dictionary<string, string>();
            foreach (string k in ControllerContext.HttpContext.Request.Query.Keys)
            {
                docField.Fields.Add(k, ControllerContext.HttpContext.Request.Query[k].ToString());
            }

            MemoryStream dataStream = g.Generate(docField);

            return File(dataStream.GetBuffer(), "application/pdf", $"{docField.FileName.Split('.')[0]}.pdf");
        }

        [HttpGet("list/{fileName}/")]
        //public IEnumerable<string> GetList(string fileName)
        public async Task<IActionResult> GetList(string fileName)
        {
            if(!System.IO.File.Exists($"/files/{fileName}"))
            {
                return BadRequest("Brak podanego pliku w lokalizacji dokumentów");
            }
            GenerateDocument g = new GenerateDocument();
            return Ok(g.List($"/files/{fileName}"));
        }

        [HttpPost]
        public async Task<IActionResult> GenerateDocument(DocField docField)
        {
            if (!System.IO.File.Exists($"/files/{docField.FileName}"))
            {
                return BadRequest("Brak podanego pliku w lokalizacji dokumentów");
            }
            GenerateDocument g = new GenerateDocument();

            MemoryStream dataStream = g.Generate(docField);
            
            return File(dataStream.GetBuffer(), "application/pdf", $"{docField.FileName.Split('.')[0]}.pdf");            
        }
    }
}
