namespace Proxy
{
    public class Program
    {
        public interface IDocument
        {
            void DisplayDocument();
        }

        /// <summary>
        /// Real Documet
        /// </summary>
        public class RealDocument : IDocument
        {
            public int AuthorId { get; set; }
            public string Content { get; set; }
            public string Title { get; set; }
            private string _fileName;
            public RealDocument(string fileName)
            {
                _fileName = fileName;
                LoadDocument();
            }

            private void LoadDocument()
            {
                Title = "Cuộc chiến kinh khung";
                Content = "Rất nhiều cuộc chiến";
                AuthorId = 1;
            }

            public void DisplayDocument()
            {
                Console.WriteLine($"Title: {Title}, Content: {Content}");
            }
        }

        /// <summary>
        /// Document Proxy
        /// </summary>
        public class DocumentProxy : IDocument
        {
            private Lazy<RealDocument> realDocument;
            private string _fileName;
            public DocumentProxy(string fileName)
            {
                _fileName = fileName;
                realDocument = new Lazy<RealDocument>(() => new RealDocument(_fileName));
            }

            public void DisplayDocument()
            {
                realDocument.Value.DisplayDocument();
            }
        }

        public class ProtectionDocumentProxy : IDocument
        {
            private string _fileName;
            private string _userRole;
            private DocumentProxy docProxy;

            public ProtectionDocumentProxy(string fileName, string role)
            {
                _fileName = fileName;
                _userRole = role;
                docProxy = new DocumentProxy(_fileName);
            }

            public void DisplayDocument()
            {
                Console.WriteLine($"Entering DisplayDocument of {nameof(ProtectionDocumentProxy)}");

                if (_userRole != "viewer" )
                {
                    throw new UnauthorizedAccessException();
                }

                docProxy.DisplayDocument();

                Console.WriteLine($"Finished DisplayDocument of {nameof(ProtectionDocumentProxy)}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Client: Executing the client code with a real subject:");
            RealDocument real = new RealDocument("1");
            real.DisplayDocument();
            Console.WriteLine("Completed: RealDocument");
            Console.WriteLine("--------------------------------");

            Console.WriteLine("Client: Executing Document Proxy");
            DocumentProxy docProxy = new("2");
            docProxy.DisplayDocument();
            Console.WriteLine("Client: Complete Document Proxy");
            Console.WriteLine("--------------------------------");


            Console.WriteLine("Client: Executing Protection Document Proxy with right role user");
            ProtectionDocumentProxy protect = new ProtectionDocumentProxy("1", "viewer");
            protect.DisplayDocument();
            Console.WriteLine("Client: Complete right Protection Document Proxy");
            Console.WriteLine("--------------------------------");


            Console.WriteLine("Client: Executing Protection Document Proxy with WRONG role user");
            ProtectionDocumentProxy protect2 = new ProtectionDocumentProxy("2", "other");
            protect2.DisplayDocument();
            Console.WriteLine("Client: Complete WRONG Protection Document Proxy");
        }
    }
}