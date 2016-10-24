using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCFCommon
{
    public class ClientMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            //Decrypt
            Console.WriteLine("ClientMessageInspector.AfterReceiveReply");
        }
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //Encrypt
            Console.WriteLine("ClientMessageInspector.BeforeSendRequest");
            return null;
        }
    }

    //public class ClientMessageBehavior : IEndpointBehavior
    //{
    //    #region IEndpointBehavior Members

    //    public void AddBindingParameters(
    //        ServiceEndpoint endpoint,
    //        BindingParameterCollection bindingParameters)
    //    {
    //    }

    //    public void ApplyClientBehavior(
    //        ServiceEndpoint endpoint,
    //        ClientRuntime clientRuntime)
    //    {
    //        Console.WriteLine("ClientMessageBehavior.ApplyClientBehavior");
    //        clientRuntime.MessageInspectors.Add(new ClientMessageInspector());
    //    }

    //    public void ApplyDispatchBehavior(
    //        ServiceEndpoint endpoint,
    //        EndpointDispatcher endpointDispatcher)
    //    {
    //        Console.WriteLine("ClientMessageBehavior.ApplyDispatchBehavior");
    //        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new ServerMessageInspector());
    //    }

    //    public void Validate(
    //        ServiceEndpoint endpoint)
    //    {
    //    }

    //    #endregion
    //}

    public class ServerMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Console.WriteLine("ServerMessageInspector.AfterReceiveRequest");
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("ServerMessageInspector.BeforeSendReply");
        }
    }

    //public class ServerBehavior : IEndpointBehavior
    //{
    //    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
    //    {
    //    }

    //    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    //    {
    //        Console.WriteLine("ServerBehavior.ApplyClientBehavior");
    //        clientRuntime.CallbackDispatchRuntime.MessageInspectors.Add(new ServerMessageInspector());
    //        //clientRuntime.ClientMessageInspectors.Add(new ClientMessageInspector());
    //    }

    //    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
    //    {
    //        Console.WriteLine("ServerBehavior.ApplyDispatchBehavior");
    //        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new ServerMessageInspector());
    //    }

    //    public void Validate(ServiceEndpoint endpoint)
    //    {
    //    }
    //}
}
