using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Proxy
{
    public class DiskReader : IDiskReader
    {
        public long NumFilesRead { get; set; }
        public long NumBytesRead { get; set; }
        public int NumDirectoriesNotSearched { get; set; }

        public List<string> GetDrives()
        {
            return DriveInfo.GetDrives()
                .Select(d => d.Name)
                .ToList();
        }

        public List<string> GetFiles(string path, string extension = null)
        {
            NumFilesRead = 0;
            NumBytesRead = 0;
            NumDirectoriesNotSearched = 0;

            List<string> getFiles(string _path, string _extension, List<string> _files)
            {
                try
                {
                    var _newFiles = Directory.GetFiles(_path).ToList().ToList();

                    foreach (var _file in _newFiles)
                    {
                        var _fileInfo = new FileInfo(_file);
                        NumFilesRead++;
                        NumBytesRead += _fileInfo.Length;

                        if (_extension == _fileInfo.Extension)
                        {
                            _files.Add(_fileInfo.FullName);
                        }
                    }

                    Directory.GetDirectories(_path)
                        .ToList()
                        .ForEach(d => getFiles(d, _extension, _files));
                }
                catch (Exception)
                {
                    NumDirectoriesNotSearched++;
                }

                return _files;
            }

            return getFiles(path, extension, new List<string>());
        }
    }
}
