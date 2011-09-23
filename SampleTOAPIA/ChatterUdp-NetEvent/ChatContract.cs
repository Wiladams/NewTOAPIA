namespace Chatter
{
    using System;
    using System.ServiceModel;

    [ServiceContract(Name = "IChatContract", Namespace = "http://samples.microsoft.com/ServiceModel/Relay/")]
    public interface IChatContract
    {
        [OperationContract]
        void ReceiveText(string name, string text);
   }

    public interface IChatChannel : IChatContract, IClientChannel { }

}