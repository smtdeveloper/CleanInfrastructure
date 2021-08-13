using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.UrlIncluder
{
    public static class UrlIncluder
    {
        private static IHttpContextAccessor httpContextAccessor;
        static UrlIncluder()
        {
            httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        /// <summary>
        /// Verilen local Path'i global olarak dışarı çıkartmak için gereken url pathlerini ekler.
        /// </summary>
        /// <param name="path">Lokal Dosya Yolu</param>
        /// <returns>String tipinde url döner.</returns>
        public static string Include(string path)
        {
            return httpContextAccessor.HttpContext.Request.Scheme + "://" + httpContextAccessor.HttpContext.Request.Host + "/" + path;
        }
    }
}
