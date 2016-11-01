using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    public class ServiceMessageBehavior : IEndpointBehavior
    {

        #region IEndpointBehavior Members
        public void AddBindingParameters(
            ServiceEndpoint endpoint,
            BindingParameterCollection bindingParameters)
        {

        }



        public void ApplyClientBehavior(
            ServiceEndpoint endpoint,
            ClientRuntime clientRuntime)
        {
        }



        public void ApplyDispatchBehavior(
            ServiceEndpoint endpoint,
            EndpointDispatcher endpointDispatcher)
        {
            Console.WriteLine("ClientMessageBehavior.ApplyDispatchBehavior");
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new ServerMessageInspector());
        }



        public void Validate(
            ServiceEndpoint endpoint)
        {

        }



        #endregion

    }

    public class ServerMessageInspector : IDispatchMessageInspector
    {

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {

            Console.WriteLine("ServerMessageInspector.BeforeSendReply");

        }
    }
}
