namespace DPAll
{
    public class Program
    {

        #region Viết lại design pattern composite (Object Tree)
        public abstract class FileService
        {
            public string FileName { get; set; }
            public long Size { get; set; }

            public abstract long GetSize();

        }

        public class File : FileService
        {
            public File(string name, long size)
            {
                FileName = name;
                Size = size;
            }
            public override long GetSize()
            {
                return Size;
            }
        }

        public class Folder : FileService
        {
            public Folder(string name, long size)
            {
                FileName = name;
                Size = size;
            }

            public List<FileService> listFileService { get; set; } = new List<FileService>();

            public void AddFile(FileService file)
            {
                listFileService.Add(file);
            }

            public void RemoveFile(FileService file)
            {
                listFileService.Remove(file);
            }

            public override long GetSize()
            {
                long size = Size;
                foreach (FileService file in listFileService)
                {
                    var getSize = file.GetSize();
                    size += getSize;
                }
                return size;
            }
        }
        #endregion

        static void Main(string[] args)
        {
            #region using Composite Pattern
            //File fileRoot = new File("fileroot.txt", 200);

            //Folder folder1 = new Folder("Folder 1", 10);
            //File fileUnderFolder1 = new File("file 1", 100);
            //File fileUnderFolder2 = new File("file 2", 200);

            //folder1.AddFile(fileUnderFolder1);
            //folder1.AddFile(fileUnderFolder2);

            //Folder folder2 = new Folder("Folder 2", 5);
            //File fileUnderFolder3 = new File("file 3", 1000);
            //File fileUnderFolder4 = new File("file 4", 2000);
            //folder2.AddFile(fileUnderFolder3);
            //folder2.AddFile(fileUnderFolder4);


            //Console.WriteLine($"Size of File Root: {fileRoot.GetSize()}");
            //Console.WriteLine($"Size of Folder 1: {folder1.GetSize()}");
            //Console.WriteLine($"Size of Folder 2: {folder2.GetSize()}");
            #endregion

        }
    }
}