using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Proxy
{
    public class DiskReaderProxy : IDiskReader
    {
        public long NumFilesRead
        {
            get
            {
                return DiskReader.NumFilesRead;
            }
            set{}
        }

        public long NumBytesRead
        {
            get
            {
                return DiskReader.NumBytesRead;
            }
            set { }
        }

        public int NumDirectoriesNotSearched
        {
            get
            {
                return DiskReader.NumDirectoriesNotSearched;
            }
            set { }
        }

        private IDiskReader DiskReader { get; set; }
        private bool IsGettingFiles { get; set; }

        public DiskReaderProxy()
            : this(new DiskReader()) { }

        public DiskReaderProxy(IDiskReader diskReader)
        {
            DiskReader = diskReader;
        }

        public List<string> GetDrives()
        {
            return DiskReader.GetDrives();
        }

        public List<string> GetFiles(string path, string extension)
        {
            var cleanedExtension = extension; //CleanExtension(extension);

            if (string.IsNullOrEmpty(cleanedExtension))
            {
                return null;
            }

            IsGettingFiles = true;

            var printScanInfo = Task.Run(() => PrintScanInfo());
            var files = DiskReader.GetFiles(path, cleanedExtension);

            IsGettingFiles = false;
            printScanInfo.Wait();

            return files;
        }

        private string CleanExtension(string extension)
        {
            var dotlessExtension = extension.Replace(".", string.Empty);

            return Path.GetExtension(dotlessExtension);
        }

        private void PrintScanInfo()
        {
            var msgBytes = "bytes scanned: ";
            var msgFiles = "files scanned: ";
            long numBytesRead = 0;
            long numFilesRead = 0;

            Console.WriteLine();
            Console.WriteLine(msgBytes);
            Console.WriteLine(msgFiles);
            Console.CursorTop = Console.CursorTop - 2;
            Console.CursorVisible = false;

            while (IsGettingFiles)
            {
                if (NumBytesRead > numBytesRead || NumFilesRead > numFilesRead)
                {
                    numBytesRead = NumBytesRead;
                    numFilesRead = NumFilesRead;

                    Console.CursorLeft = msgBytes.Length;
                    Console.Write(numBytesRead);
                    Console.CursorTop = Console.CursorTop + 1;
                    Console.CursorLeft = msgFiles.Length;
                    Console.Write(numFilesRead);
                    Console.CursorTop = Console.CursorTop - 1;
                }
            }

            Console.CursorTop = Console.CursorTop + 3;
            Console.CursorLeft = 0;
            Console.CursorVisible = true;
        }
    }
}
