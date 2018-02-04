using System;
using System.Collections.Generic;
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
            var isGettingFiles = true;
            long _numBytesRead = 0;
            long _numFilesRead = 0;

            var printScanInfo = Task.Run(printBytesScanned);

            var files = DiskReader.GetFiles(path, extension);

            isGettingFiles = false;
            printScanInfo.Wait();

            async Task printBytesScanned()
            {
                var msgBytes = "bytes scanned: ";
                var msgFiles = "files scanned: ";

                Console.WriteLine();
                Console.WriteLine(msgBytes);
                Console.WriteLine(msgFiles);
                Console.CursorTop = Console.CursorTop - 2;
                Console.CursorVisible = false;

                while (isGettingFiles)
                {
                    if (NumBytesRead > _numBytesRead || NumFilesRead > _numFilesRead)
                    {
                        _numBytesRead = NumBytesRead;
                        _numFilesRead = NumFilesRead;

                        Console.CursorLeft = msgBytes.Length;
                        Console.Write(_numBytesRead);
                        Console.CursorTop = Console.CursorTop + 1;
                        Console.CursorLeft = msgFiles.Length;
                        Console.Write(_numFilesRead);
                        Console.CursorTop = Console.CursorTop - 1;
                    }
                }

                Console.CursorTop = Console.CursorTop + 3;
                Console.CursorLeft = 0;
                Console.CursorVisible = true;
            }

            return files;
        }

        
    }
}
