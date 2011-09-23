namespace Chatter
{
    using System;
    using System.ServiceModel;

    public delegate void ReceiveTextHandler(string name, string text);

    [ServiceBehavior(Name = "ChatService", Namespace = "http://samples.microsoft.com/ServiceModel/Relay/")]
    public class ChatService : IChatContract
    {
        public event ReceiveTextHandler TextReceived;

        public void ReceiveText(string name, string text)
        {
            if (TextReceived != null)
                TextReceived(name, text);

            Console.WriteLine("Received: {0}", text);
        }
    }

}