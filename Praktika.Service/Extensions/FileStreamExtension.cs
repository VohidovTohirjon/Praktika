using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Praktika.Service.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Praktika.Service.Extensions
{
    internal static class FileStreamExtension
    {
        public static async Task<string> SavefileAsync(Stream file, string fileName, IConfiguration config, IWebHostEnvironment env)
        {
            string hostUrl = HttpContextHelper.Context?.Request?.Scheme + "://" + HttpContextHelper.Context?.Request?.Host.Value;


            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;

            string storagePath = config.GetSection("Storage:ImageUrl").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");
            FileStream mainFile = File.Create(filePath);

            string webUrl = $@"{hostUrl}/{storagePath}/{fileName}";



            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return webUrl;
        }
    }
}