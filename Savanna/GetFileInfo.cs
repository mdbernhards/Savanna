using System.IO;

namespace Savanna
{
    /// <summary>
    /// Class that interacts with files to get information
    /// </summary>
    public class GetFileInfo
    {
        /// <summary>
        /// Gets information on all files in dllFiles folder and returns it
        /// </summary>
        public FileInfo[] GetDllFileInfo()
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\martins.d.bernhards\source\repos\Savanna\dllFiles");
            return d.GetFiles();
        }
    }
}