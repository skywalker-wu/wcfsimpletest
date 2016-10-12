using System;
using System.Collections.Generic;
using System.Linq;
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
                host.AddServiceEndpoint(typeof(ICalculator), binding, Constants.Address);
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
