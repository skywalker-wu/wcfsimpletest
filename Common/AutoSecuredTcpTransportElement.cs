using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AutoSecuredTcpTransportElement : TcpTransportBindingElement, ITransportTokenAssertionProvider
    {
        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(ISecurityCapabilities))
            {
                return (T)(ISecurityCapabilities)new AutoSecruedTcpSecurityCapability();
            }

            return base.GetProperty<T>(context);
        }

        public System.Xml.XmlElement GetTransportTokenAssertion()
        {
            return null;
        }
    }
}
