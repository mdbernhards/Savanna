using System.IO;

namespace Savanna
{
    public class GetFiles
    {

        public FileInfo[] GetDllFileInfo()
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\martins.d.bernhards\source\repos\Savanna\dllFiles");
            return d.GetFiles();
        }
    }
}
