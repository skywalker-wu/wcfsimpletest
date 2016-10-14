using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Threading;
using WCFTest;
using System.ServiceModel.Channels;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A:
                        Send(_ => _.Add(1, 1));
                        break;

                    case ConsoleKey.D:
                        Send(_ => _.Divide(6, 2));
                        break;

                    case ConsoleKey.M:
                        Send(_ => _.Multiply(2, 2));
                        break;

                    case ConsoleKey.S:
                        Send(_ => _.Subtract(1, 1));
                        break;

                    case ConsoleKey.Q:
                        Console.WriteLine("Goodbye.");
                        return;
                }
                Thread.Sleep(1);
            }
        }

        static double Send(Func<ICalculator, double> func)
        {
            var binding = new NetTcpBinding(SecurityMode.Transport);
            Uri ServiceUri = new Uri(string.Format("{0}://{1}/{2}", Constants.Protocal, Constants.Url, Constants.Path));
            EndpointAddress ServiceAddress = new EndpointAddress(string.Format("{0}/{1}", ServiceUri.OriginalString, Constants.Address));
            var factory = new ChannelFactory<ICalculator>(binding, ServiceAddress);
            var channel = factory.CreateChannel();

            try
            {
                (channel as IChannel).Open();
                return func(channel);
            }
            finally
            {
                (channel as IChannel).Close();
            }
        }
    }
}
