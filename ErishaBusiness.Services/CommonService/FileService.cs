using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ErishaBusiness.Services.CommonService
{
    public class FileService
    {
        private string sharedDirectoryPath = SiteKeys.ImageFolder;
        private string assetsUrl = SiteKeys.AssetsDomain;
        public List<string> SaveFile(List<IFormFile> files, string subDirectory ,string imgPrefix=null)
        {
            List<string> tempFileAddress = new List<string>();
            subDirectory = subDirectory ?? string.Empty;
            var target = sharedDirectoryPath + subDirectory;

            Directory.CreateDirectory(target);

            files.ForEach(file =>
           {
               if (file.Length <= 0) return;

               var nFilename = string.Format(imgPrefix+"-{0}{1}"
               //, Path.GetFileNameWithoutExtension(file.FileName)
               , Guid.NewGuid().ToString("N")
               , Path.GetExtension(file.FileName));
               var filePath = Path.Combine(target, nFilename);
               tempFileAddress.Add(assetsUrl + subDirectory+"/" + nFilename);
               using (var stream = new FileStream(filePath, FileMode.Create))
               {
                   file.CopyTo(stream);
               }
           });
            return tempFileAddress;
        }
        public bool MoveFile(string rootDirectory, string fileName, string folderType)
        {
            try
            {
                rootDirectory = rootDirectory ?? string.Empty;
                var sourceFile = rootDirectory + "\\UploadTemp\\" + fileName;
                var target = rootDirectory + "\\" + folderType + "\\";
                Directory.CreateDirectory(target);

                File.Move(sourceFile, target + fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public (string fileType, byte[] archiveData, string archiveName) FetechFiles(string subDirectory)
        {
            var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

            var files = Directory.GetFiles(Path.Combine("D:\\webroot\\", subDirectory)).ToList();

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    files.ForEach(file =>
                    {
                        var theFile = archive.CreateEntry(file);
                        using (var streamWriter = new StreamWriter(theFile.Open()))
                        {
                            streamWriter.Write(File.ReadAllText(file));
                        }

                    });
                }

                return ("application/zip", memoryStream.ToArray(), zipName);
            }

        }

        public static string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"Less then 1KB";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }

        public void DeleteExistingFile(string old,string newurl=null)
        {
            try
            {
                if (old != null && newurl != null && newurl != old)
                {
                    var filelocation = old.Replace(assetsUrl, sharedDirectoryPath);
                    if (File.Exists($"{filelocation}"))
                    {
                        File.Delete($"{filelocation}");
                    }
                }
            }
            catch { }
        }

    }
}