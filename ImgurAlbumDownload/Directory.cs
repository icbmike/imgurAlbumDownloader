using System.IO;

namespace ImgurAlbumDownload
{
    public static class DirectoryExtensions
    {
        public static void EnsureExists(string location)
        {
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }
        }
    }
}