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
                    case ConsoleKey.T:
                        Send(_ => _.Add(1, 1));
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
            Uri ServiceUri = new Uri(string.Format("{0}://{1}/{2}", Constants.Protocal, Constants.Url, Constants.Path));
            EndpointAddress ServiceAddress = new EndpointAddress(string.Format("{0}/{1}", ServiceUri.OriginalString, Constants.Address));
            var channel = ChannelFactory<ICalculator>.CreateChannel(new NetTcpBinding(), ServiceAddress);

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
