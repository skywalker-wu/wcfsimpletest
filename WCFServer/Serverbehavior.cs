using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using WCFCommon;

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
            //clientRuntime.ClientMessageInspectors.Add(new ClientMessageInspector());
        }

        public void ApplyDispatchBehavior(
            ServiceEndpoint endpoint,
            EndpointDispatcher endpointDispatcher)
        {
            Console.WriteLine("ClientMessageBehavior.ApplyDispatchBehavior");
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new ServerMessageInspector());
            endpointDispatcher.DispatchRuntime.CallbackClientRuntime.ClientMessageInspectors.Add(new ClientMessageInspector());
        }

        public void Validate(
            ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
