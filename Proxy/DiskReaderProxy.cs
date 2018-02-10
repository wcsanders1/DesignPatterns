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
            var initialColor = Console.ForegroundColor;
            var msgBytes = "Bytes scanned: ";
            var msgFiles = "Files scanned: ";
            long numBytesRead = 0;
            long numFilesRead = 0;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(msgBytes);
            Console.WriteLine(msgFiles);
            Console.ForegroundColor = initialColor;
            var (statusBarXPosition, statusBarYPosition, statusBarLength) = InitializeStatusBar();
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

                    if (percentageChecked >= percentageStatusBarFilled)
                    {
                        UpdateStatusBar(statusBarXPosition + (int)(portionOfStatusBarFilled++), statusBarYPosition, statusBarLength, (int)portionOfStatusBarFilled);
                    }

                    numBytesRead = NumBytesRead;
                    numFilesRead = NumFilesRead;

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.CursorLeft = msgBytes.Length;
                    Console.Write(numBytesRead);
                    Console.CursorTop = Console.CursorTop + 1;
                    Console.CursorLeft = msgFiles.Length;
                    Console.Write(numFilesRead);
                    Console.CursorTop = Console.CursorTop - 1;
                    Console.ForegroundColor = initialColor;
                }
            }

            // Make sure the status shows 100%, in case it didn't completely update in the loop above.
            UpdateStatusBar(statusBarXPosition + statusBarLength, statusBarYPosition, statusBarLength, statusBarLength);

            Console.CursorTop = Console.CursorTop + 4;
            Console.CursorLeft = 0;
            Console.CursorVisible = true;
        }

        private (int, int, int) InitializeStatusBar()
        {
            var initialColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var statusMsg = "Status: [";
            Console.Write(statusMsg);

            var statusBarXPosition = Console.CursorLeft;
            var statusBarYPosition = Console.CursorTop;
            var statusBarLength = Console.WindowWidth / 2;
            
            Console.CursorLeft = Console.CursorLeft + statusBarLength + 1;
            Console.Write("]");
            Console.ForegroundColor = initialColor;

            return (statusBarXPosition, statusBarYPosition, statusBarLength);
        }

        private void UpdateStatusBar(int xPosition, int yPosition, int statusBarLength, int portionOfStatusBarFilled)
        {
            var initialCursorYPosition = Console.CursorTop;
            var percentage = Math.Round((portionOfStatusBarFilled / (decimal)statusBarLength), 2) * 100;
            var initialColor = Console.ForegroundColor;

            Console.CursorTop = yPosition;
            Console.CursorLeft = xPosition;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("=");
            Console.CursorLeft = Console.CursorLeft + (statusBarLength - portionOfStatusBarFilled) + 2;
            Console.Write($"  {(int)percentage}%");
            Console.CursorLeft = 0;
            Console.CursorTop = initialCursorYPosition;
            Console.ForegroundColor = initialColor;
        }
    }
}
