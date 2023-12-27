namespace DPAll
{
    public class Program
    {

        #region Viết lại design pattern composite (Object Tree)
        //public abstract class FileService
        //{
        //    public string FileName { get; set; }
        //    public long Size { get; set; }

        //    public abstract long GetSize();

        //}

        //public class File : FileService
        //{
        //    public File(string name, long size)
        //    {
        //        FileName = name;
        //        Size = size;
        //    }
        //    public override long GetSize()
        //    {
        //        return Size;
        //    }
        //}

        //public class Folder : FileService
        //{
        //    public Folder(string name, long size)
        //    {
        //        FileName = name;
        //        Size = size;
        //    }

        //    public List<FileService> listFileService { get; set; } = new List<FileService>();

        //    public void AddFile(FileService file)
        //    {
        //        listFileService.Add(file);
        //    }

        //    public void RemoveFile(FileService file)
        //    {
        //        listFileService.Remove(file);
        //    }

        //    public override long GetSize()
        //    {
        //        long size = Size;
        //        foreach (FileService file in listFileService)
        //        {
        //            var getSize = file.GetSize();
        //            size += getSize;
        //        }
        //        return size;
        //    }
        //}
        #endregion


        #region practice for Factory Method Pattern
        
        public abstract class SaleService
        {
            public abstract int PercentageSale { get;  }
        }

        public class CountrySaleService : SaleService {
            private readonly string country;

            public CountrySaleService(string countryCode)
            {
                this.country = countryCode;
            }

            public override int PercentageSale
            {
                get
                {
                    if (country == "us")  return 10; 
                    else if (country == "uk") return 20;
                    else return 5;
                }
            }
        }

        public class LoyaltyCustomerSaleService : SaleService
        {
            public override int PercentageSale => 50;
        }

        public abstract class DiscountFactory
        {
            public abstract SaleService CreateDiscountService();
        }

        public class CountrySaleFactory : DiscountFactory { 
        
            private readonly string country;
            public CountrySaleFactory(string countryInput)
            {
                this.country =countryInput;
            }

            public override SaleService CreateDiscountService()
            {
                return new CountrySaleService(country);
            }
        }

        public class LoyalSaleFactory : DiscountFactory
        {
            public override SaleService CreateDiscountService()
            {
                return new LoyaltyCustomerSaleService();
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


            #region using Factory Method pattern
            List<DiscountFactory> discs = new List<DiscountFactory>()
            {
                new CountrySaleFactory("uk"),
                   new LoyalSaleFactory()
            };

            foreach (var item in discs)
            {
                var eachService = item.CreateDiscountService();
                Console.WriteLine($"Percentage sale is {eachService.PercentageSale} coming from service {(eachService)}");
            }
            #endregion
        }
    }
}