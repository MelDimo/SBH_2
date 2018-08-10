using com.sbh.dll.support;
using com.sbh.srv.implementations;
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
                //Assembly assembly = Assembly.LoadFrom($"{Environment.CurrentDirectory}\\services\\{oservices.Name}\\{oservices.FileName}");
                //Type iassembly = assembly.GetTypes()[0].GetInterface("com.sbh.srv.interfaces.IBroadcastorService");

                //ServiceHost host = new ServiceHost(assembly.GetType());

                //ServiceMetadataBehavior smb = new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpGetUrl = new Uri($"http://localhost:584/{oservices.Name}") };

                //host.Description.Behaviors.Add(smb);

                //host.AddServiceEndpoint(assembly.GetTypes()[0].GetInterface("com.sbh.srv.interfaces.IBroadcastorService"), 
                //    new WSDualHttpBinding(), 
                //    new Uri($"http://localhost:584/{oservices.Name}"));

                //host.Open();

                //lHost.Add(host);

                //Uri BaseAddress = new Uri($"http://localhost:584/");

                Assembly assembly = Assembly.LoadFile($"{Environment.CurrentDirectory}\\services\\{oservices.Name}\\{oservices.FileName}");
                Type service = assembly.GetType(oservices.Namespace);
                Type contract = service.GetInterface(oservices.INamespace);
                Uri baseAddress = new Uri($"http://localhost:584/{oservices.Name}");

                ServiceHost host = new ServiceHost(service, baseAddress);
                ServiceEndpoint ep = host.AddServiceEndpoint(contract, new WSDualHttpBinding(), "");

                //host.AddServiceEndpoint(assembly.GetType(oservices.INamespace), new WSDualHttpBinding(), oservices.Name);

                //ServiceMetadataBehavior smb = new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpGetUrl = new Uri($"http://localhost:584/{oservices.Name}") };
                //host.Description.Behaviors.Add(smb);




                //host.AddServiceEndpoint(itype, new WSDualHttpBinding(), new Uri($"http://localhost:584/{oservices.Name}"));

                host.Open();

                lHost.Add(host);

                Console.WriteLine($"Service '{oservices.Name}' loaded.");

            }


            Console.WriteLine($"Count loaded services: {lHost.Count()}");
        }
    }
}
