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

            pathConfig = $"{Environment.CurrentDirectory}{GValues.GValues.PathConfig}\\services.xml";
        }

        public void LoadServices()
        {
            string content = File.ReadAllText(pathConfig);
            LOServices = XMLToFromObject.XMLToObject<List<OService>>(content);

            foreach (OService oservices in LOServices)
            {
                Assembly assembly = 
                    Assembly.LoadFrom($"{GValues.GValues.CurrentDirectory}{GValues.GValues.ServicesDirectory}\\{oservices.FileName}");
                Type type = assembly.GetType(oservices.Namespace);

                Assembly iassembly =
                    Assembly.LoadFrom($"{GValues.GValues.CurrentDirectory}{GValues.GValues.ServicesDirectory}\\{oservices.IFileName}");
                Type itype = iassembly.GetType(oservices.INamespace);


                ServiceHost host = new ServiceHost(type);

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpGetUrl = new Uri($"http://localhost:584/{oservices.Name}")};

                host.Description.Behaviors.Add(smb);

                host.AddServiceEndpoint(itype, new WSDualHttpBinding(), new Uri($"http://localhost:584/{oservices.Name}"));

                host.Open();

                lHost.Add(host);

                Console.WriteLine($"Service '{oservices.Name}' loaded.");
            }

            Console.WriteLine($"Count loaded services: {lHost.Count}");
        }
    }
}
