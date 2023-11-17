namespace Template_Method
{
    #region Miner information of various documents (pdf, excels, docs) for processing data  made by hoang :)
    public abstract class Miner
    {
        //remember template method is fixed. It can't be overriden.
        public void TemplateMethod()
        {
            OpenFile();
            ExtractData();
            ParseData();
            AnalyzeData();
            ReportData();
            CloseFile();
        }

        public virtual void OpenFile()
        {
            Console.WriteLine("Open file from base class");
        }

        public abstract void ExtractData();
        public abstract void ParseData();

        public void AnalyzeData()
        {
            Console.WriteLine("Analyze Data from base class");
        }

        public void ReportData() 
        {
            Console.WriteLine("Report Data from base class");
        }

        public void CloseFile()
        {
            Console.WriteLine("Close FIle from base class");
        }
    }

    public class DocMiner : Miner
    {
        public override void ExtractData()
        {
            Console.WriteLine($"Extract Data for doc type of document in {typeof(DocMiner)}");
        }

        public override void ParseData()
        {
            Console.WriteLine($"Parse Data for doc type of document in {typeof(DocMiner)}");
        }
    }

    public class ExcelMiner : Miner
    {
        public override void ExtractData()
        {
            Console.WriteLine($"Extract Data for doc type of document in {typeof(ExcelMiner)}");
        }

        public override void ParseData()
        {
            Console.WriteLine($"Parse Data for doc type of document in {typeof(ExcelMiner)}");
        }
    }

    public class PdfMiner : Miner
    {
        public override void ExtractData()
        {
            Console.WriteLine($"Extract Data for doc type of document in {typeof(PdfMiner)}");
        }

        public override void ParseData()
        {
            Console.WriteLine($"Parse Data for doc type of document in {typeof(PdfMiner)}");
        }

        public override void OpenFile()
        {
            Console.WriteLine($"Open file for doc type of document in {typeof(PdfMiner)}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DocMiner doc = new();
            doc.TemplateMethod();
            Console.WriteLine("----------------------");

            ExcelMiner excel = new();
            excel.TemplateMethod();
            Console.WriteLine("----------------------");

            PdfMiner pdf = new();
            pdf.TemplateMethod();
            Console.WriteLine("----------------------");
        }
    }
    #endregion

    #region 
    #endregion

    #region 
    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Hello, World!");
    //    }
    //}
    #endregion

}
