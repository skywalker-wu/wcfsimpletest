using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFTest;

namespace WCFServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService), new Uri(string.Format("{0}://{1}/{2}", Constants.Protocal, Constants.Url, Constants.Path))))
            {
                var binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
                binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
                
                var d = host.AddServiceEndpoint(typeof(ICalculator), binding, Constants.Address);
                host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, "localhost");
                host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = System.ServiceModel.Security.UserNamePasswordValidationMode.Custom;
                host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new CustomUserNameValidator();

                host.Open();
                foreach (var serviceEndpoint in host.Description.Endpoints)
                {
                    Console.WriteLine("Endpoint:{0}", serviceEndpoint.ListenUri.AbsoluteUri);
                    Console.WriteLine("ContractType:{0}", serviceEndpoint.Contract.ContractType.FullName);
                    Console.WriteLine("-----------------------");
                }

                Console.WriteLine("Service Started");
                ConsoleKey k;
                while ((k = Console.ReadKey(true).Key) != ConsoleKey.Q)
                {
                    Thread.Sleep(1);
                }
            }

            Console.WriteLine("Goodbye.");
        }
    }

    public class CustomUserNameValidator : System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        // This method validates users. It allows in two users, test1 and test2 
        // with passwords 1tset and 2tset respectively.
        // This code is for illustration purposes only and 
        // MUST NOT be used in a production environment because it is NOT secure.	
        public override void Validate(string userName, string password)
        {
            //if (null == userName || null == password)
            //{
            //    throw new ArgumentNullException();
            //}

            //if (!(userName == "test1" && password == "1tset") && !(userName == "test2" && password == "2tset"))
            //{
            //    throw new FaultException("Unknown Username or Incorrect Password");
            //}
        }
    }

    

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class CalculatorService : ICalculator
    {

        public double Add(double n1, double n2)
        {
            double result = n1 + n2;
            Console.WriteLine("Received Add({0},{1})", n1, n2);
            Console.WriteLine("Return: {0}", result);
            return result;
        }


        public double Subtract(double n1, double n2)
        {
            double result = n1 - n2;
            Console.WriteLine("Received Subtract({0},{1})", n1, n2);
            Console.WriteLine("Return: {0}", result);
            return result;
        }


        public double Multiply(double n1, double n2)
        {
            double result = n1 * n2;
            Console.WriteLine("Received Multiply({0},{1})", n1, n2);
            Console.WriteLine("Return: {0}", result);
            return result;
        }


        public double Divide(double n1, double n2)
        {
            double result = n1 / n2;
            Console.WriteLine("Received Divide({0},{1})", n1, n2);
            Console.WriteLine("Return: {0}", result);
            return result;
        }
    }
}
