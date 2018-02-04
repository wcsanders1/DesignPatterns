using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private DriveInfo DriveInformation { get; set; }

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
            var cleanedExtension = CleanExtension(extension);
            if (string.IsNullOrEmpty(cleanedExtension))
            {
                return null;
            }

            DriveInformation = DriveInfo.GetDrives()
                .FirstOrDefault(d => d.Name == path);

            if (DriveInformation == null)
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
            if (string.IsNullOrEmpty(extension))
            {
                return null;
            }

            var dotlessExtension = extension.Replace(".", string.Empty);
            var dottedExtension = dotlessExtension.Insert(0, ".");

            return Path.GetExtension(dottedExtension);
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
            var (statusBarXPosition, statusBarYPosition, statusBarLength) = InitiateStatusBar();
            var totalBytesToCheck = DriveInformation?.TotalSize - DriveInformation?.TotalFreeSpace;
            decimal portionOfStatusBarFilled = 0;
            
            Console.CursorTop = Console.CursorTop - 2;
            Console.CursorVisible = false;

            while (IsGettingFiles)
            {
                if (NumBytesRead > numBytesRead || NumFilesRead > numFilesRead)
                {
                    decimal percentageChecked = NumBytesRead / (decimal)totalBytesToCheck;
                    decimal percentageStatusBarFilled = portionOfStatusBarFilled / statusBarLength;

                    if (percentageChecked > percentageStatusBarFilled)
                    {
                        UpdateStatusBar(statusBarXPosition + (int)(++portionOfStatusBarFilled), statusBarYPosition, statusBarLength, (int)portionOfStatusBarFilled);
                    }

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

            UpdateStatusBar(statusBarXPosition + --statusBarLength, statusBarYPosition, statusBarLength, statusBarLength);
            Console.CursorTop = Console.CursorTop + 4;
            Console.CursorLeft = 0;
            Console.CursorVisible = true;
        }

        private (int, int, int) InitiateStatusBar()
        {
            var statusMsg = "status: [";
            Console.Write(statusMsg);

            var statusBarXPosition = Console.CursorLeft;
            var statusBarYPosition = Console.CursorTop;
            var statusBarLength = Console.WindowWidth / 2;
            
            Console.CursorLeft = Console.CursorLeft + statusBarLength + 1;
            Console.Write("]");

            return (statusBarXPosition, statusBarYPosition, statusBarLength);
        }

        private void UpdateStatusBar(int xPosition, int yPosition, int statusBarLength, int portionOfStatusBarFilled)
        {
            var initialCursorYPosition = Console.CursorTop;
            var percentage = Math.Round((portionOfStatusBarFilled / (decimal)statusBarLength), 2) * 100;

            Console.CursorTop = yPosition;
            Console.CursorLeft = xPosition;

            // This is a hack to fix a problem where, if the console window is full width, the second-to-last '=' would be missing
            if (percentage > 50)
            {
                Console.CursorLeft--;
                Console.Write("==");
            }
            else
            {
                Console.Write("=");
            }

            Console.CursorLeft = Console.CursorLeft + (statusBarLength - portionOfStatusBarFilled) + 3;
            Console.Write($"{(int)percentage}%");
            Console.CursorLeft = 0;
            Console.CursorTop = initialCursorYPosition;
        }
    }
}
