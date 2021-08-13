namespace Core.Utilities.Results.Abstract
{
    public interface IFileResult
    {
        /// <summary>
        /// Kısa path'i gösterir. (images/x.png)
        /// </summary>
        public string ShortPath { get; }
        /// <summary>
        /// Local Full Path'i gösterir. (C:/Users:/../../../wwwroot/images/x.png)
        /// </summary>
        public string FullPath { get; }
        /// <summary>
        /// Dosya Adı
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// Kayıt Durumunu Gösterir.
        /// </summary>
        public bool Success { get; }
    }
}
