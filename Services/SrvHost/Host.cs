using Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SrvHost
{
    public class Host
    {
        AppDomain ServicesAppDomain;
        List<ServiceHost> lHost;
        List<OService> LOServices;

        string pathConfig = string.Empty;

        public Host()
        {
            LOServices = new List<OService>();
            lHost = new List<ServiceHost>();
            ServicesAppDomain = AppDomain.CreateDomain("SERVICES_DOMAIN");
            pathConfig = $"{Environment.CurrentDirectory}{GValues.GValues.PathConfig}\\services.xml";
        }

        public void LoadServices()
        {
            string content = File.ReadAllText(pathConfig);
            LOServices = XMLToFromObject.XMLToObject<List<OService>>(content);

            foreach (OService oservices in LOServices)
            {
                Assembly assembly = Assembly.LoadFrom($"{GValues.GValues.CurrentDirectory}{GValues.GValues.ServicesDirectory}\\{oservices.FileName}");
                Type type = assembly.GetType(oservices.Namespace);

                ServiceHost host = new ServiceHost(type, new Uri($"http://localhost:584/{oservices.Name}"));
                host.AddDefaultEndpoints();
                host.Open();
                lHost.Add(host);

            }
        }
    }
}
