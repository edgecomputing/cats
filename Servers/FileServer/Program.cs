using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Cats.Service.TemplateEditor;

namespace FileServer
{
    class Program
    {

        static ServiceHost host = null;
		static FileRepositoryService service = null;
        static void Main(string[] args)
        {
            service = new FileRepositoryService();
            service.RepositoryDirectory = "storage";

           

            host = new ServiceHost(service);
            

            try
            {
                host.Open();
                Console.WriteLine("Press a key to close the service:");
                Console.ReadKey();
            }

            finally
            {
                host.Close();
            }

        }
    }
}
