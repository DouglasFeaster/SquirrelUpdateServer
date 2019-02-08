using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace SquirrelUpdateServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateUnAuthenticatedController : ControllerBase
    {
        private string _webRoot;
        public UpdateUnAuthenticatedController(IHostingEnvironment env)
        {
            _webRoot = env.WebRootPath;
        }

        // GET: api/UpdateUnAuthenticated/file.exe
        [HttpGet("{fileName}", Name = "Get")]
        public IActionResult Get(string fileName)
        {
            return DownloadFile(_webRoot, fileName);
        }

        public FileResult DownloadFile(string filePath, string fileName)
        {
            IFileProvider provider = new PhysicalFileProvider(filePath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/*";
            return File(readStream, mimeType, fileName);
        }

    }
}