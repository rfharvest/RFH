using System;
using System.IO;
using System.IO.Packaging;

namespace RFH.Services
{
    public class ZipService
    {
        private Package _zip;

        public void OpenZipFile(string zipFilename)
        {
            _zip = Package.Open(zipFilename, FileMode.Create);
        }

        public void CloseZipFile()
        {
            _zip.Close();
        }

        public void AddFileToZip(Stream streamToAdd, string streamFileNameToAdd, string destFolder)
        {
            string destFilename = GetDestFilename(destFolder, streamFileNameToAdd);
            Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));

            PackagePart part = _zip.CreatePart(uri, "", CompressionOption.Normal);

            using (Stream dest = part.GetStream())
            {
                CopyStream(streamToAdd, dest);
            }
        }

        public void AddFileToZip(string fileToAdd, string destFolder)
        {
            using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
            {
                AddFileToZip(fileStream, Path.GetFileName(fileToAdd), destFolder);
            }
        }

        private string GetDestFilename(string destFolder, string fileName)
        {
            string destFilename = null;

            if (string.IsNullOrEmpty(destFolder))
                destFilename = ".\\" + fileName;
            else
                destFilename = ".\\" + destFolder + "\\" + fileName;

            return destFilename;
        }

        private void CopyStream(Stream input, Stream output)
        {
            byte[] b = new byte[32768];
            int r;
            while ((r = input.Read(b, 0, b.Length)) > 0)
                output.Write(b, 0, r);
        }
    }
}