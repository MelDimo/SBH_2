
using com.sbh.srv.implementations;
using System;
using System.IO;

namespace SrvHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(GValues.CurrentDirectory);

            Host host = new Host();
            host.LoadServices();

            while (true)
            {
                Console.ReadKey();
                break;
            }

        }
    }
}
