using System.IO;
using System.Web;

namespace RFH.Extensions
{
    public static class DirectoryExtensions
    {
        public static FileInfo GetFileInfo(this string url, HttpServerUtilityBase server)
        {
            var physicalPath = server.MapPath(url);
            var file = new FileInfo(physicalPath);
            return file;
        }

        public static DirectoryInfo GetDirectoryInfo(this string folderUrl, HttpServerUtilityBase server)
        {
            var folderPhysicalPath = server.MapPath(folderUrl);
            var directoryInfo = new DirectoryInfo(folderPhysicalPath);
            return directoryInfo;
        }
    }
}