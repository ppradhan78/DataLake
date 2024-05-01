using Microsoft.Extensions.Configuration;

namespace DataLake.Data.BusinessObject
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        #region Global Variable(s)
        private readonly IConfiguration _configuration;
        #endregion

        public ConfigurationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Public Prop(s)

        public string StorageConnectionString => _configuration["AzureDataLake:StorageConnectionString"];

        public string StorageAccountName => _configuration["AzureDataLake:StorageAccountName"];

        public string StorageAccountKey => _configuration["AzureDataLake:StorageAccountKey"];

        public string StorageUri => _configuration["AzureDataLake:StorageUri"];

        public string ServiceUri => _configuration["AzureDataLake:ServiceUri"];

        public string FileShareName => _configuration["AzureDataLake:FileShareName"];

        public string dirName => _configuration["AzureDataLake:dirName"];

        public string SubdirName => _configuration["AzureDataLake:SubdirName"];

        public string sourceFilePath => _configuration["AzureDataLake:sourceFilePath"];

        public string destinationFilePath => _configuration["AzureDataLake:destinationFilePath"];
        #endregion

    }
}
