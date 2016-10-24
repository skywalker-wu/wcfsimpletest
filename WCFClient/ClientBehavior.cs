using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using WCFCommon;

namespace WCFClient
{
    public class ClientMessageBehavior : IEndpointBehavior
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
            Console.WriteLine("ClientMessageBehavior.ApplyClientBehavior");
            clientRuntime.MessageInspectors.Add(new ClientMessageInspector());
            clientRuntime.CallbackDispatchRuntime.MessageInspectors.Add(new ServerMessageInspector());
        }

        public void ApplyDispatchBehavior(
            ServiceEndpoint endpoint,
            EndpointDispatcher endpointDispatcher)
        {
            
        }

        public void Validate(
            ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
