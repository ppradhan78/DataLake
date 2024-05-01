using Azure;
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;
using DataLake.Data.BusinessObject;

namespace DataLake.Data.BusinessObjects
{
    public class FileBo : IFileBo
    {
        private readonly IConfigurationSettings _configuration;

        public FileBo(IConfigurationSettings configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteDirectory()
        {
            try
            {
                var client = GetDataLakeDirectoryClient();
                await client.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UploadFileAsync(string filename)
        {
            try
            {
                var client = GetDataLakeFileSystemClient();
                var fileClient = client.GetFileClient(filename);
                FileStream fileStream = File.OpenRead(filename);
                fileClient.UploadAsync(content: fileStream, overwrite: true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteFileAsync(string fileName, string localPath)
        {
            try
            {
                var directoryClient = GetDataLakeDirectoryClient();
                DataLakeFileClient fileClient = directoryClient.GetFileClient(fileName);
                Response<FileDownloadInfo> downloadResponse = await fileClient.ReadAsync();
                BinaryReader reader = new BinaryReader(downloadResponse.Value.Content);
                FileStream fileStream = File.OpenWrite(localPath);
                int bufferSize = 4096;
                byte[] buffer = new byte[bufferSize];
                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fileStream.Write(buffer, 0, count);
                }
                await fileStream.FlushAsync();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<string>> ListFilesInDirectory()
        {
            List<string> files = new List<string>();
            try
            {
                var fileSystemClient = GetDataLakeFileSystemClient();
                IAsyncEnumerator<PathItem> enumerator = fileSystemClient.GetPathsAsync(_configuration.dirName).GetAsyncEnumerator();
                await enumerator.MoveNextAsync();
                PathItem item = enumerator.Current;
                while (item != null)
                {
                    files.Add(item.Name);
                    if (!await enumerator.MoveNextAsync())
                    {
                        break;
                    }
                    item = enumerator.Current;
                }

                return files;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DownloadFile(string fileName,string localPath)
        {
            var directoryClient = GetDataLakeDirectoryClient();
            DataLakeFileClient fileClient = directoryClient.GetFileClient(fileName);
            Response<FileDownloadInfo> downloadResponse = await fileClient.ReadAsync();
            BinaryReader reader = new BinaryReader(downloadResponse.Value.Content);
            FileStream fileStream = File.OpenWrite(localPath);
            int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            int count;
            while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
            {
                fileStream.Write(buffer, 0, count);
            }
            await fileStream.FlushAsync();
            fileStream.Close();
        }

        #region Private Method(s)
        private DataLakeFileSystemClient GetDataLakeFileSystemClient()
        {
            StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(_configuration.StorageAccountName, _configuration.StorageAccountKey);
            //DataLakeServiceClient serviceClient = new DataLakeServiceClient(new Uri(_configuration.ServiceUri), sharedKeyCredential);
            DataLakeServiceClient serviceClient = new DataLakeServiceClient(_configuration.StorageConnectionString);

            // Create a DataLake Filesystem
            DataLakeFileSystemClient filesystem = serviceClient.GetFileSystemClient(_configuration.FileShareName);
            return filesystem;
        }

        private DataLakeServiceClient GetDataLakeServiceClient()
        {
            DataLakeServiceClient dataLakeServiceClient = new DataLakeServiceClient(
                new Uri(_configuration.ServiceUri),
                new DefaultAzureCredential());

            return dataLakeServiceClient;
        }

        private DataLakeDirectoryClient GetDataLakeDirectoryClient()
        {
            var fileSystemClient = GetDataLakeFileSystemClient();
            DataLakeDirectoryClient directoryClient = fileSystemClient.GetDirectoryClient(_configuration.dirName);

            return directoryClient;
        }

        #endregion



        //############
        //        public async Task DownloadFile(
        //DataLakeDirectoryClient directoryClient,
        //string fileName,
        //string localPath)
        //        {
        //            DataLakeFileClient fileClient =
        //                directoryClient.GetFileClient(fileName);

        //            Response<FileDownloadInfo> downloadResponse = await fileClient.ReadAsync();

        //            BinaryReader reader = new BinaryReader(downloadResponse.Value.Content);

        //            FileStream fileStream = File.OpenWrite(localPath);

        //            int bufferSize = 4096;

        //            byte[] buffer = new byte[bufferSize];

        //            int count;

        //            while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
        //            {
        //                fileStream.Write(buffer, 0, count);
        //            }

        //            await fileStream.FlushAsync();

        //            fileStream.Close();
        //        }


        //    public async Task DeleteDirectory(
        //DataLakeFileSystemClient fileSystemClient,
        //string directoryName)
        //    {
        //        DataLakeDirectoryClient directoryClient =
        //            fileSystemClient.GetDirectoryClient(directoryName);

        //        await directoryClient.DeleteAsync();
        //    }

        //    public async Task AppendDataToFile(DataLakeDirectoryClient directoryClient,
        //string fileName, Stream stream)
        //    {
        //        var client = GetDataLakeFileSystemClient();
        //        var fileClient = client.GetFileClient("");
        //        long fileSize = fileClient.GetProperties().Value.ContentLength;
        //        await fileClient.AppendAsync(stream, offset: fileSize);
        //        await fileClient.FlushAsync(position: fileSize + stream.Length);
        //    }
    }
}