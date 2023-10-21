namespace Composite
{
    public class Program
    {
        public abstract class FileSystemItem
        {
            public string Name { get; set; }

            public abstract long GetSize();
            public FileSystemItem(string name)
            {
                Name = name;
            }
        }

        public class File : FileSystemItem
        {
            private long _size;
            public File(string name, long size): base(name)
            {
                _size = size;
            }
            public override long GetSize()
            {
                return _size;
            }
        }

        public class Directory : FileSystemItem
        {
            public List<FileSystemItem> listFile  = new List<FileSystemItem>();
            private long _size;
            public Directory(string name, long size) : base(name)
            {
                _size = size;
            }

            public void Add(FileSystemItem item)
            {
                listFile.Add(item);
            }

            public void Remove(FileSystemItem item)
            {
                listFile.Remove(item);
            }

            public override long GetSize()
            {
                long sizeAll = _size;
                foreach (var item in listFile)
                {
                    sizeAll += item.GetSize();
                }
                return sizeAll;
            }
        }


        public static void Main(string[] args)
        {

            var root = new Directory("root", 0);
            var toplevelFile = new File("toplevel.txt", 100);
            var toplevelDirectory1 = new Directory("toplevelDirectory1", 4);
            var toplevelDirectory2 = new Directory("toplevelDirectory2", 4);

            root.Add(toplevelFile);
            root.Add(toplevelDirectory1);
            root.Add(toplevelDirectory2);

            var fileInTopDi1 = new File("fileInTopDi1", 140);
            toplevelDirectory1.Add(fileInTopDi1);

            var fileInTopDi2a = new File("fileInTopDi2a", 300);
            var fileInTopDi2b = new File("fileInTopDi2b", 600);
            toplevelDirectory2.Add(fileInTopDi2a);
            toplevelDirectory2.Add(fileInTopDi2b);

            Console.WriteLine($"Size of toplevelDirectory1: {toplevelDirectory1.GetSize()}");
            Console.WriteLine($"Size of toplevelDirectory2: {toplevelDirectory2.GetSize()}");
            Console.WriteLine($"Size of all: {root.GetSize()}");

          
            toplevelDirectory2.Remove(fileInTopDi2b);
            Console.WriteLine($"Size of toplevelDirectory2: {toplevelDirectory2.GetSize()}");
            Console.WriteLine($"Size of all: {root.GetSize()}");

        }
    }
}