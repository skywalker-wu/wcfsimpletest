using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AutoSecruedTcpSecurityCapability : ISecurityCapabilities
    {
        public ProtectionLevel SupportedRequestProtectionLevel
        {
            get { return ProtectionLevel.EncryptAndSign; }
        }

        public ProtectionLevel SupportedResponseProtectionLevel
        {
            get { return ProtectionLevel.EncryptAndSign; }
        }

        public bool SupportsClientAuthentication
        {
            get { return false; }
        }

        public bool SupportsClientWindowsIdentity
        {
            get { return false; }
        }

        public bool SupportsServerAuthentication
        {
            get { return true; }
        }
    }
}
