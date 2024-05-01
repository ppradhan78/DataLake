
using DataLake.Data.BusinessObject;
using DataLake.Data.BusinessObjects;

namespace DataLake.Data.Core
{
    public class FileCore : IFileCore
    {
        private readonly IConfigurationSettings _configuration;
        IFileBo _fileSharesBo;
        public FileCore(IConfigurationSettings configuration, IFileBo fileSharesBo)
        {
            _configuration = configuration;
            _fileSharesBo = fileSharesBo;
        }

        public Task DeleteDirectory()
        {
           return _fileSharesBo.DeleteDirectory();
        }

        public Task DeleteFileAsync(string fileName, string localPath)
        {
            return _fileSharesBo.DeleteFileAsync(fileName, localPath);
        }

        public Task DownloadFile(string fileName, string localPath)
        {
            return _fileSharesBo.DownloadFile(fileName, localPath);
        }

        public Task<IEnumerable<string>> ListFilesInDirectory()
        {
            return _fileSharesBo.ListFilesInDirectory();
        }

        public void UploadFileAsync(string filename)
        {
             _fileSharesBo.UploadFileAsync(filename);
        }
    }
}