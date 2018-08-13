using com.sbh.dll.support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SrvHost
{
    public class Host
    {
        List<ServiceHost> lHost;
        List<OService> LOServices;

        string pathConfig = string.Empty;

        public Host()
        {
            LOServices = new List<OService>();
            lHost = new List<ServiceHost>();

            pathConfig = $"{Environment.CurrentDirectory}\\config\\services.xml";
        }

        public void LoadServices()
        {
            string content = File.ReadAllText(pathConfig);
            LOServices = XMLToFromObject.XMLToObject<List<OService>>(content);

            foreach (OService oservices in LOServices)
            {
                Assembly assembly = Assembly.LoadFile($"{Environment.CurrentDirectory}\\services\\{oservices.Name}\\{oservices.FileName}");
                Type service = assembly.GetType(oservices.Namespace);
                Type contract = service.GetInterface(oservices.INamespace);
                Uri baseAddress = new Uri($"http://localhost:584/{oservices.Name}");

                ServiceHost host = new ServiceHost(service, baseAddress);
                ServiceEndpoint ep = host.AddServiceEndpoint(contract, new WSDualHttpBinding(), "");

                host.Open();

                lHost.Add(host);

                Console.WriteLine($"Service '{oservices.Name}' loaded.");

            }


            Console.WriteLine($"Count loaded services: {lHost.Count()}");
        }
    }
}
