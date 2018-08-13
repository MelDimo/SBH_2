using com.sbh.dll.services;
using com.sbh.dto.srv;
using com.sbh.srv.interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Windows;


namespace RefOrgModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private delegate void HandleBroadcastCallback(object sender, EventArgs e);
        //private HandleBroadcastCallback broadcastCallback;

        public MainWindow()
        {
            InitializeComponent();

            Directory.SetCurrentDirectory(GValues.GValues.CurrentDirectory);

            //broadcastCallback = HandleBroadcast;
        }

        public void HandleBroadcast(object sender, EventArgs e)
        {
            Msg eventData = (Msg)sender;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("From server:");
            sb.AppendLine($"eventData.ClientName: {eventData.ClientName};");
            sb.AppendLine($"eventData.Error: {eventData.Error};");
            sb.AppendLine($"eventData.GUID: {eventData.GUID};");
            sb.AppendLine($"eventData.MsgStatus: {eventData.MsgStatus};");
            sb.AppendLine($"eventData.MsgTypeIn: {eventData.MsgTypeIn};");
            sb.AppendLine($"eventData.MsgTypeOut: {eventData.MsgTypeOut};");
            sb.AppendLine($"eventData.Obj: {eventData.Obj};");

            Debug.Print(sb.ToString());
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServiceChannel srvChannel = new ServiceChannel();
            srvChannel.Subscribe(MSGTYPE.WATCHONLINE, HandleBroadcast);
        }

        private void DuplexSample()
        {
            EndpointAddress endpoint = new EndpointAddress("http://192.168.1.222:584/BroadcastorService");

            BroadcastorCallback bc = new BroadcastorCallback();
            bc.SetHandler(this.HandleBroadcast);

            DuplexChannelFactory<IBroadcastorService> dualFactory =
                new DuplexChannelFactory<IBroadcastorService> (bc, new WSDualHttpBinding(), endpoint);
            IBroadcastorService channel = dualFactory.CreateChannel();

            Msg result = channel.RegisterClient(new Msg() { ClientName = "Client_1", GUID = new Guid() });

            Debug.Print($"{result.MsgStatus}");

            channel.NotifyServer(new Msg() { ClientName = "client_1", GUID = new Guid(), MsgTypeIn = MSGTYPE.WATCHONLINE});

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
