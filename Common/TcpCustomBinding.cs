using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TcpCustomBinding : CustomBinding
    {
        private MessageVersion messageVersion = MessageVersion.None;

        public void SetMessageVersion(MessageVersion value)
        {
            this.messageVersion = value;
        }

        public override BindingElementCollection CreateBindingElements()
        {
            var res = new BindingElementCollection();
            res.Add(new TransactionFlowBindingElement());
            res.Add(new BinaryMessageEncodingBindingElement());
            res.Add(SecurityBindingElement.CreateUserNameOverTransportBindingElement());
            res.Add(new AutoSecuredTcpTransportElement());

            return res;
        }

        public override string Scheme
        {
            get
            {
                return "net.tcp";
            }
        }
    }
}
