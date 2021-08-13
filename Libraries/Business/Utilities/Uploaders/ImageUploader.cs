using Core.Utilities.IoC;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Utilities.Uploaders
{
    public static class ImageUploader
    {
        private static IHostEnvironment _hostEnvironment;
        private static string imageFolderName = "images";

        static ImageUploader()
        {
            _hostEnvironment = ServiceTool.ServiceProvider.GetService<IHostEnvironment>();
        }

        /// <summary>
        /// Fotoğrafı ayarlanmış dosya yoluna kayıt eder.
        /// </summary>
        /// <param name="fileName">Fotoğraf Adı (Boş olursa Guid üretir.)</param>
        /// <param name="folderName">Kayıt Edilecek Klasör Adı</param>
        /// <returns></returns>
        public static async Task<IFileResult> ImageUploadAsync(IFormFile formFiles, string fileName = "", string folderName = "")
        {
            try
            {
                string fullFolderPath = string.Join(@"\", _hostEnvironment.ContentRootPath, "wwwroot", imageFolderName, folderName);

                CreateIfNoFolder(fullFolderPath);

                string fileExtension = GetFileExtension(formFiles);

                string imageName = string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() : fileName;

                imageName += fileExtension;

                string fullPath = string.Join("/", fullFolderPath, imageName);

                using (FileStream fileStream = File.Create(fullPath))
                {
                    await formFiles.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return new SuccessFileResult(shortPath: string.Join(@"/", imageFolderName, folderName, imageName), fullPath: fullPath, imageName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        private static void CreateIfNoFolder(string fullFolderPath)
        {
            if (!Directory.Exists(fullFolderPath))
                Directory.CreateDirectory(fullFolderPath);
        }

        private static string GetFileExtension(IFormFile formFiles)
        {
            FileInfo fileInfo = new FileInfo(formFiles.FileName);

            string fileExtension = fileInfo.Extension;
            return fileExtension;
        }
    }
}
