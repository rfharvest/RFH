using System;
using System.IO;

namespace RFH.Services
{
    public class BackupService
    {
        public void ExecuteBackup(string rootPath, string tempZipPath)
        {   
            ZipAllFiles(rootPath, tempZipPath);
        }

        private void ZipAllFiles(string rootPath, string tempZipPath)
        {
            var zipService = new ZipService();
            zipService.OpenZipFile(tempZipPath);
            ZipDirectory(zipService, rootPath, "root");
            zipService.CloseZipFile();
        }

        private void ZipDirectory(ZipService zipService, string directoryPath, string relativePath)
        {
            var dirInfo = new DirectoryInfo(directoryPath);

            foreach (var file in dirInfo.GetFiles())
            {
                if (!file.Name.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
                {
                    zipService.AddFileToZip(file.FullName, relativePath);
                }
            }

            foreach (var childDirInfo in dirInfo.GetDirectories())
            {
                var childPath = string.Format("{0}\\{1}", relativePath, childDirInfo.Name);
                ZipDirectory(zipService, childDirInfo.FullName, childPath);
            }
        }
    }
}