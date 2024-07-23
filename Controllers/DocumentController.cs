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

        //[HttpGet(Name = "GetDocumentPDFold")]
        //public HttpResponseMessage Get1()
        //{
        //    //GenerateDocument g = new GenerateDocument();
        //    //return g.List();

        //    return new HttpResponseMessage()
        //    {
        //        Content = new StringContent(
        //            "<strong> abd </strong>",
        //            Encoding.UTF8,
        //            "text/html"
        //        )
        //    };
        //}

        //[HttpGet(Name = "GetDocumentPDF")]
        ////public FileContentResult Get([FromQuery] string surname)
        ////public FileContentResult Get(string fileName)
        //public FileContentResult Get()
        //{
        //    GenerateDocument g = new GenerateDocument();
        //    List<string> listk = new List<string>();
        //    List<string> listv = new List<string>();            
        //    foreach(string k in ControllerContext.HttpContext.Request.Query.Keys)
        //    {
        //        //Console.WriteLine(ControllerContext.HttpContext.Request.Query[k].ToString());
        //        listk.Add(k);
        //        listv.Add(ControllerContext.HttpContext.Request.Query[k].ToString());
        //    }
        //    string[] vals = listv.ToArray();
        //    string[] keys = listk.ToArray();

        //    return File(g.Generate("test.docx", "IN008978DOCXXXX", keys, vals).GetBuffer(), "application/pdf", "test.pdf");
        //}

        //[HttpGet(Name = "GetDocumentPDF")]
        [HttpGet("Download")]
        //public FileContentResult Get([FromQuery] string surname)
        //public FileContentResult Get(string fileName)
        public async Task<IActionResult> Post()
        {
            GenerateDocument g = new GenerateDocument();
            List<string> listk = new List<string>();
            List<string> listv = new List<string>();
            foreach (string k in ControllerContext.HttpContext.Request.Query.Keys)
            {
                //Console.WriteLine(ControllerContext.HttpContext.Request.Query[k].ToString());
                listk.Add(k);
                listv.Add(ControllerContext.HttpContext.Request.Query[k].ToString());
            }
            string[] vals = listv.ToArray();
            string[] keys = listk.ToArray();

            MemoryStream dataStream = g.Generate("/files/test.docx", "IN008978DOCXXXX", keys, vals);

            //var dataBytes = System.IO.File.ReadAllBytes("/files/test1.pdf");
            ////adding bytes to memory stream
            //var dataStream = new MemoryStream(dataBytes);

            //HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK); //ControllerContext.HttpContext.Request.CreateResponse(HttpStatusCode.OK);
            //httpResponseMessage.Content = new StreamContent(dataStream);
            //httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //httpResponseMessage.Content.Headers.ContentDisposition.FileName = "test.pdf";
            //httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

            return File(dataStream.GetBuffer(), "application/pdf", "test.pdf");
        }

        [HttpGet("list")]
        public IEnumerable<string> Get()
        {
            GenerateDocument g = new GenerateDocument();
            return g.List();
        }


        [HttpGet("listdir")]
        //public HttpResponseMessage Get1()
        public string Get1()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // Change the directory to %WINDIR%
                Environment.CurrentDirectory = Environment.GetEnvironmentVariable("windir");
                DirectoryInfo info = new DirectoryInfo(".");
                //return "Directory Info:   " + info.FullName + "   " + Environment.GetEnvironmentVariable("path").ToString();
                //var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                //var IntExample = MyConfig.GetValue<int>("AppSettings:SampleIntValue");
                //var AppName = MyConfig.GetValue<string>("AppSettings:APP_Name");
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["token"];
                //return new HttpResponseMessage()
                //{
                //    Content = new StringContent(
                //    "Directory Info:   " + info.FullName,
                //    Encoding.UTF8,
                //    "text/html"
                //    )
                //};
            }
            else
            {
                return "This example runs on Windows only.";
                //return new HttpResponseMessage()
                //{
                //    Content = new StringContent(
                //    "This example runs on Windows only.",
                //    Encoding.UTF8,
                //    "text/html"
                //    )
                //};
            }

        }
    }
}
