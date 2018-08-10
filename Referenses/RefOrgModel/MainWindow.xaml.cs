using com.sbh.dll.support;
using com.sbh.dto.complexobjects;
using com.sbh.dto.simpleobjects;
using com.sbh.srv.interfaces;
using SomeProcess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RefOrgModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate void HandleBroadcastCallback(object sender, EventArgs e);
        //private BroadcastorServiceClient

        public MainWindow()
        {
            InitializeComponent();

            Directory.SetCurrentDirectory(GValues.GValues.CurrentDirectory);
        }

        public void HandleBroadcast(object sender, EventArgs e)
        {
            var eventData = (com.sbh.srv.interfaces.EventDataType)sender;

            Debug.Print($"Hello:    {eventData.EventMessage}");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
             DuplexSample();
        }

        private void DuplexSample()
        {
            EndpointAddress endpoint = new EndpointAddress("http://192.168.1.222:584/BroadcastorService");

            BroadcastorCallback bc = new BroadcastorCallback();
            bc.SetHandler(this.HandleBroadcast);

            DuplexChannelFactory<IBroadcastorService> dualFactory =
                new DuplexChannelFactory<IBroadcastorService> (bc, new WSDualHttpBinding(), endpoint);
            IBroadcastorService channel = dualFactory.CreateChannel();

            bool result = channel.RegisterClient("client_1");

            channel.NotifyServer(new EventDataType() { ClientName = "client_1", EventMessage = "Hello from client_1" });

        }
    }

    //[CallbackBehavior(UseSynchronizationContext = false)]
    //public class CallbackHandler : IProcessCallback
    //{
    //    Task IProcessCallback.TaskProgress(int percentDone)
    //    {
    //        Debug.Print($"Current percent: {percentDone}");
    //        return null;
    //    }
    //}
}
