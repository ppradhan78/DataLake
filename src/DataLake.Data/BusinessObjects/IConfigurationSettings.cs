namespace DataLake.Data.BusinessObject
{
    public interface IConfigurationSettings
    {
        string StorageConnectionString { get; }
        string StorageAccountName { get; }
        string StorageAccountKey { get; }
        string StorageUri { get; }
        string ServiceUri { get; }
        string FileShareName { get; }
        string dirName { get; }
        string SubdirName { get; }
        string sourceFilePath { get; }
        string destinationFilePath { get; }
    }
}
