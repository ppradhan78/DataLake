using DataLake.Data.BusinessObject;
using DataLake.Data.Core;
using Microsoft.AspNetCore.Mvc;

namespace DataLake.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureDatalakeFileController : ControllerBase
    {
        #region Global Variable(s)
        private readonly IConfigurationSettings _configuration;
        private readonly IFileCore _fileCore;
        private readonly ILogger _logger;
        #endregion

        public AzureDatalakeFileController(IFileCore fileCore, IConfigurationSettings configuration,
           ILogger<AzureDatalakeFileController> logger)
        {
            _fileCore = fileCore;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
             var output= await _fileCore.ListFilesInDirectory();
            return output;
        }

        [HttpPost()]
        [Route("UploadFile")]
        public void Post(string filepath)
        {
            _fileCore.UploadFileAsync(filepath);
        }

        [HttpPost()]
        [Route("DownloadFile")]
        public void Post(string fileName, string localPath)
        {
            _fileCore.DownloadFile(fileName, localPath);
        }

        [HttpDelete()]
        [Route("DeleteDirectory")]
        public async Task Delete()
        {
            await _fileCore.DeleteDirectory();
        }

        [HttpDelete()]
        [Route("DeleteFile")]
        public async Task Delete(string fileName, string localPath)
        {
            await _fileCore.DeleteFileAsync(fileName, localPath);
        }
    }
}
