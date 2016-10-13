using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SSLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //X509Certificate2 ValidateServerCertificate = null;
            IPHostEntry heserver = Dns.GetHostEntry("localhost");
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(heserver.AddressList, 876);
            SslStream sslStream = new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);

            sslStream.AuthenticateAsClient("localhost");
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
