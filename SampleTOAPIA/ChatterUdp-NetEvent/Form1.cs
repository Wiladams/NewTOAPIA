

namespace Chatter
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using System.ServiceModel;
    using System.ServiceModel.Description;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Description;

    using System.Net;


    public partial class Form1 : Form
    {
        #region Service Related Fields
        // Service General
        Uri serviceUri;
        TransportClientEndpointBehavior sharedSecretServiceBusCredential;

        // Server Specifics
        ServiceHost host;

        // Client Specifics
        ChannelFactory<IChatChannel> channelFactory;
        IChatChannel channel;
        #endregion

        #region Form Fields
        string newText;
        string newName;
        #endregion

        public Form1()
        {
            InitializeComponent();

            SetupService();
        }

        #region Service Setup
        void SetupService()
        {
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.AutoDetect;

            string serviceNamespace = namespaceBox.Text;
            string issuerName = issuerBox.Text;
            string issuerSecret = passwordBox.Text;

            // create the service URI based on the service namespace
            serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, "ChatService");

            // create the credentials object for the endpoint
            sharedSecretServiceBusCredential = new TransportClientEndpointBehavior();
            sharedSecretServiceBusCredential.CredentialType = TransportClientCredentialType.SharedSecret;
            sharedSecretServiceBusCredential.Credentials.SharedSecret.IssuerName = issuerName;
            sharedSecretServiceBusCredential.Credentials.SharedSecret.IssuerSecret = issuerSecret;

            
            //Console.WriteLine("Service address: " + serviceUri);

            SetupServer(serviceNamespace, issuerName, issuerSecret);

            SetupClient(serviceNamespace, issuerName, issuerSecret);
        }

        void SetupServer(string serviceNamespace, string issuerName, string issuerSecret)
        {
            // create the service host reading the configuration
            host = new ServiceHost(typeof(ChatService), serviceUri);

            // create the ServiceRegistrySettings behavior for the endpoint
            IEndpointBehavior serviceRegistrySettings = new ServiceRegistrySettings(DiscoveryType.Public);

            // add the Service Bus credentials to all endpoints specified in configuration
            foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                endpoint.Behaviors.Add(serviceRegistrySettings);
                endpoint.Behaviors.Add(sharedSecretServiceBusCredential);
            }

            // open the service
            host.Open();

        }

        void SetupClient(string serviceNamespace, string issuerName, string issuerSecret)
        {
            // create the channel factory loading the configuration
            channelFactory = new ChannelFactory<IChatChannel>("ChatEndpoint", new EndpointAddress(serviceUri));

            // apply the Service Bus credentials
            channelFactory.Endpoint.Behaviors.Add(sharedSecretServiceBusCredential);

            // create and open the client channel
            channel = channelFactory.CreateChannel();
            channel.Open();

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            // Send the text to the network
            // Get the text from rick text box 2
            string currentText = textBox1.Text;

            SendText(currentText);

            // Clear Rich Text Box 2
            textBox1.Clear();

        }

        void DisplayText(string name, string text)
        {
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.AppendText("["+name+"]\n");
            richTextBox1.AppendText(text + "\n\n");
            richTextBox1.ScrollToCaret();
        }


        void SendText(string text)
        {
            channel.ReceiveText(Environment.UserName, text);
        }

        #region Collaboration
        protected override void OnPaint(PaintEventArgs e)
        {
            if (newText != null)
                ReceiveText(newName, newText);
            newText = null;

            base.OnPaint(e);
        }

        void ReceiveText(string name, string text)
        {
            // Save the string so we can display it later on the 
            // UI thread
            newText = text;
            newName = name;

            this.Invalidate();
        }
        #endregion

    }
}
