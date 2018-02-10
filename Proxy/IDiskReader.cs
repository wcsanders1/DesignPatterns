using System.Collections.Generic;

namespace Proxy
{
    public interface IDiskReader
    {
        long NumFilesRead { get; set; }
        long NumBytesRead { get; set; }
        int NumDirectoriesNotSearched { get; set; }

        List<string> GetDrives();
        List<string> GetFiles(string path, string extension);
    }
}
