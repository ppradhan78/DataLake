
using DataLake.Data.BusinessObject;
using DataLake.Data.BusinessObjects;
using DataLake.Data.Core;

namespace MicrosoftAzure.API.Extension
{
    public static class DependencyRegistrationExtension
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection Services)
        {
            Services.AddSingleton<IConfigurationSettings, ConfigurationSettings>();
            Services.AddTransient<IFileCore, FileCore>();
            Services.AddTransient<IFileBo, FileBo>();
            return Services;
        }
        //public static IServiceCollection AddApiDependencies(this IServiceCollection Services)
        //{
            //Services.AddEndpointsApiExplorer();
            ////Services.Configure<ConfigurationSettings>(builder.Configuration.GetSection("AzureAISearch"));
            //Services.AddSwaggerGen();
            //Services.AddControllers();
            //Services.AddApplicationInsightsTelemetry();
            //Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins",
            //        builder =>
            //        {
            //            builder
            //                .AllowAnyOrigin()
            //                .AllowAnyHeader()
            //                .AllowAnyMethod();
            //        });
            //});
            //return Services;
        //}
    }
}
