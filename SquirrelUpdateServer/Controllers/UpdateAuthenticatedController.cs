using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace SquirrelUpdateServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateAuthenticatedController : ControllerBase
    {
        private string _webRoot;
        public UpdateAuthenticatedController(IHostingEnvironment env)
        {
            _webRoot = env.WebRootPath;
        }

        // GET: api/UpdateAuthenticated/file.exe
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