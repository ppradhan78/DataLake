namespace DataLake.Data.Core
{
    public interface IFileCore
    {
        void UploadFileAsync(string filename);
        Task DeleteDirectory();
        Task DeleteFileAsync(string fileName, string localPath);
        Task<IEnumerable<string>> ListFilesInDirectory();
        Task DownloadFile(string fileName, string localPath);
    }
}