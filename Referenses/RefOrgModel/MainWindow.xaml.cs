using com.sbh.dto.complexobjects;
using com.sbh.dto.simpleobjects;
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
        public MainWindow()
        {
            InitializeComponent();

            Directory.SetCurrentDirectory(GValues.GValues.CurrentDirectory);

            //SrvHost.Host host = new SrvHost.Host();
            //host.LoadServices();


           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             DuplexSampleAsync();
        }

        private async void DuplexSampleAsync()
        {
            EndpointAddress endpoint = new EndpointAddress("http://192.168.1.222:584/SrvSomeProcess");
            DuplexChannelFactory<SomeProcess.IProcess> dualFactory =
                new DuplexChannelFactory<SomeProcess.IProcess> (new CallbackHandler(), new WSDualHttpBinding(), endpoint);
            SomeProcess.IProcess channel = dualFactory.CreateChannel();

            await channel.TaskProcess();
        }
    }

    [CallbackBehavior(UseSynchronizationContext = false)]
    public class CallbackHandler : IProcessCallback
    {
        Task IProcessCallback.TaskProgress(int percentDone)
        {
            Debug.Print($"Current percent: {percentDone}");
            return null;
        }
    }
}
